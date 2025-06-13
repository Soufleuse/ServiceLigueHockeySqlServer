using System;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnneeStats : ControllerBase
    {private readonly ServiceLigueHockeyContext _context;

        public AnneeStats(ServiceLigueHockeyContext context)
        {
            _context = context;
        }

        // GET: api/AnneeStats
        [HttpGet]
        public ActionResult<IQueryable<AnneeStatsDto>> GetAnneeStatsDto()
        {
            var listeAnneeStats = from monAnneeStats in _context.anneeStats
                              select new AnneeStatsDto
                              {
                                AnneeStats = monAnneeStats.AnneeStats,
                                DescnCourte = monAnneeStats.DescnCourte,
                                DescnLongue = monAnneeStats.DescnLongue/*,
                                listeCalendrier = (from patate in monAnneeStats.listeCalendrier select new CalendrierDto { IdPartie = patate.IdPartie }).ToList()*/
                              };
            return Ok(listeAnneeStats);
        }

        // GET: api/AnneeStats/5
        [HttpGet("{pAnneeStats}")]
        public async Task<ActionResult<AnneeStatsDto>> GetAnneeStatsDto(short pAnneeStats)
        {
            var anneeStats = await _context.anneeStats.FindAsync(pAnneeStats);

            if (anneeStats == null)
            {
                return NotFound();
            }

            var anneeStatsDto = new AnneeStatsDto
            {
                AnneeStats = anneeStats.AnneeStats,
                DescnCourte = anneeStats.DescnCourte,
                DescnLongue = anneeStats.DescnLongue
            };

            return Ok(anneeStatsDto);
        }

        // PUT: api/AnneeStats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{pAnneeStats}")]
        public async Task<IActionResult> PutAnneeStats(short pAnneeStats, AnneeStatsDto anneeStatsDto)
        {
            if (pAnneeStats != anneeStatsDto.AnneeStats)
            {
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
            catch (DbUpdateConcurrencyException)
            {
                if (!AnneestatsBdExists(pAnneeStats))
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

        // POST: api/AnneeStats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnneeStatsDto>> PostAnneeStatsDto(AnneeStatsDto anneeStats)
        {
            var anneeStatsBd = new AnneeStatsBd
            {
                AnneeStats = anneeStats.AnneeStats,
                DescnCourte = anneeStats.DescnCourte,
                DescnLongue = anneeStats.DescnLongue
            };

            _context.anneeStats.Add(anneeStatsBd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostAnneeStatsDto", anneeStats);
        }

        // DELETE: api/AnneeStats/5
        [HttpDelete("{pAnneeStats}")]
        public async Task<IActionResult> DeletePointeurs(short pAnneeStats)
        {
            var anneeStats = await _context.anneeStats.FindAsync(pAnneeStats);
            if (anneeStats == null)
            {
                return NotFound();
            }

            _context.anneeStats.Remove(anneeStats);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnneestatsBdExists(short anneeStats)
        {
            return _context.anneeStats.Any(e => e.AnneeStats == anneeStats);
        }
    }
}
