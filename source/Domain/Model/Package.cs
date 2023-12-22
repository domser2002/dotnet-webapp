namespace Domain.Model
{
    public class Package : Base
    {
        public float? Length { get; set; }
        public float? Width { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public object this[string fieldname]
        {
            set
            {
                switch (fieldname)
                {
                    case "Length":
                        this.Length = value == null ? null : Convert.ToSingle((int)value);
                        break;
                    case "Width":
                        this.Width = value == null ? null : Convert.ToSingle((int)value);
                        break;
                    case "Height":
                        this.Height = value == null ? null : Convert.ToSingle((int)value);
                        break;
                    case "Weight":
                        this.Weight = value == null ? null : Convert.ToSingle((int)value);
                        break;
                }
            }
        }
    }
}