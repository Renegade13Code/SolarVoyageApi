using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SolarVoyage.Core.Models.Domain;

namespace SolarVoyage.Core.Data;
//Auto-generate this context with: dotnet ef dbcontext scaffold --project SolarVoyage.Core --output-dir Models/Domain "Connection_String" Npgsql.EntityFrameworkCore.PostgreSQL
public partial class SolarVoyageContext : DbContext
{
    private readonly IConfiguration _configuration;

    public SolarVoyageContext()
    {
    }

    public SolarVoyageContext(DbContextOptions<SolarVoyageContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<CargoItem> CargoItems { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Ship> Ships { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("SolarVoyageDb"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CargoItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CargoItems_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.FlightId).HasColumnName("flight_id");
            entity.Property(e => e.LastUpdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.Status)
                .HasComment("Current status of cargo e.g. checked-in/on-board ect.")
                .HasColumnType("character varying")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Flight).WithMany(p => p.CargoItems)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("CargoItems_flight_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.CargoItems)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("CargoItems_user_id_fkey");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Flights_pkey");

            entity.ToTable(tb => tb.HasComment("Flights can potentialy have refuleing stops across the solar system"));

            entity.HasIndex(e => e.FlightNumber, "Flights_flight_number_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ArrivalTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("arrival_time");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DepartureTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("departure_time");
            entity.Property(e => e.FlightNumber)
                .HasColumnType("character varying")
                .HasColumnName("flight_number");
            entity.Property(e => e.LastUpdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.LaunchpadArrival)
                .HasColumnType("character varying")
                .HasColumnName("launchpad_arrival");
            entity.Property(e => e.LaunchpadDepature)
                .HasComment("The location of the launchpad used for departure")
                .HasColumnType("character varying")
                .HasColumnName("launchpad_depature");
            entity.Property(e => e.ShipId).HasColumnName("ship_id");

            entity.HasOne(d => d.Ship).WithMany(p => p.Flights)
                .HasForeignKey(d => d.ShipId)
                .HasConstraintName("Flights_ship_id_fkey");
        });

        modelBuilder.Entity<Ship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Ships_pkey");

            entity.HasIndex(e => e.Name, "Ships_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Acceleration).HasColumnName("acceleration");
            entity.Property(e => e.CargoCapacity)
                .HasColumnType("character varying")
                .HasColumnName("cargo_capacity");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.LastUpdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.PersonalCapacity)
                .HasColumnType("character varying")
                .HasColumnName("personal_capacity");
            entity.Property(e => e.Range).HasColumnName("range");
            entity.Property(e => e.TopSpeed).HasColumnName("top_speed");
            entity.Property(e => e.Weight).HasColumnName("weight");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tickets_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.FlightId).HasColumnName("flight_id");
            entity.Property(e => e.LastUpdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Flight).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("Tickets_flight_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Tickets_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.LastUpdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
