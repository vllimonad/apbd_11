using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace mock_test2.Models;

public class Order
{
    [Key]
    public int ID { get; set; }
    public DateTime AcceptedAt { get; set; }
    public DateTime? FulfilledAt { get; set; }
    [MaxLength(300)]
    public string? Comments { get; set; }
    public int ClientId { get; set; }
    public int EmployeeId { get; set; }

    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; } = null!;
    [ForeignKey(nameof(EmployeeId))]
    public Employee Employee { get; set; } = null!;
    public ICollection<Order_Pastry> OrderPastries { get; set; } = new List<Order_Pastry>();
}