using System.ComponentModel.DataAnnotations;

namespace mock_test2.Models;

public class Client
{
    [Key]
    public int ID { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(120)]
    public string LastName { get; set; } = string.Empty;
    
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}