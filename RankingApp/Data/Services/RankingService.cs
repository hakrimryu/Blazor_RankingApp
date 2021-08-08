using Newtonsoft.Json;
using SharedData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RankingApp.Data.Services
{
    // [c <=> s] <-> [s] - DB

    public class RankingService
    {
        HttpClient _httpClient;

        public RankingService(HttpClient client)
        {
            this._httpClient = client;
        }

        // Create
        /// <summary>
        /// Result 追加
        /// </summary>
        public async Task<GameResult> AddGameResult(GameResult gameResult)
        {
            // GameResultをJsonに変更
            string jsonStr = JsonConvert.SerializeObject(gameResult);
            var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            // Postしてresult取得
            var result = await _httpClient.PostAsync("api/ranking", content);

            // result取得の失敗
            if (result.IsSuccessStatusCode == false)
                throw new Exception("Add GameResult Failed");

            // 成功時取得
            var resultContent = await result.Content.ReadAsStringAsync();
            // 取得したstring Game Resultでパッシング
            GameResult resGameResult = JsonConvert.DeserializeObject<GameResult>(resultContent);
            return resGameResult;
        }

        // Read
        /// <summary>
        /// GameResult DB (sort Score)
        /// </summary>
        public async Task<List<GameResult>> GetGameResultsAsync()
        {
            // Getしてresult取得
            var result = await _httpClient.GetAsync("api/ranking");

            // result取得の失敗
            if (result.IsSuccessStatusCode == false)
                throw new Exception("Get GameResult Failed");

            // 成功時取得
            var resultContent = await result.Content.ReadAsStringAsync();
            // 取得したstring Game Resultでパッシング
            List<GameResult> resGameResults = JsonConvert.DeserializeObject<List<GameResult>>(resultContent);
            return resGameResults;
        }

        // Update
        /// <summary>
        /// Result 変更
        /// </summary>
        public async Task<bool> UpdateGameResult(GameResult gameResult)
        {
            // GameResultをJsonに変更
            string jsonStr = JsonConvert.SerializeObject(gameResult);
            var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            // Putしてresult取得
            var result = await _httpClient.PutAsync("api/ranking", content);

            // result取得の失敗
            if (result.IsSuccessStatusCode == false)
                throw new Exception("Update GameResult Failed");

            return true;
        }
        
        // Delete
        /// <summary>
        /// Result 削除
        /// </summary>
        public async Task<bool> DeletGameResult(GameResult gameResult)
        {
            // IDを使って、 Deleteしてresult取得
            var result = await _httpClient.DeleteAsync($"api/ranking/{gameResult.Id}");

            // result取得の失敗
            if (result.IsSuccessStatusCode == false)
                throw new Exception("Delet GameResult Failed");

            return true;
        }
    }
}
