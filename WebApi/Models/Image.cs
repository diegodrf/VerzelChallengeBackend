using Dapper.Contrib.Extensions;

namespace WebApi.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Filename { get; set; } = string.Empty;

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if(obj?.GetType() == typeof(Image))
            {
                var _ = (Image)obj;
                return _.Id == Id;
            }
            return false;
        }
    }
}
