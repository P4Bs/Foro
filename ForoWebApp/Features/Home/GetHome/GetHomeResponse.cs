using ForoWebApp.Features.Common;
using ForoWebApp.Models.Domain;

namespace ForoWebApp.Features.Home.GetHome;

public class GetHomeResponse(bool success, string[]? errors = null) : BaseResponse(success, errors)
{
    public IEnumerable<Theme> Themes { get; set; }
}
