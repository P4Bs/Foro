using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ForoWebApp.Controllers.Base;

public class BaseController : Controller
{
    protected string GetRequestId()
    {
        return Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}
