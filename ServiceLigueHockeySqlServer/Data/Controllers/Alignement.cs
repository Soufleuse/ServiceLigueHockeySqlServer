using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockeySqlServer.Data.Models;
using ServiceLigueHockeySqlServer.Data.Models.Dto;

namespace ServiceLigueHockeySqlServer.Data.Controllers
{
    /*
     * Controleur pour Alignement
     */
    [ApiController]
    [Route("api/[controller]")]
    public class Alignement : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;

        public Alignement(ServiceLigueHockeyContext context)
        {
            _context = context;
        }

        // GET: api/alignement
        [HttpGet]
        public ActionResult<IQueryable<AlignementDto>> GetAlignement()
        {
            var listeAlignement = from monAlignement in _context.alignements
                                  select new AlignementDto
                                  {
                                    Id = monAlignement.Id,
                                    DateDebut = monAlignement.DateDebut,
                                    DateFin = monAlignement.DateFin,
                                    EquipeId = monAlignement.EquipeId,
                                    JoueurId = monAlignement.JoueurId/*,
                                    equipe = new EquipeDto {
                                        Id = monAlignement.equipe.Id,
                                        NomEquipe = monAlignement.equipe.NomEquipe,
                                        Ville = monAlignement.equipe.Ville,
                                        AnneeDebut = monAlignement.equipe.AnneeDebut,
                                        AnneeFin = monAlignement.equipe.AnneeFin,
                                        EstDevenueEquipe = monAlignement.equipe.EstDevenueEquipe
                                    },
                                    joueur = new JoueurDto {
                                        Id = monAlignement.Id,
                                        Prenom = monAlignement.joueur.Prenom,
                                        Nom = monAlignement.joueur.Nom,
                                        DateNaissance = monAlignement.joueur.DateNaissance,
                                        VilleNaissance = monAlignement.joueur.VilleNaissance,
                                        PaysOrigine = monAlignement.joueur.PaysOrigine
                                    }*/
                                  };
            return Ok(listeAlignement);
        }

        // GET: api/alignement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlignementDto>> GetAlignement(int id)
        {
            var AlignementBd = await _context.alignements.FindAsync(id);

            if (AlignementBd == null)
            {
                return NotFound();
            }

            var alignementDto = new AlignementDto
            {
                Id = AlignementBd.Id,
                EquipeId = AlignementBd.EquipeId,
                JoueurId = AlignementBd.JoueurId,
                DateDebut = AlignementBd.DateDebut,
                DateFin = AlignementBd.DateFin/*,
                equipe = new EquipeDto {
                    Id = AlignementBd.equipe.Id,
                    NomEquipe = AlignementBd.equipe.NomEquipe,
                    Ville = AlignementBd.equipe.Ville,
                    AnneeDebut = AlignementBd.equipe.AnneeDebut,
                    AnneeFin = AlignementBd.equipe.AnneeFin
                },
                joueur = new JoueurDto {
                    Id = AlignementBd.joueur.Id,
                    Prenom = AlignementBd.joueur.Prenom,
                    Nom = AlignementBd.joueur.Nom,
                    DateNaissance = AlignementBd.joueur.DateNaissance,
                    VilleNaissance = AlignementBd.joueur.VilleNaissance,
                    PaysOrigine = AlignementBd.joueur.PaysOrigine
                }*/
            };

            return Ok(alignementDto);
        }

        // PUT: api/alignement/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlignement(int id, AlignementDto alignementDto)
        {
            if (id != alignementDto.Id)
            {
                return BadRequest();
            }

            var alignementBd = new AlignementBd
            {
                Id = alignementDto.Id,
                EquipeId = alignementDto.EquipeId,
                JoueurId = alignementDto.JoueurId,
                DateDebut = alignementDto.DateDebut,
                DateFin = alignementDto.DateFin/*,
                equipe = new EquipeBd {
                    Id = alignementDto.equipe.Id,
                    NomEquipe = alignementDto.equipe.NomEquipe,
                    Ville = alignementDto.equipe.Ville,
                    AnneeDebut = alignementDto.equipe.AnneeDebut,
                    AnneeFin = alignementDto.equipe.AnneeFin,
                    EstDevenueEquipe = alignementDto.equipe.EstDevenueEquipe
                },
                joueur = new JoueurBd {
                    Id = alignementDto.joueur.Id,
                    Prenom = alignementDto.joueur.Prenom,
                    Nom = alignementDto.joueur.Nom,
                    DateNaissance = alignementDto.joueur.DateNaissance,
                    VilleNaissance = alignementDto.joueur.VilleNaissance,
                    PaysOrigine = alignementDto.joueur.PaysOrigine
                }*/
            };

            _context.Entry(alignementBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlignemenBdExists(id))
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

        // POST: api/alignement
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlignementDto>> PostAlignementDto(AlignementDto alignement)
        {
            var alignementBd = new AlignementBd
            {
                Id = alignement.Id,
                DateDebut = alignement.DateDebut,
                DateFin = alignement.DateFin,
                EquipeId = alignement.EquipeId,
                JoueurId = alignement.JoueurId/*,
                equipe = new EquipeBd {
                    Id = alignement.equipe.Id,
                    NomEquipe = alignement.equipe.NomEquipe,
                    Ville = alignement.equipe.Ville,
                    AnneeDebut = alignement.equipe.AnneeDebut,
                    AnneeFin = alignement.equipe.AnneeFin
                },
                joueur = new JoueurBd {
                    Id = alignement.joueur.Id,
                    Prenom = alignement.joueur.Prenom,
                    Nom = alignement.joueur.Nom,
                    DateNaissance = alignement.joueur.DateNaissance,
                    VilleNaissance = alignement.joueur.VilleNaissance,
                    PaysOrigine = alignement.joueur.VilleNaissance
                }*/
            };

            _context.alignements.Add(alignementBd);
            await _context.SaveChangesAsync();

            alignement.Id = alignementBd.Id;

            return CreatedAtAction("PostAlignementDto", alignement);
        }

        // DELETE: api/alignement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletealignement(int id)
        {
            var alignementBd = await _context.alignements.FindAsync(id);
            if (alignementBd == null)
            {
                return NotFound();
            }

            _context.alignements.Remove(alignementBd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlignemenBdExists(int id)
        {
            return _context.alignements.Any(e => e.Id == id);
        }
    }
}