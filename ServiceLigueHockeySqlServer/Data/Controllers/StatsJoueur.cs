using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Serilog;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsJoueur : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;
        private readonly ILogger<StatsJoueur> _logger;

        public StatsJoueur(ServiceLigueHockeyContext context, ILogger<StatsJoueur> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/StatsJoueur/parannee/2020
        [HttpGet("parannee/{annee}")]
        public ActionResult<IEnumerable<StatsJoueurDto>> GetStatsJoueurBd(short annee)
        {
            this._logger.LogInformation("--- Début GetStatsJoueurBd ---");

            var listeStats = _context.statsJoueurBd
                .Where(x => x.AnneeStats == annee)
                .OrderByDescending(x => x.NbPoints)
                .Select(item => new StatsJoueurDto
                {
                    JoueurId = item.JoueurId,
                    EquipeId = item.EquipeId,
                    AnneeStats = item.AnneeStats,
                    NbPartiesJouees = item.NbPartiesJouees,
                    NbButs = item.NbButs,
                    NbPasses = item.NbPasses,
                    NbPoints = item.NbPoints,
                    NbMinutesPenalites = item.NbMinutesPenalites,
                    PlusseMoins = item.PlusseMoins,
                    MinutesJouees = item.MinutesJouees,
                    Victoires = item.Victoires,
                    Defaites = item.Defaites,
                    DefaitesEnProlongation = item.DefaitesEnProlongation,
                    Nulles = item.Nulles,
                    ButsAlloues = item.ButsAlloues,
                    TirsAlloues = item.TirsAlloues,
                    Joueur = new JoueurDto
                    {
                        Id = item.Joueur.Id,
                        Prenom = item.Joueur.Prenom,
                        Nom = item.Joueur.Nom,
                        VilleNaissance = item.Joueur.VilleNaissance,
                        PaysOrigine = item.Joueur.PaysOrigine,
                        DateNaissance = item.Joueur.DateNaissance
                    },
                    Equipe = new EquipeDto
                    {
                        Id = item.Equipe.Id,
                        NomEquipe = item.Equipe.NomEquipe,
                        Ville = item.Equipe.Ville,
                        AnneeDebut = item.Equipe.AnneeDebut,
                        AnneeFin = item.Equipe.AnneeFin,
                        EstDevenueEquipe = item.Equipe.EstDevenueEquipe
                    }
                });

            if (listeStats == null || !listeStats.Any())
            {
                this._logger.LogError("Liste de statistiques vide");
                return NotFound();
            }

            this._logger.LogInformation("--- Fin GetStatsJoueurBd ---");
            return Ok(listeStats.ToList());
        }

        // GET: api/StatsJoueur/5/2020
        [HttpGet("{id}/{anneeStats}")]
        public ActionResult<StatsJoueurDto> GetStatsJoueurBd(int id, short anneeStats)
        {
            this._logger.LogInformation("--- Début GetStatsJoueurBd avec id et anneeStats ---");

            var retour = _context.statsJoueurBd
                .Where(x => x.JoueurId == id && x.AnneeStats == anneeStats)
                .Select(statsJoueurBd => new StatsJoueurDto
                {
                    JoueurId = statsJoueurBd.JoueurId,
                    EquipeId = statsJoueurBd.EquipeId,
                    AnneeStats = statsJoueurBd.AnneeStats,
                    NbPartiesJouees = statsJoueurBd.NbPartiesJouees,
                    NbButs = statsJoueurBd.NbButs,
                    NbPasses = statsJoueurBd.NbPasses,
                    NbPoints = statsJoueurBd.NbPoints,
                    NbMinutesPenalites = statsJoueurBd.NbMinutesPenalites,
                    PlusseMoins = statsJoueurBd.PlusseMoins,
                    Victoires = statsJoueurBd.Victoires,
                    Defaites = statsJoueurBd.Defaites,
                    DefaitesEnProlongation = statsJoueurBd.DefaitesEnProlongation,
                    Nulles = statsJoueurBd.Nulles,
                    MinutesJouees = statsJoueurBd.MinutesJouees,
                    ButsAlloues = statsJoueurBd.ButsAlloues,
                    TirsAlloues = statsJoueurBd.TirsAlloues,
                    Joueur = new JoueurDto
                    {
                        Id = statsJoueurBd.Joueur.Id,
                        Prenom = statsJoueurBd.Joueur.Prenom,
                        Nom = statsJoueurBd.Joueur.Nom,
                        VilleNaissance = statsJoueurBd.Joueur.VilleNaissance,
                        PaysOrigine = statsJoueurBd.Joueur.PaysOrigine,
                        DateNaissance = statsJoueurBd.Joueur.DateNaissance
                    },
                    Equipe = new EquipeDto
                    {
                        Id = statsJoueurBd.Equipe.Id,
                        NomEquipe = statsJoueurBd.Equipe.NomEquipe,
                        Ville = statsJoueurBd.Equipe.Ville,
                        AnneeDebut = statsJoueurBd.Equipe.AnneeDebut,
                        AnneeFin = statsJoueurBd.Equipe.AnneeFin,
                        EstDevenueEquipe = statsJoueurBd.Equipe.EstDevenueEquipe
                    }
                }).FirstOrDefault();

            if (retour == null)
            {
                this._logger.LogError("Statistique de joueur non-trouvée");
                return NotFound();
            }

            this._logger.LogInformation("--- Fin GetStatsJoueurBd avec id et anneeStats ---");
            return Ok(retour);
        }

        // PUT: api/StatsJoueur/5/2023
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{annee}")]
        public async Task<IActionResult> PutStatsJoueurBd(int id, short annee, StatsJoueurDto statsJoueurDto)
        {
            this._logger.LogInformation("--- Début PutStatsJoueurBd ---");

            if (id != statsJoueurDto.JoueurId && annee != statsJoueurDto.AnneeStats)
            {
                this._logger.LogError("Mauvaise requête");
                return BadRequest();
            }

            var statsJoueurBd = new StatsJoueurBd
            {
                JoueurId = statsJoueurDto.JoueurId,
                EquipeId = statsJoueurDto.EquipeId,
                AnneeStats = statsJoueurDto.AnneeStats,
                NbPartiesJouees = statsJoueurDto.NbPartiesJouees,
                NbButs = statsJoueurDto.NbButs,
                NbPasses = statsJoueurDto.NbPasses,
                NbPoints = statsJoueurDto.NbPoints,
                NbMinutesPenalites = statsJoueurDto.NbMinutesPenalites,
                PlusseMoins = statsJoueurDto.PlusseMoins,
                Victoires = statsJoueurDto.Victoires,
                Defaites = statsJoueurDto.Defaites,
                DefaitesEnProlongation = statsJoueurDto.DefaitesEnProlongation,
                Nulles = statsJoueurDto.Nulles,
                MinutesJouees = statsJoueurDto.MinutesJouees,
                ButsAlloues = statsJoueurDto.ButsAlloues,
                TirsAlloues = statsJoueurDto.TirsAlloues,
                Joueur = new JoueurBd
                {
                    Id = statsJoueurDto.Joueur.Id,
                    Prenom = statsJoueurDto.Joueur.Prenom,
                    Nom = statsJoueurDto.Joueur.Nom,
                    VilleNaissance = statsJoueurDto.Joueur.VilleNaissance,
                    PaysOrigine = statsJoueurDto.Joueur.PaysOrigine,
                    DateNaissance = statsJoueurDto.Joueur.DateNaissance
                },
                Equipe = new EquipeBd
                {
                    Id = statsJoueurDto.Equipe.Id,
                    NomEquipe = statsJoueurDto.Equipe.NomEquipe,
                    Ville = statsJoueurDto.Equipe.Ville,
                    AnneeDebut = statsJoueurDto.Equipe.AnneeDebut,
                    AnneeFin = statsJoueurDto.Equipe.AnneeFin,
                    EstDevenueEquipe = statsJoueurDto.Equipe.EstDevenueEquipe
                }
            };

            _context.Entry(statsJoueurBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!StatsJoueurBdExists(id, annee))
                {
                    this._logger.LogError("Statistique non-trouvée");
                    return NotFound();
                }
                else
                {
                    this._logger.LogError(string.Format("Erreur dans PutStatsJoueurBd : {0}", dbEx.Message));
                    return StatusCode(500);
                }
            }
            finally
            {
                this._logger.LogInformation("--- Fin PutStatsJoueurBd ---");
            }

            return NoContent();
        }

        // POST: api/StatsJoueur
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatsJoueurDto>> PostStatsJoueurBd(StatsJoueurDto statsJoueurDto)
        {
            this._logger.LogInformation("--- Début PostStatsJoueurBd ---");

            var joueurBd = new JoueurBd
            {
                Id = statsJoueurDto.Joueur.Id,
                Prenom = statsJoueurDto.Joueur.Prenom,
                Nom = statsJoueurDto.Joueur.Nom,
                VilleNaissance = statsJoueurDto.Joueur.VilleNaissance,
                PaysOrigine = statsJoueurDto.Joueur.PaysOrigine,
                DateNaissance = statsJoueurDto.Joueur.DateNaissance,
                listeStatsJoueur = new List<StatsJoueurBd>()
            };

            var statsJoueurBd = new StatsJoueurBd
            {
                JoueurId = statsJoueurDto.JoueurId,
                EquipeId = statsJoueurDto.EquipeId,
                AnneeStats = statsJoueurDto.AnneeStats,
                NbPartiesJouees = statsJoueurDto.NbPartiesJouees,
                NbButs = statsJoueurDto.NbButs,
                NbPasses = statsJoueurDto.NbPasses,
                NbPoints = statsJoueurDto.NbPoints,
                NbMinutesPenalites = statsJoueurDto.NbMinutesPenalites,
                PlusseMoins = statsJoueurDto.PlusseMoins,
                Victoires = statsJoueurDto.Victoires,
                Defaites = statsJoueurDto.Defaites,
                DefaitesEnProlongation = statsJoueurDto.DefaitesEnProlongation,
                Nulles = statsJoueurDto.Nulles,
                MinutesJouees = statsJoueurDto.MinutesJouees,
                ButsAlloues = statsJoueurDto.ButsAlloues,
                TirsAlloues = statsJoueurDto.TirsAlloues,
                Joueur = new JoueurBd
                {
                    Id = statsJoueurDto.Joueur.Id,
                    Prenom = statsJoueurDto.Joueur.Prenom,
                    Nom = statsJoueurDto.Joueur.Nom,
                    VilleNaissance = statsJoueurDto.Joueur.VilleNaissance,
                    PaysOrigine = statsJoueurDto.Joueur.PaysOrigine,
                    DateNaissance = statsJoueurDto.Joueur.DateNaissance
                },
                Equipe = new EquipeBd
                {
                    Id = statsJoueurDto.Equipe.Id,
                    NomEquipe = statsJoueurDto.Equipe.NomEquipe,
                    Ville = statsJoueurDto.Equipe.Ville,
                    AnneeDebut = statsJoueurDto.Equipe.AnneeDebut,
                    AnneeFin = statsJoueurDto.Equipe.AnneeFin,
                    EstDevenueEquipe = statsJoueurDto.Equipe.EstDevenueEquipe
                }
            };

            _context.joueur.Attach(statsJoueurBd.Joueur);
            _context.equipe.Attach(statsJoueurBd.Equipe);
            _context.statsJoueurBd.Add(statsJoueurBd);
            joueurBd.listeStatsJoueur.Add(statsJoueurBd);
            //equipeBd.listeEquipeJoueur.Add(joueurBd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                if (StatsJoueurBdExists(statsJoueurBd.JoueurId, statsJoueurBd.AnneeStats))
                {
                    this._logger.LogError("Un autre item de type statistique joueur avec le même id existe; joueur id {0} - equipe id {1} - annee stats {2}",
                                          statsJoueurBd.JoueurId, statsJoueurBd.EquipeId, statsJoueurBd.AnneeStats);
                    return Conflict(dbEx);
                }
                else
                {
                    this._logger.LogError(string.Format("Erreur dans PostStatsJoueurBd : {0} ", dbEx.Message));
                    return StatusCode(500);
                }
            }
            finally
            {
                this._logger.LogInformation("--- Fin PostStatsJoueurBd ---");
            }

            return CreatedAtAction("PostStatsJoueurBd", statsJoueurDto);
        }

        // DELETE: api/StatsJoueur/5
        [HttpDelete("{id}/{annee}")]
        public async Task<IActionResult> DeleteStatsJoueurBd(int id, short annee)
        {
            this._logger.LogInformation("--- Début DeleteStatsJoueurBd ---");
            var statsJoueurBd = await _context.statsJoueurBd.FindAsync(id, annee);
            if (statsJoueurBd == null)
            {
                this._logger.LogError("Stats joueur non-trouvée");
                return NotFound();
            }

            _context.statsJoueurBd.Remove(statsJoueurBd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(string.Format("Erreur dans DeleteStatsJoueurBd : {0}", ex.Message));
                return StatusCode(500);
            }
            finally
            {
                this._logger.LogInformation("--- Fin DeleteStatsJoueurBd ---");
            }

            return NoContent();
        }

        private bool StatsJoueurBdExists(int id, short annee)
        {
            this._logger.LogInformation("Passage dans StatsJoueurBdExists");
            return _context.statsJoueurBd.Any(e => e.JoueurId == id && e.AnneeStats == annee);
        }
    }
}