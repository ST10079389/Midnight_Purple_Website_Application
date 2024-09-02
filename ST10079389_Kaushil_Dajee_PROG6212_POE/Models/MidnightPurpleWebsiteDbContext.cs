using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ST10079389_Kaushil_Dajee_PROG6212_POE.Models;

namespace ST10079389_Kaushil_Dajee_PROG6212_POE.Models;

public partial class MidnightPurpleWebsiteDbContext : DbContext
{
    public MidnightPurpleWebsiteDbContext()
    {
    }

    public MidnightPurpleWebsiteDbContext(DbContextOptions<MidnightPurpleWebsiteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ModuleInformation> ModuleInformations { get; set; }

    public virtual DbSet<SemesterDate> SemesterDates { get; set; }

    public virtual DbSet<StudyRecord> StudyRecords { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Midnight_Purple_WebsiteDB; Encrypt=False; Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ModuleInformation>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("PK__ModuleIn__2B7477876B02C1A0");

            entity.ToTable("ModuleInformation");

            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.ModuleCode)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Module_Code");
            entity.Property(e => e.ModuleName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Module_Name");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.ModuleInformations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ModuleInf__UserI__3C69FB99");
        });

        modelBuilder.Entity<SemesterDate>(entity =>
        {
            entity.HasKey(e => e.SemesterDatesId).HasName("PK__Semester__C2F9D9FC3EC24DE0");

            entity.Property(e => e.SemesterDatesId).HasColumnName("SemesterDatesID");
            entity.Property(e => e.SemesterEnd)
                .HasColumnType("date")
                .HasColumnName("Semester_End");
            entity.Property(e => e.SemesterStart)
                .HasColumnType("date")
                .HasColumnName("Semester_Start");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.SemesterDates)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__SemesterD__UserI__398D8EEE");
        });

        modelBuilder.Entity<StudyRecord>(entity =>
        {
            entity.HasKey(e => e.StudyRecordId).HasName("PK__StudyRec__A3E72390CF615E4F");

            entity.ToTable("StudyRecord");

            entity.Property(e => e.StudyRecordId).HasColumnName("StudyRecordID");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.StudyDates).HasColumnType("date");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Module).WithMany(p => p.StudyRecords)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK__StudyReco__Modul__403A8C7D");

            entity.HasOne(d => d.User).WithMany(p => p.StudyRecords)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__StudyReco__UserI__3F466844");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC8285BCDD");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<ST10079389_Kaushil_Dajee_PROG6212_POE.Models.Notification> Notification { get; set; } = default!;
}
