using System.ComponentModel.DataAnnotations;

namespace mock_test2.Models.DTOs;

public class AddOrderDto
{
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public DateTime AcceptedAt { get; set; }
    [MaxLength(300)] 
    public string? Comments { get; set; }
    [Required]
    public ICollection<AddPartyDto> Pastries { get; set; } = null!;
}

public class AddPartyDto
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public int Amount { get; set; }
    [MaxLength(300)] 
    public string? Comments { get; set; }
}