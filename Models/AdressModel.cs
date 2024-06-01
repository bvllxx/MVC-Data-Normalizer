using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class NormalizedAddresses{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Indica que el valor se generará automáticamente
    public int address_id { get; set; }
    public string? street_name { get; set; }
    public int street_number { get; set; }
    public string? city_state_prov { get; set; }
    public string? country { get;set; }

}