using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Penalite_TypePenalite : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;

        public Penalite_TypePenalite(ServiceLigueHockeyContext context)
        {
            _context = context;
        }

        // GET: api/Penalite
        [HttpGet]
        public ActionResult<IQueryable<Penalite_TypePenaliteDto>> GetPenalite_TypePenaliteDto()
        {
            var listePenalite_TypePenalites = from monPenalite_TypePenalites in _context.penalite_TypePenalites
                              select new Penalite_TypePenaliteDto
                              {
                              };
            return Ok(listePenalite_TypePenalites);
        }

        // GET: api/Penalite_TypePenalite/5
        [HttpGet("{idPenalite}")]
        public async Task<ActionResult<Penalite_TypePenaliteDto>> GetPenalite_TypePenalitesDto(int idPenalite)
        {
            var penalite_TypePenalitesBd = await _context.penalite_TypePenalites.FindAsync(idPenalite);

            if (penalite_TypePenalitesBd == null)
            {
                return NotFound();
            }

            var penalite_TypePenalitesDto = new Penalite_TypePenaliteDto
            {
            };

            return Ok(penalite_TypePenalitesDto);
        }

        // PUT: api/Penalite_TypePenalite/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idPenalite}")]
        public async Task<IActionResult> PutPenalites(int idPenalite, Penalite_TypePenaliteDto penalite_TypePenaliteDto)
        {
            if (idPenalite != penalite_TypePenaliteDto.IdPenalite)
            {
                return BadRequest();
            }

            var penalite_TypePenaliteBd = new Penalite_TypePenaliteBd
            {
            };

            _context.Entry(penalite_TypePenaliteBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Penalite_TypePenalitesBdExists(idPenalite))
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

        // POST: api/Penalite_TypePenalites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Penalite_TypePenaliteDto>> PostPenalite_TypePenalitesDto(Penalite_TypePenaliteDto penalites)
        {
            var penalite_TypePenalitebd = new Penalite_TypePenaliteBd
            {
            };

            _context.penalite_TypePenalites.Add(penalite_TypePenalitebd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostPenalitesDto", penalites);
        }

        // DELETE: api/Penalite_TypePenalites/5
       /* [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePenalite_TypePenalites(int idPartie)
        {
            var Penalite_TypePenalitesBd = await _context.Penalite_TypePenalites.FindAsync(idPartie);
            if (Penalite_TypePenalitesBd == null)
            {
                return NotFound();
            }

            _context.calendrier.Remove(calendrierBd);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool Penalite_TypePenalitesBdExists(int idPenalite)
        {
            return _context.penalite_TypePenalites.Any(e => e.IdPenalite == idPenalite);
        }
    }
}
