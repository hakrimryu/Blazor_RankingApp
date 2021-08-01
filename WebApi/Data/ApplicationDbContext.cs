using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedData.Models;

namespace WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// GameResults DB連動
        /// </summary>
        public DbSet<GameResult> GameResults { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
