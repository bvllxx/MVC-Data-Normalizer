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

    public string NormalizePlaces(string place){
        var regex = new Regex(@"^(.*?);(.*?);([-.\d]+),\s*([-.\d]+)$");
        return "a";
    }
}