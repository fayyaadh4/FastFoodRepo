using System.ComponentModel.DataAnnotations;

namespace FastFood.Dto
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
    }
}
