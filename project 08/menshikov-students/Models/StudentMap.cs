
using CsvHelper.Configuration;
using StudentManager.Models;

public sealed class StudentMap : ClassMap<Student>
{
    public StudentMap()
    {
        Map(m => m.Id).Name("Номер");
        Map(m => m.Surname).Name("Фамилия");
        Map(m => m.Name).Name("Имя");
        Map(m => m.Patronymic).Name("Отчество");
        Map(m => m.Course).Name("Курс");
        Map(m => m.Group).Name("Группа");
        Map(m => m.BirthDate).Name("Дата_рождения").TypeConverterOption.Format("dd.MM.yyyy");
        Map(m => m.Email).Name("Email");
        Map(m => m.Phone).Name("Телефон");
    }
}