namespace SecurePayHub.Application.DTOs
{
    public record CreateProductRequest(string Name, decimal Price, int Stock);
    public record CreateOrderRequest(int ProductId, int Quantity);
}