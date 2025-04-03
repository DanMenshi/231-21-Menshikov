using System;
using System.Text.RegularExpressions;
using StudentManager.Models;

namespace StudentManager.Utilities
{
    public static class Validators
    {
        public static void ValidateStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Surname)) throw new ArgumentException("Фамилия обязательна");
            if (student.BirthDate < new DateTime(1991, 12, 25) || student.BirthDate > DateTime.Now)
                throw new ArgumentException("Некорректная дата рождения");

            if (!ValidateEmail(student.Email))
                throw new ArgumentException("Некорректный email");

            if (!ValidatePhone(student.Phone))
                throw new ArgumentException("Некорректный телефон");
        }

        private static bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^[a-zA-Z0-9._%+-]{3,}@(yandex.ru|gmail.com|icloud.com)$");
        }

        private static bool ValidatePhone(string phone)
        {
            return Regex.IsMatch(phone,
                @"^\+7\d{3}\d{3}\d{2}\d{2}$");
        }
    }
}