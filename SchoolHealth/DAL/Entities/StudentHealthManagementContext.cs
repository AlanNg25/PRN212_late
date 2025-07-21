using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class StudentHealthManagementContext : DbContext
{
    public StudentHealthManagementContext()
    {
    }

    public StudentHealthManagementContext(DbContextOptions<StudentHealthManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<HealthCheck> HealthChecks { get; set; }

    public virtual DbSet<HealthCheckForm> HealthCheckForms { get; set; }

    public virtual DbSet<HealthRecord> HealthRecords { get; set; }

    public virtual DbSet<MedicalEvent> MedicalEvents { get; set; }

    public virtual DbSet<MedicalSupply> MedicalSupplies { get; set; }

    public virtual DbSet<MedicineSent> MedicineSents { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<Vaccination> Vaccinations { get; set; }

    public virtual DbSet<VaccinationConsentForm> VaccinationConsentForms { get; set; }
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blog__54379E30E805F4F4");

            entity.ToTable("Blog");

            entity.HasIndex(e => e.DatePosted, "IX_Blog_DatePosted");

            entity.Property(e => e.DatePosted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Author).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Blog__AuthorId__5DCAEF64");
        });

        modelBuilder.Entity<HealthCheck>(entity =>
        {
            entity.HasKey(e => e.CheckId).HasName("PK__HealthCh__86815766174E9A40");

            entity.ToTable("HealthCheck");

            entity.HasIndex(e => e.Date, "IX_HealthCheck_Date");

            entity.HasIndex(e => e.StudentId, "IX_HealthCheck_StudentId");

            entity.Property(e => e.DoctorNotes).HasMaxLength(1000);
            entity.Property(e => e.Result).HasMaxLength(1000);

            entity.HasOne(d => d.Student).WithMany(p => p.HealthChecks)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HealthChe__Stude__5535A963");
        });

        modelBuilder.Entity<HealthCheckForm>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__HealthCh__FB05B7DD4A5F3D40");

            entity.ToTable("HealthCheckForm");

            entity.HasIndex(e => e.StudentId, "IX_HealthCheckForm_StudentId");

            entity.Property(e => e.Confirmed).HasDefaultValue(false);
            entity.Property(e => e.SentDate).HasColumnType("datetime");

            entity.HasOne(d => d.Parent).WithMany(p => p.HealthCheckForms)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HealthChe__Paren__59FA5E80");

            entity.HasOne(d => d.Student).WithMany(p => p.HealthCheckForms)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HealthChe__Stude__59063A47");
        });

        modelBuilder.Entity<HealthRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__HealthRe__FBDF78E961BBA1B3");

            entity.ToTable("HealthRecord");

            entity.HasIndex(e => e.StudentId, "IX_HealthRecord_StudentId");

            entity.Property(e => e.Allergy).HasMaxLength(500);
            entity.Property(e => e.ChronicDisease).HasMaxLength(500);
            entity.Property(e => e.Hearing).HasMaxLength(100);
            entity.Property(e => e.MedicalHistory).HasMaxLength(1000);
            entity.Property(e => e.Vision).HasMaxLength(100);

            entity.HasOne(d => d.Student).WithMany(p => p.HealthRecords)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HealthRec__Stude__403A8C7D");
        });

        modelBuilder.Entity<MedicalEvent>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__MedicalE__7944C810C274024D");

            entity.ToTable("MedicalEvent");

            entity.HasIndex(e => e.Date, "IX_MedicalEvent_Date");

            entity.HasIndex(e => e.StudentId, "IX_MedicalEvent_StudentId");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.TreatmentGiven).HasMaxLength(1000);

            entity.HasOne(d => d.Nurse).WithMany(p => p.MedicalEvents)
                .HasForeignKey(d => d.NurseId)
                .HasConstraintName("FK__MedicalEv__Nurse__440B1D61");

            entity.HasOne(d => d.Student).WithMany(p => p.MedicalEvents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalEv__Stude__4316F928");
        });

        modelBuilder.Entity<MedicalSupply>(entity =>
        {
            entity.HasKey(e => e.SupplyId).HasName("PK__MedicalS__7CDD6CAE85909B37");

            entity.ToTable("MedicalSupply");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<MedicineSent>(entity =>
        {
            entity.HasKey(e => e.SendId).HasName("PK__Medicine__333B5E1924015090");

            entity.ToTable("MedicineSent");

            entity.HasIndex(e => e.StudentId, "IX_MedicineSent_StudentId");

            entity.Property(e => e.Dosage).HasMaxLength(200);
            entity.Property(e => e.Instruction).HasMaxLength(1000);
            entity.Property(e => e.MedicineName).HasMaxLength(200);

            entity.HasOne(d => d.Parent).WithMany(p => p.MedicineSents)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicineS__Paren__49C3F6B7");

            entity.HasOne(d => d.Student).WithMany(p => p.MedicineSents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicineS__Stude__48CFD27E");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.ParentId).HasName("PK__Parent__D339516F64579541");

            entity.ToTable("Parent");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52B998EA6F9FD");

            entity.ToTable("Student");

            entity.HasIndex(e => e.ParentId, "IX_Student_ParentId");

            entity.Property(e => e.Class).HasMaxLength(20);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);

            entity.HasOne(d => d.Parent).WithMany(p => p.Students)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Student__ParentI__398D8EEE");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__UserAcco__349DA5A6A6055A04");

            entity.ToTable("UserAccount");

            entity.HasIndex(e => e.Username, "UQ__UserAcco__536C85E466C77A60").IsUnique();

            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Vaccination>(entity =>
        {
            entity.HasKey(e => e.VaccinationId).HasName("PK__Vaccinat__46643047B64432B3");

            entity.ToTable("Vaccination");

            entity.HasIndex(e => e.Date, "IX_Vaccination_Date");

            entity.HasIndex(e => e.StudentId, "IX_Vaccination_StudentId");

            entity.Property(e => e.Result).HasMaxLength(500);
            entity.Property(e => e.VaccineName).HasMaxLength(100);

            entity.HasOne(d => d.Nurse).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.NurseId)
                .HasConstraintName("FK__Vaccinati__Nurse__4D94879B");

            entity.HasOne(d => d.Student).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vaccinati__Stude__4CA06362");
        });

        modelBuilder.Entity<VaccinationConsentForm>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__Vaccinat__FB05B7DD85837F6E");

            entity.ToTable("VaccinationConsentForm");

            entity.HasIndex(e => e.StudentId, "IX_VaccinationConsentForm_StudentId");

            entity.Property(e => e.Confirmed).HasDefaultValue(false);
            entity.Property(e => e.SentDate).HasColumnType("datetime");

            entity.HasOne(d => d.Parent).WithMany(p => p.VaccinationConsentForms)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vaccinati__Paren__52593CB8");

            entity.HasOne(d => d.Student).WithMany(p => p.VaccinationConsentForms)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vaccinati__Stude__5165187F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
