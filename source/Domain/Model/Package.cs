namespace Domain.Model
{
    public class Package : Base
    {
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }

        public Package() { }
        public Package(Package package) 
        { 
            Length = package.Length;
            Width = package.Width;
            Height = package.Height;
            Weight = package.Weight;
        }
    }
}