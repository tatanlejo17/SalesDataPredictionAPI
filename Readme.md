# <p align="center">Sales Date Prediction - WebApi - .Net 8 </p>

## Descripción

Este proyecto consiste en una Web API desarrollada en .NET 8, diseñada para consultar información de varias tablas de la base de datos StoreSample y para realizar la inserción de órdenes con productos específicos.

> [!NOTE]
> **La API utiliza el ORM Dapper para la interacción con la base de datos. A nivel arquitectónico, se implementaron los patrones Repository y Factory. Se configuró un cors para permitir la conexión con un proyecto de Angular**

## Librerías Utilizadas

> [!NOTE]
> - Dapper
> - Microsoft.Data.SqlClient

## Requisitos

Antes de ejecutar el proyecto, asegúrate de tener instalados los siguientes elementos:

> [!IMPORTANT]
>
> - [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
> - [Visual Studio Code](https://code.visualstudio.com/)

## Clonación del Repositorio

1. Clona el repositorio en tu máquina local:

   ```bash
   git clone https://github.com/tatanlejo17/SalesDataPredictionAPI.git
   ```

## Configuración del Proyecto

1. Instalar Dependencias:

> [!IMPORTANT]
> Ejecutar el siguiente comando en la raíz del proyecto:

```bash
dotnet restore SalesDataPredictionAPI.csproj

dotnet build SalesDataPredictionAPI.csproj

```

2. Configurar el archivo "appSettings.json" según la configuración local que tengas para el connection string a la BD.

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=StoreSample;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;"
   }
   ```

3. En la ruta Data/Script dentro de la solución se encuentra un script para ejecutar en la BD.

> [!IMPORTANT]
> Abrir el entorno que se tenga para la gestión de SQL Server, y ejecutar el script.

## Ejecución del Proyecto

1. Ejecutar la Web API: Para iniciar la Web API, usa el siguiente comando:

   ```bash
   dotnet run
   ```

   Esto iniciará el servidor y podrás acceder a la API en http://localhost:5041 `_El puerto se asigna automáticamente_` o el puerto configurado en tu launchSettings.json.

2. En el navegador ingresar a la url:

> [!TIP]
> Ejemplo

        localhost:5041/swagger/index.html

4.  Esto usará Swagger como aplicación para la documentación de la API y se podrán probar cada uno de los métodos expuestos.

## Ejemplo de trama para consumo del método POST

> [!TIP]
> Ejemplo:
> {
> "custId": 51,
> "empId": "3",
> "shipperId": "1",
> "shipName": "Ship Name",
> "shipAddress": "Ship Address",
> "shipCity": "Ship City",
> "shipCountry": "Ship Country",
> "orderDate": "2024-08-31",
> "requiredDate": "2024-09-05",
> "shippedDate": "2024-09-10",
> "freight": 35,
> "productId": "44",
> "unitPrice": 32.8,
> "qty": 20,
> "discount": "0.300"
> }
