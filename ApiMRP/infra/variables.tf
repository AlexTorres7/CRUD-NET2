variable "aws_region" {
  type        = string
  default     = "us-east-2"
  description = "Región de AWS donde se desplegarán los recursos"
}

variable "project_name" {
  type        = string
  default     = "apimrp"
  description = "Nombre base para los recursos"
}

variable "environment" {
  type        = string
  default     = "prod"
  description = "Entorno de ejecución"
}

variable "db_connection_string" {
  type        = string
  sensitive   = true
  default     = "Host=localhost;Port=5432;Database=ProductCatalogDb;Username=postgres;Password=mysecretpassword123"
  description = "Cadena de conexión a PostgreSQL"
}