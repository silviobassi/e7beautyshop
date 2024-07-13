using E7BeautyShop.AgendaService.Application.DTOs;
using E7BeautyShop.AgendaService.Application.DTOs.Mappings;
using E7BeautyShop.AgendaService.Application.DTOs.Requests;
using E7BeautyShop.AgendaService.Application.DTOs.Responses;
using E7BeautyShop.AgendaService.Application.Interfaces;
using E7BeautyShop.AgendaService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E7BeautyShop.AgendaService.Api.Controllers;

[ApiController]
[Route("api")]
public class AgendasController(IAgendaCreateUseCase agendaCreate, IAgendaRepository agendaRepository) : ControllerBase
{
    [HttpGet("get_agendas")]
    public async Task<ActionResult<IEnumerable<AgendaResponse>>> Get()
    {
        var agendas = await agendaRepository.GetAgendasAsync();
        return Ok(agendas.ToAgendasResponse());
    }

    [HttpPost("agenda_create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(AgendaRequest projection)
    {
        var agenda = await agendaCreate.ExecuteAsync(projection);
        return Ok(agenda);
    }
}