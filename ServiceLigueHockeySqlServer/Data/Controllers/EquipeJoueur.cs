using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    /*
     * Controlleur pour EquipeJoueur
     */
    [ApiController]
    [Route("api/[controller]")]
    public class EquipeJoueur : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;

        public EquipeJoueur(ServiceLigueHockeyContext context)
        {
            _context = context;
        }

        // GET: api/equipeJoueur
        [HttpGet]
        public ActionResult<IList<EquipeJoueurDto>> GetEquipeJoueur()
        {
            var listeEquipeJoueur = from item in _context.equipeJoueur
                                    select new EquipeJoueurDto
                                    {
                                        Id = item.Id,
                                        EquipeId = item.EquipeId,
                                        JoueurId = item.JoueurId,
                                        NoDossard = item.NoDossard,
                                        DateDebutAvecEquipe = item.DateDebutAvecEquipe,
                                        DateFinAvecEquipe = item.DateFinAvecEquipe
                                    };

            var retour = new List<EquipeJoueurDto>();
            foreach (var item in listeEquipeJoueur)
            {
                retour.Add(new EquipeJoueurDto
                {
                    Id = item.Id,
                    EquipeId = item.EquipeId,
                    JoueurId = item.JoueurId,
                    NoDossard = item.NoDossard,
                    DateDebutAvecEquipe = item.DateDebutAvecEquipe,
                    DateFinAvecEquipe = item.DateFinAvecEquipe
                });
            }

            retour.ForEach(monJoueur =>
            {
                var unJoueur = _context.joueur.Find(monJoueur.JoueurId);

                if(unJoueur == null) unJoueur = new JoueurBd();
                monJoueur.PrenomNomJoueur = unJoueur.Prenom + " " + unJoueur.Nom;
            });

            return Ok(retour);
        }

        // GET: api/equipeJoueur/parequipe/5
        [HttpGet("parequipe/{equipeId}/")]
        public ActionResult<EquipeJoueurDto> GetEquipeJoueurParEquipe(int equipeId)
        {
            var lecture = from item in _context.equipeJoueur
                             where item.EquipeId == equipeId &&
                                   (!item.DateFinAvecEquipe.HasValue || item.DateFinAvecEquipe.Value > DateTime.Now)
                             select new EquipeJoueurDto
                             {
                                Id = item.Id,
                                EquipeId = item.EquipeId,
                                JoueurId = item.JoueurId,
                                NoDossard = item.NoDossard,
                                DateDebutAvecEquipe = item.DateDebutAvecEquipe,
                                DateFinAvecEquipe = item.DateFinAvecEquipe,
                                PrenomNomJoueur = string.Format("{0} {1}", item.Joueur.Prenom, item.Joueur.Nom)
                             };

            if (lecture == null)
            {
                return NotFound();
            }

            return Ok(lecture.AsEnumerable());
        }

        // GET: api/equipeJoueur/5
        [HttpGet("{id}/")]
        public ActionResult<EquipeJoueurDto> GetEquipeJoueur(int Id)
        {
            var lecture = (from item in _context.equipeJoueur
                           where item.Id == Id
                           select new EquipeJoueurDto
                           {
                              Id = item.Id,
                              EquipeId = item.EquipeId,
                              JoueurId = item.JoueurId,
                              NoDossard = item.NoDossard,
                              DateDebutAvecEquipe = item.DateDebutAvecEquipe,
                              DateFinAvecEquipe = item.DateFinAvecEquipe
                           }).FirstOrDefault();

            if (lecture == null)
            {
                return NotFound();
            }

            var retour = new EquipeJoueurDto {
                Id = lecture.Id,
                EquipeId = lecture.EquipeId,
                JoueurId = lecture.JoueurId,
                NoDossard = lecture.NoDossard,
                DateDebutAvecEquipe = lecture.DateDebutAvecEquipe,
                DateFinAvecEquipe = lecture.DateFinAvecEquipe
            };

            return Ok(retour);
        }

        // PUT: api/equipeJoueur/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipeJoueurBd(int Id, EquipeJoueurDto equipeJoueurDto)
        {
            if (Id != equipeJoueurDto.Id)
            {
                return BadRequest();
            }

            var equipeJoueurBd = new EquipeJoueurBd
            {
                Id = equipeJoueurDto.Id,
                EquipeId = equipeJoueurDto.EquipeId,
                JoueurId = equipeJoueurDto.JoueurId,
                NoDossard = equipeJoueurDto.NoDossard,
                DateDebutAvecEquipe = equipeJoueurDto.DateDebutAvecEquipe,
                DateFinAvecEquipe = equipeJoueurDto.DateFinAvecEquipe
            };

            _context.Entry(equipeJoueurBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipeJoueurBdExists(Id))
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

        // POST: api/equipeJoueur
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EquipeJoueurDto>> PostEquipeJoueur(EquipeJoueurDto equipeJoueurDto)
        {
            var equipeJoueurBd = new EquipeJoueurBd
            {
                Id = equipeJoueurDto.Id,
                EquipeId = equipeJoueurDto.EquipeId,
                JoueurId = equipeJoueurDto.JoueurId,
                NoDossard = equipeJoueurDto.NoDossard,
                DateDebutAvecEquipe = equipeJoueurDto.DateDebutAvecEquipe,
                DateFinAvecEquipe = equipeJoueurDto.DateFinAvecEquipe
            };

            _context.equipeJoueur.Add(equipeJoueurBd);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EquipeJoueurBdExists(equipeJoueurBd.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            equipeJoueurDto.Id = equipeJoueurBd.Id;

            return CreatedAtAction("PostEquipeJoueur", equipeJoueurDto);
        }

        // DELETE: api/equipeJoueur/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEquipeJoueurBd(int Id)
        {
            var equipeJoueurBd = await _context.equipeJoueur.FindAsync(Id);
            if (equipeJoueurBd == null)
            {
                return NotFound();
            }

            _context.equipeJoueur.Remove(equipeJoueurBd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipeJoueurBdExists(int Id)
        {
            return _context.equipeJoueur.Any(e => e.Id == Id);
        }
    }
}