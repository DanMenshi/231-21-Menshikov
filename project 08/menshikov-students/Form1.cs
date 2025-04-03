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
        // �������� � ������ ������
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
            dataGridView.ReadOnly = true; // ������ ��������������
            dataGridView.AllowUserToAddRows = false; // ������ ���������� �����
            dataGridView.AllowUserToDeleteRows = false; // ������ �������� �����
            dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically; // �������������� ������ ����� ���

            // �������������� ���������
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // ��������� ���� ������
            dataGridView.MultiSelect = false; // ������ �������������� ������
            dataGridView.DataSource = students;
            dataGridView.AutoGenerateColumns = false;

            // ��������� ������� ID
            //dataGridView.Columns["Id"].DefaultCellStyle.Format = "D";
            //dataGridView.Columns["Id"].ReadOnly = true;

            // ���������, ��� ��� ������� ��������� ���������
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.DataPropertyName = column.HeaderText switch
                {
                    "�����" => nameof(Student.Id),
                    "�������" => nameof(Student.Surname),
                    "���" => nameof(Student.Name),
                    "��������" => nameof(Student.Patronymic),
                    "����" => nameof(Student.Course),
                    "������" => nameof(Student.Group),
                    "���� ��������" => nameof(Student.BirthDate),
                    "����������� �����" => nameof(Student.Email),
                    "�������" => nameof(Student.Phone),
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
                    MessageBox.Show("������� �����������!", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSurname.Focus();
                    return;
                }

                if (!int.TryParse(txtCourse.Text, out int course))
                {
                    MessageBox.Show("������������ ����! ������� �����.", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (course < 1 || course > 4)
                {
                    MessageBox.Show("���� ������ ���� �� 1 �� 4!", "������",
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

                Validators.ValidateStudent(student); // ���������
                students.Add(student); // ���������� � ������
                hasUnsavedChanges = true; // ��������� �� ���������

                // ������� ����� (�����������)
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
                MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. �������� ������� ������ ��� ����������
                if (students == null || students.Count == 0)
                {
                    MessageBox.Show("��� ������ ��� ����������!", "����������",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 2. �������� ���������� ��� �������������
                var directory = Path.GetDirectoryName(savePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    Console.WriteLine($"������� ����������: {directory}");
                }

                // 3. ������������ ������
                var jsonData = JsonConvert.SerializeObject(students.ToList(), Formatting.Indented);

                // 4. ������ � ���� � ���������
                File.WriteAllText(savePath, jsonData, Encoding.UTF8);

                // 5. ���������� �������
                hasUnsavedChanges = false;

                // 6. ����������� ������������
                MessageBox.Show($"������ ������� ��������� �:\n{savePath}", "����������",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 7. ����������� ������
                Console.WriteLine($"�������� ���������� � {DateTime.Now}");
            }
            catch (Exception ex)
            {
                // ��������� ����������� ������
                string errorDetails = $"��� ������: {ex.GetType().Name}\n" +
                                      $"���������: {ex.Message}\n" +
                                      $"���� �������: {ex.StackTrace}";

                MessageBox.Show($"������ ����������:\n{errorDetails}", "����������� ������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // ����������� ������
                File.AppendAllText("error.log", $"[{DateTime.Now}] ERROR: {errorDetails}\n");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {


            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("�������� �������� ��� ��������!", "������",
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
                MessageBox.Show("������ ��������� ����!", "����������",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ���������� �� ������� � �����
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
                MessageBox.Show("������ ��������� ����!", "����������",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("�������� �������� ��� ��������������!", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var student = dataGridView.CurrentRow.DataBoundItem as Student;
            if (student != null)
            {
                // ��������� ���� ����� ������� ���������� ��������
                txtSurname.Text = student.Surname;
                txtName.Text = student.Name;
                txtPatronymic.Text = student.Patronymic;
                txtCourse.Text = student.Course.ToString();
                txtGroup.Text = student.Group;
                BirthDate.Value = student.BirthDate;
                txtEmail.Text = student.Email;
                txtPhone.Text = student.Phone;

                // ������� ������ ������
                students.Remove(student);
                hasUnsavedChanges = true;
            }

        }
        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            try
            {
                // �������� ������������� ���������
                if (hasUnsavedChanges)
                {
                    var result = MessageBox.Show("��������� ������� ��������� ����� ��������� ������ �����?",
                        "������������� ���������",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes) btnSave_Click(sender, e);
                    else if (result == DialogResult.Cancel) return;
                }

                using (OpenFileDialog openDialog = new OpenFileDialog())
                {
                    openDialog.Filter = "JSON files|*.json|All files|*.*";
                    openDialog.Title = "�������� ���� ������ ���������";
                    openDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    if (openDialog.ShowDialog() == DialogResult.OK)
                    {
                        // ������ � �������������� �����
                        string json = File.ReadAllText(openDialog.FileName, Encoding.UTF8);
                        List<Student> loadedStudents = JsonConvert.DeserializeObject<List<Student>>(json);

                        // ���������� ������
                        students.Clear();
                        foreach (var student in loadedStudents)
                        {
                            students.Add(student);
                        }

                        // ���������� ����������
                        dataGridView.Refresh();
                        hasUnsavedChanges = false;
                        MessageBox.Show($"������� ��������� {loadedStudents.Count} �������", "�������� �����",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show($"������ ������� �����: {jsonEx.Message}", "������ JSON",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� �������� �����: {ex.Message}", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void toolStripButtonSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    // ��������� �������
                    saveDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    saveDialog.FilterIndex = 1;
                    saveDialog.RestoreDirectory = true;
                    saveDialog.Title = "��������� ���� ���...";
                    saveDialog.DefaultExt = "json";
                    saveDialog.AddExtension = true;
                    saveDialog.OverwritePrompt = true;
                    saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    // �������� ������
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        // ��������� ����������
                        SaveToFile(saveDialog.FileName);

                        // �������� ������� ����
                        currentFilePath = saveDialog.FileName;
                        hasUnsavedChanges = false;

                        MessageBox.Show($"���� ������� �������:\n{currentFilePath}",
                                      "����������",
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
                // �������� ������� ������
                if (students == null || students.Count == 0)
                {
                    throw new InvalidOperationException("��� ������ ��� ����������");
                }

                // ������� ���������� ���� �����
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // ������������ ������
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    DateFormatString = "yyyy-MM-dd"
                };

                string json = JsonConvert.SerializeObject(students, settings);

                // ������ � ����
                File.WriteAllText(filePath, json, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new Exception("������ ���������� �����", ex);
            }
        }

        private void HandleSaveError(Exception ex)
        {
            string errorMessage = $"������ ����������:\n{ex.Message}";

            if (ex.InnerException != null)
            {
                errorMessage += $"\n������: {ex.InnerException.Message}";
            }

            MessageBox.Show(errorMessage,
                          "������ ����������",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);

            // ����������� ������
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

        // ����������� ������ ��� ������ � CSV
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

                    // ������� � ��������� ����� ������
                    students.Clear();
                    foreach (var student in imported)
                    {
                        students.Add(student);
                    }

                    hasUnsavedChanges = true;
                    MessageBox.Show($"������������� {imported.Count} �������", "������",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"������ �������: {ex.Message}", "������",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (students.Count == 0)
            {
                MessageBox.Show("��� ������ ��� ��������", "����������",
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
                    MessageBox.Show($"�������������� {students.Count} �������", "�������",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"������ ��������: {ex.Message}", "������",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}