namespace AzureTraining.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("/")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<string> values = await this.ValueRepo(default);
            return this.Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == default)
            {
                return BadRequest($"Invalid id: {id}");
            }

            List<string> values = await this.ValueRepo(id);
            return this.Ok(values.FirstOrDefault());
        }

        /// <summary>
        /// The value repo.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private Task<List<string>> ValueRepo(int id)
        {
            var values = new List<string>();

            if (id == default)
            {
                for (var i = 0; i < 100; i++)
                {
                    values.Add($"value{i}");
                }
            }
            else
            {
                values.Add($"value{id}");
            }

            return Task.FromResult(values);
        }
    }
}