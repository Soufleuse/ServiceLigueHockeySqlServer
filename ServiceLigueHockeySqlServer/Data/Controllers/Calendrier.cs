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

        public Calendrier(ServiceLigueHockeyContext context)
        {
            _context = context;
        }

        // GET: api/Calendrier
        [HttpGet]
        public ActionResult<IQueryable<CalendrierDto>> GetCalendrierDto()
        {
            var listeCalendrier = from monCalendrier in _context.calendriers
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

            return Ok(listeCalendrier);
        }

        // GET: api/Calendrier/5
        [HttpGet("{idPartie}")]
        public async Task<ActionResult<CalendrierDto>> GetCalendrierDto(int idPartie)
        {
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

            return Ok(calendrierDto);
        }

        // PUT: api/Calendrier/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idPartie}")]
        public async Task<IActionResult> PutCalendrier(int idPartie, CalendrierDto calendrier)
        {
            if (idPartie != calendrier.IdPartie)
            {
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

            _context.Entry(calendrier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendrierBdExists(idPartie))
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

        // POST: api/Calendrier
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CalendrierDto>> PostCalendrierDto(CalendrierDto calendrier)
        {
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

            return CreatedAtAction("PostCalendrierDto", calendrier);
        }

        // DELETE: api/Calendrier/5
        [HttpDelete("{idPartie}")]
        public async Task<IActionResult> DeleteCalendrier(int idPartie)
        {
            var calendrierBd = await _context.calendriers.FindAsync(idPartie);
            if (calendrierBd == null)
            {
                return NotFound();
            }

            _context.calendriers.Remove(calendrierBd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalendrierBdExists(int idPartie)
        {
            return _context.calendriers.Any(e => e.IdPartie == idPartie);
        }
    }
}
