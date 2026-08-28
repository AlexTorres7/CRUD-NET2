terraform {
  required_version = ">= 1.0.0"

  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
}

provider "aws" {
  region = var.aws_region
}

# 1. Repositorio en AWS ECR
resource "aws_ecr_repository" "api_repo" {
  name                 = var.app_name
  image_tag_mutability = "MUTABLE"

  image_scanning_configuration {
    scan_on_push = true
  }

  tags = {
    Environment = "Dev"
    Project     = "PruebaTecnica-NET"
  }
}

# 2. Cluster para ejecutar el contenedor (ECS)
resource "aws_ecs_cluster" "api_cluster" {
  name = "${var.app_name}-cluster"
}

# 3. Definición del contenedor
resource "aws_ecs_task_definition" "api_task" {
  family                   = var.app_name
  network_mode             = "awsvpc"
  requires_compatibilities = ["FARGATE"]
  cpu                      = "256"
  memory                   = "512"

  container_definitions = jsonencode([{
    name      = var.app_name
    image     = "${aws_ecr_repository.api_repo.repository_url}:latest"
    essential = true
    portMappings = [{
      containerPort = 8080
      hostPort      = 8080
    }]
    environment = [
      { name = "ASPNETCORE_ENVIRONMENT", value = "Production" },
      { name = "ASPNETCORE_HTTP_PORTS", value = "8080" },
      { name = "ConnectionStrings__DefaultConnection", value = var.db_connection_string }
    ]
  }])
}