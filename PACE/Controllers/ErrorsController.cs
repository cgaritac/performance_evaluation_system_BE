using Microsoft.AspNetCore.Mvc;
using PACE.Utils.Constants;

namespace PACE.Controllers;

public class ErrorsController : Controller
{
    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        throw new Exception(ErrorConstants.TestError);
    }
}
