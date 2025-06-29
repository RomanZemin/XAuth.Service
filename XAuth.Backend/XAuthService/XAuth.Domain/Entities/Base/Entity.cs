using System.ComponentModel.DataAnnotations;

namespace XAuth.Domain.Entities.Base;

/// <summary>
/// Абстрактный базовый класс для сущностей с уникальным идентификатором.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Уникальный идентификатор сущности.
    /// </summary>
    [Key]
    public Guid Id { get; set; }
}