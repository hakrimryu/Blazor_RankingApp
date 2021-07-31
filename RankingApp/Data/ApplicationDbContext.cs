﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RankingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        /// GameResults DB連動
        /// </summary>
        public DbSet<GameResult> GameResults { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
