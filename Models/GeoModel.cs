using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class NormalizedGeoreference {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Indica que el valor se generará automáticamente
    public int georeference_id { get; set; }
    public string? longitud { get; set; }
    public string? latitud { get; set; }
    
}