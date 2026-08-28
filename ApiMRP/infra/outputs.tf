output "ecr_repository_url" {
  value       = aws_ecr_repository.repo.repository_url
  description = "URL del repositorio de Amazon ECR"
}

output "ecs_cluster_name" {
  value       = aws_ecs_cluster.cluster.name
  description = "Nombre del cluster de ECS"
}