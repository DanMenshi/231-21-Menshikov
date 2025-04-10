using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using CsvHelper.Configuration;
using CsvHelper;
using Newtonsoft.Json;
using System.Windows.Forms;
using StudentManager.Models;
using System.IO;
using StudentManager.Utilities;
using System.Linq;



namespace StudentManager52
{
    public partial class Form1 : Form
    {
        private BindingList<Student> students = new BindingList<Student>();
        private bool hasUnsavedChanges = false;
        // Добавьте в начало класса
        private readonly string savePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "StudentManager",
            "students.json");
        public Form1()
        {
            InitializeComponent();
            ConfigureDataGridView();

        }
        private string currentFilePath;
        private int GetNextId()
        {
            return students.Any()
                ? students.Max(s => s.Id) + 1
                : 1;
        }

        private void ConfigureDataGridView()
        {
            dataGridView.ReadOnly = true; // Запрет редактирования
            dataGridView.AllowUserToAddRows = false; // Запрет добавления строк
            dataGridView.AllowUserToDeleteRows = false; // Запрет удаления строк
            dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically; // Редактирование только через код

            // Дополнительные настройки
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Выделение всей строки
            dataGridView.MultiSelect = false; // Запрет множественного выбора
            dataGridView.DataSource = students;
            dataGridView.AutoGenerateColumns = false;

            // Настройка формата ID
            //dataGridView.Columns["Id"].DefaultCellStyle.Format = "D";
            //dataGridView.Columns["Id"].ReadOnly = true;

            // Убедитесь, что все колонки правильно привязаны
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.DataPropertyName = column.HeaderText switch
                {
                    "Номер" => nameof(Student.Id),
                    "Фамилия" => nameof(Student.Surname),
                    "Имя" => nameof(Student.Name),
                    "Отчество" => nameof(Student.Patronymic),
                    "Курс" => nameof(Student.Course),
                    "Группа" => nameof(Student.Group),
                    "Дата рождения" => nameof(Student.BirthDate),
                    "Электронная почта" => nameof(Student.Email),
                    "Телефон" => nameof(Student.Phone),
                    _ => column.DataPropertyName
                };
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSurname.Text))
                {
                    MessageBox.Show("Фамилия обязательна!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSurname.Focus();
                    return;
                }

                if (!int.TryParse(txtCourse.Text, out int course))
                {
                    MessageBox.Show("Некорректный курс! Введите число.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (course < 1 || course > 4)
                {
                    MessageBox.Show("Курс должен быть от 1 до 4!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var student = new Student
                {
                    Id = GetNextId(),
                    Surname = txtSurname.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    Patronymic = txtPatronymic.Text.Trim(),
                    Course = course,
                    Group = txtGroup.Text.Trim(),
                    BirthDate = BirthDate.Value,
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim()
                };

                Validators.ValidateStudent(student); // Валидация
                students.Add(student); // Добавление в список
                hasUnsavedChanges = true; // Изменения не сохранены

                // Очистка полей (опционально)
                txtSurname.Clear();
                txtName.Clear();
                txtPatronymic.Clear();
                txtCourse.Clear();
                txtGroup.Clear();
                txtEmail.Clear();
                txtPhone.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Проверка наличия данных для сохранения
                if (students == null || students.Count == 0)
                {
                    MessageBox.Show("Нет данных для сохранения!", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 2. Создание директории при необходимости
                var directory = Path.GetDirectoryName(savePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    Console.WriteLine($"Создана директория: {directory}");
                }

                // 3. Сериализация данных
                var jsonData = JsonConvert.SerializeObject(students.ToList(), Formatting.Indented);

                // 4. Запись в файл с проверкой
                File.WriteAllText(savePath, jsonData, Encoding.UTF8);

                // 5. Обновление статуса
                hasUnsavedChanges = false;

                // 6. Уведомление пользователя
                MessageBox.Show($"Данные успешно сохранены в:\n{savePath}", "Сохранение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 7. Логирование успеха
                Console.WriteLine($"Успешное сохранение в {DateTime.Now}");
            }
            catch (Exception ex)
            {
                // Подробная диагностика ошибок
                string errorDetails = $"Тип ошибки: {ex.GetType().Name}\n" +
                                      $"Сообщение: {ex.Message}\n" +
                                      $"Стек вызовов: {ex.StackTrace}";

                MessageBox.Show($"Ошибка сохранения:\n{errorDetails}", "Критическая ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Логирование ошибки
                File.AppendAllText("error.log", $"[{DateTime.Now}] ERROR: {errorDetails}\n");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {


            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Выберите студента для удаления!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var student = dataGridView.CurrentRow.DataBoundItem as Student;
            if (student != null)
            {
                students.Remove(student);
                hasUnsavedChanges = true;
            }

        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            if (students.Count == 0)
            {
                MessageBox.Show("Список студентов пуст!", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Сортировка по фамилии и имени
            var sorted = new BindingList<Student>(
                students.OrderBy(s => s.Surname)
                        .ThenBy(s => s.Name)
                        .ToList());

            students.Clear();
            foreach (var student in sorted)
            {
                students.Add(student);
            }

            dataGridView.Refresh();
            hasUnsavedChanges = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (students.Count == 0)
            {
                MessageBox.Show("Список студентов пуст!", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Выберите студента для редактирования!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var student = dataGridView.CurrentRow.DataBoundItem as Student;
            if (student != null)
            {
                // Заполняем поля формы данными выбранного студента
                txtSurname.Text = student.Surname;
                txtName.Text = student.Name;
                txtPatronymic.Text = student.Patronymic;
                txtCourse.Text = student.Course.ToString();
                txtGroup.Text = student.Group;
                BirthDate.Value = student.BirthDate;
                txtEmail.Text = student.Email;
                txtPhone.Text = student.Phone;

                // Удаляем старую запись
                students.Remove(student);
                hasUnsavedChanges = true;
            }

        }
        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверка несохраненных изменений
                if (hasUnsavedChanges)
                {
                    var result = MessageBox.Show("Сохранить текущие изменения перед открытием нового файла?",
                        "Несохраненные изменения",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes) btnSave_Click(sender, e);
                    else if (result == DialogResult.Cancel) return;
                }

                using (OpenFileDialog openDialog = new OpenFileDialog())
                {
                    openDialog.Filter = "JSON files|*.json|All files|*.*";
                    openDialog.Title = "Выберите файл данных студентов";
                    openDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    if (openDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Чтение и десериализация файла
                        string json = File.ReadAllText(openDialog.FileName, Encoding.UTF8);
                        List<Student> loadedStudents = JsonConvert.DeserializeObject<List<Student>>(json);

                        // Обновление данных
                        students.Clear();
                        foreach (var student in loadedStudents)
                        {
                            students.Add(student);
                        }

                        // Обновление интерфейса
                        dataGridView.Refresh();
                        hasUnsavedChanges = false;
                        MessageBox.Show($"Успешно загружено {loadedStudents.Count} записей", "Открытие файла",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show($"Ошибка формата файла: {jsonEx.Message}", "Ошибка JSON",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void toolStripButtonSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    // Настройка диалога
                    saveDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    saveDialog.FilterIndex = 1;
                    saveDialog.RestoreDirectory = true;
                    saveDialog.Title = "Сохранить файл как...";
                    saveDialog.DefaultExt = "json";
                    saveDialog.AddExtension = true;
                    saveDialog.OverwritePrompt = true;
                    saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    // Показать диалог
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Выполнить сохранение
                        SaveToFile(saveDialog.FileName);

                        // Обновить текущий путь
                        currentFilePath = saveDialog.FileName;
                        hasUnsavedChanges = false;

                        MessageBox.Show($"Файл успешно сохранён:\n{currentFilePath}",
                                      "Сохранение",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleSaveError(ex);
            }
        }

        private void SaveToFile(string filePath)
        {
            try
            {
                // Проверка входных данных
                if (students == null || students.Count == 0)
                {
                    throw new InvalidOperationException("Нет данных для сохранения");
                }

                // Создать директорию если нужно
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // Сериализация данных
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    DateFormatString = "yyyy-MM-dd"
                };

                string json = JsonConvert.SerializeObject(students, settings);

                // Запись в файл
                File.WriteAllText(filePath, json, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка сохранения файла", ex);
            }
        }

        private void HandleSaveError(Exception ex)
        {
            string errorMessage = $"Ошибка сохранения:\n{ex.Message}";

            if (ex.InnerException != null)
            {
                errorMessage += $"\nДетали: {ex.InnerException.Message}";
            }

            MessageBox.Show(errorMessage,
                          "Ошибка сохранения",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);

            // Логирование ошибки
            Debug.WriteLine($"[{DateTime.Now}] Save Error: {ex}");
        }

        public class CsvDataService
        {
            public List<Student> ImportFromCsv(string filePath)
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    MissingFieldFound = null,
                    HeaderValidated = null
                };

                using var reader = new StreamReader(filePath);
                using var csv = new CsvReader(reader, config);
                return csv.GetRecords<Student>().ToList();
            }

            public void ExportToCsv(string filePath, IEnumerable<Student> students)
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";"
                };

                using var writer = new StreamWriter(filePath);
                using var csv = new CsvWriter(writer, config);
                csv.WriteRecords(students);
            }
        }

        // Обновленные методы для работы с CSV
        private void ImportButton_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var service = new CsvDataService();
                    var imported = service.ImportFromCsv(openFileDialog.FileName);

                    // Очищаем и добавляем новые данные
                    students.Clear();
                    foreach (var student in imported)
                    {
                        students.Add(student);
                    }

                    hasUnsavedChanges = true;
                    MessageBox.Show($"Импортировано {imported.Count} записей", "Импорт",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка импорта: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (students.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var service = new CsvDataService();
                    service.ExportToCsv(saveFileDialog.FileName, students.ToList());
                    MessageBox.Show($"Экспортировано {students.Count} записей", "Экспорт",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}