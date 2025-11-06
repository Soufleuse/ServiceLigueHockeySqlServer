using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pointeur : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;
        private readonly ILogger<Pointeur> _logger;

        public Pointeur(ServiceLigueHockeyContext context, ILogger<Pointeur> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Pointeur
        [HttpGet]
        public ActionResult<IQueryable<PointeursDto>> GetPointeursDto()
        {
            this._logger.LogInformation("--- Début GetPointeursDto ---");
            var listePointeur = from monPointeur in _context.pointeurs
                                select new PointeursDto
                                {
                                    IdPartie = monPointeur.IdPartie,
                                    IdJoueurButMarque = monPointeur.IdJoueurButMarque,
                                    IdJoueurPremiereAssistance = monPointeur.IdJoueurPremiereAssistance,
                                    IdJoueurSecondeAssistance = monPointeur.IdJoueurSecondeAssistance,
                                    MomentDuButMarque = monPointeur.MomentDuButMarque
                                };

            this._logger.LogInformation("--- Fin GetPointeursDto ---");
            return Ok(listePointeur);
        }

        // GET: api/Pointeur/5
        [HttpGet("{idPartie}")]
        public async Task<ActionResult<PointeursDto>> GetPointeursDto(int idPartie)
        {
            this._logger.LogInformation("--- Début GetPointeursDto avec idPartie ---");

            var pointeursBd = await _context.pointeurs.FindAsync(idPartie);

            if (pointeursBd == null)
            {
                this._logger.LogError("Pointeur non-trouvé");
                return NotFound();
            }

            var pointeursDto = new PointeursDto
            {
                IdPartie = pointeursBd.IdPartie,
                IdJoueurButMarque = pointeursBd.IdJoueurButMarque,
                IdJoueurPremiereAssistance = pointeursBd.IdJoueurPremiereAssistance,
                IdJoueurSecondeAssistance = pointeursBd.IdJoueurSecondeAssistance,
                MomentDuButMarque = pointeursBd.MomentDuButMarque
            };

            this._logger.LogInformation("--- Fin GetPointeursDto avec idPartie ---");
            return Ok(pointeursDto);
        }

        // PUT: api/Pointeur/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idPartie}/{idMarqueur}/{momentButMarque}")]
        public async Task<IActionResult> PutPointeurs(int idPartie, int idMarqueur, DateTime momentButMarque, PointeursDto pointeurs)
        {
            if (idPartie != pointeurs.IdPartie)
            {
                return BadRequest();
            }

            var pointeurBd = new PointeursBd
            {
            };

            _context.Entry(pointeurBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointeursBdExists(idPartie, idMarqueur, momentButMarque))
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

        // POST: api/Pointeurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PointeursDto>> PostPointeursDto(PointeursDto pointeurs)
        {
            var pointeursBd = new PointeursBd
            {
            };

            _context.pointeurs.Add(pointeursBd);
            await _context.SaveChangesAsync();

            pointeurs.IdPartie = pointeursBd.IdPartie;

            return CreatedAtAction("PostPointeursDto", pointeurs);
        }

        // DELETE: api/Pointeurs/5
        [HttpDelete("{idPartie}")]
        public async Task<IActionResult> DeletePointeurs(int idPartie)
        {
            var pointeursBd = await _context.pointeurs.FindAsync(idPartie);
            if (pointeursBd == null)
            {
                return NotFound();
            }

            _context.pointeurs.Remove(pointeursBd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PointeursBdExists(int idPartie, int idJoueurQuiAMarque, DateTime momentButMarque)
        {
            return _context.pointeurs.Any(e => e.IdPartie == idPartie && e.IdJoueurButMarque == idJoueurQuiAMarque && e.MomentDuButMarque.Equals(momentButMarque));
        }
    }
}
