output "ecr_repository_url" {
  value       = aws_ecr_repository.api_repo.repository_url
  description = "URL del repositorio de Amazon ECR"
}

output "ecr_repository_arn" {
  description = "ARN del repositorio ECR"
  value       = aws_ecr_repository.api_repo.arn
}