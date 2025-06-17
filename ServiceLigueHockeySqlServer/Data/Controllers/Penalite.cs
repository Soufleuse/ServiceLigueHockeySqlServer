using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Penalite : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;

        public Penalite(ServiceLigueHockeyContext context)
        {
            _context = context;
        }

        // GET: api/Penalite
        [HttpGet]
        public ActionResult<IQueryable<PenalitesDto>> GetPenaliteDto()
        {
            var listePenalites = from monPenalites in _context.penalites
                              select new PenalitesDto
                              {
                              };
            return Ok(listePenalites);
        }

        // GET: api/Penalite/5
        [HttpGet("{idPartie}")]
        public async Task<ActionResult<PenalitesDto>> GetPenalitesDto(int idPartie)
        {
            var penalitesBd = await _context.penalites.FindAsync(idPartie);

            if (penalitesBd == null)
            {
                return NotFound();
            }

            var penalitesDto = new PenalitesDto
            {
            };

            return Ok(penalitesDto);
        }

        // PUT: api/Penalite/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idPartie}/{tempsPenalite}")]
        public async Task<IActionResult> PutPenalites(int idPartie, DateTime tempsPenalite, PenalitesDto penalites)
        {
            if (idPartie != penalites.IdPartie || tempsPenalite.CompareTo(penalites.MomentDelaPenalite) != 0)
            {
                return BadRequest();
            }

            var penalitesBd = new PenalitesBd
            {
            };

            _context.Entry(penalitesBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PenalitesBdExists(idPartie, tempsPenalite))
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

        // POST: api/Penalites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PenalitesDto>> PostPenalitesDto(PenalitesDto penalites)
        {
            var penalitesBd = new PenalitesBd
            {
            };

            _context.penalites.Add(penalitesBd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostPenalitesDto", penalites);
        }

        // DELETE: api/Penalites/5
       /* [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePenalites(int idPartie)
        {
            var penalitesBd = await _context.penalites.FindAsync(idPartie);
            if (penalitesBd == null)
            {
                return NotFound();
            }

            _context.calendrier.Remove(calendrierBd);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool PenalitesBdExists(int idPartie, DateTime tempsPenalite)
        {
            return _context.penalites.Any(e => e.IdPartie == idPartie && e.MomentDelaPenalite.Equals(tempsPenalite));
        }
    }
}
