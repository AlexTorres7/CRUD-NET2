namespace ApiMRP.Dtos;

public record CreateProductDto(
    string Name,
    string Description,
    decimal Price,
    int Stock
);

public record UpdateProductDto(
    string Name,
    string Description,
    decimal Price,
    int Stock
);

public record ProductResponseDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Stock,
    DateTime CreatedAt
);