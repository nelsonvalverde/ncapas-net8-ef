using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApiController : ControllerBase
    {
        protected ISender Mediator => HttpContext.RequestServices.GetRequiredService<ISender>();
       
    }
}
