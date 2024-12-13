console.log("Ejecutando extensión...")

// Crea la opción en el menú contextual
chrome.runtime.onInstalled.addListener(() => {
    chrome.contextMenus.create({
        id: "abrir-con-extension",
        title: "Abrir con la extensión",
        contexts: ["link"]
    });
});

// Cuando se hace clic en el menú contextual
chrome.contextMenus.onClicked.addListener((info) => {
    if (info.menuItemId === "abrir-con-extension") {
        const url = info.linkUrl; // Obtiene la URL
        chrome.tabs.create({ url: url });
        console.log("Has pulsado en el enlace: ", url)

        // Tengo que poner alerts en el fetch porque si no, no lanza errores
        // No hago await porque no me interesa ralentizar el flujo
        // SI CAMBIA EL PUERTO, CAMBIARLO AQUÍ TAMBIÉN
        fetch('https://localhost:7036/WebScrapping', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(url)
        })
            .then(response => response.json())
            .then(data => {
                alert("")
                console.log("Respuesta del servidor:", data)
            })
            .catch(error => {
                alert("")
                console.error("Error al realizar la solicitud POST:", error)
            });
    }
});
