using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Database.Models;

[Table("black_list")]
public class BlackList : IBaseModel
{
    public Guid Uid { get; init; } = Guid.NewGuid();
    public Guid UserUid { get; init; }
    [ForeignKey("UserUid")] public virtual User User { get; init; } = null!;

    public string? Reason { get; set; }
    public DateTime AddedTime { get; init; }
    public DateTime? EndTime { get; set; }
}