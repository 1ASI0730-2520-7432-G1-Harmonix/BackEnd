using System.Net.Mime;
using com.split.backend.IAM.Infrastructure.Pipeline.MiddleWare.Attributes; // Para [Authorize]
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;



namespace com.split.backend.Contributions.Interface.REST
{
    [ApiController]
    [Authorize] // <-- 1. COPIAMOS LA SEGURIDAD
    [Route("api/v1/households/{householdId}/[controller]")] // <-- 2. COPIAMOS LA RUTA
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Manage contributions and their related operations")] // (Le cambié la descripción)
    public class ContributionsController : ControllerBase
    {
  
     

        // 4. Esta función para obtener el ID del usuario es perfecta, ¡cópiala tal cual!
        int GetUserId() =>
           ((com.split.backend.IAM.Domain.Model.Aggregates.User)HttpContext.Items["User"]!).Id;


        // 5. AQUÍ CREAMOS ENDPOINTS 

        [HttpPost]
        public IActionResult CreateContribution(string householdId)
        {
            int userId = GetUserId(); // Probamos que podemos obtener el ID del usuario
            return Ok($"Creando contribución para household {householdId} por el usuario {userId}");
        }

        [HttpGet]
        public IActionResult GetContributions(string householdId)
        {
            return Ok($"Obteniendo contribuciones para household {householdId}");
        }
    }
}