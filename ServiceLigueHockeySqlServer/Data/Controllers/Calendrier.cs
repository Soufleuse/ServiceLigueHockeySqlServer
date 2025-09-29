using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Calendrier : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;
        private readonly ILogger<Calendrier> _logger;

        public Calendrier(ServiceLigueHockeyContext context,
                          ILogger<Calendrier> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Calendrier
        [HttpGet]
        public ActionResult<IQueryable<CalendrierDto>> GetCalendrierDto()
        {
            this._logger.LogInformation("--- GetCalendrierDto ---");

            var ListeParties = from monCalendrier in _context.calendriers

                              select new CalendrierDto
                              {
                                IdPartie = monCalendrier.IdPartie,
                                IdEquipeHote = monCalendrier.IdEquipeHote,
                                IdEquipeVisiteuse = monCalendrier.IdEquipeVisiteuse,
                                AnneeStats = monCalendrier.AnneeStats,
                                DatePartieJouee = monCalendrier.DatePartieJouee,
                                NbreButsComptesParHote = monCalendrier.NbreButsComptesParHote,
                                NbreButsComptesParVisiteur = monCalendrier.NbreButsComptesParVisiteur,
                                AFiniEnProlongation = monCalendrier.AFiniEnProlongation,
                                AFiniEnTirDeBarrage = monCalendrier.AFiniEnTirDeBarrage,
                                EstUnePartieDeSerie = monCalendrier.EstUnePartieDeSerie,
                                EstUnePartiePresaison=monCalendrier.EstUnePartiePresaison,
                                EstUnePartieSaisonReguliere = monCalendrier.EstUnePartieSaisonReguliere,
                                SommairePartie = monCalendrier.SommairePartie
                              };

            this._logger.LogInformation("--- Fin GetCalendrierDto ---");
            return Ok(ListeParties);
        }

        // GET: api/Calendrier/5
        [HttpGet("{idPartie}")]
        public async Task<ActionResult<CalendrierDto>> GetCalendrierDto(int idPartie)
        {
            this._logger.LogInformation("--- Début GetCalendrierDto ---");

            var calendrierBd = await _context.calendriers.FindAsync(idPartie);

            if (calendrierBd == null)
            {
                return NotFound();
            }

            var calendrierDto = new CalendrierDto
            {
                IdPartie = calendrierBd.IdPartie,
                IdEquipeHote = calendrierBd.IdEquipeHote,
                IdEquipeVisiteuse = calendrierBd.IdEquipeVisiteuse,
                AnneeStats = calendrierBd.AnneeStats,
                DatePartieJouee = calendrierBd.DatePartieJouee,
                NbreButsComptesParHote = calendrierBd.NbreButsComptesParHote,
                NbreButsComptesParVisiteur = calendrierBd.NbreButsComptesParVisiteur,
                AFiniEnProlongation = calendrierBd.AFiniEnProlongation,
                AFiniEnTirDeBarrage = calendrierBd.AFiniEnTirDeBarrage,
                EstUnePartieDeSerie = calendrierBd.EstUnePartieDeSerie,
                EstUnePartiePresaison=calendrierBd.EstUnePartiePresaison,
                EstUnePartieSaisonReguliere = calendrierBd.EstUnePartieSaisonReguliere,
                SommairePartie = calendrierBd.SommairePartie
            };

            this._logger.LogInformation("--- Fin GetCalendrierDto ---");
            return Ok(calendrierDto);
        }

        // PUT: api/Calendrier/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idPartie}")]
        public async Task<IActionResult> PutCalendrier(int idPartie, CalendrierDto calendrier)
        {
            this._logger.LogInformation("--- Début PutCalendrier ---");

            if (idPartie != calendrier.IdPartie)
            {
                this._logger.LogError("Mauvaise requête");
                return BadRequest();
            }

            var calendrierBd = new CalendrierBd
            {
                IdPartie = calendrier.IdPartie,
                IdEquipeHote = calendrier.IdEquipeHote,
                IdEquipeVisiteuse = calendrier.IdEquipeVisiteuse,
                AnneeStats = calendrier.AnneeStats,
                DatePartieJouee = calendrier.DatePartieJouee,
                NbreButsComptesParHote = calendrier.NbreButsComptesParHote,
                NbreButsComptesParVisiteur = calendrier.NbreButsComptesParVisiteur,
                AFiniEnProlongation = calendrier.AFiniEnProlongation,
                AFiniEnTirDeBarrage = calendrier.AFiniEnTirDeBarrage,
                EstUnePartieDeSerie = calendrier.EstUnePartieDeSerie,
                EstUnePartiePresaison=calendrier.EstUnePartiePresaison,
                EstUnePartieSaisonReguliere = calendrier.EstUnePartieSaisonReguliere,
                SommairePartie = calendrier.SommairePartie
            };

            _context.Entry(calendrierBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!CalendrierBdExists(idPartie))
                {
                    this._logger.LogError("Partie non-trouvée");
                    return NotFound();
                }
                else
                {
                    this._logger.LogError(string.Format("Erreur DbUpdateConcurrencyException : {0}", dbEx.Message));
                    throw;
                }
            }
            finally
            {
                this._logger.LogInformation("--- Fin PutCalendrier ---");
            }

            return NoContent();
        }

        // POST: api/Calendrier
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CalendrierDto>> PostCalendrierDto(CalendrierDto calendrier)
        {
            this._logger.LogInformation("--- Début PostCalendrierDto ---");

            var calendrierBd = new CalendrierBd
            {
                IdPartie = calendrier.IdPartie,
                IdEquipeHote = calendrier.IdEquipeHote,
                IdEquipeVisiteuse = calendrier.IdEquipeVisiteuse,
                AnneeStats = calendrier.AnneeStats,
                DatePartieJouee = calendrier.DatePartieJouee,
                NbreButsComptesParHote = calendrier.NbreButsComptesParHote,
                NbreButsComptesParVisiteur = calendrier.NbreButsComptesParVisiteur,
                AFiniEnProlongation = calendrier.AFiniEnProlongation,
                AFiniEnTirDeBarrage = calendrier.AFiniEnTirDeBarrage,
                EstUnePartieDeSerie = calendrier.EstUnePartieDeSerie,
                EstUnePartiePresaison=calendrier.EstUnePartiePresaison,
                EstUnePartieSaisonReguliere = calendrier.EstUnePartieSaisonReguliere,
                SommairePartie = calendrier.SommairePartie
            };

            _context.calendriers.Add(calendrierBd);
            await _context.SaveChangesAsync();

            calendrier.IdPartie = calendrierBd.IdPartie;

            this._logger.LogInformation("--- Fin PostCalendrierDto ---");
            return CreatedAtAction("PostCalendrierDto", calendrier);
        }

        // DELETE: api/Calendrier/5
        [HttpDelete("{idPartie}")]
        public async Task<IActionResult> DeleteCalendrier(int idPartie)
        {
            this._logger.LogInformation("--- Début DeleteCalendrier ---");

            var calendrierBd = await _context.calendriers.FindAsync(idPartie);
            if (calendrierBd == null)
            {
                this._logger.LogError("Partie non-trouvée");
                return NotFound();
            }

            _context.calendriers.Remove(calendrierBd);
            await _context.SaveChangesAsync();

            this._logger.LogInformation("--- Fin DeleteCalendrier ---");
            return NoContent();
        }

        private bool CalendrierBdExists(int idPartie)
        {
            this._logger.LogInformation("--- Passage dans CalendrierBdExists ---");
            return _context.calendriers.Any(e => e.IdPartie == idPartie);
        }
    }
}
