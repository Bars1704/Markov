namespace MarkovTest.TwoDimension
{
    public interface IResizable
    {
        public void Resize(Vector2Int newSize);
        public Vector2Int Size { get; }
    }
}