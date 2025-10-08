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
     * Controleur pour Conference
     */
    [ApiController]
    [Route("api/[controller]")]
    public class Conference : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;
        private readonly ILogger<Conference> _logger;

        public Conference(ServiceLigueHockeyContext context, ILogger<Conference> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Conference/prochainid
        [HttpGet("prochainid")]
        public ActionResult<int> GetProchainIdConference()
        {
            this._logger.LogInformation("--- Début GetProchainIdConference ---");
            var listeConference = (from conference in _context.conference
                                   select conference)
                                   .OrderByDescending(x => x.Id)
                                   .FirstOrDefault();
            
            int retour = 1;
            if(listeConference != null)
            {
                retour = listeConference.Id + 1;
            }

            this._logger.LogInformation("--- Fin GetProchainIdConference ---");
            return Ok(retour);
        }

        // GET: api/Conference
        [HttpGet]
        public ActionResult<IQueryable<ConferenceDto>> GetConferenceDto()
        {
            this._logger.LogInformation("--- Début GetConferenceDto ---");

            var listeConference = from conference in _context.conference
                                  select new ConferenceDto
                                  {
                                      Id = conference.Id,
                                      NomConference = conference.NomConference,
                                      AnneeDebut = conference.AnneeDebut,
                                      AnneeFin = conference.AnneeFin,
                                      EstDevenueConference = conference.EstDevenueConference
                                  };

            this._logger.LogInformation("--- Fin GetConferenceDto ---");
            return Ok(listeConference);
        }

        // GET: api/Conference/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConferenceDto>> GetConferenceDto(int id)
        {
            this._logger.LogInformation("--- Début GetConferenceDto ---");
            var conferenceBd = await _context.conference.FindAsync(id);

            if (conferenceBd == null)
            {
                this._logger.LogInformation(string.Format("Conférence no {0} non trouvée", id));
                return NotFound();
            }

            var conferenceDto = new ConferenceDto
            {
                Id = conferenceBd.Id,
                NomConference = conferenceBd.NomConference,
                AnneeDebut = conferenceBd.AnneeDebut,
                AnneeFin = conferenceBd.AnneeFin,
                EstDevenueConference = conferenceBd.EstDevenueConference
            };

            this._logger.LogInformation("--- Fin GetConferenceDto ---");
            return Ok(conferenceDto);
        }

        // PUT: api/Conference/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConference(int id, ConferenceDto conference)
        {
            this._logger.LogInformation("--- Début PutConference ---");
            if (id != conference.Id)
            {
                this._logger.LogInformation("Mauvaise requête");
                return BadRequest();
            }

            var conferenceBd = new ConferenceBd
            {
                Id = conference.Id,
                NomConference = conference.NomConference,
                AnneeDebut = conference.AnneeDebut,
                AnneeFin = conference.AnneeFin,
                EstDevenueConference = conference.EstDevenueConference
            };

            _context.Entry(conferenceBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ConferenceBdExists(id))
                {
                    this._logger.LogInformation(string.Format("Conférence no {0} non trouvée", id));
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
                        this._logger.LogError(string.Format("Erreur sur la modification de la conférence, message : {0}", innerExMsgUtf8));
                        return new JsonResult(new { Message = "Une erreur est survenue, veuillez contacter le soutien technique." },
                                              mesDefautsSerialisation);
                    }
                    
                    this._logger.LogError(string.Format("Erreur sur la modification de la conférence, message : {0}", ex.Message));
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
                    this._logger.LogError(string.Format("Erreur sur la modification de la conférence, message : {0}", ex.InnerException.Message));
                    return new JsonResult(new { Message = ex.InnerException.Message }, mesDefautsSerialisation);
                }

                this._logger.LogError(string.Format("Erreur sur la modification de la conférence, message : {0}", ex.Message));
                return new JsonResult(new { Message = ex.Message }, mesDefautsSerialisation);
            }

            this._logger.LogInformation("--- Fin PutConference ---");
            return NoContent();
        }

        // POST: api/Conference
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConferenceDto>> PostConferenceDto(ConferenceDto conference)
        {
            this._logger.LogInformation("--- Début PostConferenceDto ---");

            var conferenceBd = new ConferenceBd
            {
                Id = conference.Id,
                NomConference = conference.NomConference,
                AnneeDebut = conference.AnneeDebut,
                AnneeFin = conference.AnneeFin
            };

            try
            {
                _context.conference.Add(conferenceBd);
                await _context.SaveChangesAsync();

                conference.Id = conferenceBd.Id;
            }
            catch (Exception ex)
            {
                var mesDefautsSerialisation = new JsonSerializerOptions(JsonSerializerDefaults.General);
                mesDefautsSerialisation.WriteIndented = true;
                Response.StatusCode = 500;

                if (ex.InnerException != null)
                {
                    this._logger.LogError(string.Format("Erreur à la création d'une conférence, erreur : {0}", ex.InnerException.Message));
                    return new JsonResult(new { Message = ex.InnerException.Message }, mesDefautsSerialisation);
                }

                this._logger.LogError(string.Format("Erreur à la création d'une conférence, erreur : {0}", ex.Message));
                return new JsonResult(new { Message = ex.Message }, mesDefautsSerialisation);
            }

            this._logger.LogInformation("--- Fin PostConferenceDto ---");
            return CreatedAtAction("PostConferenceDto", conference);
        }

        // DELETE: api/Conference/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConference(int id)
        {
            this._logger.LogInformation("--- Début DeleteConference ---");
            var conferenceBd = await _context.conference.FindAsync(id);
            if (conferenceBd == null)
            {
                this._logger.LogError("Conférence non trouvée");
                return NotFound();
            }

            _context.conference.Remove(conferenceBd);
            await _context.SaveChangesAsync();

            this._logger.LogInformation("--- Fin DeleteConference ---");
            return NoContent();
        }

        private bool ConferenceBdExists(int id)
        {
            this._logger.LogInformation("On passe dans ConferenceBdExists");
            return _context.conference.Any(e => e.Id == id);
        }
    }
}