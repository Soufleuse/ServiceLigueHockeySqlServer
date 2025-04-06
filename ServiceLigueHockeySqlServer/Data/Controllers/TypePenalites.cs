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

        // POST: api/TypePenalites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypePenalitesDto>> PostTypePenalitesBd(TypePenalitesDto pTypePenalitesDto)
        {
            var typePenaliteBd = new TypePenalitesBd
            {
                IdTypePenalite = pTypePenalitesDto.IdTypePenalite,
                NbreMinutesPenalitesPourCetteInfraction = pTypePenalitesDto.NbreMinutesPenalitesPourCetteInfraction
            };

            _context.typePenalitesBd.Add(typePenaliteBd);

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

            return CreatedAtAction("PostTypePenalitesBd", pTypePenalitesDto);
        }

        // DELETE: api/TypePenalites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypePenalitesBd(int id)
        {
            var typePenalitesBd = await _context.typePenalitesBd.FindAsync(id);
            if (typePenalitesBd == null)
            {
                return NotFound();
            }

            _context.typePenalitesBd.Remove(typePenalitesBd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypePenalitesBdExists(int id)
        {
            return _context.typePenalitesBd.Any(e => e.IdTypePenalite == id);
        }
    }
}