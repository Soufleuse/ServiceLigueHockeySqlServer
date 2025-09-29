using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypePenalites : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;
        private readonly ILogger<TypePenalites> _logger;

        public TypePenalites(ServiceLigueHockeyContext context, ILogger<TypePenalites> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/TypePenalites
        [HttpGet]
        public ActionResult<IQueryable<TypePenalitesDto>> GetTypePenalitesDto()
        {
            this._logger.LogInformation("--- Début GetTypePenalitesDto ---");
            var listeTypePenalites = from monTypePenalites in _context.typePenalites
                                     select new TypePenalitesDto
                                     {
                                         IdTypePenalite = monTypePenalites.IdTypePenalite,
                                         NbreMinutesPenalitesPourCetteInfraction = monTypePenalites.NbreMinutesPenalitesPourCetteInfraction,
                                         DescriptionPenalite = monTypePenalites.DescriptionPenalite
                                     };

            this._logger.LogInformation("--- Fin GetTypePenalitesDto ---");
            return Ok(listeTypePenalites);
        }

        // GET: api/TypePenalites/5
        [HttpGet("{IdTypePenalite}")]
        public async Task<ActionResult<TypePenalitesDto>> GetTypePenalitesDto(short IdTypePenalite)
        {
            this._logger.LogInformation("--- Début GetTypePenalitesDto avec IdTypePenalite ---");
            var typePenalitesBd = await _context.typePenalites.FindAsync(IdTypePenalite);

            if (typePenalitesBd == null)
            {
                this._logger.LogError("Type de pénalité non-trouvée");
                return NotFound();
            }

            var typePenalitesDto = new TypePenalitesDto
            {
                IdTypePenalite = typePenalitesBd.IdTypePenalite,
                NbreMinutesPenalitesPourCetteInfraction = typePenalitesBd.NbreMinutesPenalitesPourCetteInfraction,
                DescriptionPenalite = typePenalitesBd.DescriptionPenalite
            };

            this._logger.LogInformation("--- Fin GetTypePenalitesDto avec IdTypePenalite ---");
            return Ok(typePenalitesDto);
        }

        // POST: api/TypePenalites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypePenalitesDto>> PostTypePenalitesBd(TypePenalitesBd typePenaliteBd)
        {
            this._logger.LogInformation("--- Début PostTypePenalitesBd ---");
            /*var typePenaliteBd = new TypePenalitesBd
            {
                IdTypePenalite = pTypePenalitesDto.IdTypePenalite,
                NbreMinutesPenalitesPourCetteInfraction = pTypePenalitesDto.NbreMinutesPenalitesPourCetteInfraction,
                DescriptionPenalite = pTypePenalitesDto.DescriptionPenalite
            };*/

            _context.typePenalites.Add(typePenaliteBd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                if (TypePenalitesBdExists(typePenaliteBd.IdTypePenalite))
                {
                    this._logger.LogError("Un autre item de type pénalité avec le même id existe; pénalité id {0}",
                                          typePenaliteBd.IdTypePenalite);
                    return Conflict(dbEx);
                }
                else
                {
                    this._logger.LogError(string.Format("Erreur dans PostTypePenalitesBd : {0}", dbEx.Message));
                    return StatusCode(500);
                }
            }
            finally
            {
                this._logger.LogInformation("--- Fin PostTypePenalitesBd ---");
            }

            return CreatedAtAction("PostTypePenalitesBd", typePenaliteBd);
        }

        // PUT: api/TypePenalites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{IdTypePenalite}")]
        public async Task<IActionResult> PutAnneeStats(short IdTypePenalite, TypePenalitesDto pTypePenalitesDto)
        {
            this._logger.LogInformation("--- Début PutAnneeStats ---");
            if (IdTypePenalite != pTypePenalitesDto.IdTypePenalite)
            {
                this._logger.LogError("Mauvaise requête");
                return BadRequest();
            }

            var typePenalitesBd = new TypePenalitesBd
            {
                IdTypePenalite = pTypePenalitesDto.IdTypePenalite,
                NbreMinutesPenalitesPourCetteInfraction = pTypePenalitesDto.NbreMinutesPenalitesPourCetteInfraction,
                DescriptionPenalite = pTypePenalitesDto.DescriptionPenalite
            };

            _context.Entry(typePenalitesBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!TypePenalitesBdExists(IdTypePenalite))
                {
                    this._logger.LogError("Type pénalité non-trouvé");
                    return NotFound();
                }
                else
                {
                    this._logger.LogError(string.Format("Erreur dans PutAnneeStats : {0}", dbEx.Message));
                    return StatusCode(500);
                }
            }
            finally
            {
                this._logger.LogInformation("--- Fin PutAnneeStats ---");
            }

            return NoContent();
        }

        // DELETE: api/TypePenalites/5
        [HttpDelete("{IdTypePenalite}")]
        public async Task<IActionResult> DeleteTypePenalitesBd(short IdTypePenalite)
        {
            this._logger.LogInformation("--- Début DeleteTypePenalitesBd ---");
            var typePenalitesBd = await _context.typePenalites.FindAsync(IdTypePenalite);
            if (typePenalitesBd == null)
            {
                this._logger.LogError("TypePenalites non-trouvée");
                return NotFound();
            }

            _context.typePenalites.Remove(typePenalitesBd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(string.Format("Erreur dans DeleteTypePenalitesBd {0}", ex.Message));
                return StatusCode(500);
            }
            finally
            {
                this._logger.LogInformation("--- Fin DeleteTypePenalitesBd ---");
            }

            return NoContent();
        }

        private bool TypePenalitesBdExists(short id)
        {
            this._logger.LogInformation("Passage dans TypePenalitesBdExists");
            return _context.typePenalites.Any(e => e.IdTypePenalite == id);
        }
    }
}