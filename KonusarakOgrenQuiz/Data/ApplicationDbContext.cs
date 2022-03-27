using KonusarakOgrenQuiz.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KonusarakOgrenQuiz.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Questions> questions { get; set; }
        public DbSet<QuestionList> questionList { get; set; }

        public DbSet<KonusarakOgrenQuiz.Models.Wired> Wired { get; set; }

        public DbSet<KonusarakOgrenQuiz.Models.Quiz> Quiz { get; set; }
        
    }
}
