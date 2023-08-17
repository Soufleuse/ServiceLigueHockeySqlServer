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
        public ActionResult<IQueryable<StatsEquipeDto>> GetStatsEquipeDto(short annee)
        {
            var listeStatsEquipe = _context.statsEquipe
                                   .Where(statEquipe => statEquipe.AnneeStats == annee)
                                   .OrderByDescending(x => x.NbVictoires)
                                   .Select(item => new StatsEquipeDto
                                   {
                                       AnneeStats = item.AnneeStats,
                                       NbPartiesJouees = item.NbPartiesJouees,
                                       NbVictoires = item.NbVictoires,
                                       NbDefaites = item.NbDefaites,
                                       NbDefProlo = item.NbDefProlo,
                                       NbButsPour = item.NbButsPour,
                                       NbButsContre = item.NbButsContre,
                                       EquipeId = item.EquipeId
                                   });
            return Ok(listeStatsEquipe);
        }

        [HttpGet("{equipeId}/{anneeStats}")]
        public ActionResult<IQueryable<StatsEquipeDto>> GetStatsEquipe(int equipeId, short anneeStats)
        {
            var retour = _context.statsEquipe
                .Where(x => x.EquipeId == equipeId && x.AnneeStats == anneeStats)
                .OrderByDescending(x => x.NbVictoires)
                .Select(item => new StatsEquipeDto {
                    AnneeStats = anneeStats,
                    NbPartiesJouees = item.NbPartiesJouees,
                    NbVictoires = item.NbVictoires,
                    NbDefaites = item.NbDefaites,
                    NbDefProlo = item.NbDefProlo,
                    NbButsPour = item.NbButsPour,
                    NbButsContre = item.NbButsContre,
                    EquipeId = item.EquipeId
                });

            if (retour == null)
            {
                return NotFound();
            }

            return Ok(retour);
        }

        [HttpPut("{id}/{annee}")]
        public async Task<IActionResult> PutStatsEquipeBd(int id, short annee, StatsEquipeDto statsEquipeDto)
        {
            if (id != statsEquipeDto.EquipeId && annee != statsEquipeDto.AnneeStats)
            {
                return BadRequest();
            }

            var statsEquipeBd = new StatsEquipeBd {
                AnneeStats = statsEquipeDto.AnneeStats,
                NbPartiesJouees = statsEquipeDto.NbPartiesJouees,
                NbVictoires = statsEquipeDto.NbVictoires,
                NbDefaites = statsEquipeDto.NbDefaites,
                NbDefProlo = statsEquipeDto.NbDefProlo,
                NbButsPour = statsEquipeDto.NbButsPour,
                NbButsContre = statsEquipeDto.NbButsContre,
                EquipeId = statsEquipeDto.EquipeId
            };

            _context.Entry(statsEquipeBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatsEquipeBdExists(id, annee))
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
        public async Task<ActionResult<StatsEquipeDto>> PostStatsEquipeBd(StatsEquipeDto statsEquipeDto)
        {
            var statsEquipeBd = new StatsEquipeBd {
                AnneeStats = statsEquipeDto.AnneeStats,
                NbPartiesJouees = statsEquipeDto.NbPartiesJouees,
                NbVictoires = statsEquipeDto.NbVictoires,
                NbDefaites = statsEquipeDto.NbDefaites,
                NbDefProlo = statsEquipeDto.NbDefProlo,
                NbButsPour = statsEquipeDto.NbButsPour,
                NbButsContre = statsEquipeDto.NbButsContre,
                EquipeId = statsEquipeDto.EquipeId,
                Equipe = new EquipeBd {
                    Id = statsEquipeDto.EquipeId,
                    NomEquipe = statsEquipeDto.Equipe.NomEquipe,
                    Ville = statsEquipeDto.Equipe.Ville,
                    AnneeDebut = statsEquipeDto.Equipe.AnneeDebut,
                    AnneeFin = statsEquipeDto.Equipe.AnneeFin,
                    EstDevenueEquipe = statsEquipeDto.Equipe.EstDevenueEquipe,
                    listeEquipeJoueur = statsEquipeDto.Equipe.listeEquipeJoueur
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

            return CreatedAtAction("PostStatsEquipeBd", statsEquipeBd);
        }

        [HttpDelete("{id}/{annee}")]
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
        }

        private bool StatsEquipeBdExists(int id, short annee)
        {
            return _context.statsEquipe.Any(e => e.EquipeId == id && e.AnneeStats == annee);
        }
    }
}
