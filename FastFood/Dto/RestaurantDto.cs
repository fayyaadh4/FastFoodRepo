using FastFood.Models;

namespace FastFood.Dto
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public bool HasDelivery { get; set; }
        public Location? Location { get; set; }
    }
}
