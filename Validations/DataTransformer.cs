using System.Text.RegularExpressions;

namespace DataNormalizer.Validations;

public class DataTransformer {

    
    public string normalizeCityName(string cityName){
        var normalizedCityName = Regex.Replace(cityName.Normalize(System.Text.NormalizationForm.FormD), @"[\p{M}]", "");
        var finalCityName = Regex.Replace(normalizedCityName.ToLower(), @"[^\w\s\d]", "");
        string textoSinNumeros = Regex.Replace(finalCityName, @"\d", "");
        return textoSinNumeros;
    }

    public string NormalizeDate(string date){   
        string normalizedDate = date.Replace("/", "-"); // Reemplazar barras con guiones
        return DateTime.TryParse(normalizedDate, out DateTime parsedDate) ? parsedDate.ToString("dd-MM-yyyy") : normalizedDate;
    }

    public HashSet<(string,string,string,string)> SplitGeoreferences(string[] places){
        var pattern = new Regex(@"^(.*?);(.*?);([-.\d]+),\s*([-.\d]+)$"); // Patron de busqueda

        var placeSet = new HashSet<(string,string,string,string)>(); // Se almacenara cada linea como una tupla dentro de un hashset
                                                                    //  el hashset por defecto filtra los datos duplicados y los omite
        foreach (string reader in places){ // Se recorre el archivo recibido
            var match = pattern.Match(reader);
            if (match.Success){ // Comprueba si la frase coincide con el patron de busqueda
                var placeName = match.Groups[1].Value;
                var fullAddress = match.Groups[2].Value;
                var latitude = match.Groups[3].Value;
                var longitude = match.Groups[4].Value;
                placeSet.Add((placeName, fullAddress, latitude, longitude)); // 2 Parentesis porque toma solo un argumento, el cual es una tupla
            }
        }
        return placeSet;
    }

    public string[] normalizeAddress(string fullAddress){
        var addressRegex = new Regex(@"^(.*?),\s*(.*?),\s*(.*)$");
        var addressMatch = addressRegex.Match(fullAddress);
        string[] normalizedAdress;

        if (addressMatch.Success){
            var streetNumber = addressMatch.Groups[1].Value;
            var streetName = addressMatch.Groups[2].Success ? addressMatch.Groups[2].Value : string.Empty;
            var cityState = addressMatch.Groups[3].Value;
            var country = addressMatch.Groups[4].Value;
            
            normalizedAdress = [streetName,streetNumber,cityState,country];
            return normalizedAdress;
        } else {
            normalizedAdress = ["","","",""];
            return normalizedAdress;
        }
    }

    
}