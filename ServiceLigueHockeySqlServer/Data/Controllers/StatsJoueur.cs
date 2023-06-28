using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLigueHockey.Data;
using ServiceLigueHockey.Data.Models;
using ServiceLigueHockey.Data.Models.Dto;

namespace ServiceLigueHockey.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsJoueur : ControllerBase
    {
        private readonly ServiceLigueHockeyContext _context;

        public StatsJoueur(ServiceLigueHockeyContext context)
        {
            _context = context;
        }

        // GET: api/StatsJoueurBds/parannee/2020
        [HttpGet("parannee/{annee}")]
        public ActionResult<IEnumerable<StatsJoueurDto>> GetStatsJoueurBd(short annee)
        {
            var listeStats = _context.statsJoueurBd
                .Where(x => x.AnneeStats == annee)
                .OrderByDescending(x => x.NbPoints)
                .Select(item => new StatsJoueurDto
                {
                    JoueurId = item.JoueurId,
                    EquipeId = item.EquipeId,
                    AnneeStats = item.AnneeStats,
                    NbPartiesJouees = item.NbPartiesJouees,
                    NbButs = item.NbButs,
                    NbPasses = item.NbPasses,
                    NbPoints = item.NbPoints,
                    NbMinutesPenalites = item.NbMinutesPenalites,
                    PlusseMoins = item.PlusseMoins,
                    MinutesJouees = item.MinutesJouees,
                    Victoires = item.Victoires,
                    Defaites = item.Defaites,
                    DefaitesEnProlongation = item.DefaitesEnProlongation,
                    Nulles = item.Nulles,
                    ButsAlloues = item.ButsAlloues,
                    TirsAlloues = item.TirsAlloues,
                    Joueur = new JoueurDto
                    {
                        Id = item.Joueur.Id,
                        Prenom = item.Joueur.Prenom,
                        Nom = item.Joueur.Nom,
                        VilleNaissance = item.Joueur.VilleNaissance,
                        PaysOrigine = item.Joueur.PaysOrigine,
                        DateNaissance = item.Joueur.DateNaissance
                    },
                    Equipe = new EquipeDto
                    {
                        Id = item.Equipe.Id,
                        NomEquipe = item.Equipe.NomEquipe,
                        Ville = item.Equipe.Ville,
                        AnneeDebut = item.Equipe.AnneeDebut,
                        AnneeFin = item.Equipe.AnneeFin,
                        EstDevenueEquipe = item.Equipe.EstDevenueEquipe
                    }
                });

            return Ok(listeStats.ToList());
        }

        // GET: api/StatsJoueurBds/5/2020
        [HttpGet("{noJoueur}/{anneeStats}")]
        public ActionResult<StatsJoueurDto> GetStatsJoueurBd(int joueurId, short anneeStats)
        {
            var retour = _context.statsJoueurBd
                .Where(x => x.JoueurId == joueurId && x.AnneeStats == anneeStats)
                .Select(statsJoueurBd => new StatsJoueurDto
                {
                    JoueurId = statsJoueurBd.JoueurId,
                    EquipeId = statsJoueurBd.EquipeId,
                    AnneeStats = statsJoueurBd.AnneeStats,
                    NbPartiesJouees = statsJoueurBd.NbPartiesJouees,
                    NbButs = statsJoueurBd.NbButs,
                    NbPasses = statsJoueurBd.NbPasses,
                    NbPoints = statsJoueurBd.NbPoints,
                    NbMinutesPenalites = statsJoueurBd.NbMinutesPenalites,
                    PlusseMoins = statsJoueurBd.PlusseMoins,
                    Victoires = statsJoueurBd.Victoires,
                    Defaites = statsJoueurBd.Defaites,
                    DefaitesEnProlongation = statsJoueurBd.DefaitesEnProlongation,
                    Nulles = statsJoueurBd.Nulles,
                    MinutesJouees = statsJoueurBd.MinutesJouees,
                    ButsAlloues = statsJoueurBd.ButsAlloues,
                    TirsAlloues = statsJoueurBd.TirsAlloues,
                    Joueur = new JoueurDto
                    {
                        Id = statsJoueurBd.Joueur.Id,
                        Prenom = statsJoueurBd.Joueur.Prenom,
                        Nom = statsJoueurBd.Joueur.Nom,
                        VilleNaissance = statsJoueurBd.Joueur.VilleNaissance,
                        PaysOrigine = statsJoueurBd.Joueur.PaysOrigine,
                        DateNaissance = statsJoueurBd.Joueur.DateNaissance
                    },
                    Equipe = new EquipeDto
                    {
                        Id = statsJoueurBd.Equipe.Id,
                        NomEquipe = statsJoueurBd.Equipe.NomEquipe,
                        Ville = statsJoueurBd.Equipe.Ville,
                        AnneeDebut = statsJoueurBd.Equipe.AnneeDebut,
                        AnneeFin = statsJoueurBd.Equipe.AnneeFin,
                        EstDevenueEquipe = statsJoueurBd.Equipe.EstDevenueEquipe
                    }
                }).FirstOrDefault();

            if (retour == null)
            {
                return NotFound();
            }

            return Ok(retour);
        }

        // PUT: api/StatsJoueurBds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{annee}")]
        public async Task<IActionResult> PutStatsJoueurBd(int id, short annee, StatsJoueurDto statsJoueurDto)
        {
            if (id != statsJoueurDto.JoueurId && annee != statsJoueurDto.AnneeStats)
            {
                return BadRequest();
            }

            var statsJoueurBd = new StatsJoueurBd
            {
                JoueurId = statsJoueurDto.JoueurId,
                EquipeId = statsJoueurDto.EquipeId,
                AnneeStats = statsJoueurDto.AnneeStats,
                NbPartiesJouees = statsJoueurDto.NbPartiesJouees,
                NbButs = statsJoueurDto.NbButs,
                NbPasses = statsJoueurDto.NbPasses,
                NbPoints = statsJoueurDto.NbPoints,
                NbMinutesPenalites = statsJoueurDto.NbMinutesPenalites,
                PlusseMoins = statsJoueurDto.PlusseMoins,
                Victoires = statsJoueurDto.Victoires,
                Defaites = statsJoueurDto.Defaites,
                DefaitesEnProlongation = statsJoueurDto.DefaitesEnProlongation,
                Nulles = statsJoueurDto.Nulles,
                MinutesJouees = statsJoueurDto.MinutesJouees,
                ButsAlloues = statsJoueurDto.ButsAlloues,
                TirsAlloues = statsJoueurDto.TirsAlloues,
                Joueur = new JoueurBd
                {
                    Id = statsJoueurDto.Joueur.Id,
                    Prenom = statsJoueurDto.Joueur.Prenom,
                    Nom = statsJoueurDto.Joueur.Nom,
                    VilleNaissance = statsJoueurDto.Joueur.VilleNaissance,
                    PaysOrigine = statsJoueurDto.Joueur.PaysOrigine,
                    DateNaissance = statsJoueurDto.Joueur.DateNaissance
                },
                Equipe = new EquipeBd
                {
                    Id = statsJoueurDto.Equipe.Id,
                    NomEquipe = statsJoueurDto.Equipe.NomEquipe,
                    Ville = statsJoueurDto.Equipe.Ville,
                    AnneeDebut = statsJoueurDto.Equipe.AnneeDebut,
                    AnneeFin = statsJoueurDto.Equipe.AnneeFin,
                    EstDevenueEquipe = statsJoueurDto.Equipe.EstDevenueEquipe
                }
            };

            _context.Entry(statsJoueurBd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatsJoueurBdExists(id, annee))
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

        // POST: api/StatsJoueurBds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatsJoueurDto>> PostStatsJoueurBd(StatsJoueurDto statsJoueurDto)
        {
            var joueurBd = new JoueurBd
            {
                Id = statsJoueurDto.Joueur.Id,
                Prenom = statsJoueurDto.Joueur.Prenom,
                Nom = statsJoueurDto.Joueur.Nom,
                VilleNaissance = statsJoueurDto.Joueur.VilleNaissance,
                PaysOrigine = statsJoueurDto.Joueur.PaysOrigine,
                DateNaissance = statsJoueurDto.Joueur.DateNaissance,
                listeStatsJoueur = new List<StatsJoueurBd>()
            };

            var equipeBd = new EquipeBd
            {
                Id = statsJoueurDto.Equipe.Id,
                NomEquipe = statsJoueurDto.Equipe.NomEquipe,
                Ville = statsJoueurDto.Equipe.Ville,
                AnneeDebut = statsJoueurDto.Equipe.AnneeDebut,
                AnneeFin = statsJoueurDto.Equipe.AnneeFin,
                EstDevenueEquipe = statsJoueurDto.Equipe.EstDevenueEquipe
            };

            var statsJoueurBd = new StatsJoueurBd
            {
                JoueurId = statsJoueurDto.JoueurId,
                EquipeId = statsJoueurDto.EquipeId,
                AnneeStats = statsJoueurDto.AnneeStats,
                NbPartiesJouees = statsJoueurDto.NbPartiesJouees,
                NbButs = statsJoueurDto.NbButs,
                NbPasses = statsJoueurDto.NbPasses,
                NbPoints = statsJoueurDto.NbPoints,
                NbMinutesPenalites = statsJoueurDto.NbMinutesPenalites,
                PlusseMoins = statsJoueurDto.PlusseMoins,
                Victoires = statsJoueurDto.Victoires,
                Defaites = statsJoueurDto.Defaites,
                DefaitesEnProlongation = statsJoueurDto.DefaitesEnProlongation,
                Nulles = statsJoueurDto.Nulles,
                MinutesJouees = statsJoueurDto.MinutesJouees,
                ButsAlloues = statsJoueurDto.ButsAlloues,
                TirsAlloues = statsJoueurDto.TirsAlloues,
                Joueur = new JoueurBd
                {
                    Id = statsJoueurDto.Joueur.Id,
                    Prenom = statsJoueurDto.Joueur.Prenom,
                    Nom = statsJoueurDto.Joueur.Nom,
                    VilleNaissance = statsJoueurDto.Joueur.VilleNaissance,
                    PaysOrigine = statsJoueurDto.Joueur.PaysOrigine,
                    DateNaissance = statsJoueurDto.Joueur.DateNaissance
                },
                Equipe = new EquipeBd
                {
                    Id = statsJoueurDto.Equipe.Id,
                    NomEquipe = statsJoueurDto.Equipe.NomEquipe,
                    Ville = statsJoueurDto.Equipe.Ville,
                    AnneeDebut = statsJoueurDto.Equipe.AnneeDebut,
                    AnneeFin = statsJoueurDto.Equipe.AnneeFin,
                    EstDevenueEquipe = statsJoueurDto.Equipe.EstDevenueEquipe
                }
            };

            _context.joueur.Attach(statsJoueurBd.Joueur);
            _context.equipe.Attach(statsJoueurBd.Equipe);
            _context.statsJoueurBd.Add(statsJoueurBd);
            joueurBd.listeStatsJoueur.Add(statsJoueurBd);
            //equipeBd.listeEquipeJoueur.Add(joueurBd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (StatsJoueurBdExists(statsJoueurBd.JoueurId, statsJoueurBd.AnneeStats))
                {
                    return Conflict(ex);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("PostStatsJoueurBd", statsJoueurDto);
        }

        // DELETE: api/StatsJoueurBds/5
        [HttpDelete("{id}/{annee}")]
        public async Task<IActionResult> DeleteStatsJoueurBd(int id, short annee)
        {
            var statsJoueurBd = await _context.statsJoueurBd.FindAsync(id, annee);
            if (statsJoueurBd == null)
            {
                return NotFound();
            }

            _context.statsJoueurBd.Remove(statsJoueurBd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatsJoueurBdExists(int id, short annee)
        {
            return _context.statsJoueurBd.Any(e => e.JoueurId == id && e.AnneeStats == annee);
        }
    }
}