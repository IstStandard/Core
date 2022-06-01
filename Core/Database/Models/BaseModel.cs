using System.ComponentModel.DataAnnotations;

namespace Core.Database.Models;

public interface IBaseModel
{
    [Key] public Guid Uid { get; init; }
}