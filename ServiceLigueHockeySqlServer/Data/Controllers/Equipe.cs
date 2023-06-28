using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockey.Data.Models;
using ServiceLigueHockey.Data.Models.Dto;

namespace ServiceLigueHockey.Data.Controllers
{
    /*
     * Controleur pour Equipe
     */
    [ApiController]
    [Route("api/[controller]")]
    public class Equipe : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;

        public Equipe(ServiceLigueHockeyContext context)
        {
            _context = context;
        }

        // GET: api/Equipe
        [HttpGet]
        public ActionResult<IQueryable<EquipeDto>> GetEquipeDto()
        {
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
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipeBdExists(id))
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

            _context.equipe.Add(equipeBd);
            await _context.SaveChangesAsync();

            equipe.Id = equipeBd.Id;

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