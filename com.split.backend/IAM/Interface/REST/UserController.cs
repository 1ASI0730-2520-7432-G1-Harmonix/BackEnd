using System.Net.Mime;
using com.split.backend.IAM.Domain.Model.Queries;
using com.split.backend.IAM.Domain.Services;
using com.split.backend.IAM.Infrastructure.Pipeline.MiddleWare.Attributes;
using com.split.backend.IAM.Interface.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace com.split.backend.IAM.Interface;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User endpoints")]
public class UserController(IUserQueryService userQueryService) : ControllerBase
{
    [HttpGet("/id/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetUsersByIdQuery(id);
        var user = await userQueryService.Handle(getUserByIdQuery);
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user!);
        return Ok(userResource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUserQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUserQuery);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }

    [HttpGet("/houseHoldId/{mainHouseHoldId}")]
    public async Task<IActionResult> GetUserByHouseHoldId(string houseHoldId)
    {
        var getUserByMainHouseHoldIdQuery = new GetUserByMainHouseHoldId(houseHoldId);
        var user = await userQueryService.Handle(getUserByMainHouseHoldIdQuery);
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user!);
        return Ok(userResource);
    }

}