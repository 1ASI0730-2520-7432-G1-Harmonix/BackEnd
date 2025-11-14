using System.Net.Mime;
using com.split.backend.Bills.Domain.Models.Queries;
using com.split.backend.Bills.Domain.Services;
using com.split.backend.Bills.Interface.REST.Resources;
using com.split.backend.Bills.Interface.REST.Transform;
using com.split.backend.IAM.Infrastructure.Pipeline.MiddleWare.Attributes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace com.split.backend.Bills.Interface.REST;

[ApiController]
[Authorize]
[Route("api/v1/households/{householdId}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Manage bills and their related operations")]
public class BillsController(
    IBillCommandService commandService,
    IBillQueryService queryService) : ControllerBase
{
    int GetUserId() =>
        ((com.split.backend.IAM.Domain.Model.Aggregates.User)HttpContext.Items["User"]!).Id;
    
    [HttpGet]
    [SwaggerOperation(Summary = "Retrieve all bills for the authenticated user")]
    public async Task<ActionResult<IEnumerable<BillResource>>> List(
        string householdId,                     
        [FromQuery] DateOnly? from,
        [FromQuery] DateOnly? to,
        [FromQuery] string? status)
    {
        var items = await queryService.Handle(
            new GetBillsByHouseholdQuery(householdId, from, to, status)
        );
        return Ok(items.Select(BillResourceAssemblers.ToResource));
    }
    
    [HttpPost]
    public async Task<ActionResult<BillResource>> Create(string householdId, [FromBody] CreateBillResource resource)
    {
        int userId = GetUserId(); 
        var created = await commandService.Handle(
            BillResourceAssemblers.ToCommand(householdId, userId, resource)
        );
        if (created is null) return BadRequest();

        return CreatedAtAction(nameof(List), new { householdId }, BillResourceAssemblers.ToResource(created));
    }
    
    [HttpPatch("{id:guid}")]
    [SwaggerOperation("Actualizar un Bill existente")]
    public async Task<ActionResult<BillResource>> Update(
        string householdId,                     
        Guid id,                              
        [FromBody] UpdateBillResource resource)
    {
        var updated = await commandService.Handle(
            BillResourceAssemblers.ToCommand(id, householdId, resource)
        );

        return updated is null
            ? NotFound()
            : Ok(BillResourceAssemblers.ToResource(updated));
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation("Eliminar un Bill")]
    public async Task<IActionResult> Delete(
        string householdId,                     
        Guid id)                              
    {
        var ok = await commandService.Handle(
            new com.split.backend.Bills.Domain.Models.Commands.DeleteBillCommand(id, householdId)
        );
        return ok ? NoContent() : NotFound();
    }
}
