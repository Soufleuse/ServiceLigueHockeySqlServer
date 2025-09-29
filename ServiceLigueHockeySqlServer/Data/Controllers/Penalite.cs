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
        private readonly ILogger<Penalite> _logger;

        public Penalite(ServiceLigueHockeyContext context, ILogger<Penalite> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Penalite
        [HttpGet]
        public ActionResult<IQueryable<PenalitesDto>> GetPenaliteDto()
        {
            this._logger.LogInformation("--- Début GetPenaliteDto ---");
            var listePenalites = from monPenalites in _context.penalites
                                 select new PenalitesDto
                                 {
                                     IdJoueurPenalise = monPenalites.IdJoueurPenalise,
                                     IdPartie = monPenalites.IdPartie,
                                     MomentDelaPenalite = monPenalites.MomentDelaPenalite
                                 };

            this._logger.LogInformation("--- Fin GetPenaliteDto ---");
            return Ok(listePenalites);
        }

        // GET: api/Penalite/5
        [HttpGet("{idPartie}")]
        public async Task<ActionResult<PenalitesDto>> GetPenalitesDto(int idPartie)
        {
            this._logger.LogInformation("--- Début GetPenalitesDto ---");
            var penalitesBd = await _context.penalites.FindAsync(idPartie);

            if (penalitesBd == null)
            {
                this._logger.LogError("Pénalité non-trouvée");
                return NotFound();
            }

            var penalitesDto = new PenalitesDto
            {
                IdJoueurPenalise = penalitesBd.IdJoueurPenalise,
                IdPartie = penalitesBd.IdPartie,
                MomentDelaPenalite = penalitesBd.MomentDelaPenalite
            };

            this._logger.LogInformation("--- Fin GetPenalitesDto ---");
            return Ok(penalitesDto);
        }

        // PUT: api/Penalite/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idPartie}/{tempsPenalite}")]
        public async Task<IActionResult> PutPenalites(int idPartie, DateTime tempsPenalite, PenalitesDto penalites)
        {
            this._logger.LogInformation("--- Début PutPenalites ---");

            if (idPartie != penalites.IdPartie || tempsPenalite.CompareTo(penalites.MomentDelaPenalite) != 0)
            {
                this._logger.LogError("Mauvaise requête");
                return BadRequest();
            }

            var penalitesBd = new PenalitesBd
            {
                IdJoueurPenalise = penalites.IdJoueurPenalise,
                IdPartie = penalites.IdPartie,
                MomentDelaPenalite = penalites.MomentDelaPenalite
            };

            _context.Entry(penalitesBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!PenalitesBdExists(idPartie, tempsPenalite))
                {
                    this._logger.LogInformation("--- Pénalité non-trouvée ---");
                    return NotFound();
                }
                else
                {
                    this._logger.LogError(string.Format("Erreur dans PutPenalites : {0}", dbEx.Message));
                    return StatusCode(500);
                }
            }
            finally
            {
                this._logger.LogInformation("--- Fin PutPenalites ---");
            }

            return NoContent();
        }

        // POST: api/Penalites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PenalitesDto>> PostPenalitesDto(PenalitesDto penalites)
        {
            this._logger.LogInformation("PostPenalitesDto");
            var penalitesBd = new PenalitesBd
            {
                IdJoueurPenalise = penalites.IdJoueurPenalise,
                IdPartie = penalites.IdPartie,
                MomentDelaPenalite = penalites.MomentDelaPenalite
            };

            _context.penalites.Add(penalitesBd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(string.Format("Erreur dans PostPenalitesDto", ex.Message));
                return StatusCode(500);
            }
            finally
            {
                this._logger.LogInformation("--- Fin PostPenalitesDto ---");
            }

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
            this._logger.LogInformation("Passage dans PenalitesBdExists");
            return _context.penalites.Any(e => e.IdPartie == idPartie && e.MomentDelaPenalite.Equals(tempsPenalite));
        }
    }
}
