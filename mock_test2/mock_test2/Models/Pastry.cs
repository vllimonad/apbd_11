using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace mock_test2.Models;

public class Pastry
{
    [Key]
    public int ID { get; set; }
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    [DataType("decimal")]
    [Precision(10,2)]
    public decimal Price { get; set; }
    [MaxLength(40)]
    public string Type { get; set; } = string.Empty;
    
    public ICollection<Order_Pastry> OrderPastries { get; set; } = new List<Order_Pastry>();
}