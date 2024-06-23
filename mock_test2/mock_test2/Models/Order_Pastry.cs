using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace mock_test2.Models;

[PrimaryKey(nameof(OrderId), nameof(PastryId))]
public class Order_Pastry
{
    public int OrderId { get; set; }
    public int PastryId { get; set; }
    public int Amount { get; set; }
    [MaxLength(300)]
    public string? Comments { get; set; }

    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = null!;
    [ForeignKey(nameof(PastryId))]
    public Pastry Pastry { get; set; } = null!;
}