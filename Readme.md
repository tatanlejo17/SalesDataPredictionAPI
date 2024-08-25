# Sales Data Prediction - WebApi - .Net 8

## Descripción

Este proyecto es una Web API desarrollada en .NET 8 para la consulta de información de algunas tablas de la BD StoreSample, además de tener un método especifico para la insercción de una orden nueva con un producto.

## Librerías Utilizadas

- Dapper
- Microsoft.Data.SqlClient

## Requisitos

Antes de ejecutar el proyecto, asegúrate de tener instalados los siguientes elementos:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio Code](https://code.visualstudio.com/) o [Visual Studio](https://visualstudio.microsoft.com/) (opcional, pero recomendado para un desarrollo más fácil)

## Clonación del Repositorio

1. Clona el repositorio en tu máquina local:

   ```bash
   git clone https://github.com/tu_usuario/tu_repositorio.git
   ```

## Configuración del Proyecto

1. Instalar Dependencias: Ejecutar el siguiente comando en la raíz del proyecto:

   ```bash
   dotnet restore

   ```

2. Configurar el archivo "appSettings.json" según la configuración local que se tenga del connection string a la BD.

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=StoreSample;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;"
   }
   ```

## Ejecución del Proyecto

1. Ejecutar la Web API: Para iniciar la Web API, usa el siguiente comando:

   ```bash
   dotnet run
   ```

Esto iniciará el servidor y podrás acceder a la API en http://localhost:"El puerto se asgina automáticamente" o el puerto configurado en tu launchSettings.json.

2.  En el navegador ingresar a la url: http://localhost:"El puerto se asgina automáticamente"/swagger/index.html
    Ejemplo:

        ```text
        localhost:5041/swagger/index.html

3.  Esto usará Swagger como aplicación para la documentación de la API y se podrán probar cada uno de los métodos expuestos.

.[!NOTE]

> El proyecto usa el ORM Dapper para interactuar con la Base de datos, a nivel de arquitectura se usaron los patrones de Repository y Factory (Para la conexión con la BD), además de un manejador general excepciones en try - cacth dentro de las clases del repository.
