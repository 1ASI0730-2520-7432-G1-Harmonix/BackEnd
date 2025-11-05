using System.Net.Mime;
using com.split.backend.Households.Domain.Models.Queries;
using com.split.backend.Households.Domain.Services;
using com.split.backend.Households.Interface.REST.Resources;
using com.split.backend.Households.Interface.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace com.split.backend.Households.Interface.REST;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class HouseHoldController(
    IHouseHoldCommandService commandService,
    IHouseHoldQueryService queryService) : ControllerBase
{
    [HttpGet("{Id}")]
    [SwaggerOperation("Get HouseHold ById", "Get a Household by its unique identifier",
        OperationId = "GetHouseHoldById")]
    [SwaggerResponse(200, "The household was found and returned", typeof(HouseHoldResource))]
    [SwaggerResponse(404, "The household was not found")]
    public async Task<IActionResult> GetHouseHoldById(string id)
    {
        var getHouseholdByIdQuery = new GetHouseHoldByIdQuery(id);
        var houseHold = await queryService.Handle(getHouseholdByIdQuery);
        if (houseHold is null) return NotFound();
        var householdResource = HouseholdResourceFromEntityAssembler.ToResourceFromEntity(houseHold);
        return Ok(householdResource);
    }

    [HttpPost]
    [SwaggerOperation("Create Household", "Creates a new Household")]
    [SwaggerResponse(201, "The HouseHold has been created")]
    [SwaggerResponse(400, "The household was not created")]
    public async Task<IActionResult> CreateHousehold(CreateHouseHoldResource resource)
    {
        var createHouseHoldCommand = CreateHouseHoldCommandFromResourceAssembler.ToCommandFromResource(resource);
        var household = await commandService.Handle(createHouseHoldCommand);
        if (household is null) return BadRequest();
        var houseHoldResource = HouseholdResourceFromEntityAssembler.ToResourceFromEntity(household);
        return CreatedAtAction(nameof(GetHouseHoldById), new { householdId = household.Id }, houseHoldResource);
    }
    
    
}