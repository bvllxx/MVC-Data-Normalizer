using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataNormalizer.Models;


// Esta clase de modelo representa las ciudades como
// una entidad en una base de datos relacional. 

public class CityModel{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Indica que el valor se generará automáticamente
    public int cityID { get; set; }
    public string? cityName { get; set; }
}


// Esta clase de modelo representa las ciudades aun no formateadas
// una entidad en una base de datos relacional. 


public class UnformattedCityModel{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Indica que el valor se generará automáticamente
    public int unforCityID { get; set; }
    public string? unformattedCityName { get; set; }
}
