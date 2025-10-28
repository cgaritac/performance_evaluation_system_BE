using Microsoft.EntityFrameworkCore;
using PACE.Models.ActivityModels;
using PACE.Models.EmployeeModels;
using PACE.Models.EvaluationModels;
using PACE.Models.GoalModels;

namespace PACE.Data;

public class PaceDbContext : DbContext
{
    public DbSet<EmployeeModel> Employees { get; set; }
    public DbSet<ActivityModel> Activities { get; set; }
    public DbSet<EvaluationModel> Evaluations { get; set; }
    public DbSet<GoalModel> Goals { get; set; }

    public PaceDbContext(DbContextOptions<PaceDbContext> options)
    : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EmployeeModel>(entity =>
        {
            entity.ToTable("Employees");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.EmployeeNumber).HasColumnName("EmployeeNumber").HasColumnType("int").IsRequired();
            entity.Property(e => e.FirstName).HasColumnName("FirstName").HasColumnType("varchar(100)").IsRequired();
            entity.Property(e => e.LastName).HasColumnName("LastName").HasColumnType("varchar(100)").IsRequired();
            entity.Property(e => e.EmployeeEmail).HasColumnName("EmployeeEmail").HasColumnType("varchar(100)").IsRequired();
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentId").HasColumnType("int").IsRequired();

            entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy").HasColumnType("varchar(100)").IsRequired();
            entity.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.UpdatedBy).HasColumnName("UpdatedBy").HasColumnType("varchar(100)").HasDefaultValue(null);
            entity.Property(e => e.UpdatedOn).HasColumnName("UpdatedOn").HasColumnType("datetime").HasDefaultValue(null);
            entity.Property(e => e.DeletedBy).HasColumnName("DeletedBy").HasColumnType("varchar(100)").HasDefaultValue(null);
            entity.Property(e => e.DeletedOn).HasColumnName("DeletedOn").HasColumnType("datetime").HasDefaultValue(null);
            entity.Property(e => e.IsActive).HasColumnName("IsActive").HasColumnType("bit").HasDefaultValue(true);

            entity.HasMany(e => e.Evaluations)
              .WithOne(ev => ev.Employee)
              .HasForeignKey(ev => ev.EmployeeId);
        });

        modelBuilder.Entity<ActivityModel>(entity =>
        {
            entity.ToTable("Activities");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).ValueGeneratedOnAdd();
            entity.Property(a => a.GoalId).HasColumnName("GoalId").HasColumnType("int").IsRequired();
            entity.Property(a => a.Title).HasColumnName("Title").HasColumnType("varchar(100)").IsRequired();
            entity.Property(a => a.Description).HasColumnName("Description").HasColumnType("varchar(500)").IsRequired();

            entity.Property(a => a.CreatedBy).HasColumnName("CreatedBy").HasColumnType("varchar(100)").IsRequired();
            entity.Property(a => a.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
            entity.Property(a => a.UpdatedBy).HasColumnName("UpdatedBy").HasColumnType("varchar(100)").HasDefaultValue(null);
            entity.Property(a => a.UpdatedOn).HasColumnName("UpdatedOn").HasColumnType("datetime").HasDefaultValue(null);
            entity.Property(a => a.DeletedBy).HasColumnName("DeletedBy").HasColumnType("varchar(100)").HasDefaultValue(null);
            entity.Property(a => a.DeletedOn).HasColumnName("DeletedOn").HasColumnType("datetime").HasDefaultValue(null);
            entity.Property(a => a.IsActive).HasColumnName("IsActive").HasColumnType("bit").HasDefaultValue(true);

            entity.HasOne(a => a.Goal)
               .WithMany(g => g.Activities)
               .HasForeignKey(a => a.GoalId);
        });

        modelBuilder.Entity<EvaluationModel>(entity =>
        {
            entity.ToTable("Evaluations");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeId").HasColumnType("int").IsRequired();
            entity.Property(e => e.Year).HasColumnName("Year").HasColumnType("int").IsRequired();
            entity.Property(e => e.FeedbackComments).HasColumnName("FeedbackComments").HasColumnType("varchar(500)").HasDefaultValue(null);
            entity.Property(e => e.Feedback).HasColumnName("FeedBack").HasColumnType("int");

            entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy").HasColumnType("varchar(100)").IsRequired();
            entity.Property(e => e.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.UpdatedBy).HasColumnName("UpdatedBy").HasColumnType("varchar(100)").HasDefaultValue(null);
            entity.Property(e => e.UpdatedOn).HasColumnName("UpdatedOn").HasColumnType("datetime").HasDefaultValue(null);
            entity.Property(e => e.DeletedBy).HasColumnName("DeletedBy").HasColumnType("varchar(100)").HasDefaultValue(null);
            entity.Property(e => e.DeletedOn).HasColumnName("DeletedOn").HasColumnType("datetime").HasDefaultValue(null);
            entity.Property(e => e.IsActive).HasColumnName("IsActive").HasColumnType("bit").HasDefaultValue(true);

            entity.HasMany(e => e.Goals)
              .WithOne(g => g.Evaluation)
              .HasForeignKey(g => g.EvaluationId);

            entity.HasOne(e => e.Employee)
                  .WithMany(emp => emp.Evaluations)
                  .HasForeignKey(e => e.EmployeeId);
        });

        modelBuilder.Entity<GoalModel>(entity =>
        {
            entity.ToTable("Goals");
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Id).ValueGeneratedOnAdd();
            entity.Property(g => g.EvaluationId).HasColumnName("EvaluationId").HasColumnType("int").IsRequired();
            entity.Property(g => g.GoalCategory).HasColumnName("GoalCategory").HasColumnType("int").IsRequired();
            entity.Property(g => g.GoalType).HasColumnName("GoalType").HasColumnType("int").IsRequired();
            entity.Property(g => g.Title).HasColumnName("Title").HasColumnType("varchar(100)").IsRequired();
            entity.Property(g => g.Description).HasColumnName("Description").HasColumnType("varchar(500)").IsRequired();
            entity.Property(g => g.Approval).HasColumnName("Approval").HasColumnType("int").HasDefaultValue(null);
            entity.Property(g => g.StartDate).HasColumnName("StartDate").HasColumnType("datetime").HasDefaultValue(null);
            entity.Property(g => g.EndDate).HasColumnName("EndDate").HasColumnType("datetime").HasDefaultValue(null);
            entity.Property(g => g.DueDate).HasColumnName("DueDate").HasColumnType("datetime").IsRequired();
            entity.Property(g => g.Status).HasColumnName("Status").HasColumnType("int").IsRequired();

            entity.Property(g => g.CreatedBy).HasColumnName("CreatedBy").HasColumnType("varchar(100)").IsRequired();
            entity.Property(g => g.CreatedOn).HasColumnName("CreatedOn").HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
            entity.Property(g => g.UpdatedBy).HasColumnName("UpdatedBy").HasColumnType("varchar(100)").HasDefaultValue(null);
            entity.Property(g => g.UpdatedOn).HasColumnName("UpdatedOn").HasColumnType("datetime").HasDefaultValue(null);
            entity.Property(g => g.DeletedBy).HasColumnName("DeletedBy").HasColumnType("varchar(100)").HasDefaultValue(null);
            entity.Property(g => g.DeletedOn).HasColumnName("DeletedOn").HasColumnType("datetime").HasDefaultValue(null);
            entity.Property(g => g.IsActive).HasColumnName("IsActive").HasColumnType("bit").HasDefaultValue(true);

            entity.HasMany(g => g.Activities)
                  .WithOne(a => a.Goal)
                  .HasForeignKey(a => a.GoalId);

            entity.HasOne(g => g.Evaluation)
                  .WithMany(ev => ev.Goals)
                  .HasForeignKey(g => g.EvaluationId);
        });
    }
}
