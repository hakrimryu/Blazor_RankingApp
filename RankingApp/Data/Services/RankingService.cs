using RankingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RankingApp.Data.Services
{
    public class RankingService
    {
        /// <summary>
        /// DB Data
        /// </summary>
        ApplicationDbContext _context;

        public RankingService(ApplicationDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// GameResult DB (sort Score)
        /// </summary>
        public Task<List<GameResult>> GetGameResultsAsync()
        {
            // GameResult DBで、全データスコア順に取得
            List<GameResult> results = this._context.GameResults
                .OrderByDescending(item => item.Score)
                .ToList();

            return Task.FromResult(results);
        }
    }
}
