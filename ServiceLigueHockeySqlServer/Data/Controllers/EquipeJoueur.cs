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
        private readonly ILogger<EquipeJoueur> _logger;

        public EquipeJoueur(ServiceLigueHockeyContext context,
                            ILogger<EquipeJoueur> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/equipeJoueur
        [HttpGet]
        public ActionResult<IList<EquipeJoueurDto>> GetEquipeJoueur()
        {
            this._logger.LogInformation("--- Début GetEquipeJoueur ---");

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

            this._logger.LogInformation("--- Fin GetEquipeJoueur ---");
            return Ok(retour);
        }

        // GET: api/equipeJoueur/parequipe/5
        [HttpGet("parequipe/{equipeId}/")]
        public ActionResult<EquipeJoueurDto> GetEquipeJoueurParEquipe(int equipeId)
        {
            this._logger.LogInformation("--- Début GetEquipeJoueurParEquipe ---");

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
                this._logger.LogError("Items non-trouvées");
                return NotFound();
            }

            this._logger.LogInformation("--- Fin GetEquipeJoueurParEquipe ---");
            return Ok(lecture.AsEnumerable());
        }

        // GET: api/equipeJoueur/5
        [HttpGet("{id}/")]
        public ActionResult<EquipeJoueurDto> GetEquipeJoueur(int Id)
        {
            this._logger.LogInformation("--- Début GetEquipeJoueur ---");

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
                this._logger.LogError("Item non-trouvé");
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

            this._logger.LogInformation("--- Fin GetEquipeJoueur ---");
            return Ok(retour);
        }

        // PUT: api/equipeJoueur/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipeJoueurBd(int Id, EquipeJoueurDto equipeJoueurDto)
        {
            this._logger.LogInformation("--- Début PutEquipeJoueurBd ---");

            if (Id != equipeJoueurDto.Id)
            {
                this._logger.LogError("Mauvaise requête");
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
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!EquipeJoueurBdExists(Id))
                {
                    this._logger.LogError("Item non-trouvé");
                    return NotFound();
                }
                else
                {
                    this._logger.LogError(dbEx.Message + (dbEx.InnerException == null ? string.Empty : dbEx.InnerException.Message));
                    throw;
                }
            }
            finally
            {
                this._logger.LogInformation("--- Fin PutEquipeJoueurBd ---");
            }

            return NoContent();
        }

        // POST: api/equipeJoueur
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EquipeJoueurDto>> PostEquipeJoueur(EquipeJoueurDto equipeJoueurDto)
        {
            this._logger.LogInformation("--- Début PostEquipeJoueur ---");

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
            catch (DbUpdateException dbEx)
            {
                if (EquipeJoueurBdExists(equipeJoueurBd.Id))
                {
                    this._logger.LogError(string.Format("Un autre item de type equipeJoueur avec le même id existe; id {0}", equipeJoueurBd.Id));
                    return Conflict();
                }
                else
                {
                    this._logger.LogError(string.Format("Erreur dans PostEquipeJoueur, message : {0}", dbEx.Message));
                    throw;
                }
            }

            equipeJoueurDto.Id = equipeJoueurBd.Id;

            this._logger.LogInformation("--- Fin PostEquipeJoueur ---");
            return CreatedAtAction("PostEquipeJoueur", equipeJoueurDto);
        }

        // DELETE: api/equipeJoueur/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEquipeJoueurBd(int Id)
        {
            this._logger.LogInformation("--- Début DeleteEquipeJoueurBd ---");

            var equipeJoueurBd = await _context.equipeJoueur.FindAsync(Id);
            if (equipeJoueurBd == null)
            {
                this._logger.LogInformation("Équipe non-trouvée");
                return NotFound();
            }

            try
            {
                _context.equipeJoueur.Remove(equipeJoueurBd);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return StatusCode(500);
            }
            finally
            {
                this._logger.LogInformation("--- Fin DeleteEquipeJoueurBd ---");
            }

            return NoContent();
        }

        private bool EquipeJoueurBdExists(int Id)
        {
            this._logger.LogInformation("--- On passe dans EquipeJoueurBdExists ---");
            return _context.equipeJoueur.Any(e => e.Id == Id);
        }
    }
}