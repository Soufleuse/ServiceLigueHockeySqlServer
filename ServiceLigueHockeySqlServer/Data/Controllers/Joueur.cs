using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    /*
     * Controlleur pour Joueur
     */
    [ApiController]
    [Route("api/[controller]")]
    public class Joueur : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;
        private readonly ILogger<Joueur> _logger;

        public Joueur(ServiceLigueHockeyContext context, ILogger<Joueur> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Joueur
        [HttpGet]
        public ActionResult<IQueryable<JoueurDto>> GetJoueurBd()
        {
            this._logger.LogInformation("--- Début GetJoueurBd ---");
            var listeJoueur = from joueur in _context.joueur
                              select new JoueurDto
                              {
                                  Id = joueur.Id,
                                  Prenom = joueur.Prenom,
                                  Nom = joueur.Nom,
                                  VilleNaissance = joueur.VilleNaissance,
                                  PaysOrigine = joueur.PaysOrigine,
                                  DateNaissance = joueur.DateNaissance
                              };

            this._logger.LogInformation("--- Fin GetJoueurBd ---");
            return Ok(listeJoueur);
        }

        // GET: api/Joueur/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JoueurDto>> GetJoueurBd(int id)
        {
            this._logger.LogInformation("--- Début GetJoueurBd ---");

            var joueurBd = await _context.joueur.FindAsync(id);

            if (joueurBd == null)
            {
                this._logger.LogError("Joueur non-trouvé");
                return NotFound();
            }

            var joueurDto = new JoueurDto
            {
                Id = joueurBd.Id,
                Prenom = joueurBd.Prenom,
                Nom = joueurBd.Nom,
                VilleNaissance = joueurBd.VilleNaissance,
                PaysOrigine = joueurBd.PaysOrigine,
                DateNaissance = joueurBd.DateNaissance
            };

            this._logger.LogInformation("--- Fin GetJoueurBd ---");
            return Ok(joueurDto);
        }

        // GET: api/Joueur/obtenirprenomnom/5
        [HttpGet("obtenirprenomnom/{id}")]
        public async Task<ActionResult<string>> GetPrenomNomJoueur(int id)
        {
            this._logger.LogInformation("--- Début GetPrenomNomJoueur ---");
            var joueurBd = await _context.joueur.FindAsync(id);

            if (joueurBd == null)
            {
                this._logger.LogError("Joueur non-trouvé");
                return NotFound();
            }

            var prenomNom = joueurBd.Prenom + " " + joueurBd.Nom;

            this._logger.LogInformation("--- Fin GetPrenomNomJoueur ---");
            return Ok(prenomNom);
        }

        // PUT: api/Joueur/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJoueurBd(int id, JoueurBd joueurBd)
        {
            this._logger.LogInformation("--- Début PutJoueurBd ---");

            if (id != joueurBd.Id)
            {
                this._logger.LogError("Bad request PutJoueurBd");
                return BadRequest();
            }

            _context.Entry(joueurBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!JoueurBdExists(id))
                {
                    this._logger.LogError("Joueur non-trouvé");
                    return NotFound();
                }
                else
                {
                    this._logger.LogError(dbEx.Message);
                    return StatusCode(500);
                }
            }
            finally
            {
                this._logger.LogInformation("--- Fin PutJoueurBd ---");
            }

            return NoContent();
        }

        // POST: api/Joueur
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JoueurDto>> PostJoueurDto(JoueurDto joueur)
        {
            this._logger.LogInformation("--- Début PostJoueurDto ---");

            var joueurBd = new JoueurBd
            {
                Id = joueur.Id,
                Prenom = joueur.Prenom,
                Nom = joueur.Nom,
                DateNaissance = joueur.DateNaissance,
                VilleNaissance = joueur.VilleNaissance,
                PaysOrigine = joueur.PaysOrigine
            };

            _context.joueur.Add(joueurBd);

            try
            {
                await _context.SaveChangesAsync();
                joueur.Id = joueurBd.Id;
            }
            catch (Exception ex)
            {
                this._logger.LogError(string.Format("Erreur dans PostJoueurDto : {0}", ex.Message));
                return StatusCode(500);
            }
            finally
            {
                this._logger.LogInformation("--- Fin PostJoueurDto ---");
            }

            return CreatedAtAction("PostJoueurDto", joueur);
        }

        // DELETE: api/Joueur/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJoueurBd(int id)
        {
            this._logger.LogInformation("--- Début DeleteJoueurBd ---");

            var joueurBd = await _context.joueur.FindAsync(id);
            if (joueurBd == null)
            {
                return NotFound();
            }

            _context.joueur.Remove(joueurBd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(string.Format("Erreur dans DeleteJoueurBd {0}", ex.Message));
                return StatusCode(500);
            }
            finally
            {
                this._logger.LogInformation("--- Fin DeleteJoueurBd ---");
            }

            return NoContent();
        }

        private bool JoueurBdExists(int id)
        {
            this._logger.LogInformation("--- Passage dans JoueurBdExists ---");
            return _context.joueur.Any(e => e.Id == id);
        }
    }
}