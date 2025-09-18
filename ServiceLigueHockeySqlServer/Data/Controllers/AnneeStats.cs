using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnneeStats : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;
        private readonly ILogger<AnneeStats> _logger;

        public AnneeStats(ServiceLigueHockeyContext context,
                          ILogger<AnneeStats> logger)
        {
            _context = context;
            this._logger = logger;
        }

        // GET: api/AnneeStats
        [HttpGet]
        public ActionResult<IQueryable<AnneeStatsDto>> GetAnneeStatsDto()
        {
            this._logger.LogInformation("--- Début GetAnneeStatsDto ---");

            var listeAnneeStats = from monAnneeStats in _context.anneeStats
                                  select new AnneeStatsDto
                                  {
                                      AnneeStats = monAnneeStats.AnneeStats,
                                      DescnCourte = monAnneeStats.DescnCourte,
                                      DescnLongue = monAnneeStats.DescnLongue/*,
                                      listeCalendrier = (from patate in monAnneeStats.listeCalendrier select new CalendrierDto { IdPartie = patate.IdPartie }).ToList()*/
                                  };

            this._logger.LogInformation("--- Fin GetAnneeStatsDto ---");
            return Ok(listeAnneeStats);
        }

        // GET: api/AnneeStats/5
        [HttpGet("{pAnneeStats}")]
        public async Task<ActionResult<AnneeStatsDto>> GetAnneeStatsDto(short pAnneeStats)
        {
            this._logger.LogInformation("--- Début GetAnneeStatsDto ---");

            var anneeStats = await _context.anneeStats.FindAsync(pAnneeStats);

            if (anneeStats == null)
            {
                this._logger.LogError("Année nno-trouvée");
                return NotFound();
            }

            var anneeStatsDto = new AnneeStatsDto
            {
                AnneeStats = anneeStats.AnneeStats,
                DescnCourte = anneeStats.DescnCourte,
                DescnLongue = anneeStats.DescnLongue
            };

            this._logger.LogInformation("--- Fin GetAnneeStatsDto ---");
            return Ok(anneeStatsDto);
        }

        // PUT: api/AnneeStats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{pAnneeStats}")]
        public async Task<IActionResult> PutAnneeStats(short pAnneeStats, AnneeStatsDto anneeStatsDto)
        {
            this._logger.LogInformation("--- Début PutAnneeStats ---");

            if (pAnneeStats != anneeStatsDto.AnneeStats)
            {
                this._logger.LogError("Mauvaise requête");
                return BadRequest();
            }

            var anneeStatsBd = new AnneeStatsBd
            {
                AnneeStats = anneeStatsDto.AnneeStats,
                DescnCourte = anneeStatsDto.DescnCourte,
                DescnLongue = anneeStatsDto.DescnLongue
            };

            _context.Entry(anneeStatsBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!AnneestatsBdExists(pAnneeStats))
                {
                    this._logger.LogError("Année non-trouvée");
                    return NotFound();
                }
                else
                {
                    this._logger.LogError(string.Format("{0} - {1}",
                                                        dbEx.Message,
                                                        dbEx.InnerException == null ? string.Empty : dbEx.InnerException.Message));
                    throw;
                }
            }
            finally
            {
                this._logger.LogInformation("--- Fin PutAnneeStats ---");
            }

            return NoContent();
        }

        // POST: api/AnneeStats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnneeStatsDto>> PostAnneeStatsDto(AnneeStatsDto anneeStats)
        {
            this._logger.LogInformation("--- Début PostAnneeStatsDto ---");

            var anneeStatsBd = new AnneeStatsBd
            {
                AnneeStats = anneeStats.AnneeStats,
                DescnCourte = anneeStats.DescnCourte,
                DescnLongue = anneeStats.DescnLongue
            };

            _context.anneeStats.Add(anneeStatsBd);
            await _context.SaveChangesAsync();

            this._logger.LogInformation("--- Fin PostAnneeStatsDto ---");
            return CreatedAtAction("PostAnneeStatsDto", anneeStats);
        }

        // DELETE: api/AnneeStats/5
        [HttpDelete("{pAnneeStats}")]
        public async Task<IActionResult> DeleteAnnee(short pAnneeStats)
        {
            this._logger.LogInformation("--- Début DeleteAnnee ---");

            var anneeStats = await _context.anneeStats.FindAsync(pAnneeStats);
            if (anneeStats == null)
            {
                this._logger.LogError("Année non-trouvée");
                return NotFound();
            }

            _context.anneeStats.Remove(anneeStats);
            await _context.SaveChangesAsync();

            this._logger.LogInformation("--- Fin DeleteAnnee ---");
            return NoContent();
        }

        private bool AnneestatsBdExists(short anneeStats)
        {
            this._logger.LogInformation("Passage dans AnneestatsBdExists");
            return _context.anneeStats.Any(e => e.AnneeStats == anneeStats);
        }
    }
}
