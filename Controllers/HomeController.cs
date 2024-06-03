using DataNormalizer.Validations;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace DataNormalizer.Controllers;

public class HomeController : Controller {
    private readonly IWebHostEnvironment _hostingEnvironment;
    public DataValidator validator = new DataValidator();
    public DataTransformer transformer = new DataTransformer();

    public HomeController(IWebHostEnvironment hostingEnvironment){
        _hostingEnvironment = hostingEnvironment;
    }

    // Ruta encargada de mostrar la vista de inicio
    public IActionResult Index(){
        return View();
    }

    // Carga la vista para normalizar fechas de nacimiento
    public IActionResult NormalizeBirthDate(IFormFile file){
        if (file != null && file.Length > 0){
            string[] text = DataSaver.fileReader(file,_hostingEnvironment);
            var uniqueNames = new HashSet<(string,string,int,bool)>();
            foreach (string line in text){
                Match match = Regex.Match(line, @"^(.*?)\s*-\s*(.*)$");
                if (match.Success){
                    string name = match.Groups[1].Value;
                    name = Regex.Replace(name, @"^[\d.]*", "");
                    string originalDate = match.Groups[2].Value;
                    DataSaver.SaveFamousData(match.Groups[1].Value,match.Groups[2].Value);
                    string formattedDate = transformer.NormalizeDate(originalDate);
                    int age = validator.CalculateAge(originalDate);
                    bool isBirthday = validator.IsBirthday(originalDate);
                    uniqueNames.Add((name,formattedDate,age,isBirthday));
                    ViewBag.uniqueNames = uniqueNames;
                }
            }

            DataSaver.saveNormalizedFamousData(uniqueNames);

        } else {
            ViewBag.Message = "No se ha seleccionado ningún archivo.";
        }
        
        return View(ViewBag.uniqueNames);
    }
    
    // Carga las vistas relacionadas con la normalizacion de ciudades
    public IActionResult NormalizeCity(IFormFile file){
        if (file != null && file.Length > 0){
            string[] text = DataSaver.fileReader(file,_hostingEnvironment);
            HashSet<string> normalizedCities = new HashSet<string>();

            foreach (string cityName in text){
                string normalizedCityName = transformer.normalizeCityName(cityName);
                normalizedCities.Add(normalizedCityName);
            }

            DataSaver.saveCity(normalizedCities);

            ViewBag.normalizedCities = normalizedCities;
            ViewBag.Message = "El archivo se ha cargado correctamente.";

        }else{
            ViewBag.Message = "No se ha seleccionado ningún archivo.";
        }

        return View();
    }

    // Carga las vistas relacionadas con la normalizacion de ubicaciones geograficas
    public IActionResult NormalizePlaces(IFormFile file){
        var placeSet = new HashSet<(string,string,string,string,string,string,string)>();
        if (file != null && file.Length > 0){
            string[] text = DataSaver.fileReader(file,_hostingEnvironment);
            var formattedText = transformer.SplitGeoreferences(text);
            foreach (var reader in formattedText){
                var normalizedAddress = transformer.normalizeAddress(reader.Item2);
                placeSet.Add((reader.Item1, normalizedAddress[0],normalizedAddress[1],normalizedAddress[2],normalizedAddress[3],reader.Item3,reader.Item4)) ;
            }
            DataSaver.savePlace(placeSet);
            DataSaver.saveAddress(placeSet);
            DataSaver.saveGeoreference(placeSet);
            ViewBag.places = placeSet;
        } else{
            ViewBag.Message = "No se ha seleccionado ningún archivo.";
        }

        return View();
    }
}

