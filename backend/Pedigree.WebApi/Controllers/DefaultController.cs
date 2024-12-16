using Microsoft.AspNetCore.Mvc;

namespace Pedigree.WebApi.Controllers
{
    [ApiController]
    public class DefaultController : ControllerBase
    {
        protected void ValidateModelState()
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                       .SelectMany(v => v.Errors)
                       .Select(e => e.ErrorMessage));

                throw new BadHttpRequestException(message);
            }
        }
    }
}