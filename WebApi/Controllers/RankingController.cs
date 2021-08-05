using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using SharedData.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        ApplicationDbContext _context;

        public RankingController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // Create
        [HttpPost]
        public GameResult AddGameResult([FromBody]GameResult gameResult)
        {
            this._context.GameResults.Add(gameResult);
            _context.SaveChanges();

            return gameResult;
        }

        // Read
        [HttpGet]
        public List<GameResult> GetGameResults()
        {
            List<GameResult> results = _context.GameResults
                .OrderByDescending(item => item.Score)
                .ToList();

            return results;
        }

        [HttpGet("{id}")]
        public GameResult GetGameResult(int id)
        {
            GameResult result = _context.GameResults
                .Where(item => item.Id == id)
                .FirstOrDefault();

            return result;
        }

        // Update
        [HttpPut]
        public bool UpdateGameResult([FromBody] GameResult gameResult)
        {
            var findResult = this._context.GameResults
                .Where(x => x.Id == gameResult.Id)
                .FirstOrDefault();

            if (findResult == null)
                return false;

            findResult.UserName = gameResult.UserName;
            findResult.Score = gameResult.Score;

            this._context.SaveChanges();

            return true;
        }


        // Delete
        [HttpDelete("{id}")]
        public bool DeleteGameResult(int id)
        {
            var findResult = this._context.GameResults
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (findResult == null)
                return false;

            this._context.GameResults.Remove(findResult);
            this._context.SaveChanges();

            return true;
        }

    }
}
