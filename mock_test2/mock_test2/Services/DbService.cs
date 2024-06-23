using Microsoft.EntityFrameworkCore;
using mock_test2.Data;
using mock_test2.Models;

namespace mock_test2.Services;

public class DbService : IDbService
{
    private readonly ApplicationContext _context;

    public DbService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Order>> GetAllOrders()
    {
        return await _context.Orders
            .Include(o => o.OrderPastries)
            .ThenInclude(o => o.Pastry)
            .ToListAsync();
    }

    public async Task<ICollection<Order>> GetOrdersByLastName(string LastName)
    {
        return await _context.Orders
            .Include(o => o.Client)
            .Include(o => o.OrderPastries)
            .ThenInclude(o => o.Pastry)
            .Where(o => o.Client.LastName.Equals(LastName))
            .ToListAsync();
    }

    public async Task<bool> ClientExist(int id)
    {
        return await _context.Clients.AnyAsync(c => c.ID == id);
    }
    
    public async Task<Pastry> GetPastryByName(string name)
    {
        return await _context.Pastries.Where(c => c.Name.Equals(name)).FirstAsync();
    }
    
    public async Task AddNewOrder(Order order)
    {
        await _context.AddAsync(order);
        await _context.SaveChangesAsync();
    }
    
    public async Task AddNewOrderPastries(ICollection<Order_Pastry> orderPastries)
    {
        await _context.AddRangeAsync(orderPastries);
        await _context.SaveChangesAsync();
    }
}