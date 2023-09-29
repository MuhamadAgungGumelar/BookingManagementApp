using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Principal;

namespace BookingManagementApp.Data;

    public class BookingManagementDbContext : DbContext
    {
    public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options) 
    { }

    // Add Models to migrate
    public DbSet<Accounts> Accounts { get; set; }
    public DbSet<AccountRoles> AccountRoles { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<Rooms> Rooms { get; set; }
    public DbSet<Bookings> Bookings { get; set; }
    public DbSet<Educations> Educations { get; set; }
    public DbSet<Employees> Employees { get; set; }
    public DbSet<Universities> Universities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employees>().HasIndex(e => new {
            e.Nik,
            e.Email,
            e.PhoneNumber
        }).IsUnique();

        // University has many Educations
        modelBuilder.Entity<Universities>()
            .HasMany(e => e.Educations)
            .WithOne(u => u.universities)
            .HasForeignKey(e => e.UniversityGuid);
        /*
            modelBuilder.Entity<Educations>()
                .HasOne(u => u.universities)
                .WithMany(e => e.Educations)
                .HasForeignKey(u => u.UniversityGuid);
        */

        // Educations has one Employees
        modelBuilder.Entity<Educations>()
            .HasOne(em => em.Employees)
            .WithOne(e => e.Educations)
            .HasForeignKey<Educations>(e => e.Guid);

        // Employees has many Bookings
        modelBuilder.Entity<Employees>()
            .HasMany(b => b.Bookings)
            .WithOne(em => em.Employees)
            .HasForeignKey(b => b.EmployeeGuid);

        // Rooms has many Bookings
        modelBuilder.Entity<Rooms>()
            .HasMany(b => b.Bookings)
            .WithOne(r => r.Rooms)
            .HasForeignKey(b => b.RoomGuid);

        //Employess has one Accounts
        modelBuilder.Entity<Employees>()
            .HasOne(a => a.Accounts)
            .WithOne(em => em.Employees)
            .HasForeignKey<Accounts>(a => a.Guid);

        //Accounts has many Accounts Roles
        modelBuilder.Entity<Accounts>()
            .HasMany(ar => ar.AccountRoles)
            .WithOne(a => a.Accounts)
            .HasForeignKey(ar => ar.AccountGuid);

        //Roles has many Accounts roles
        modelBuilder.Entity<Roles>()
            .HasMany(ar => ar.AccountRoles)
            .WithOne(rl => rl.Roles)
            .HasForeignKey(ar => ar.RoleGuid);
    }


}

