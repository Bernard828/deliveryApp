//using deliveryApp.Server.Data;
//using deliveryApp.Server.DTOs;
//using deliveryApp.Server.Models;
//using Microsoft.EntityFrameworkCore;

//namespace deliveryApp.Server.Endpoints
//{
//    public static class RestaurantEndpoints
//    {
//        public static void MapRestaurantEndpoints(this IEndpointRouteBuilder app)
//        {
//            var group = app.MapGroup("api.Restaurant");

//            group.MapGet("/", async (DeliveryAppDbContext db) =>
//            {

//                var currentDay = DateTime.Today.DayOfWeek;
//                var currentTime = DateTime.Now.TimeOfDay;

//                var restaurants = await db.Restaurants
//                //.Include(r => r.CuisineType)
//                //.Include(r => r.OperatingHours)
//                .AsNoTracking()
//                .ToListAsync();

//                var dtoResult = restaurants.Select(r => new RestaurantDto
//                {
//                    RestaurantId = r.RestaurantId,
//                    Name = r.Name,
//                    CuisineTypeId = r.CuisineTypeId ?? 0,
//                    //cuisine = r.cuisinetype,
//                    //price = r.price,
//                    //ImageUrl = r.ImageUrl,
//                    //Address = r.Address,
//                    // Check if the current time sits inside operational windows
//                    IsCurrentlyOpen = r.OperatingHours.Any(h =>

//                        h.DayOfWeek == currentDay &&
//                        currentTime >= h.OpenTime &&
//                        currentTime <= h.CloseTime),
//                    OperatingHours = r.OperatingHours.Select(h => new RestaurantHourDto
//                    {
//                        DayOfWeek = h.DayOfWeek,
//                        OpenTime = h.OpenTime.ToString(@"hh\:mm"),
//                        CloseTime = h.CloseTime.ToString(@"hh\:mm")
//                    }).ToList()
//                }).ToList();
//                return Results.Ok(dtoResult);
//            });

//            group.MapGet("/{id:int}", async (int id, DeliveryAppDbContext db) =>
//            {
//                var restaurant = await db.Restaurants
//                .Include(r => r.CuisineType)
//                .Include(r => r.OperatingHours)
//                .AsNoTracking()
//                .FirstOrDefaultAsync(r => r.RestaurantId == id);

//                if (restaurant is null) return Results.NotFound();

//                var currentDay = DateTime.Today.DayOfWeek;
//                var currentTime = DateTime.Now.TimeOfDay;

//                var dto = new RestaurantDto
//                {
//                    RestaurantId = restaurant.RestaurantId,
//                    Name = restaurant.Name,
//                    CuisineTypeId = restaurant.CuisineTypeId ?? 0,
//                    //Cuisine = restaurant.CuisineType,
//                    //Price = restaurant.Price,
//                    //ImageUrl = restaurant.ImageUrl,
//                    //Address = restaurant.Address,
//                    IsCurrentlyOpen = restaurant.OperatingHours.Any(h =>
//                        h.DayOfWeek == currentDay &&
//                        currentTime >= h.OpenTime &&
//                        currentTime <= h.CloseTime),
//                    OperatingHours = restaurant.OperatingHours.Select(h => new RestaurantHourDto
//                    {
//                        DayOfWeek = h.DayOfWeek,
//                        OpenTime = h.OpenTime.ToString(@"hh\:mm"),
//                        CloseTime = h.CloseTime.ToString(@"hh\:mm")
//                    }).ToList()
//                };
//                return Results.Ok(dto);
//            });

//            group.MapPost("/", async (Restaurant restaurant, DeliveryAppDbContext db) =>
//            {
//                if (string.IsNullOrWhiteSpace(restaurant.Name))
//                {
//                    return Results.BadRequest("Restaurant Name is required.");
//                }

//                db.Restaurants.Add(restaurant);
//                await db.SaveChangesAsync();

//                return Results.Created($"/api/Restaurant/{restaurant.RestaurantId}", restaurant);
//            });
//            // 4. UPDATE EXISTING RESTAURANT
//            group.MapPut("/{id:int}", async (int id, Restaurant input, DeliveryAppDbContext db) =>
//            {
//                var restaurant = await db.Restaurants.FindAsync(id);
//                if (restaurant is null) return Results.NotFound();

//                // Explicitly modify tracked attributes cleanly
//                restaurant.Name = input.Name;
//                restaurant.CuisineTypeId = input.CuisineTypeId;
//                restaurant.Address = input.Address;
//                //restaurant.Price = input.Price;
//                //restaurant.ImageUrl = input.ImageUrl;

//                await db.SaveChangesAsync();
//                return Results.NoContent();
//            });

//            // 5. DELETE RESTAURANT
//            group.MapDelete("/{id:int}", async (int id, DeliveryAppDbContext db) =>
//            {
//                var restaurant = await db.Restaurants.FindAsync(id);
//                if (restaurant is null) return Results.NotFound();

//                db.Restaurants.Remove(restaurant);
//                await db.SaveChangesAsync();
//                return Results.NoContent();
//            });

//            //var groupedByCuisine = restaurants
//            //    .GroupBy(r => r.Cuisine)
//            //    .Select(g => new
//            //    {
//            //        CuisineType = g.Key,
//            //        Restaurant = g.ToList()
//            //    });
//            //return Results.Ok(groupedByCuisine);
//            //});
//        }
//    }
//}
