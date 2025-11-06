using System;
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
        private readonly ILogger<Parametres> _logger;

        public Parametres(ServiceLigueHockeyContext context, ILogger<Parametres> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Parametres
        [HttpGet]
        public ActionResult<IQueryable<ParametresDto>> GetParametresBd()
        {
            this._logger.LogInformation("--- Début GetParametresBd ---");
            var listeParametres = from parametre in _context.parametres
                                  select new ParametresDto
                                  {
                                      nom = parametre.nom,
                                      valeur = parametre.valeur,
                                      dateDebut = parametre.dateDebut,
                                      dateFin = parametre.dateFin
                                  };

            this._logger.LogInformation("--- Fin GetParametresBd ---");
            return Ok(listeParametres);
        }

        // GET: api/Parametres/5
        [HttpGet("{nom}")]
        public ActionResult<IQueryable<JoueurDto>> GetParametresBd(string nom)
        {
            this._logger.LogInformation("--- Début GetParametresBd avec nom ---");
            var listeParametres = from parametre in _context.parametres
                                  where string.Compare(nom, parametre.nom) == 0
                                  select new ParametresDto
                                  {
                                      nom = parametre.nom,
                                      valeur = parametre.valeur,
                                      dateDebut = parametre.dateDebut,
                                      dateFin = parametre.dateFin
                                  };
                                  
            this._logger.LogInformation("--- Fin GetParametresBd avec nom ---");
            return Ok(listeParametres);
        }

        // GET: api/Parametres/5/2000-01-01
        [HttpGet("{nom}/{datevalidite}")]
        public async Task<ActionResult<List<ParametresDto>>> GetParametresBd(string nom, DateTime datevalidite)
        {
            this._logger.LogInformation("--- Début GetParametresBd avec nom et datevalidite ---");

            var parametresBd = await _context.parametres
                .Where(x => string.Compare(x.nom, nom) == 0 &&
                       x.dateDebut <= datevalidite && (!x.dateFin.HasValue || x.dateFin.HasValue && x.dateFin >= datevalidite))
                .ToListAsync();

            if (parametresBd == null)
            {
                this._logger.LogInformation("Paramètre non-trouvé");
                return NotFound();
            }

            List<ParametresDto> retour = new List<ParametresDto>();
            parametresBd.ForEach(x => {
                var ajout = new ParametresDto
                {
                    nom = x.nom,
                    valeur = x.valeur,
                    dateDebut = x.dateDebut,
                    dateFin = x.dateFin
                };
                retour.Add(ajout);
            });

            this._logger.LogInformation("--- Fin GetParametresBd avec nom et datevalidite ---");
            return Ok(retour);
        }

        // PUT: api/Parametres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{nom}/{datedebut}")]
        public async Task<IActionResult> PutJoueurBd(string nom, DateTime datedebut, ParametresBd parametreBd)
        {
            this._logger.LogInformation("--- Début PutJoueurBd ---");

            if (string.Compare(nom, parametreBd.nom) != 0 || datedebut != parametreBd.dateDebut)
            {
                this._logger.LogError("Mauvaise requête");
                return BadRequest();
            }

            _context.Entry(parametreBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!ParametreBdExists(parametreBd.nom, parametreBd.dateDebut, parametreBd.dateFin))
                {
                    this._logger.LogInformation("Joueur non-trouvé");
                    return NotFound();
                }
                else
                {
                    this._logger.LogError(string.Format("Erreur dans PutJoueurBd : {0}", dbEx.Message));
                    return StatusCode(500);
                }
            }
            finally
            {
                this._logger.LogInformation("--- Fin PutJoueurBd ---");
            }

            return NoContent();
        }

        // POST: api/Parametres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParametresDto>> PostParametreDto(ParametresDto parametre)
        {
            this._logger.LogInformation("--- Début PostParametreDto ---");

            var parametresBd = new ParametresBd
            {
                nom = parametre.nom,
                valeur = parametre.valeur,
                dateDebut = parametre.dateDebut,
                dateFin = parametre.dateFin
            };

            _context.parametres.Add(parametresBd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(string.Format("Erreur dans PostParametreDto : {0}", ex.Message));
                return StatusCode(500);
            }
            finally
            {
                this._logger.LogInformation("--- Fin PostParametreDto ---");
            }

            return CreatedAtAction("PostParametreDto", parametre);
        }

        private bool ParametreBdExists(string nom, DateTime datedebut, DateTime? datefin)
        {
            this._logger.LogInformation("--- Passage dans ParametreBdExists ---");
            return _context.parametres.Any(e => e.nom == nom && e.dateDebut.Equals(datedebut) && datefin == e.dateFin);
        }
    }
}