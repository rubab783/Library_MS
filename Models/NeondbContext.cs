using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LMS.Models;

public partial class NeondbContext : DbContext
{
    public NeondbContext()
    {
    }

    public NeondbContext(DbContextOptions<NeondbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BorrowRecord> BorrowRecords { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-steep-resonance-a4uifzdp-pooler.us-east-1.aws.neon.tech;Database=neondb;Username=neondb_owner;Password=npg_rxy4gczYe9sR");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Authorid).HasName("authors_pkey");

            entity.ToTable("authors");

            entity.Property(e => e.Authorid)
                .ValueGeneratedNever()
                .HasColumnName("authorid");
            entity.Property(e => e.Authorname)
                .HasMaxLength(255)
                .HasColumnName("authorname");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Bookid).HasName("books_pkey");

            entity.ToTable("books");

            entity.Property(e => e.Bookid)
                .ValueGeneratedNever()
                .HasColumnName("bookid");
            entity.Property(e => e.Authorid).HasColumnName("authorid");
            entity.Property(e => e.Genre)
                .HasMaxLength(100)
                .HasColumnName("genre");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("isbn");
            entity.Property(e => e.Publishedyear).HasColumnName("publishedyear");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.Authorid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("books_authorid_fkey");
        });

        modelBuilder.Entity<BorrowRecord>(entity =>
        {
            entity.HasKey(e => e.Recordid).HasName("borrow_records_pkey");

            entity.ToTable("borrow_records");

            entity.Property(e => e.Recordid)
                .ValueGeneratedNever()
                .HasColumnName("recordid");
            entity.Property(e => e.Bookid).HasColumnName("bookid");
            entity.Property(e => e.Borrowdate).HasColumnName("borrowdate");
            entity.Property(e => e.Memberid).HasColumnName("memberid");
            entity.Property(e => e.Returndate).HasColumnName("returndate");

            entity.HasOne(d => d.Book).WithMany(p => p.BorrowRecords)
                .HasForeignKey(d => d.Bookid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("borrow_records_bookid_fkey");

            entity.HasOne(d => d.Member).WithMany(p => p.BorrowRecords)
                .HasForeignKey(d => d.Memberid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("borrow_records_memberid_fkey");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Memberid).HasName("members_pkey");

            entity.ToTable("members");

            entity.HasIndex(e => e.Email, "members_email_key").IsUnique();

            entity.Property(e => e.Memberid)
                .ValueGeneratedNever()
                .HasColumnName("memberid");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Joindate).HasColumnName("joindate");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
