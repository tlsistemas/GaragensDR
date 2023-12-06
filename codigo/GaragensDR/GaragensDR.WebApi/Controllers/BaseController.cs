using Microsoft.AspNetCore.Mvc;

namespace GaragensDR.WebApi.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        // returns the current authenticated account (null if not logged in)
    }
}
