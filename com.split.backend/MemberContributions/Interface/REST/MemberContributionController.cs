using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.split.backend.MemberContributions.Interface.REST;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class MemberContributionController
{
    
    
}