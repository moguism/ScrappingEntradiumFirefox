# ScrappingEntradiumFirefox
Compra de entradas en Entradium con Web Scrapping en un servidor en ASP y una extensión de Firefox, de manera que cuando se haga clic derecho sobre un enlace a Entradium en este navegador, se procederá a la compra automática de una entrada.

## PASOS PARA LA INSTALACIÓN
### Backend
- Tener instalado .NET
- Acceder al archivo WebScrappingController.cs (dentro de Server/Controllers/) y modificar los datos necesarios
### Firefox
1) Acceder a "about:debugging", en la barra de búsqueda.
2) Pinchar en "Este Firefox"
3) Darle a "Cargar complemento temporal"
4) Cargar uno de los dos archivos de la carpeta "Extension"
5) Cada vez que se reinicie Firefox, habrá que repetir el proceso
6) ¡Listo! La extensión ya funciona y si se hace clic sobre cualquier enlace, aparecerá una opción para mandar este al servidor y procesar la petición
## A TENER EN CUENTA
- El servidor no hace ninguna verificación de si está recibiendo o no un enlace a Entradium
- El servidor solo compra entradas que directamente pidan cantidad, nombre, apellidos y mail, ningún otro campo (aunque es fácilmente ampliable)
- Si la dirección del backend cambiar, poner la nueva en el archivo "background.js"
