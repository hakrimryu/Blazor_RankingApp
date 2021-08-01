using System;
using System.Collections.Generic;
using System.Text;

namespace SharedData.Models
{
    class GameResult
    {
        /// <summary>
        /// DB ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// スコア
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 時間
        /// </summary>
        public DateTime Data { get; set; }
    }
}
