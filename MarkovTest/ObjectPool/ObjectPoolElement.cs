namespace MarkovTest.ObjectPool
{
    /// <summary>
    /// Container for element, stored in ObjectPool
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPoolElement<T> where T : new()
    {
        /// <summary>
        /// Is Object free to use
        /// </summary>
        public bool IsFree { get; set; }

        /// <summary>
        /// Wrapped element, that will be used in pool
        /// </summary>
        public T Value { get; init; }

        /// <summary>
        /// Return element to ObjectPool
        /// </summary>
        public virtual void Return() => IsFree = true;
    
        public ObjectPoolElement()
        {
            Value = new T();
            IsFree = true;
        }
    }
}