using Microsoft.EntityFrameworkCore;
using System;

namespace TestASP.NetIdentity.DataAccess
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
        }

        public DbSet<TestASP.NetIdentity.DomainEntities.People> People { get; set; }
        public DbSet<TestASP.NetIdentity.DomainEntities.Advertisement> Advertisement { get; set; }
        public DbSet<TestASP.NetIdentity.DomainEntities.Question> Question { get; set; }
        public DbSet<TestASP.NetIdentity.DomainEntities.QuestionType> QuestionType { get; set; }
        public DbSet<TestASP.NetIdentity.DomainEntities.Skill> Skill { get; set; }
        public DbSet<TestASP.NetIdentity.DomainEntities.Duty> Duty { get; set; }
        public DbSet<TestASP.NetIdentity.DomainEntities.Questionnaire> Questionnaire { get; set; }
        public DbSet<TestASP.NetIdentity.DomainEntities.Application> Application { get; set; }
        public DbSet<TestASP.NetIdentity.DomainEntities.Answer> Answer { get; set; }
        public DbSet<TestASP.NetIdentity.DomainEntities.AnswerType> AnswerType { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
