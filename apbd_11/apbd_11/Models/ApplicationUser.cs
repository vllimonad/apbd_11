using System.ComponentModel.DataAnnotations;

namespace apbd_11.Models;

public class ApplicationUser
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Login { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExp { get; set; }
}