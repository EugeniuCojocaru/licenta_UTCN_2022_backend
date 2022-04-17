using Microsoft.AspNetCore.Mvc;

namespace licenta.Controllers
{
    [ApiController]
    [Route("api/institutions")]
    public class InstitutionController: ControllerBase
    {
        [HttpGet()]
        public JsonResult GetInstitutions ()
        {
             return new JsonResult(
                new List<object>

                {
                    new {id=1, name="sadasdas"},
                    new {id=2, name="dasdwscas"}
                }
                );
        }

    }
}
