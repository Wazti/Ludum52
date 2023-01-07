namespace CodeBase.Infrastructure.Factory.Interfaces
{
    public interface IConfigContainer<TValue, TKey>
    {
        /// <summary>
        /// Returns location prefab by type
        /// </summary>
        TValue Get(TKey value);

        public bool IsKey(TKey key);
    }
}