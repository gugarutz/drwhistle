namespace DrWhistle.Domain.Common
{
    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }
}