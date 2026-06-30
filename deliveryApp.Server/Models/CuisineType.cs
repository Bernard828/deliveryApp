namespace deliveryApp.Server.Models
{
    //public class CuisineType
    //{
    //}
    public class CuisineType
    {
        public int CuisineTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

    }
    public class CuisineTypeCreateDto
    {
        public string Name { get; set; } = string.Empty;

    }
    public class CuisineTypeUpdateDto
    {
        public int CuisineTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
