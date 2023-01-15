using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VirtualIT.Shared.Server;

namespace VirtualIT.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServerController : ControllerBase
    {
        private readonly IServerService serverService;

        public ServerController(IServerService serverService)
        {
            this.serverService = serverService;
        }

        [HttpGet]
        [SwaggerOperation("Geeft een lijst van de servers met hun Id & naam")]
        [Authorize(Roles = "Administrator, Beheerder, Gebruiker")]
        public async Task<ServerResponse.Index> GetAllIndexAsync()
        {
            var response = await serverService.GetAllIndexAsync();
            return response!;
        }
    }
}
