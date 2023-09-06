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
     * Controleur pour Parametres
     */
    [ApiController]
    [Route("api/[controller]")]
    public class Parametres : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;

        public Parametres(ServiceLigueHockeyContext context)
        {
            _context = context;
        }

        // GET: api/ParametresBds
        [HttpGet]
        public ActionResult<IQueryable<ParametresDto>> GetParametresBd()
        {
            var listeParametres = from parametre in _context.parametres
                                  select new ParametresDto
                                  {
                                      nom = parametre.nom,
                                      valeur = parametre.valeur,
                                      dateDebut = parametre.dateDebut,
                                      dateFin = parametre.dateFin
                                  };

            return Ok(listeParametres);
        }

        // GET: api/ParametresBds/5
        [HttpGet("{nom}")]
        public ActionResult<IQueryable<JoueurDto>> GetParametresBd(string nom)
        {
            var listeParametres = from parametre in _context.parametres
                                  where string.Compare(nom, parametre.nom) == 0
                                  select new ParametresDto
                                  {
                                      nom = parametre.nom,
                                      valeur = parametre.valeur,
                                      dateDebut = parametre.dateDebut,
                                      dateFin = parametre.dateFin
                                  };
                                  
            return Ok(listeParametres);
        }

        // GET: api/ParametresBds/5/2000-01-01/2009-01-01
        /*[HttpGet("{nom}/{datevalidite}")]
        public async Task<ActionResult<ParametresDto>> GetParametresBd(string nom, DateTime datevalidite)
        {
            var parametresBd = await _context.parametres.FindAsync(nom, datevalidite);

            if (parametresBd == null)
            {
                return NotFound();
            }

            var joueurDto = new ParametresDto
            {
                nom = parametresBd.nom,
                valeur = parametresBd.valeur,
                dateDebut = parametresBd.dateDebut,
                dateFin = parametresBd.dateFin
            };

            return Ok(joueurDto);
        }*/

        // PUT: api/ParametresBds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{nom}/{datedebut}/{datefin}")]
        public async Task<IActionResult> PutJoueurBd(string nom, DateTime datedebut, DateTime datefin, ParametresBd parametreBd)
        {
            if (string.Compare(nom, parametreBd.nom) !=0 || datedebut != parametreBd.dateDebut || datefin != parametreBd.dateFin)
            {
                return BadRequest();
            }

            _context.Entry(parametreBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametreBdExists(parametreBd.nom, parametreBd.dateDebut, parametreBd.dateFin))
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

        // POST: api/ParametresBds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParametresDto>> PostParametreDto(ParametresDto parametre)
        {
            var parametresBd = new ParametresBd
            {
                nom = parametre.nom,
                valeur = parametre.valeur,
                dateDebut = parametre.dateDebut,
                dateFin = parametre.dateFin
            };

            _context.parametres.Add(parametresBd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostParametreDto", parametre);
        }

        private bool ParametreBdExists(string nom, DateTime datedebut, DateTime? datefin)
        {
            return _context.parametres.Any(e => e.nom == nom && e.dateDebut.Equals(datedebut) && datefin == e.dateFin);
        }
    }
}