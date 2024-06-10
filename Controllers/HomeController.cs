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
    public IActionResult NormalizeBirthDate(IFormFile file) {
        if (file != null && file.Length > 0) {
            string[] text = DataSaver.fileReader(file, _hostingEnvironment);
            var uniqueNames = new HashSet<(string, string, int, bool)>();
            
            foreach (string line in text)
            {
                Match match = Regex.Match(line, @"^(.*?)\s*-\s*(.*)$");
                if (match.Success)
                {
                    string name = match.Groups[1].Value;
                    name = Regex.Replace(name, @"^[\d.]*", "");
                    string originalDate = match.Groups[2].Value;
                    string formattedDate = transformer.NormalizeDate(originalDate);
                    int age = validator.CalculateAge(originalDate);
                    bool isBirthday = validator.IsBirthday(originalDate);
                    uniqueNames.Add((name, formattedDate, age, isBirthday));
                }
            }

            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, file.FileName);

            // Convertir las tuplas a una lista de cadenas de texto
            List<string> linesToWrite = uniqueNames
                .Select(t => $"{t.Item1},{t.Item2},{t.Item3},{t.Item4}")
                .ToList();

            // Guardar el contenido normalizado en el archivo original
            System.IO.File.WriteAllLines(filePath, linesToWrite);

            ViewBag.uniqueNames = uniqueNames;
            ViewBag.Message = "El archivo se ha cargado y normalizado correctamente.";
            ViewBag.ShowDataAlert = true;
            ViewBag.FileName = filePath;
        } else {
            ViewBag.Message = "No se ha seleccionado ningún archivo.";
            ViewBag.ShowDataAlert = false;
        }

        return View(ViewBag.uniqueNames);
    }

    
    // Carga las vistas relacionadas con la normalizacion de ciudades
    public IActionResult NormalizeCity(IFormFile file) {
        if (file != null && file.Length > 0){
            string[] text = DataSaver.fileReader(file,_hostingEnvironment);
            HashSet<string> normalizedCities = new HashSet<string>();

            foreach (string cityName in text){
                string normalizedCityName = transformer.normalizeCityName(cityName);
                normalizedCities.Add(normalizedCityName);
            }

            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, file.FileName);

            // Guardar el contenido normalizado en el archivo original
            System.IO.File.WriteAllLines(filePath, normalizedCities);

            ViewBag.normalizedCities = normalizedCities;
            ViewBag.Message = "El archivo se ha cargado correctamente.";
            ViewBag.ShowDataAlert = true;   
            ViewBag.FileName = filePath;

        }else{
            ViewBag.Message = "No se ha seleccionado ningún archivo.";
        }

        return View();
    }

    public IActionResult DownloadFile(string fileName) {
        try{
            if (fileName != null)
            {
                var filePath = fileName; // Aquí pasamos directamente el nombre del archivo, ya que ya tiene la ruta completa.
                var memory = new MemoryStream();

                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    stream.CopyTo(memory);
                }
                memory.Position = 0;
                return File(memory, "text/plain", Path.GetFileName(filePath));
            }
            else
            {   
                Console.WriteLine("NILO!!!");
                return NotFound();
            }
        }catch (Exception e){
            return BadRequest(e.Message);
        }
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
             string filePath = Path.Combine(_hostingEnvironment.WebRootPath, file.FileName);

            // Convertir las tuplas a una lista de cadenas de texto
            List<string> linesToWrite = placeSet
                .Select(t => $"{t.Item1},{t.Item2},{t.Item3},{t.Item4},{t.Item5},{t.Item6},{t.Item7}")
                .ToList();

            // Guardar el contenido normalizado en el archivo original
            System.IO.File.WriteAllLines(filePath, linesToWrite);

            ViewBag.Message = "El archivo se ha cargado y normalizado correctamente.";
            ViewBag.ShowDataAlert = true;
            ViewBag.FileName = filePath;
            ViewBag.places = placeSet;
        } else{
            ViewBag.Message = "No se ha seleccionado ningún archivo.";
        }

        return View();
    }
}

