using Dapper.Contrib.Extensions;

namespace WebApi.Models
{
    public class Vehicle
    {
        private readonly HashSet<Image> _images = new HashSet<Image>();

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public Brand Brand { get; set; } = default!;
        public IReadOnlySet<Image> Images => _images;

        public void AddImage(Image image)
        {
            _images.Add(image);
        }
    }
}
