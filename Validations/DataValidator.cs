namespace DataNormalizer.Validations;

public class DataValidator {
    public int CalculateAge(string birthDate){
        if (DateTime.TryParse(birthDate, out DateTime parsedBirthDate)){
            DateTime today = DateTime.Today;
            int age = today.Year - parsedBirthDate.Year;
            if (parsedBirthDate > today.AddYears(-age)){
                age--;
            }
            return age;
        }
        return -1;
    }
    public bool IsBirthday(string birthDate){
        return DateTime.TryParse(birthDate, out DateTime parsedBirthDate) && parsedBirthDate.Month == DateTime.Today.Month && parsedBirthDate.Day == DateTime.Today.Day;
    }
}