namespace SecurePayHub.Application.Contracts
{
    public record OrderCreatedEvent
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}