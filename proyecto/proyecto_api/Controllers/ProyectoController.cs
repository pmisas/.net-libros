using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public ProyectoController(ILogger<ProyectoController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProyectosDto>>> GetProyectos()
        {
            _logger.LogInformation("Obtener Obras");

            IEnumerable<Proyecto> proyectoList = await _db.Proyectos.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<ProyectosDto>>(proyectoList));
        }

        [HttpGet("id:int", Name ="GetProyecto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProyectosDto>> GetProyecto(int id)
        {
            if(id==0)
            {
                _logger.LogError("Error al traer obra con Id " + id);
                return BadRequest();
            }
            //var proyecto = ProyectoStore.proyectoList.FirstOrDefault(p => p.Id == id);
            var proyecto = await _db.Proyectos.FirstOrDefaultAsync(p => p.Id == id);

            if(proyecto== null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProyectosDto>(proyecto));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProyectoDto>> CrearProyecto([FromBody] ProyectoDto proyectoCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _db.Proyectos.FirstOrDefaultAsync(p=> p.Nombre.ToLower() == proyectoCreateDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExistente", "El libro con este nombre ya existe!");
                return BadRequest(ModelState);
            }

            if (proyectoCreateDto == null)
            {
                return BadRequest(proyectoCreateDto);
            }

            Proyecto modelo = _mapper.Map<Proyecto>(proyectoCreateDto);

            await _db.Proyectos.AddAsync(modelo);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetProyecto", new {id= modelo.Id}, modelo);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProyecto(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var proyecto = await _db.Proyectos.FirstOrDefaultAsync(p=> p.Id == id);
            
            if (proyecto == null)
            {
                return NotFound();
            }
            _db.Proyectos.Remove(proyecto);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProyecto(int id, [FromBody] ProyectoUpdateDto proyectoUpdateDto)
        {
            if (proyectoUpdateDto == null || proyectoUpdateDto.Id != id)
            {
                return BadRequest();
            }

            Proyecto modelo = _mapper.Map<Proyecto>(proyectoUpdateDto);


            await _db.Proyectos.AddAsync(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpPatch]
        [Route("{id:int}UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialProyecto(int id, JsonPatchDocument<ProyectoUpdateDto> patchDto)
        {

            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var proyecto = await _db.Proyectos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            ProyectoUpdateDto proyectoDto = _mapper.Map<ProyectoUpdateDto>(proyecto);

            if (proyecto == null) return NotFound();

            patchDto.ApplyTo(proyectoDto, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Proyecto modelo = _mapper.Map<Proyecto>(proyectoDto);

            _db.Proyectos.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }

    }
}
