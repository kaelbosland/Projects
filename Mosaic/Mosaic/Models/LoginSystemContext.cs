using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Mosaic.Models
{
    public partial class LoginSystemContext : DbContext
    {
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Professor> Professor { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        public LoginSystemContext(DbContextOptions<LoginSystemContext> options) : base(options)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.ClassCode);

                entity.Property(e => e.ClassCode)
                    .HasColumnName("classCode")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ClassName)
                    .HasColumnName("className")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MaxEnroll).HasColumnName("maxEnroll");

                entity.Property(e => e.NumEnrolled).HasColumnName("numEnrolled");

                entity.Property(e => e.ProfessorId)
                .HasColumnName("professorID")
                .HasMaxLength(20)
                .IsUnicode(false);
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ClassOne)
                    .HasColumnName("classOne")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClassOneNavigation)
                    .WithMany(p => p.Professor)
                    .HasForeignKey(d => d.ClassOne)
                    .HasConstraintName("FK__Professor__class__06CD04F7");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ClassOne)
                    .HasColumnName("classOne")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ClassTwo)
                    .HasColumnName("classTwo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClassOneNavigation)
                    .WithMany(p => p.StudentClassOneNavigation)
                    .HasForeignKey(d => d.ClassOne)
                    .HasConstraintName("FK__Student__classOn__04E4BC85");

                entity.HasOne(d => d.ClassTwoNavigation)
                    .WithMany(p => p.StudentClassTwoNavigation)
                    .HasForeignKey(d => d.ClassTwo)
                    .HasConstraintName("FK__Student__classTw__05D8E0BE");
            });
        }
    }
}
