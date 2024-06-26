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

    public static void savePlace(HashSet<(string, string, string, string, string, string, string)> normalizedPlaces){
        var places = normalizedPlaces.Select(name => new NormalizedPlace { place_name = name.Item1 }).ToList();
        SaveToDatabase(places);
    }

    public static void saveAddress(HashSet<(string, string, string, string, string, string, string)> normalizedAddresses){
        var addresses = normalizedAddresses.Select(name => new NormalizedAddresses 
        { 
            street_name = name.Item2,
            street_number = name.Item3,
            city_state_prov = name.Item4
        }).ToList();
        SaveToDatabase(addresses);
    }

    public static void saveGeoreference(HashSet<(string, string, string, string, string, string, string)> normalizedGeoreference){
        var georeferences = normalizedGeoreference.Select(name => new NormalizedGeoreference 
        { 
            longitud = name.Item6,
            latitud = name.Item7,
        }).ToList();
        SaveToDatabase(georeferences);
    }

    // Este metodo se encarga de leer el contenido de un archivo

    public static string[] fileReader(IFormFile file,IWebHostEnvironment _hostingEnvironment){
        var savingPath = Path.Combine(_hostingEnvironment.WebRootPath, file.FileName);
        using (var stream = new FileStream(savingPath, FileMode.Create)){
            file.CopyTo(stream);
        }
        var text = System.IO.File.ReadAllLines(savingPath);
        return text;
    }


}