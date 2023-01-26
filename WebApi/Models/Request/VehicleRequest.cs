namespace WebApi.Models.Request
{
    public class VehicleRequest
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public int BrandId { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
