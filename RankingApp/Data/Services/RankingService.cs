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

        // Create
        /// <summary>
        /// Result 追加
        /// </summary>
        public Task<GameResult> AddGameResult(GameResult gameResult)
        {
            // DBに、追加
            this._context.GameResults.Add(gameResult);
            // DBに、保存
            this._context.SaveChanges();

            return Task.FromResult(gameResult);
        }

        // Read
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

        // Update
        /// <summary>
        /// Result 変更
        /// </summary>
        public Task<bool> UpdateGameResult(GameResult gameResult)
        {
            // ID Check
            var findResult = this._context.GameResults
                .Where(x => x.Id == gameResult.Id)
                .FirstOrDefault();

            // IDがない場合、false返却
            if (findResult == null)
                return Task.FromResult(false);

            // データ変更
            findResult.UserName = gameResult.UserName;
            findResult.Score = gameResult.Score;

            // DBに、保存
            this._context.SaveChanges();
            // true返却
            return Task.FromResult(true);
        }

        // Delete
        /// <summary>
        /// Result 削除
        /// </summary>
        public Task<bool> DeletGameResult(GameResult gameResult)
        {
            // ID Check
            var findResult = this._context.GameResults
                .Where(x => x.Id == gameResult.Id)
                .FirstOrDefault();

            // IDがない場合、false返却
            if (findResult == null)
                return Task.FromResult(false);

            this._context.GameResults.Remove(gameResult);
            // DBに、保存
            this._context.SaveChanges();
            // true返却
            return Task.FromResult(true);

        }
    }
}
