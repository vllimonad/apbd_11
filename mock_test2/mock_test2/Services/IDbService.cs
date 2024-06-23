using mock_test2.Models;

namespace mock_test2.Services;

public interface IDbService
{
    Task<ICollection<Order>> GetAllOrders();
    Task<ICollection<Order>> GetOrdersByLastName(string LastName);
    Task<bool> ClientExist(int id);
    Task<Pastry> GetPastryByName(string name);
    Task AddNewOrderPastries(ICollection<Order_Pastry> orderPastries);
    Task AddNewOrder(Order order);
}