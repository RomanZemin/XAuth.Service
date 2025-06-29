namespace XAuth.Domain.Entities.Base;

/// <summary>
/// Абстрактный базовый класс для сущностей с поддержкой аудита.
/// </summary>
public abstract class AuditableEntity
{
    /// <summary>
    /// Дата и время создания сущности.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Идентификатор пользователя, который создал сущность.
    /// </summary>
    public Guid? CreatedBy { get; set; }

    /// <summary>
    /// Дата и время последнего изменения сущности.
    /// </summary>
    public DateTime ModifiedAt { get; set; }

    /// <summary>
    /// Идентификатор пользователя, который последний раз изменил сущность.
    /// </summary>
    public Guid? ModifiedBy { get; set; }
}