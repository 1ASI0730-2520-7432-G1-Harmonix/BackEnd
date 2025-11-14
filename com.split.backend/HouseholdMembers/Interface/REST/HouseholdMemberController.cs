using System.Net.Mime;
using com.split.backend.HouseholdMembers.Domain.Models.Queries;
using com.split.backend.HouseholdMembers.Domain.Services;
using com.split.backend.HouseholdMembers.Interface.REST.Resources;
using com.split.backend.HouseholdMembers.Interface.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace com.split.backend.HouseholdMembers.Interface.REST;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class HouseholdMemberController(
    IHouseholdMemberCommandService commandService,
    IHouseholdMemberQueryService queryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation("Create Household Member", "Creates a new household member")]
    [SwaggerResponse(201, "The household member has been created")]
    [SwaggerResponse(400, "The household member was not created")]
    public async Task<IActionResult> CreateHouseholdMember(CreateHouseholdMemberResource resource)
    {
        try
        {
            var createCommand = CreateHouseholdMemberCommandFromResourceAssembler.ToCommandFromResource(resource);
            var member = await commandService.Handle(createCommand);
            if (member is null) return BadRequest();
            var memberResource = HouseholdMemberResourceFromEntityAssembler.ToResourceFromEntity(member);
            return CreatedAtAction(nameof(GetHouseholdMemberById), new { id = member.Id }, memberResource);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get Household Member By Id", "Get a household member by its unique identifier")]
    [SwaggerResponse(200, "The household member was found and returned", typeof(HouseholdMemberResource))]
    [SwaggerResponse(404, "The household member was not found")]
    public async Task<IActionResult> GetHouseholdMemberById(int id)
    {
        var query = new GetHouseholdMemberByIdQuery(id);
        var member = await queryService.Handle(query);
        if (member is null) return NotFound();
        var memberResource = HouseholdMemberResourceFromEntityAssembler.ToResourceFromEntity(member);
        return Ok(memberResource);
    }

    [HttpGet("household/{householdId}")]
    [SwaggerOperation("Get Household Members By Household Id", "Get all members of a household")]
    [SwaggerResponse(200, "The household members were found and returned")]
    public async Task<IActionResult> GetHouseholdMembersByHouseholdId(string householdId)
    {
        var query = new GetHouseholdMembersByHouseholdIdQuery(householdId);
        var members = await queryService.Handle(query);
        var memberResources = members.Select(HouseholdMemberResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(memberResources);
    }

    [HttpGet("user/{userId}")]
    [SwaggerOperation("Get Household Members By User Id", "Get all households where a user is a member")]
    [SwaggerResponse(200, "The household members were found and returned")]
    public async Task<IActionResult> GetHouseholdMembersByUserId(int userId)
    {
        var query = new GetHouseholdMembersByUserIdQuery(userId);
        var members = await queryService.Handle(query);
        var memberResources = members.Select(HouseholdMemberResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(memberResources);
    }

    [HttpGet]
    [SwaggerOperation("Get All Household Members", "Get all household members")]
    [SwaggerResponse(200, "The household members were found and returned")]
    public async Task<IActionResult> GetAllHouseholdMembers()
    {
        var query = new GetAllHouseholdMembersQuery();
        var members = await queryService.Handle(query);
        var memberResources = members.Select(HouseholdMemberResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(memberResources);
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Update Household Member", "Updates a household member")]
    [SwaggerResponse(200, "The household member was updated")]
    [SwaggerResponse(404, "The household member was not found")]
    public async Task<IActionResult> UpdateHouseholdMember(int id, UpdateHouseholdMemberResource resource)
    {
        try
        {
            var updateCommand = UpdateHouseholdMemberCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var member = await commandService.Handle(updateCommand);
            if (member is null) return NotFound();
            var memberResource = HouseholdMemberResourceFromEntityAssembler.ToResourceFromEntity(member);
            return Ok(memberResource);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete Household Member", "Deletes a household member")]
    [SwaggerResponse(200, "The household member was deleted")]
    [SwaggerResponse(404, "The household member was not found")]
    public async Task<IActionResult> DeleteHouseholdMember(int id)
    {
        var command = new Domain.Models.Commands.DeleteHouseholdMemberCommand(id);
        var result = await commandService.Handle(command);
        if (!result) return NotFound();
        return Ok(new { message = "Household member deleted successfully" });
    }

    [HttpPost("{id}/promote-representative")]
    [SwaggerOperation("Promote To Representative", "Promotes a household member to representative")]
    [SwaggerResponse(200, "The household member was promoted to representative")]
    [SwaggerResponse(404, "The household member was not found")]
    public async Task<IActionResult> PromoteToRepresentative(int id)
    {
        try
        {
            var command = new Domain.Models.Commands.PromoteToRepresentativeCommand(id);
            var member = await commandService.Handle(command);
            if (member is null) return NotFound();
            var memberResource = HouseholdMemberResourceFromEntityAssembler.ToResourceFromEntity(member);
            return Ok(memberResource);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{id}/demote-representative")]
    [SwaggerOperation("Demote Representative", "Demotes a representative to regular member")]
    [SwaggerResponse(200, "The representative was demoted")]
    [SwaggerResponse(404, "The household member was not found")]
    public async Task<IActionResult> DemoteRepresentative(int id)
    {
        try
        {
            var command = new Domain.Models.Commands.DemoteRepresentativeCommand(id);
            var member = await commandService.Handle(command);
            if (member is null) return NotFound();
            var memberResource = HouseholdMemberResourceFromEntityAssembler.ToResourceFromEntity(member);
            return Ok(memberResource);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

