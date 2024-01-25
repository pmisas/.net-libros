using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using proyecto_api.Datos;
using proyecto_api.Modelos;
using proyecto_api.Modelos.Dto;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace proyecto_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly ILogger<ProyectoController> _logger;
        private readonly ApplicationDbContext _db;

        public ProyectoController(ILogger<ProyectoController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProyectoDto>> GetProyectos()
        {
            _logger.LogInformation("Obtener Obras");
            return Ok(_db.Proyectos.ToList());
        }

        [HttpGet("id:int", Name ="GetProyecto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProyectoDto> GetProyecto(int id)
        {
            if(id==0)
            {
                _logger.LogError("Error al traer obra con Id " + id);
                return BadRequest();
            }
            //var proyecto = ProyectoStore.proyectoList.FirstOrDefault(p => p.Id == id);
            var proyecto = _db.Proyectos.FirstOrDefault(p => p.Id == id);

            if(proyecto== null)
            {
                return NotFound();
            }
            return Ok(proyecto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProyectoDto> CrearProyecto([FromBody] ProyectoDto proyectoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_db.Proyectos.FirstOrDefault(p=> p.Nombre.ToLower() == proyectoDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExistente", "El libro con este nombre ya existe!");
                return BadRequest(ModelState);
            }

            if (proyectoDto == null)
            {
                return BadRequest(proyectoDto);
            }

            if (proyectoDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Proyecto modelo = new()
            {
                Id = proyectoDto.Id,
                Nombre = proyectoDto.Nombre,
                Descripcion = proyectoDto.Descripcion,
                Terminado = proyectoDto.Terminado,
                Autor = proyectoDto.Autor,

            };

            _db.Proyectos.Add(modelo);
            _db.SaveChanges();

            return CreatedAtRoute("GetProyecto", new {id= proyectoDto.Id}, proyectoDto);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProyecto(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var proyecto = _db.Proyectos.FirstOrDefault(p=> p.Id == id);
            
            if (proyecto == null)
            {
                return NotFound();
            }
            _db.Proyectos.Remove(proyecto);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProyecto(int id, [FromBody] ProyectoDto proyectoDto)
        {
            if (proyectoDto == null || proyectoDto.Id != id)
            {
                return BadRequest();
            }

            //var proyecto = ProyectoStore.proyectoList.FirstOrDefault(p => p.Id == id);
            //proyecto.Nombre = proyectoDto.Nombre;
            //proyecto.Terminado = proyectoDto.Terminado;
            //proyecto.Descripcion = proyectoDto.Descripcion;
            //proyecto.Autor = proyectoDto.Autor;

            Proyecto modelo = new()
            {
                Id = proyectoDto.Id,
                Nombre = proyectoDto.Nombre,
                Descripcion = proyectoDto.Descripcion,
                Puntuacion = proyectoDto.Puntuacion,
                FechaPublicacion = proyectoDto.FechaPublicacion,
                Terminado = proyectoDto.Terminado,
                Autor = proyectoDto.Autor,
            };

            _db.Proyectos.Update(modelo);
            _db.SaveChanges();
            return NoContent();
        }
        
        [HttpPatch]
        [Route("{id:int}UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialProyecto(int id, [FromBody]  JsonPatchDocument<ProyectoDto> patchDto)
        {

            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var proyecto = _db.Proyectos.FirstOrDefault(p => p.Id == id);

            if (proyecto == null)
            {
                return NotFound();
            }

            var libroDto = new ProyectoDto
            {
                Id = proyecto.Id,
                Nombre = proyecto.Nombre,
                Descripcion = proyecto.Descripcion,
                Puntuacion = proyecto.Puntuacion,
                FechaPublicacion = proyecto.FechaPublicacion,
                Terminado = proyecto.Terminado,
                Autor = proyecto.Autor,
            };
            patchDto.ApplyTo(libroDto, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            proyecto.Id = libroDto.Id;
            proyecto.Nombre = libroDto.Nombre;
            proyecto.Descripcion = libroDto.Descripcion;
            proyecto.Puntuacion = libroDto.Puntuacion;
            proyecto.FechaPublicacion = libroDto.FechaPublicacion;
            proyecto.Terminado = libroDto.Terminado;
            proyecto.Autor = libroDto.Autor;

            _db.Proyectos.Update(proyecto);
            _db.SaveChanges();
            return NoContent();
        }

    }
}
