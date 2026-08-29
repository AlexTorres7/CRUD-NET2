# 🚀 Product Catalog API (.NET 8 + Docker + AWS + Terraform)

API RESTful desarrollada en **.NET 8** para la gestión de un catálogo de productos. Cuenta con persistencia de datos en **PostgreSQL**, contenerización con **Docker**, pipeline automatizado de **CI/CD con GitHub Actions (AWS ECR)** e infraestructura definida como código con **Terraform (AWS)**.

---

## 🛠️ Requisitos Previos

Asegúrate de tener instalado en tu máquina local:

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* [Terraform](https://developer.hashicorp.com/terraform/downloads) (v1.5.0 o superior)
* [Git](https://git-scm.com/)

---

## 🚀 1. Cómo Ejecutar Localmente

### Opción A: Con Docker Compose (Recomendado)

1. Clona el repositorio:
   ```bash
   git clone [https://github.com/AlexTorres7/CRUD-NET2.git](https://github.com/AlexTorres7/CRUD-NET2.git)
   cd CRUD-NET2

2. Levantar la base de Datos
docker run -d `
  --name postgres-db `
  -e POSTGRES_DB=ProductCatalogDb `
  -e POSTGRES_USER=postgres `
  -e POSTGRES_PASSWORD=mysecretpassword123 `
  -p 5433:5432 `
  postgres:16-alpine  

3. Ejecutar la aplicación .NET
cd ApiMRP
dotnet restore ApiMRP/src/API/ApiMRP.csproj
dotnet run --project ApiMRP/src/API/ApiMRP.csproj

4. Probar en el navegador
http://localhost:5000/swagger (o el puerto asignado en consola)


🐳 2. Cómo Construir la Imagen Docker

1. # Construir la imagen Docker usando el Dockerfile de ApiMRP
docker build -t product-api -f ApiMRP/Dockerfile ApiMRP/

2. # Ejecutar el contenedor mapeado el puerto 8080 e ingresando las variables de entorno
docker run -d `
  -p 8080:8080 `
  --name api-test `
  -e "ASPNETCORE_ENVIRONMENT=Development" `
  -e "ConnectionStrings__DefaultConnection=Host=host.docker.internal;Port=5433;Database=ProductCatalogDb;Username=postgres;Password=mysecretpassword123" `
  product-api

3. # Validar
http://localhost:8080/swagger


☁️ 3. Cómo Ejecutar Terraform (AWS)


1. # Ubicarse en la carpeta 
cd ApiMRP/terraform

2. # Comandos de Terraform
terraform init
terraform init
terraform apply
