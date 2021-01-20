#Comandos para generar las imagenes de pruebas "Staging" o las productivas "Production"
#Si desea puede generar las imagenes localmente o puede ejecutar directamente el docker compose para descargar las imagenes de DOCKER HUB
#Dirijirse al directorio raiz de la solución y ejecutar los siguientes comandos un una consola de power shell:

**cd D:\..\..\ve_management_zoo**

**docker build -t victoralfa19/ve_zoo_webapi_security:v1.0.0 -f .\Dockerfile.Security --build-arg "ENVIRONMENT=Staging"  .**
**docker build -t victoralfa19/ve_zoo_webapi_zoo:v1.0.0 -f .\Dockerfile.Zoo --build-arg "ENVIRONMENT=Staging"  .**

#Probar la imagen en entorno de desarrollo con docker

#Crear localmente el contenedor y probarlo con docker compose, si se encuentra dentro del directorio donde esta el archivo docker-compose.yaml,
#sino indicar el directorio específico del archivo.


**docker-compose up -d  ## Si no se genera las imgenes locales no importa y que se pueden descargar de Docker HUB**


#Para la busqueda, filtrado y páginación se ha utilizado ODATA, en la documentación de swagger se agregan campos para probar estas características:
#Ejemplo:
#Prametros: $select, $expand

**[{"key":"$select","value":"data","description":""}]**
**[{"key":"$expand","value":"data($select = Name,Weight,TypeName;$skip = 0;$top = 3;$filter = TypeName eq 'León')","description":""}]**

#Ejemplo par Type Animal
#Prametros: $select, $expand

**[{"key":"$select","value":"data","description":""}]**
**[{"key":"$expand","value":"data($filter = Name eq 'León')","description":""}]**


#Adjunto en el código se encuentra el archivo exportado de POSTMAN para cargarlo y facilitar las pruebas funcionales al usuario el archivo se denomina:
**API ZOO.postman_collection.json**

Para la autenticación existen dos usuarios:

Usuario 1: **vecheverria**, con rol **Administrador**, clave **Vecheverria.2020**
Usuario 2: **babarca**, con rol **Funcionario**, clave **Vecheverria.2020**
