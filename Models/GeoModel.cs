using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class NormalizedGeoreference {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Indica que el valor se generará automáticamente
    public int georeference_id { get; set; }
    public int coordinate { get; set; }
    
}