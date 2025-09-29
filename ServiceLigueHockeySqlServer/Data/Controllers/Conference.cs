using System;
using System.Collections.ObjectModel;
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

        public Conference(ServiceLigueHockeyContext context)
        {
            _context = context;
        }

        // GET: api/Conference/prochainid
        [HttpGet("prochainid")]
        public ActionResult<int> GetProchainIdConference()
        {
            var listeConference = (from conference in _context.conference
                                   select conference)
                                   .OrderByDescending(x => x.Id)
                                   .FirstOrDefault();
            
            int retour = 1;
            if(listeConference != null)
            {
                retour = listeConference.Id + 1;
            }

            return Ok(retour);
        }

        // GET: api/Conference
        [HttpGet]
        public ActionResult<IQueryable<ConferenceDto>> GetConferenceDto()
        {
            var listeConference = from conference in _context.conference
                              select new ConferenceDto
                              {
                                  Id = conference.Id,
                                  NomConference = conference.NomConference,
                                  AnneeDebut = conference.AnneeDebut,
                                  AnneeFin = conference.AnneeFin,
                                  EstDevenueConference = conference.EstDevenueConference
                              };
            return Ok(listeConference);
        }

        // GET: api/Conference/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConferenceDto>> GetConferenceDto(int id)
        {
            var conferenceBd = await _context.conference.FindAsync(id);

            if (conferenceBd == null)
            {
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

            return Ok(conferenceDto);
        }

        // PUT: api/Conference/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConference(int id, ConferenceDto conference)
        {
            if (id != conference.Id)
            {
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
                        return new JsonResult(new { Message = innerExMsgUtf8, StackTrace = innerExStacktraceUtf8 },
                                              mesDefautsSerialisation);
                    }
                    return new JsonResult(new { Message = ex.Message, StackTrace = ex.StackTrace },
                                          mesDefautsSerialisation);

                }
            }
            catch (Exception ex)
            {
                var mesDefautsSerialisation = new JsonSerializerOptions(JsonSerializerDefaults.General);
                mesDefautsSerialisation.WriteIndented = true;
                Response.StatusCode = 500;

                if (ex.InnerException != null)
                {
                    return new JsonResult(new { Message = ex.InnerException.Message, StackTrace = ex.InnerException.StackTrace },
                                          mesDefautsSerialisation);
                }
                return new JsonResult(new { Message = ex.Message, StackTrace = ex.StackTrace },
                                      mesDefautsSerialisation);
            }

            return NoContent();
        }

        // POST: api/Conference
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConferenceDto>> PostConferenceDto(ConferenceDto conference)
        {
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
                    return new JsonResult(new { Message = ex.InnerException.Message, StackTrace = ex.InnerException.StackTrace },
                                          mesDefautsSerialisation);
                }
                return new JsonResult(new { Message = ex.Message, StackTrace = ex.StackTrace },
                                      mesDefautsSerialisation);
            }

            return CreatedAtAction("PostConferenceDto", conference);
        }

        // DELETE: api/Conference/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConference(int id)
        {
            var conferenceBd = await _context.conference.FindAsync(id);
            if (conferenceBd == null)
            {
                return NotFound();
            }

            _context.conference.Remove(conferenceBd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConferenceBdExists(int id)
        {
            return _context.conference.Any(e => e.Id == id);
        }
    }
}