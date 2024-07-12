using E7BeautyShop.AgendaService.Application.Commands;
using E7BeautyShop.AgendaService.Application.DTOs;
using E7BeautyShop.AgendaService.Application.DTOs.Mappings;
using E7BeautyShop.AgendaService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E7BeautyShop.AgendaService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgendasController(IMediator mediator, IAgendaRepository agendaRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgendaResponse>>> Get()
    {
        var agendas = await agendaRepository.GetAgendasAsync();
        return Ok(agendas.ToAgendas());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(AgendaCreateCommand command)
    {
        var agenda = await mediator.Send(command);
        return Ok(agenda);
    }
}