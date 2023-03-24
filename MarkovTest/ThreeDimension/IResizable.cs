namespace MarkovTest.ThreeDimension
{
    public interface IResizable
    {
        public void Resize(Vector3Int newSize);
        public Vector3Int Size { get; }
    }
}