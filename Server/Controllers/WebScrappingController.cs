using Microsoft.AspNetCore.Mvc;
using Microsoft.Playwright;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class WebScrappingController : ControllerBase
{
    private static readonly string NAME = "NOMBRE";
    private static readonly string SURNAME = "APELLIDOS";
    private static readonly string MAIL = "mail@mail.com";

    private static readonly string CARD_NUMBER = "42";
    private static readonly string CARD_EXPIRATION = "10/28";
    private static readonly string CARD_CVV = "123";

    [HttpPost]
    public async Task DoWebScrapping([FromBody] string link)
    {
        using IPlaywright playwright = await Playwright.CreateAsync();
        BrowserTypeLaunchOptions options = new BrowserTypeLaunchOptions()
        {
            Headless = false
        };

        await using IBrowser browser = await playwright.Chromium.LaunchAsync(options);
        await using IBrowserContext context = await browser.NewContextAsync();
        IPage page = await context.NewPageAsync();
        await page.GotoAsync(link);

        IElementHandle quantityElement = await page.QuerySelectorAsync("select");
        await quantityElement.ClickAsync();
        await quantityElement.SelectOptionAsync(new SelectOptionValue() { Value = "1" }); // Selecciona la opción con valor 1

        IElementHandle buyButton = await page.QuerySelectorAsync("button[type='submit']");
        await buyButton.ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        IElementHandle form = await page.QuerySelectorAsync("#purchase-confirmation");

        IElementHandle nameInput = await form.QuerySelectorAsync("#confirmation_buyers\\[0\\]\\[name\\]");
        await nameInput.FillAsync(NAME);

        IElementHandle surnameInput = await form.QuerySelectorAsync("#confirmation_buyers\\[0\\]\\[surname\\]");
        await surnameInput.FillAsync(SURNAME);

        IElementHandle emailInput = await form.QuerySelectorAsync("#confirmation_buyers\\[0\\]\\[email\\]");
        await emailInput.FillAsync(MAIL);

        IElementHandle emailConfirmationInput = await form.QuerySelectorAsync("#confirmation_buyers\\[0\\]\\[email_confirmation\\]");
        await emailConfirmationInput.FillAsync(MAIL);

        IElementHandle acceptConditions = await form.QuerySelectorAsync("#condiciones");
        await acceptConditions.ClickAsync();

        IElementHandle purchaseButton = await form.QuerySelectorAsync("input[type='submit']");
        await purchaseButton.ClickAsync();
        await Task.Delay(1500);

        IElementHandle paymentForm = await page.QuerySelectorAsync("#datosTarjeta");

        IElementHandle cardNumber = await paymentForm.QuerySelectorAsync("#card-number");
        await cardNumber.FillAsync(CARD_NUMBER);

        IElementHandle cardDate = await paymentForm.QuerySelectorAsync("#card-expiration");
        await cardDate.FillAsync(CARD_EXPIRATION);

        IElementHandle cardCvv = await paymentForm.QuerySelectorAsync("#card-cvv");
        await cardCvv.FillAsync(CARD_CVV);

        IElementHandle finishPayment = await paymentForm.QuerySelectorAsync("#divImgAceptar");
        await finishPayment.ClickAsync();

        await Task.Delay(-1);
    }
}
