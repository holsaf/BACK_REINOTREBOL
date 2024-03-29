# TrebolSchoolAPI

The TrebolSchool API is a web API which can save, edit, get and delete the Trebol school's student registration. During registration process, it does a random choice of a type sport for each student, and a data validation for its approved or reject. 
This API was made with ASP.NET Core 6, and use as database engine MySQL.

# Funcionalities aveilable.

  * Users can save a registration.
  * Users can update a registration.
  * Users can update the status of a registration.
  * Users can get all registrations saved on the database, both approved and reject. 
  * Users can get the registration's sport chosen 
  * Users can delete a registration.

# Software required 

  * .NET Core 6
  
  * Local instance MySQL

# Guia de Instalacion
  * Clone repository 
  
     ```
     git clone https://github.com/holsaf/ReinoDelTrebol.git.
     ```
     
  * Set the database connection.
  
   1. Set the connection credentials at "user secret", Configure los datos de conexion en los "secretos de usuario" dando click derecho en ReinoTrebolApi.csproj.
  
  ![image](https://user-images.githubusercontent.com/87883786/201547680-3c9696aa-a14b-401b-b583-b9658eb20a3f.png)
  
  2. Reemplace la informacion de user (id,password) por los datos de su conexion local de MySQL.
  
  ![image](https://user-images.githubusercontent.com/87883786/201547707-f21da5f3-8331-4bc3-a46e-0f894b270e19.png)
  
  3. Ir a appsettings.json y configurar la conexion.
  
  ![image](https://user-images.githubusercontent.com/87883786/201547801-29c6f14c-f86f-4fd4-a6f3-bd840ec688a9.png)
  
  * Run 
  
    ```
    dotnet build 
    ```
    
  o click derecho en ReinoTrebolApi.csproj, para compilar el proyecto.

![image](https://user-images.githubusercontent.com/87883786/201547917-c8798410-6e62-4138-9f61-6ef2a1d9f3f9.png)

  * La migracion creara de forma automatica la base de datos en la primera compilacion del proyecto.

# Uso
  * Run 
  
    ```
    dotnet run --project ReinoTrebolApi/ReinoTrebolApi.csproj
    ```
    
  o dando click en la siguiente opcion del Visual Studio:

![image](https://user-images.githubusercontent.com/87883786/201548512-216dc50c-ed82-4016-8863-2c920e4788c0.png)

  * Conectese a la API usando POSTMAN en el puerto 
  
    ```
    https://localhost:7294.
    ```

# API Endpoints

| HTTP Verbs | Endpoints | Action |
| --- | --- | --- |
| POST | /api/solicitud | Eegistar una solicitud |
| PUT | /api/solicitud  | Actualizar una solicitud |
| GET | /api/solicitud | Consultar todas las solictudes |
| GET | /api/solicitud/grimorio/:Id | Consultar el grimorio asignado a una solicitud |
| PATCH | /api/solicitud/:Id | Actualizar el estado de una solicitud |
| DELETE | /api/solicitud/:Id | Eliminar una solicitud |

# Swagger

  * La documentacion de los endpoints se genera de forma automatica en la url:
  
  ```
   https://localhost:7294/swagger/index.html
  ```  

# Autores 
  
  Holman Sanchez Fuentes



