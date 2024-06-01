using Microsoft.EntityFrameworkCore;
using DataNormalizer.Models;

// Esta clase establece la configuración necesaria para conectarse a una base de datos MySQL 
// y proporciona propiedades DbSet para acceder a las entidades de la base de datos.

public class AppDbContext : DbContext {
    public DbSet<CityModel> ciudades_norm { get; set; }
    public DbSet<FamousModel> fnac_famosos_norm { get; set; }
    public DbSet<UnformattedModel> fnac_famosos { get; set; }

    public DbSet<NormalizedPlace> lugar { get; set; }
    public DbSet<NormalizedAddresses> direcciones { get; set; }
    public DbSet<NormalizedGeoreference> georeferencias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=Fm2dj9107.;database=normDatos;",
        new MySqlServerVersion(new Version(8, 0, 36)));
    }

}



