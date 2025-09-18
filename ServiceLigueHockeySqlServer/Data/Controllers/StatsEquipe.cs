using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    /*
     * Controleur pour StatsEquipe
     */
    [ApiController]
    [Route("api/[controller]")]
    public class StatsEquipe : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;
        private readonly ILogger<StatsEquipe> _logger;

        public StatsEquipe(ServiceLigueHockeyContext context, ILogger<StatsEquipe> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/StatsEquipe/parannee/{annee})
        [HttpGet("parannee/{annee}")]
        public ActionResult<IQueryable<StatsEquipeDto>> GetStatsEquipe(short annee)
        {
            this._logger.LogInformation("--- Début GetStatsEquipe ---");

            var listeStatsEquipe = _context.statsEquipe
                                   .Where(statEquipe => statEquipe.AnneeStats == annee)
                                   .OrderByDescending(x => x.NbVictoires)
                                   .Select(item => new StatsEquipeDto
                                   {
                                       anneeStats = item.AnneeStats,
                                       nbPartiesJouees = item.NbPartiesJouees,
                                       nbVictoires = item.NbVictoires,
                                       nbDefaites = item.NbDefaites,
                                       nbDefProlo = item.NbDefProlo,
                                       nbButsPour = item.NbButsPour,
                                       nbButsContre = item.NbButsContre,
                                       equipeId = item.EquipeId,
                                       equipe = new EquipeDto {
                                            Id = item.Equipe.Id,
                                            NomEquipe = item.Equipe.NomEquipe,
                                            Ville = item.Equipe.Ville,
                                            AnneeDebut = item.Equipe.AnneeDebut,
                                            AnneeFin = item.Equipe.AnneeFin,
                                            EstDevenueEquipe = item.Equipe.AnneeFin
                                       }
                                   });

            this._logger.LogInformation("--- Fin GetStatsEquipe ---");
            return Ok(listeStatsEquipe);
        }

        [HttpGet("{equipeId}/{anneeStats}")]
        public ActionResult<StatsEquipeDto> GetStatsEquipe(int equipeId, short anneeStats)
        {
            this._logger.LogInformation("--- Début GetStatsEquipe ---");

            var retour = _context.statsEquipe
                .Where(x => x.EquipeId == equipeId && x.AnneeStats == anneeStats)
                .OrderByDescending(x => x.NbVictoires)
                .Select(item => new StatsEquipeDto {
                    anneeStats = anneeStats,
                    nbPartiesJouees = item.NbPartiesJouees,
                    nbVictoires = item.NbVictoires,
                    nbDefaites = item.NbDefaites,
                    nbDefProlo = item.NbDefProlo,
                    nbButsPour = item.NbButsPour,
                    nbButsContre = item.NbButsContre,
                    equipeId = item.EquipeId,
                    equipe = new EquipeDto {
                        Id = item.Equipe.Id,
                        NomEquipe = item.Equipe.NomEquipe,
                        Ville = item.Equipe.Ville,
                        AnneeDebut = item.Equipe.AnneeDebut,
                        AnneeFin = item.Equipe.AnneeFin,
                        EstDevenueEquipe = item.Equipe.EstDevenueEquipe
                    }
                })
                .FirstOrDefault();

            if (retour == null)
            {
                this._logger.LogError("Stat équipe non-trouvée");
                return NotFound();
            }

            this._logger.LogInformation("--- Fin GetStatsEquipe ---");
            return Ok(retour);
        }

        [HttpPut("{equipeId}/{annee}")]
        public async Task<IActionResult> PutStatsEquipe(int equipeId, short annee, StatsEquipeDto statsEquipeDto)
        {
            this._logger.LogInformation("--- Début PutStatsEquipe ---");

            if (equipeId != statsEquipeDto.equipeId && annee != statsEquipeDto.anneeStats)
            {
                this._logger.LogError("Mauvaise requête");
                return BadRequest();
            }

            var statsEquipeBd = new StatsEquipeBd {
                AnneeStats = statsEquipeDto.anneeStats,
                NbPartiesJouees = statsEquipeDto.nbPartiesJouees,
                NbVictoires = statsEquipeDto.nbVictoires,
                NbDefaites = statsEquipeDto.nbDefaites,
                NbDefProlo = statsEquipeDto.nbDefProlo,
                NbButsPour = statsEquipeDto.nbButsPour,
                NbButsContre = statsEquipeDto.nbButsContre,
                EquipeId = statsEquipeDto.equipeId
            };

            _context.Entry(statsEquipeBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                this._logger.LogError(string.Format("Erreur dans PutStatsEquipe : {0}", dbEx.Message));
                if (!StatsEquipeBdExists(equipeId, annee))
                {
                    this._logger.LogError("Stats équipe non-trouvée");
                    return NotFound();
                }
                else
                {
                    return StatusCode(500);
                }
            }
            finally
            {
                this._logger.LogInformation("--- Fin PutStatsEquipe ---");
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<StatsEquipeDto>> PostStatsEquipe(StatsEquipeDto statsEquipeDto)
        {
            this._logger.LogInformation("--- Début PostStatsEquipe ---");

            var statsEquipeBd = new StatsEquipeBd {
                AnneeStats = statsEquipeDto.anneeStats,
                NbPartiesJouees = statsEquipeDto.nbPartiesJouees,
                NbVictoires = statsEquipeDto.nbVictoires,
                NbDefaites = statsEquipeDto.nbDefaites,
                NbDefProlo = statsEquipeDto.nbDefProlo,
                NbButsPour = statsEquipeDto.nbButsPour,
                NbButsContre = statsEquipeDto.nbButsContre,
                EquipeId = statsEquipeDto.equipeId,
                Equipe = new EquipeBd {
                    Id = statsEquipeDto.equipeId,
                    NomEquipe = statsEquipeDto.equipe.NomEquipe,
                    Ville = statsEquipeDto.equipe.Ville,
                    AnneeDebut = statsEquipeDto.equipe.AnneeDebut,
                    AnneeFin = statsEquipeDto.equipe.AnneeFin,
                    EstDevenueEquipe = statsEquipeDto.equipe.EstDevenueEquipe/*,
                    listeEquipeJoueur = statsEquipeDto.Equipe.listeEquipeJoueur*/
                }
            };

            _context.equipe.Attach(statsEquipeBd.Equipe);
            _context.statsEquipe.Add(statsEquipeBd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                if (StatsEquipeBdExists(statsEquipeBd.EquipeId, statsEquipeBd.AnneeStats))
                {
                    this._logger.LogError(string.Format("Un autre item de type statistique joueur avec le même id existe; equipe id {0} - annee stats {1}",
                                                        statsEquipeBd.EquipeId, statsEquipeBd.AnneeStats));
                    return Conflict(dbEx);
                }
                else
                {
                    this._logger.LogError(string.Format("Erreur dans PostStatsEquipe : {0}", dbEx.Message));
                    return StatusCode(500);
                }
            }
            finally
            {
                this._logger.LogInformation("--- Fin PostStatsEquipe ---");
            }

            return CreatedAtAction("PostStatsEquipe", statsEquipeDto);
        }

        /*[HttpDelete("{id}/{annee}")]
        public async Task<IActionResult> DeleteStatsEquipeBd(int id, short annee)
        {
            var statsEquipeBd = await _context.statsEquipe.FindAsync(id, annee);
            if (statsEquipeBd == null)
            {
                return NotFound();
            }

            _context.statsEquipe.Remove(statsEquipeBd);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool StatsEquipeBdExists(int id, short annee)
        {
            this._logger.LogInformation("Passage dans StatsEquipeBdExists");
            return _context.statsEquipe.Any(e => e.EquipeId == id && e.AnneeStats == annee);
        }
    }
}
