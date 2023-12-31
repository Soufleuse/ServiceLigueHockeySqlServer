using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Joueur(ServiceLigueHockeyContext context)
        {
            _context = context;
        }

        // GET: api/Joueur
        [HttpGet]
        public ActionResult<IQueryable<JoueurDto>> GetJoueurBd()
        {
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
            return Ok(listeJoueur);
        }

        // GET: api/Joueur/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JoueurDto>> GetJoueurBd(int id)
        {
            var joueurBd = await _context.joueur.FindAsync(id);

            if (joueurBd == null)
            {
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

            return Ok(joueurDto);
        }

        // GET: api/Joueur/obtenirprenomnom/5
        [HttpGet("obtenirprenomnom/{id}")]
        public async Task<ActionResult<string>> GetPrenomNomJoueur(int id)
        {
            var joueurBd = await _context.joueur.FindAsync(id);

            if (joueurBd == null)
            {
                return NotFound();
            }

            var prenomNom = joueurBd.Prenom + " " + joueurBd.Nom;

            return Ok(prenomNom);
        }

        // PUT: api/Joueur/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJoueurBd(int id, JoueurBd joueurBd)
        {
            if (id != joueurBd.Id)
            {
                return BadRequest();
            }

            _context.Entry(joueurBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JoueurBdExists(id))
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

        // POST: api/Joueur
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JoueurDto>> PostJoueurDto(JoueurDto joueur)
        {
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
            await _context.SaveChangesAsync();

            joueur.Id = joueurBd.Id;

            return CreatedAtAction("PostJoueurDto", joueur);
        }

        // DELETE: api/Joueur/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJoueurBd(int id)
        {
            var joueurBd = await _context.joueur.FindAsync(id);
            if (joueurBd == null)
            {
                return NotFound();
            }

            _context.joueur.Remove(joueurBd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JoueurBdExists(int id)
        {
            return _context.joueur.Any(e => e.Id == id);
        }
    }
}