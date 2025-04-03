using System;
using System.Drawing;
using System.Windows.Forms;
    
namespace StudentManager52
{
    partial class Form1
    {   

        private DataGridView dataGridView;
        private TextBox txtSurname;
        private TextBox txtName;
        private TextBox txtPatronymic;
        private System.Windows.Forms.DateTimePicker BirthDate;
        private TextBox txtCourse;
        private TextBox txtGroup;
        private TextBox txtEmail;
        private TextBox txtPhone;

        private Button btnAdd;

        private Label labelSurname;
        private Label labelName;
        private Label labelPatronymic;
        private Label labelCourse;
        private Label labelGroup;
        private Label labelEmail;
        private Label labelPhone;
        private Label labelBirthDate;

        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dataGridView = new DataGridView();
            btnAdd = new Button();
            txtSurname = new TextBox();
            labelSurname = new Label();
            txtName = new TextBox();
            labelName = new Label();
            txtPatronymic = new TextBox();
            labelPatronymic = new Label();
            BirthDate = new DateTimePicker();
            labelBirthDate = new Label();
            txtCourse = new TextBox();
            labelCourse = new Label();
            txtGroup = new TextBox();
            labelGroup = new Label();
            txtEmail = new TextBox();
            labelEmail = new Label();
            txtPhone = new TextBox();
            labelPhone = new Label();
            toolStrip1 = new ToolStrip();
            fileManager = new ToolStripDropDownButton();
            toolStripButton1 = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            toolStripButton3 = new ToolStripButton();
            toolStripMenuItemCSV = new ToolStripMenuItem();
            toolStripButton7 = new ToolStripButton();
            toolStripButton8 = new ToolStripButton();
            editManager = new ToolStripDropDownButton();
            toolStripButton4 = new ToolStripButton();
            toolStripButton5 = new ToolStripButton();
            toolStripButton6 = new ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeight = 46;
            dataGridView.Location = new Point(13, 28);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 82;
            dataGridView.Size = new Size(960, 300);
            dataGridView.TabIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(650, 400);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(261, 36);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Добавить";
            btnAdd.Click += btnAdd_Click;
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(160, 350);
            txtSurname.Name = "txtSurname";
            txtSurname.Size = new Size(100, 23);
            txtSurname.TabIndex = 0;
            // 
            // labelSurname
            // 
            labelSurname.Location = new Point(85, 350);
            labelSurname.Name = "labelSurname";
            labelSurname.Size = new Size(100, 23);
            labelSurname.TabIndex = 1;
            labelSurname.Text = "Фамилия:";
            // 
            // txtName
            // 
            txtName.Location = new Point(160, 380);
            txtName.Name = "txtName";
            txtName.Size = new Size(100, 23);
            txtName.TabIndex = 1;
            // 
            // labelName
            // 
            labelName.Location = new Point(85, 380);
            labelName.Name = "labelName";
            labelName.Size = new Size(100, 23);
            labelName.TabIndex = 2;
            labelName.Text = "Имя:";
            // 
            // txtPatronymic
            // 
            txtPatronymic.Location = new Point(160, 410);
            txtPatronymic.Name = "txtPatronymic";
            txtPatronymic.Size = new Size(100, 23);
            txtPatronymic.TabIndex = 2;
            // 
            // labelPatronymic
            // 
            labelPatronymic.Location = new Point(85, 410);
            labelPatronymic.Name = "labelPatronymic";
            labelPatronymic.Size = new Size(100, 23);
            labelPatronymic.TabIndex = 3;
            labelPatronymic.Text = "Отчество:";
            // 
            // BirthDate
            // 
            BirthDate.Location = new Point(160, 440);
            BirthDate.Name = "BirthDate";
            BirthDate.Size = new Size(130, 23);
            BirthDate.TabIndex = 3;
            // 
            // labelBirthDate
            // 
            labelBirthDate.Location = new Point(60, 440);
            labelBirthDate.Name = "labelBirthDate";
            labelBirthDate.Size = new Size(100, 23);
            labelBirthDate.TabIndex = 4;
            labelBirthDate.Text = "День рождения:";
            // 
            // txtCourse
            // 
            txtCourse.Location = new Point(400, 350);
            txtCourse.Name = "txtCourse";
            txtCourse.Size = new Size(100, 23);
            txtCourse.TabIndex = 4;
            // 
            // labelCourse
            // 
            labelCourse.Location = new Point(350, 350);
            labelCourse.Name = "labelCourse";
            labelCourse.Size = new Size(100, 23);
            labelCourse.TabIndex = 5;
            labelCourse.Text = "Курс:";
            // 
            // txtGroup
            // 
            txtGroup.Location = new Point(400, 380);
            txtGroup.Name = "txtGroup";
            txtGroup.Size = new Size(100, 23);
            txtGroup.TabIndex = 5;
            // 
            // labelGroup
            // 
            labelGroup.Location = new Point(350, 380);
            labelGroup.Name = "labelGroup";
            labelGroup.Size = new Size(100, 23);
            labelGroup.TabIndex = 6;
            labelGroup.Text = "Группа:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(400, 410);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(100, 23);
            txtEmail.TabIndex = 6;
            // 
            // labelEmail
            // 
            labelEmail.Location = new Point(350, 410);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(100, 23);
            labelEmail.TabIndex = 7;
            labelEmail.Text = "Email:";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(400, 440);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(100, 23);
            txtPhone.TabIndex = 7;
            // 
            // labelPhone
            // 
            labelPhone.Location = new Point(350, 440);
            labelPhone.Name = "labelPhone";
            labelPhone.Size = new Size(100, 23);
            labelPhone.TabIndex = 8;
            labelPhone.Text = "Телефон:";
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = SystemColors.Menu;
            toolStrip1.Items.AddRange(new ToolStripItem[] { fileManager, editManager });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(985, 25);
            toolStrip1.TabIndex = 9;
            // 
            // fileManager
            // 
            fileManager.DisplayStyle = ToolStripItemDisplayStyle.Text;
            fileManager.DropDownItems.AddRange(new ToolStripItem[] { toolStripButton1, toolStripButton2, toolStripButton3, toolStripMenuItemCSV });
            fileManager.Image = (Image)resources.GetObject("fileManager.Image");
            fileManager.ImageTransparentColor = Color.Magenta;
            fileManager.Name = "fileManager";
            fileManager.Size = new Size(49, 22);
            fileManager.Text = "Файл";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(70, 19);
            toolStripButton1.Text = "Сохранить";
            toolStripButton1.Click += btnSave_Click;
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(91, 19);
            toolStripButton2.Text = "Сохранить как";
            toolStripButton2.Click += toolStripButtonSaveAs_Click;
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(58, 19);
            toolStripButton3.Text = "Открыть";
            toolStripButton3.Click += toolStripButtonOpen_Click;
            // 
            // toolStripMenuItemCSV
            // 
            toolStripMenuItemCSV.DropDownItems.AddRange(new ToolStripItem[] { toolStripButton7, toolStripButton8 });
            toolStripMenuItemCSV.Name = "toolStripMenuItemCSV";
            toolStripMenuItemCSV.Size = new Size(180, 22);
            toolStripMenuItemCSV.Text = "CSV";
            // 
            // toolStripButton7
            // 
            toolStripButton7.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton7.Image = (Image)resources.GetObject("toolStripButton7.Image");
            toolStripButton7.ImageTransparentColor = Color.Magenta;
            toolStripButton7.Name = "toolStripButton7";
            toolStripButton7.Size = new Size(55, 19);
            toolStripButton7.Text = "Импорт";
            toolStripButton7.Click += ImportButton_Click;
            // 
            // toolStripButton8
            // 
            toolStripButton8.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton8.Image = (Image)resources.GetObject("toolStripButton8.Image");
            toolStripButton8.ImageTransparentColor = Color.Magenta;
            toolStripButton8.Name = "toolStripButton8";
            toolStripButton8.Size = new Size(56, 19);
            toolStripButton8.Text = "Экспорт";
            toolStripButton8.Click += ExportButton_Click;
            // 
            // editManager
            // 
            editManager.DisplayStyle = ToolStripItemDisplayStyle.Text;
            editManager.DropDownItems.AddRange(new ToolStripItem[] { toolStripButton4, toolStripButton5, toolStripButton6 });
            editManager.Image = (Image)resources.GetObject("editManager.Image");
            editManager.ImageTransparentColor = Color.Magenta;
            editManager.Name = "editManager";
            editManager.Size = new Size(82, 22);
            editManager.Text = "Изменение";
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(77, 19);
            toolStripButton4.Text = "Сортировка";
            toolStripButton4.Click += btnSort_Click;
            // 
            // toolStripButton5
            // 
            toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton5.Image = (Image)resources.GetObject("toolStripButton5.Image");
            toolStripButton5.ImageTransparentColor = Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new Size(100, 19);
            toolStripButton5.Text = "Редактирование";
            toolStripButton5.Click += btnEdit_Click;
            // 
            // toolStripButton6
            // 
            toolStripButton6.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton6.ImageTransparentColor = Color.Magenta;
            toolStripButton6.Name = "toolStripButton6";
            toolStripButton6.Size = new Size(63, 19);
            toolStripButton6.Text = "Удаление";
            toolStripButton6.Click += btnDelete_Click;
            // 
            // Form1
            // 
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(985, 529);
            Controls.Add(toolStrip1);
            Controls.Add(txtSurname);
            Controls.Add(labelSurname);
            Controls.Add(txtName);
            Controls.Add(labelName);
            Controls.Add(txtPatronymic);
            Controls.Add(labelPatronymic);
            Controls.Add(BirthDate);
            Controls.Add(labelBirthDate);
            Controls.Add(txtCourse);
            Controls.Add(labelCourse);
            Controls.Add(txtGroup);
            Controls.Add(labelGroup);
            Controls.Add(txtEmail);
            Controls.Add(labelEmail);
            Controls.Add(txtPhone);
            Controls.Add(labelPhone);
            Controls.Add(dataGridView);
            Controls.Add(btnAdd);
            MaximumSize = new Size(1001, 568);
            MinimumSize = new Size(1001, 568);
            Name = "Form1";
            Text = "Student Manager";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private ToolStrip toolStrip;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton fileManager;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripDropDownButton editManager;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripButton toolStripButton6;
        private ToolStripMenuItem toolStripMenuItemCSV;
        private ToolStripButton toolStripButton7;
        private ToolStripButton toolStripButton8;
    }
}