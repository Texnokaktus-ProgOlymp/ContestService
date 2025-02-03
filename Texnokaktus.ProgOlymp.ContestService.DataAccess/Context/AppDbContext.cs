using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.ContestService.DataAccess.Entities;

namespace Texnokaktus.ProgOlymp.ContestService.DataAccess.Context;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Contest> Contests { get; set; }
    public DbSet<ContestStage> ContestStages { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contest>(builder =>
        {
            builder.HasKey(contest => contest.Id);
            builder.Property(contest => contest.Id).UseIdentityColumn();

            builder.HasOne<ContestStage>(contest => contest.PreliminaryStage)
                   .WithOne()
                   .HasForeignKey<Contest>(contest => contest.PreliminaryStageId);

            builder.HasOne<ContestStage>(contest => contest.FinalStage)
                   .WithOne()
                   .HasForeignKey<Contest>(contest => contest.FinalStageId);
        });

        modelBuilder.Entity<ContestStage>(builder =>
        {
            builder.HasKey(stage => stage.Id);
            builder.Property(stage => stage.Id).ValueGeneratedNever();

            builder.Property(stage => stage.Duration)
                   .HasConversion(timeSpan => timeSpan.Ticks, ticks => TimeSpan.FromTicks(ticks));
        });
        
        base.OnModelCreating(modelBuilder);
    }
}
