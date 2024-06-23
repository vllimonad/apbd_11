using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using mock_test2.Models;
using mock_test2.Models.DTOs;
using mock_test2.Services;

namespace mock_test2.Controllers;

[ApiController]
[Route("/api/clients")]
public class ClientsController: ControllerBase
{
    private readonly IDbService service;

    public ClientsController(IDbService service)
    {
        this.service = service;
    }

    [HttpPost("/{ClientId}/orders")]
    public async Task<IActionResult> AddOrder(int ClientId, AddOrderDto dto)
    {
        if (!await service.ClientExist(ClientId)) return NotFound("Client with this id does not exist");
        
        var order = new Order()
        {
            ClientId = ClientId,
            EmployeeId = dto.EmployeeId,
            AcceptedAt = dto.AcceptedAt,
            Comments = dto.Comments
        };
        
        ICollection<Order_Pastry> orderPastries = new List<Order_Pastry>();
        for (int i = 0; i < dto.Pastries.Count; i++)
        {
            Pastry pastry = await service.GetPastryByName(dto.Pastries.ElementAt(i).Name);
            if (pastry == null) return NotFound("Unknown pastry");
            orderPastries.Add(new Order_Pastry()
            {
                PastryId = pastry.ID,
                Order = order,
                Amount = dto.Pastries.ElementAt(i).Amount,
                Comments = dto.Pastries.ElementAt(i).Comments
            });
        }

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await service.AddNewOrder(order);
            await service.AddNewOrderPastries(orderPastries);
    
            scope.Complete();
        }

        return Ok();
    }
}