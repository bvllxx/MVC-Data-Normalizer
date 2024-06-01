using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class NormalizedPlace{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Indica que el valor se generará automáticamente
    public int place_id { get; set; }
    public string? place_name { get; set; }
}