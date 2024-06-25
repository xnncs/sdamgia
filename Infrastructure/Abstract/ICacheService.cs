namespace Infrastructure.Abstract;

public interface ICacheService
{
    public TValue? TryGetValue<TKey, TValue>(TKey id) 
        where TKey : struct;

    public void AddToCache<TKey, TValue>(TKey id, TValue data)
        where TKey : struct;
}