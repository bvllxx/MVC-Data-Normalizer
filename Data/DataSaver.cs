using DataNormalizer.Models;

public class DataSaver {

    // Este metodo es el encargado de guardar los datos en la base de datos relacionañ
    public static void SaveToDatabase<TEntity>(IEnumerable<TEntity> entities) where TEntity : class{
        using (var dbContext = new AppDbContext()){
            dbContext.Set<TEntity>().AddRange(entities);
            dbContext.SaveChanges();
        }
    }

    // Este metodo esta encargado de crear una instancia del modelo CItyModel
    // con los datos proporcionados desde el archivo para ser insertados en la base de datos
   
    public static void saveCity(HashSet<string> normalizedCities){
        foreach (string name in normalizedCities){
            var cities = normalizedCities.Select(name => new CityModel { cityName = name });
            SaveToDatabase(cities);
        }
    }

    // Este metodo esta encargado de crear una instancia del modelo UnformattedMOdel
    // para datos aun no normalizados,para ser insertados en la base de datos

    public static void SaveFamousData(string g1, string g2){
        var newFamous = new UnformattedModel{
            unformName = g1,
            unformBirthDate = g2
        };
        SaveToDatabase(new[] { newFamous });
    }

    // Este metodo esta encargado de hacer lo mismo que los otros 2 metodos anteriores pero para datos normalizados

    public static void saveNormalizedFamousData(HashSet<(string,string,int,bool)> uniqueNames){
        foreach (var name in uniqueNames){
            var formattedFamousList = uniqueNames.Select(name =>
            new FamousModel{
                famousName = name.Item1,
                formattedBirthDate = name.Item2,
                age = name.Item3,
                isBirthday = name.Item4
            });
            SaveToDatabase(formattedFamousList);
        }
    }
}