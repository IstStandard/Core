using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Database.Models;

[Table("refresh_tokens")]
public class RefreshToken : IBaseModel
{
    public Guid Uid { get; init; } = Guid.NewGuid();
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedIp { get; set; } = null!;
    public DateTime? RevokedAt { get; set; }
    public string? RevokedIp { get; set; } = null!;
    public Guid UserUid { get; set; }

    [ForeignKey("UserUid")]
    [InverseProperty("RefreshTokens")]
    public virtual User User { get; set; } = null!;
}