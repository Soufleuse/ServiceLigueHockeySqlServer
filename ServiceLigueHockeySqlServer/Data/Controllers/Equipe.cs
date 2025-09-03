using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    /*
     * Controleur pour Equipe
     */
    [ApiController]
    [Route("api/[controller]")]
    public class Equipe : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;
        private readonly ILogger _logger;

        public Equipe(ServiceLigueHockeyContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Equipe/prochainid
        [HttpGet("prochainid")]
        public ActionResult<int> GetProchainIdEquipe()
        {
            var listeEquipe = (from equipe in _context.equipe
                               select equipe)
                               .OrderByDescending(x => x.Id)
                               .FirstOrDefault();
            
            int retour = 1;
            if(listeEquipe != null)
            {
                retour = listeEquipe.Id + 1;
            }

            return Ok(retour);
        }

        // GET: api/Equipe
        [HttpGet]
        public ActionResult<IQueryable<EquipeDto>> GetEquipeDto()
        {
            _logger.LogError("--- Début GetEquipeDto ---");

            var listeEquipe = from equipe in _context.equipe
                              select new EquipeDto
                              {
                                  Id = equipe.Id,
                                  NomEquipe = equipe.NomEquipe,
                                  Ville = equipe.Ville,
                                  AnneeDebut = equipe.AnneeDebut,
                                  AnneeFin = equipe.AnneeFin,
                                  EstDevenueEquipe = equipe.EstDevenueEquipe
                              };

            _logger.LogError("--- Fin GetEquipeDto ---");
            return Ok(listeEquipe);
        }

        // GET: api/Equipe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipeDto>> GetEquipeDto(int id)
        {
            var equipeBd = await _context.equipe.FindAsync(id);

            if (equipeBd == null)
            {
                return NotFound();
            }

            var equipeDto = new EquipeDto
            {
                Id = equipeBd.Id,
                NomEquipe = equipeBd.NomEquipe,
                Ville = equipeBd.Ville,
                AnneeDebut = equipeBd.AnneeDebut,
                AnneeFin = equipeBd.AnneeFin,
                EstDevenueEquipe = equipeBd.EstDevenueEquipe
            };

            return Ok(equipeDto);
        }

        // GET: api/Equipe/nomequipeville/5
        [HttpGet("nomequipeville/{id}")]
        public async Task<ActionResult<string>> GetNomEquipe(int id)
        {
            var equipeBd = await _context.equipe.FindAsync(id);

            if (equipeBd == null)
            {
                return NotFound();
            }

            var nomEquipeVille = string.Concat(equipeBd.NomEquipe, " ", equipeBd.Ville);

            return Ok(nomEquipeVille);
        }

        // PUT: api/Equipe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipe(int id, EquipeDto equipeDto)
        {
            if (id != equipeDto.Id)
            {
                return BadRequest();
            }

            var equipeBd = new EquipeBd
            {
                Id = equipeDto.Id,
                NomEquipe = equipeDto.NomEquipe,
                Ville = equipeDto.Ville,
                AnneeDebut = equipeDto.AnneeDebut,
                AnneeFin = equipeDto.AnneeFin,
                EstDevenueEquipe = equipeDto.EstDevenueEquipe
            };

            _context.Entry(equipeBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EquipeBdExists(id))
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

        // POST: api/Equipe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EquipeDto>> PostEquipeDto(EquipeDto equipe)
        {
            var equipeBd = new EquipeBd
            {
                Id = equipe.Id,
                NomEquipe = equipe.NomEquipe,
                Ville = equipe.Ville,
                AnneeDebut = equipe.AnneeDebut,
                AnneeFin = equipe.AnneeFin,
                EstDevenueEquipe = equipe.EstDevenueEquipe
            };

            try
            {
                _context.equipe.Add(equipeBd);
                await _context.SaveChangesAsync();

                equipe.Id = equipeBd.Id;
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

            return CreatedAtAction("PostEquipeDto", equipe);
        }

        // DELETE: api/Equipe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipe(int id)
        {
            var equipeBd = await _context.equipe.FindAsync(id);
            if (equipeBd == null)
            {
                return NotFound();
            }

            _context.equipe.Remove(equipeBd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipeBdExists(int id)
        {
            return _context.equipe.Any(e => e.Id == id);
        }
    }
}