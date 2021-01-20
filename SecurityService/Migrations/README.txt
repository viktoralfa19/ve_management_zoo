Si la base de datos no esta creada aún, por favor seguir los siguientes pasos:

1.- Para crear la base de datos, seleccionamos como proyecto de Inicio la api de autenticación.
2.- Abrimos la consola de Administración de Paquetes PM> 
3.- En la parte superior de la sección del Administrador de Paquetes, que se denomina Proyecto determinado escogemos
    el nombre del microservicio Autenticacion.
4.- La base de datos debe estar creada con el nombre respectivo que se le haya colocado en el appsettings.jso. Solo la 
    base de datos vacia sin tablas.
5.- Escribimos el comando "add-migration _nombre_migracion -v", asegurandonos que en el directorio Migrations del 
    microservicio Autenticacion no hayan archivos con ese nombre y los borramos a todos los archivos aún mejor.
6.- Finalmente escribimos el comando "update-database -v".


Nota Final.


Para crear más campos en la tabla de usuarios o en la tabla de roles, en los respetivos modelos ya se a ApplicationRole y 
ApplicationUser dentro del microservicio se pueden crear más campos.
Para mayor información por favor dirigirse al siguiente link: 
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio

