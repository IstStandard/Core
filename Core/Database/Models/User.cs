using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Core.Database.Extra;

namespace Core.Database.Models;

[Table("users")]
public class User : IBaseModel
{
    public Guid Uid { get; init; } = Guid.NewGuid();
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    [JsonIgnore] public string Password { get; set; } = null!;
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Patronymic { get; set; } = String.Empty;
    public string? Fullname { get; set; }
    public int? Age { get; set; }
    public long Permissions { get; set; } = 0L;
    public int Status { get; set; } = (int) UserStatus.Undefined;
    public DateTime AddedDate { get; init; }
    public string FirstIp { get; init; } = null!;
    public ICollection<RefreshToken> RefreshTokens { get; init; } = null!;
}