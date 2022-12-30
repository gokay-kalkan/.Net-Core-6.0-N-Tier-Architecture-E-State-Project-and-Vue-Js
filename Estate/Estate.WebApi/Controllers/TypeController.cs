using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Estate.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Type = EntityLayer.Entities.Type;

namespace Estate.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        TypeService typeService;

        public TypeController(TypeService typeService)
        {
            this.typeService = typeService;


        }

        [HttpGet]
        public IActionResult TypeGet()
        {
            var list = typeService.List(x => x.Status == true);
            if (list == null)
            {
                return BadRequest();
            }
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetType(int id)
        {
            var getsituation = typeService.GetById(id);
            if (getsituation == null)
            {
                return BadRequest();
            }
            return Ok(getsituation);
        }

        [HttpPost]

        public IActionResult Create(TypeModel model)
        {
            if (model == null)

            {
                return BadRequest();
            }
            Type type = new()
            {
                TypeName = model.TypeName,
                SituationId = model.SituationId
            };
            typeService.Add(type);

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = typeService.GetById(id);
            typeService.Delete(delete);
            return Ok(delete);
        }

        [HttpPut]
        public IActionResult Update(EntityLayer.Entities.Type data)
        {

            typeService.Update(data);
            return Ok(data);
        }
    }
}
