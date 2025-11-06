using System;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    /*
     * Controleur pour Division
     */
    [ApiController]
    [Route("api/[controller]")]
    public class Division : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;
        private readonly ILogger<Division> _logger;

        public Division(ServiceLigueHockeyContext context, ILogger<Division> logger)
        {
            _context = context;
            this._logger = logger;
        }

        // GET: api/Division/prochainid
        [HttpGet("prochainid")]
        public ActionResult<int> GetProchainIdDivision()
        {
            this._logger.LogInformation("--- Début GetProchainIdDivision ---");
            var listeDivision = (from division in _context.division
                                 select division)
                                 .OrderByDescending(x => x.Id)
                                 .FirstOrDefault();
            
            int retour = 1;
            if(listeDivision != null)
            {
                retour = listeDivision.Id + 1;
            }

            this._logger.LogInformation("--- Fin GetProchainIdDivision ---");
            return Ok(retour);
        }

        // GET: api/Division
        [HttpGet]
        public ActionResult<IQueryable<DivisionDto>> GetDivisionDto()
        {
            this._logger.LogInformation("--- Début GetDivisionDto ---");

            var listeEquipe = from division in _context.division
                              select new DivisionDto
                              {
                                  Id = division.Id,
                                  NomDivision = division.NomDivision,
                                  ConferenceId = division.ConferenceId,
                                  AnneeDebut = division.AnneeDebut,
                                  AnneeFin = division.AnneeFin,
                                  Conference = new ConferenceDto
                                  {
                                      Id = division.Conference.Id,
                                      NomConference = division.Conference.NomConference,
                                      AnneeDebut = division.Conference.AnneeDebut,
                                      AnneeFin = division.Conference.AnneeFin,
                                      EstDevenueConference = division.Conference.EstDevenueConference
                                  }
                              };

            this._logger.LogInformation("--- Fin GetDivisionDto ---");
            return Ok(listeEquipe);
        }

        // GET: api/Division/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DivisionDto>> GetDivisionDto(int id)
        {
            this._logger.LogInformation("--- Début GetDivisionDto ---");
            var divisionBd = await _context.division.FindAsync(id);

            if (divisionBd == null)
            {
                this._logger.LogError("Division non-trouvée");
                return NotFound();
            }

            var divisionDto = new DivisionDto
            {
                Id = divisionBd.Id,
                NomDivision = divisionBd.NomDivision,
                ConferenceId = divisionBd.ConferenceId,
                AnneeDebut = divisionBd.AnneeDebut,
                AnneeFin = divisionBd.AnneeFin,
                Conference = new ConferenceDto
                {
                    Id = divisionBd.Conference.Id,
                    NomConference = divisionBd.Conference.NomConference,
                    EstDevenueConference = divisionBd.Conference.EstDevenueConference,
                    AnneeDebut = divisionBd.Conference.AnneeDebut,
                    AnneeFin = divisionBd.Conference.AnneeFin
                }
            };

            this._logger.LogInformation("--- Fin GetDivisionDto ---");
            return Ok(divisionDto);
        }

        // PUT: api/Division/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDivision(int id, DivisionDto division)
        {
            this._logger.LogInformation("--- Début PutDivision ---");
            if (id != division.Id)
            {
                this._logger.LogError("Mauvaise requête");
                return BadRequest();
            }

            var divisionBd = new DivisionBd
            {
                Id = division.Id,
                NomDivision = division.NomDivision,
                ConferenceId = division.ConferenceId,
                AnneeDebut = division.AnneeDebut,
                AnneeFin = division.AnneeFin
            };

            _context.Entry(divisionBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DivisionBdExists(id))
                {
                    this._logger.LogError("Non trouvé");
                    return NotFound();
                }
                else
                {
                    var mesDefautsSerialisation = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                    mesDefautsSerialisation.WriteIndented = true;
                    Response.StatusCode = 500;

                    if (ex.InnerException != null)
                    {
                        byte[] innerExMessage = Encoding.Default.GetBytes(ex.InnerException.Message);
                        string innerExMsgUtf8 = Encoding.UTF8.GetString(innerExMessage);
                        byte[] innerExStacktrace =
                            Encoding.Default.GetBytes(string.IsNullOrEmpty(ex.InnerException.StackTrace) ? string.Empty : ex.InnerException.StackTrace);
                        string innerExStacktraceUtf8 = Encoding.UTF8.GetString(innerExStacktrace);

                        this._logger.LogError(string.Format("Erreur : {0}", innerExMsgUtf8));
                        return new JsonResult(new { Message = innerExMsgUtf8 }, mesDefautsSerialisation);
                    }

                    this._logger.LogError(string.Format("Erreur : {0}", ex.Message));
                    return new JsonResult(new { Message = ex.Message }, mesDefautsSerialisation);

                }
            }
            catch (Exception ex)
            {
                var mesDefautsSerialisation = new JsonSerializerOptions(JsonSerializerDefaults.General);
                mesDefautsSerialisation.WriteIndented = true;
                Response.StatusCode = 500;

                if (ex.InnerException != null)
                {
                    this._logger.LogError(string.Format("Erreur : {0}", ex.InnerException.Message));
                    return new JsonResult(new { Message = ex.InnerException.Message },
                                          mesDefautsSerialisation);
                }

                this._logger.LogError(string.Format("Erreur : {0}", ex.Message));
                return new JsonResult(new { Message = ex.Message }, mesDefautsSerialisation);
            }

            this._logger.LogInformation("--- Fin PutDivision ---");
            return NoContent();
        }

        // POST: api/Division
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DivisionDto>> PostDivisionDto(DivisionDto division)
        {
            this._logger.LogInformation("--- Début PostDivisionDto ---");

            var divisionBd = new DivisionBd
            {
                Id = division.Id,
                NomDivision = division.NomDivision,
                ConferenceId = division.ConferenceId,
                AnneeDebut = division.AnneeDebut,
                AnneeFin = division.AnneeFin
            };

            try
            {
                _context.division.Add(divisionBd);
                await _context.SaveChangesAsync();

                division.Id = divisionBd.Id;
            }
            catch (Exception ex)
            {
                var mesDefautsSerialisation = new JsonSerializerOptions(JsonSerializerDefaults.General);
                mesDefautsSerialisation.WriteIndented = true;
                Response.StatusCode = 500;

                if (ex.InnerException != null)
                {
                    this._logger.LogError(string.Format("Erreur : {0}", ex.InnerException.Message));
                    return new JsonResult(new { Message = ex.InnerException.Message }, mesDefautsSerialisation);
                }
                
                this._logger.LogError(string.Format("Erreur : {0}", ex.Message));
                return new JsonResult(new { Message = ex.Message }, mesDefautsSerialisation);
            }

            this._logger.LogInformation("--- Fin PostDivisionDto ---");
            return CreatedAtAction("PostDivisionDto", division);
        }

        // DELETE: api/Division/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDivision(int id)
        {
            this._logger.LogInformation("--- Début DeleteDivision ---");

            var divisionBd = await _context.division.FindAsync(id);
            if (divisionBd == null)
            {
                return NotFound();
            }

            _context.division.Remove(divisionBd);
            await _context.SaveChangesAsync();
            this._logger.LogInformation("--- Fin DeleteDivision ---");

            return NoContent();
        }

        private bool DivisionBdExists(int id)
        {
            this._logger.LogInformation("On passe dans DivisionBdExists.");
            return _context.division.Any(e => e.Id == id);
        }
    }
}