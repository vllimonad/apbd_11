using Microsoft.AspNetCore.Mvc;
using mock_test2.Models;
using mock_test2.Models.DTOs;
using mock_test2.Services;

namespace mock_test2.Controllers;

[Route("/api/orders")]
[ApiController]
public class OrdersController: ControllerBase
{
    private readonly IDbService service;

    public OrdersController(IDbService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders(string? ClientLastName = null)
    {
        ICollection<Order> orders;
        if (ClientLastName == null)
        {
            orders = await service.GetAllOrders();
        }
        else
        {
            orders = await service.GetOrdersByLastName(ClientLastName);
        }
        return Ok(orders.Select(o => new GetOrdersDto()
        {
            Id = o.ID,
            AcceptedAt = o.AcceptedAt,
            FulfilledAt = o.FulfilledAt,
            Comments = o.Comments,
            Pastries = o.OrderPastries.Select(op => new GetPastryDto()
            {
                Name = op.Pastry.Name,
                Price = op.Pastry.Price,
                Amount = op.Amount
            }).ToList()
        }));
    }
}