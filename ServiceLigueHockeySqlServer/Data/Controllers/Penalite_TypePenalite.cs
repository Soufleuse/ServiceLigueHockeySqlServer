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
        private readonly ILogger<Penalite_TypePenalite> _logger;

        public Penalite_TypePenalite(ServiceLigueHockeyContext context, ILogger<Penalite_TypePenalite> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Penalite
        [HttpGet]
        public ActionResult<IQueryable<Penalite_TypePenaliteDto>> GetPenalite_TypePenaliteDto()
        {
            this._logger.LogInformation("--- Début GetPenalite_TypePenaliteDto ---");
            var listePenalite_TypePenalites = from monPenalite_TypePenalites in _context.penalite_TypePenalites
                                              select new Penalite_TypePenaliteDto
                                              {
                                                  IdJoueurPenalise = monPenalite_TypePenalites.IdJoueurPenalise,
                                                  IdPenalite = monPenalite_TypePenalites.IdPenalite,
                                                  IdTypePenalite = monPenalite_TypePenalites.IdTypePenalite,
                                                  MomentDelaPenalite = monPenalite_TypePenalites.MomentDelaPenalite
                                              };

            this._logger.LogInformation("--- Fin GetPenalite_TypePenaliteDto ---");
            return Ok(listePenalite_TypePenalites);
        }

        // GET: api/Penalite_TypePenalite/5
        [HttpGet("{idPenalite}")]
        public async Task<ActionResult<Penalite_TypePenaliteDto>> GetPenalite_TypePenalitesDto(int idPenalite)
        {
            this._logger.LogInformation("--- Début GetPenalite_TypePenalitesDto ---");
            var penalite_TypePenalitesBd = await _context.penalite_TypePenalites.FindAsync(idPenalite);

            if (penalite_TypePenalitesBd == null)
            {
                this._logger.LogError("penalite_TypePenalitesBd non-trouvé");
                return NotFound();
            }

            var penalite_TypePenalitesDto = new Penalite_TypePenaliteDto
            {
                IdJoueurPenalise = penalite_TypePenalitesBd.IdJoueurPenalise,
                IdPenalite = penalite_TypePenalitesBd.IdPenalite,
                IdTypePenalite = penalite_TypePenalitesBd.IdTypePenalite,
                MomentDelaPenalite = penalite_TypePenalitesBd.MomentDelaPenalite
            };

            this._logger.LogInformation("--- Fin GetPenalite_TypePenalitesDto ---");
            return Ok(penalite_TypePenalitesDto);
        }

        // PUT: api/Penalite_TypePenalite/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idPenalite}")]
        public async Task<IActionResult> PutPenalites(int idPenalite, Penalite_TypePenaliteDto penalite_TypePenaliteDto)
        {
            this._logger.LogInformation("--- Début PutPenalites ---");

            if (idPenalite != penalite_TypePenaliteDto.IdPenalite)
            {
                this._logger.LogInformation("Pénalité non-trouvée");
                return BadRequest();
            }

            var penalite_TypePenaliteBd = new Penalite_TypePenaliteBd
            {
                IdJoueurPenalise = penalite_TypePenaliteDto.IdJoueurPenalise,
                IdPenalite = penalite_TypePenaliteDto.IdPenalite,
                IdTypePenalite = penalite_TypePenaliteDto.IdTypePenalite,
                MomentDelaPenalite = penalite_TypePenaliteDto.MomentDelaPenalite
            };

            _context.Entry(penalite_TypePenaliteBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!Penalite_TypePenalitesBdExists(idPenalite))
                {
                    this._logger.LogInformation("Pénalité non-trouvée");
                    return NotFound();
                }
                else
                {
                    this._logger.LogInformation(string.Format("Erreur dans PutPenalites {0}",dbEx.Message));
                    return StatusCode(500);
                }
            }
            finally
            {
                this._logger.LogInformation("--- Fin PutPenalites ---");
            }

            return NoContent();
        }

        // POST: api/Penalite_TypePenalites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Penalite_TypePenaliteDto>> PostPenalite_TypePenalitesDto(Penalite_TypePenaliteDto penalites)
        {
            this._logger.LogInformation("--- Début PostPenalite_TypePenalitesDto ---");

            var penalite_TypePenalitebd = new Penalite_TypePenaliteBd
            {
                IdJoueurPenalise = penalites.IdJoueurPenalise,
                IdPenalite = penalites.IdPenalite,
                IdTypePenalite = penalites.IdTypePenalite,
                MomentDelaPenalite = penalites.MomentDelaPenalite
            };

            _context.penalite_TypePenalites.Add(penalite_TypePenalitebd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogInformation(string.Format("Erreur dans PostPenalite_TypePenalitesDto : {0}", ex));
            }
            finally
            {
                this._logger.LogInformation("--- Fin PostPenalite_TypePenalitesDto ---");
            }

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
            this._logger.LogInformation("Passage dans Penalite_TypePenalitesBdExists");
            return _context.penalite_TypePenalites.Any(e => e.IdPenalite == idPenalite);
        }
    }
}
