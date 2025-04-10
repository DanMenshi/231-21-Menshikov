using System;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace StudentManager.Models
{
    public class Student
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Surname")]
        public string Surname { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Patronymic")]
        public string Patronymic { get; set; }
        [JsonProperty("Course")]
        public int Course { get; set; }
        [JsonProperty("Group")]
        public string Group { get; set; }

        [JsonProperty("BirthDate")]
        public DateTime BirthDate { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }
    }
}