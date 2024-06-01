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
            
            var savingPath = Path.Combine(_hostingEnvironment.WebRootPath, file.FileName);

            using (var stream = new FileStream(savingPath, FileMode.Create)){
                file.CopyTo(stream);
            }

            var names = System.IO.File.ReadAllLines(savingPath);
            System.IO.File.Delete(savingPath);

            var uniqueNames = new HashSet<(string,string,int,bool)>();

            foreach (string line in names){
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

            var savingPath = Path.Combine(_hostingEnvironment.WebRootPath, file.FileName);
            using (var stream = new FileStream(savingPath, FileMode.Create)){
                file.CopyTo(stream);
            }

            var cities = System.IO.File.ReadAllLines(savingPath);

            HashSet<string> normalizedCities = new HashSet<string>();

            foreach (string cityName in cities){
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
    public IActionResult NormalizePlaces(IFormFile uploadedFile){
        if (uploadedFile != null && uploadedFile.Length > 0){

            var savingPath = Path.Combine(_hostingEnvironment.WebRootPath, uploadedFile.FileName);
            using (var stream = new FileStream(savingPath, FileMode.Create)){
                uploadedFile.CopyTo(stream);
            }
            var readingFile = System.IO.File.ReadAllLines(savingPath);
            var placeSet = new HashSet<(string,string,string)>();

            var regex = new Regex(@"^(.*?);(.*?);([-.\d]+),\s*([-.\d]+)$");
            var adressRegex = new Regex(@"^(.*?),\s*(\d+.*?)?,\s*(.*?),\s*(.*)$");

            foreach (var reader in readingFile){
                var match = regex.Match(reader);
            
                if (match.Success){
                    var placeName = match.Groups[1].Value;
                    var adress = match.Groups[2].Value;
                    var matchedAdress = adressRegex.Match(adress);

                    var latitude = match.Groups[3].Value;
                    var longitude = match.Groups[4].Value;

                    placeSet.Add((placeName,latitude,longitude));
                }
            }

            Console.WriteLine(placeSet);

            ViewBag.places = placeSet;
            
        } else {
            ViewBag.Message = "No se ha seleccionado ningún archivo.";
        }

        return View();
    }
}

