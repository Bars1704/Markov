namespace MarkovTest.ObjectPool
{
    /// <summary>
    /// Object Pool pattern realisation. Stores some instances of items,
    /// that can be taken when you need it, and then return it back,
    /// so you won`t trigger GC by allocating new memory for instance every tine you need it
    /// </summary>
    /// <typeparam name="T">Type of stored in pool objects</typeparam>
    public class ObjectPool<T> where T : new()
    {
        /// <summary>
        /// Pool elements
        /// </summary>
        private readonly List<ObjectPoolElement<T>> _elements = new List<ObjectPoolElement<T>>();

        /// <summary>
        /// Return All elements to pool
        /// </summary>
        public void ReturnAll() => _elements.ForEach(x => x.Return());


        /// <summary>
        /// Get element from pool. Creates new element if there are not free elements
        /// </summary>
        /// <returns>An element from pool</returns>
        public ObjectPoolElement<T> Get()
        {
            var freeElem = _elements.FirstOrDefault(x => x.IsFree);
            if (freeElem == default)
            {
                freeElem = new ObjectPoolElement<T>();
                _elements.Add(freeElem);
            }

            freeElem.IsFree = false;
            return freeElem;
        }
    }
}