using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Q1.Models
{
    public partial class LibraryDBContext : DbContext
    {
        public LibraryDBContext()
        {
        }

        public LibraryDBContext(DbContextOptions<LibraryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookCopy> BookCopies { get; set; } = null!;
        public virtual DbSet<BorrowTransaction> BorrowTransactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server= LAPTOP-PP10CJUG\\SQLEXPRESS02; database = LibraryDB;uid=sa;pwd=1234; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.Property(e => e.Bio).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.Publisher).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasMany(d => d.Authors)
                    .WithMany(p => p.Books)
                    .UsingEntity<Dictionary<string, object>>(
                        "BookAuthor",
                        l => l.HasOne<Author>().WithMany().HasForeignKey("AuthorId").HasConstraintName("FK__BookAutho__Autho__3B75D760"),
                        r => r.HasOne<Book>().WithMany().HasForeignKey("BookId").HasConstraintName("FK__BookAutho__BookI__3A81B327"),
                        j =>
                        {
                            j.HasKey("BookId", "AuthorId").HasName("PK__BookAuth__6AED6DE6696967FB");

                            j.ToTable("BookAuthors");

                            j.IndexerProperty<int>("BookId").HasColumnName("BookID");

                            j.IndexerProperty<int>("AuthorId").HasColumnName("AuthorID");
                        });
            });

            modelBuilder.Entity<BookCopy>(entity =>
            {
                entity.HasKey(e => e.CopyId)
                    .HasName("PK__BookCopi__C26CCCE50D77C980");

                entity.Property(e => e.CopyId).HasColumnName("CopyID");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.Condition).HasMaxLength(50);

                entity.Property(e => e.Isbn)
                    .HasMaxLength(20)
                    .HasColumnName("ISBN");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookCopies)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__BookCopie__BookI__3E52440B");
            });

            modelBuilder.Entity<BorrowTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__BorrowTr__55433A4BC4D341B7");

                entity.ToTable("BorrowTransaction");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.BorrowDate).HasColumnType("date");

                entity.Property(e => e.CopyId).HasColumnName("CopyID");

                entity.Property(e => e.ReturnDate).HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Copy)
                    .WithMany(p => p.BorrowTransactions)
                    .HasForeignKey(d => d.CopyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__BorrowTra__CopyI__44FF419A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BorrowTransactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__BorrowTra__UserI__440B1D61");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__A9D1053452445E70")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.MembershipDate).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
