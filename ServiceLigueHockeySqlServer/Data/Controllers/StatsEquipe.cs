using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data;
using ServiceLigueHockeySqlServer.Data.Controllers;
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

        public StatsEquipe(ServiceLigueHockeyContext context)
        {
            _context = context;
        }

        // GET: api/StatsEquipe/parannee/{annee})
        [HttpGet("parannee/{annee}")]
        public ActionResult<IQueryable<StatsEquipeDto>> GetStatsEquipe(short annee)
        {
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
            return Ok(listeStatsEquipe);
        }

        [HttpGet("{equipeId}/{anneeStats}")]
        public ActionResult<StatsEquipeDto> GetStatsEquipe(int equipeId, short anneeStats)
        {
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
                return NotFound();
            }

            return Ok(retour);
        }

        [HttpPut("{equipeId}/{annee}")]
        public async Task<IActionResult> PutStatsEquipe(int equipeId, short annee, StatsEquipeDto statsEquipeDto)
        {
            if (equipeId != statsEquipeDto.equipeId && annee != statsEquipeDto.anneeStats)
            {
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
            catch (DbUpdateConcurrencyException)
            {
                if (!StatsEquipeBdExists(equipeId, annee))
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

        [HttpPost]
        public async Task<ActionResult<StatsEquipeDto>> PostStatsEquipe(StatsEquipeDto statsEquipeDto)
        {
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
            catch (DbUpdateException ex)
            {
                if (StatsEquipeBdExists(statsEquipeBd.EquipeId, statsEquipeBd.AnneeStats))
                {
                    return Conflict(ex);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("PostStatsEquipeBd", statsEquipeDto);
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
            return _context.statsEquipe.Any(e => e.EquipeId == id && e.AnneeStats == annee);
        }
    }
}
