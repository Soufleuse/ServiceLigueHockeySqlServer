using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockey.Data.Models;
using ServiceLigueHockey.Data.Models.Dto;

namespace ServiceLigueHockey.Data.Controllers
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

        // GET: api/equipeJoueurBds
        [HttpGet]
        public ActionResult<IList<EquipeJoueurDto>> GetEquipeJoueur()
        {
            var listeEquipeJoueur = from item in _context.equipeJoueur
                                    select new EquipeJoueurDto
                                    {
                                        EquipeId = item.EquipeId,
                                        JoueurId = item.JoueurId,
                                        NoDossard = item.NoDossard,
                                        DateDebutAvecEquipe = item.DateDebutAvecEquipe,
                                        DateFinAvecEquipe = item.DateFinAvecEquipe
                                    };

            var retour = new List<EquipeJoueurDto>();
            foreach (var item in listeEquipeJoueur)
            {
                var unJoueur = _context.joueur.Find(item.JoueurId);

                if(unJoueur == null) unJoueur = new JoueurBd();

                retour.Add(new EquipeJoueurDto
                {
                    EquipeId = item.EquipeId,
                    JoueurId = item.JoueurId,
                    NoDossard = item.NoDossard,
                    PrenomNomJoueur = unJoueur.Prenom + " " + unJoueur.Nom,
                    DateDebutAvecEquipe = item.DateDebutAvecEquipe,
                    DateFinAvecEquipe = item.DateFinAvecEquipe
                });
            }

            return Ok(retour);
        }

        // GET: api/equipeJoueurBds/5/6
        [HttpGet("{EquipeId}/{JoueurId}")]
        public ActionResult<EquipeJoueurDto> GetEquipeJoueurBd(int equipeId, int joueurId)
        {
            var lecture = from item in _context.equipeJoueur
                             where item.EquipeId == equipeId
                             where item.JoueurId == joueurId
                             select new EquipeJoueurDto
                             {
                                 EquipeId = item.EquipeId,
                                 JoueurId = item.JoueurId,
                                 NoDossard = item.NoDossard,
                                 DateDebutAvecEquipe = item.DateDebutAvecEquipe,
                                 DateFinAvecEquipe = item.DateFinAvecEquipe
                             };

            if (lecture == null)
            {
                return NotFound();
            }

            var listeAlignement = new List<EquipeJoueurDto>();
            foreach (var item in lecture)
            {
                var unJoueur = _context.joueur.Find(item.JoueurId);
                if (unJoueur == null) unJoueur = new JoueurBd();

                listeAlignement.Add(new EquipeJoueurDto
                {
                    EquipeId = item.EquipeId,
                    JoueurId = item.JoueurId,
                    NoDossard = item.NoDossard,
                    DateDebutAvecEquipe = item.DateDebutAvecEquipe,
                    DateFinAvecEquipe = item.DateFinAvecEquipe,
                    PrenomNomJoueur = unJoueur.Prenom + " " + unJoueur.Nom
                });
            }

            return Ok(listeAlignement);
        }

        // PUT: api/equipeJoueurBds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{EquipeId}/{JoueurId}")]
        public async Task<IActionResult> PutEquipeJoueurBd(int equipeId, int joueurId, EquipeJoueurDto equipeJoueurDto)
        {
            if (equipeId != equipeJoueurDto.EquipeId || joueurId != equipeJoueurDto.JoueurId)
            {
                return BadRequest();
            }

            var equipeJoueurBd = new EquipeJoueurBd
            {
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
                if (!EquipeJoueurBdExists(equipeId, joueurId))
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

        // POST: api/equipeJoueurBds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EquipeJoueurDto>> PostEquipeJoueurBd(EquipeJoueurDto equipeJoueurDto)
        {
            var equipeJoueurBd = new EquipeJoueurBd
            {
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
                if (EquipeJoueurBdExists(equipeJoueurBd.EquipeId, equipeJoueurBd.JoueurId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(equipeJoueurDto), new { id = equipeJoueurBd.Equipe }, equipeJoueurDto);
        }

        // DELETE: api/equipeJoueurBds/5/6
        [HttpDelete("{EquipeId}/{JoueurId}")]
        public async Task<IActionResult> DeleteEquipeJoueurBd(int equipeId, int joueurId)
        {
            var equipeJoueurBd = await _context.equipeJoueur.FindAsync(equipeId, joueurId);
            if (equipeJoueurBd == null)
            {
                return NotFound();
            }

            _context.equipeJoueur.Remove(equipeJoueurBd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipeJoueurBdExists(int equipeId, int joueurId)
        {
            return _context.equipeJoueur.Any(e => e.EquipeId == equipeId && e.JoueurId == joueurId);
        }
    }
}