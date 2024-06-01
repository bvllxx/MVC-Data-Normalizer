using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataNormalizer.Models;


// Este modelo tiene la funcion de representar los datos de famosos 
// ya normalizados en la base de datos

public class FamousModel{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int famousID { get; set; }
    public string? famousName { get; set; }
    public string? formattedBirthDate { get; set; }
    public int age { get; set; }
    public bool isBirthday { get; set; }

}


// Esta clase representa los datos aun no formateados

public class UnformattedModel {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int unformFamousID { get; set; }
    public string? unformName { get; set;}
    public string? unformBirthDate { get; set;}
}