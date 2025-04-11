using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypePenalites : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;

        public TypePenalites(ServiceLigueHockeyContext context)
        {
            _context = context;
        }

        // GET: api/TypePenalites
        [HttpGet]
        public ActionResult<IQueryable<TypePenalitesDto>> GetTypePenalitesDto()
        {
            var listeTypePenalites = from monTypePenalites in _context.typePenalites
                              select new TypePenalitesDto
                              {
                                IdTypePenalite = monTypePenalites.IdTypePenalite,
                                NbreMinutesPenalitesPourCetteInfraction = monTypePenalites.NbreMinutesPenalitesPourCetteInfraction,
                                DescriptionPenalite = monTypePenalites.DescriptionPenalite
                              };

            return Ok(listeTypePenalites);
        }

        // GET: api/TypePenalites/5
        [HttpGet("{IdTypePenalite}")]
        public async Task<ActionResult<TypePenalitesDto>> GetTypePenalitesDto(short IdTypePenalite)
        {
            var typePenalitesBd = await _context.typePenalites.FindAsync(IdTypePenalite);

            if (typePenalitesBd == null)
            {
                return NotFound();
            }

            var typePenalitesDto = new TypePenalitesDto
            {
                IdTypePenalite = typePenalitesBd.IdTypePenalite,
                NbreMinutesPenalitesPourCetteInfraction = typePenalitesBd.NbreMinutesPenalitesPourCetteInfraction,
                DescriptionPenalite = typePenalitesBd.DescriptionPenalite
            };

            return Ok(typePenalitesDto);
        }

        // POST: api/TypePenalites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypePenalitesDto>> PostTypePenalitesBd(TypePenalitesBd typePenaliteBd)
        {
            /*var typePenaliteBd = new TypePenalitesBd
            {
                IdTypePenalite = pTypePenalitesDto.IdTypePenalite,
                NbreMinutesPenalitesPourCetteInfraction = pTypePenalitesDto.NbreMinutesPenalitesPourCetteInfraction,
                DescriptionPenalite = pTypePenalitesDto.DescriptionPenalite
            };*/

            _context.typePenalites.Add(typePenaliteBd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (TypePenalitesBdExists(typePenaliteBd.IdTypePenalite))
                {
                    return Conflict(ex);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("PostTypePenalitesBd", typePenaliteBd);
        }

        // PUT: api/TypePenalites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{IdTypePenalite}")]
        public async Task<IActionResult> PutAnneeStats(short IdTypePenalite, TypePenalitesDto pTypePenalitesDto) {
            
            if (IdTypePenalite != pTypePenalitesDto.IdTypePenalite)
            {
                return BadRequest();
            }

            var typePenalitesBd = new TypePenalitesBd
            {
                IdTypePenalite = pTypePenalitesDto.IdTypePenalite,
                NbreMinutesPenalitesPourCetteInfraction = pTypePenalitesDto.NbreMinutesPenalitesPourCetteInfraction,
                DescriptionPenalite = pTypePenalitesDto.DescriptionPenalite
            };

            _context.Entry(typePenalitesBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypePenalitesBdExists(IdTypePenalite))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/TypePenalites/5
        [HttpDelete("{IdTypePenalite}")]
        public async Task<IActionResult> DeleteTypePenalitesBd(short IdTypePenalite)
        {
            var typePenalitesBd = await _context.typePenalites.FindAsync(IdTypePenalite);
            if (typePenalitesBd == null)
            {
                return NotFound();
            }

            _context.typePenalites.Remove(typePenalitesBd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypePenalitesBdExists(short id)
        {
            return _context.typePenalites.Any(e => e.IdTypePenalite == id);
        }
    }
}