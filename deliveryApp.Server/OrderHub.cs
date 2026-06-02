using Microsoft.AspNetCore.SignalR;

namespace deliveryApp.Server
{
    public class OrderHub:Hub
    {
        // This hub can be used to send real-time updates about order status changes to clients (customers, drivers, restaurants).
        //inTransit
        //InTransit: user sees 'Preparing' to 'Driver is on the way' and then 'Order delivered'
        //delivered, pending, accepted
    }
}
