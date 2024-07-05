using E7BeautyShop.AgendaService.Application.Ports.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace E7BeautyShop.AgendaService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreateAgendaController(ICreateAgendaUseCasePort createAgendaUseCasePort)
{
    
}
