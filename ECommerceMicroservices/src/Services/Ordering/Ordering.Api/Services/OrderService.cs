using Ordering.Api.ViewModels.Order.Requests;
using Ordering.Domain.Entities;

namespace Ordering.Api.Services
{
    public class OrderService
    {
        public Order ProcessOrder(ProcessOrderRequest processOrderRequest)
        {
            // Validate order data
            ValidateOrder(processOrderRequest);

            // Calculate total price
            var totalPrice = CalculateTotalPrice(processOrderRequest);

            // Initialize order
            var order = InitializeOrder(processOrderRequest, totalPrice);

            // Save order
            SaveOrder(order);

            // Send notification to user
            SendNotificationToUser  (order);

            return order;
        }

        private void SendNotificationToUser(Order order)
        {
            throw new NotImplementedException();
        }

        private void ValidateOrder(ProcessOrderRequest processOrderRequest)
        {
            throw new NotImplementedException();
        }

        private object CalculateTotalPrice(ProcessOrderRequest processOrderRequest)
        {
            throw new NotImplementedException();
        }

        private Order InitializeOrder(ProcessOrderRequest processOrderRequest, object totalPrice)
        {
            throw new NotImplementedException();
        }

        private void SaveOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void CancelOrder(int orderId)
        {

        }

        public void GetOrderDetail(int orderId)
        {

        }
    }
}
