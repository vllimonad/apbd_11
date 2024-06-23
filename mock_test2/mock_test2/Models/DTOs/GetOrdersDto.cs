namespace mock_test2.Models.DTOs;

public class GetOrdersDto
{
    public int Id { get; set; }
    public DateTime AcceptedAt { get; set; }
    public DateTime? FulfilledAt { get; set; }
    public string? Comments { get; set; }
    public ICollection<GetPastryDto> Pastries { get; set; } = null!;
}

public class GetPastryDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Amount { get; set; }
}