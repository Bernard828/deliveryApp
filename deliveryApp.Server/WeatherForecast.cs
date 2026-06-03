namespace deliveryApp.Server
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }

    //NOTES
    //1. Live GPS Mapping: Integrate real-time GPS tracking for drivers and customers,
    //allowing users to see the location of their delivery in real-time.

    //2. Real Payments: Stripe or PayPal integration for secure and seamless payment processing.
    //Use a 'Pay with cash on delivery' button or mock successful transaction.

    //3.Complex Security: 
    //    Use mock login buttons to switch between customer, driver, and restaurant roles. 
    //    Implement role-based access control to ensure that users can only access features relevant to their role. OAuth.

}
