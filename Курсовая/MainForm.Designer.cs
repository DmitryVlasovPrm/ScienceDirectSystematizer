namespace CourseWork
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveWordIEEEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveWordGOSTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveBibTeXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DistributionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.YearsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KeywordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.JournalYearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConferenceYearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AuthorsYearsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DiagrammToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowAllPublicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.задатьПараметрыПоискаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Cur_status = new System.Windows.Forms.ToolStripTextBox();
            this.currentStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1282, 772);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.DistributionToolStripMenuItem,
            this.ViewToolStripMenuItem,
            this.SearchToolStripMenuItem,
            this.Cur_status});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1282, 31);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.SaveWordIEEEToolStripMenuItem,
            this.SaveWordGOSTToolStripMenuItem,
            this.SaveExcelToolStripMenuItem,
            this.SaveBibTeXToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(64, 27);
            this.FileToolStripMenuItem.Text = "Файл";
            // 
            // OpenFileToolStripMenuItem
            // 
            this.OpenFileToolStripMenuItem.Name = "OpenFileToolStripMenuItem";
            this.OpenFileToolStripMenuItem.Size = new System.Drawing.Size(460, 28);
            this.OpenFileToolStripMenuItem.Text = "Открыть новый";
            this.OpenFileToolStripMenuItem.Click += new System.EventHandler(this.OpenFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(457, 6);
            // 
            // SaveWordIEEEToolStripMenuItem
            // 
            this.SaveWordIEEEToolStripMenuItem.Name = "SaveWordIEEEToolStripMenuItem";
            this.SaveWordIEEEToolStripMenuItem.Size = new System.Drawing.Size(460, 28);
            this.SaveWordIEEEToolStripMenuItem.Text = "Сохранить список публикаций в Word (IEEE)";
            this.SaveWordIEEEToolStripMenuItem.Click += new System.EventHandler(this.SaveWordIEEEToolStripMenuItem_Click);
            // 
            // SaveWordGOSTToolStripMenuItem
            // 
            this.SaveWordGOSTToolStripMenuItem.Name = "SaveWordGOSTToolStripMenuItem";
            this.SaveWordGOSTToolStripMenuItem.Size = new System.Drawing.Size(460, 28);
            this.SaveWordGOSTToolStripMenuItem.Text = "Сохранить список публикаций в Word (ГОСТ)";
            this.SaveWordGOSTToolStripMenuItem.Click += new System.EventHandler(this.SaveWordGOSTToolStripMenuItem_Click);
            // 
            // SaveExcelToolStripMenuItem
            // 
            this.SaveExcelToolStripMenuItem.Name = "SaveExcelToolStripMenuItem";
            this.SaveExcelToolStripMenuItem.Size = new System.Drawing.Size(460, 28);
            this.SaveExcelToolStripMenuItem.Text = "Сохранить распределения публикаций в Excel";
            this.SaveExcelToolStripMenuItem.Click += new System.EventHandler(this.SaveExcelToolStripMenuItem_Click);
            // 
            // SaveBibTeXToolStripMenuItem
            // 
            this.SaveBibTeXToolStripMenuItem.Name = "SaveBibTeXToolStripMenuItem";
            this.SaveBibTeXToolStripMenuItem.Size = new System.Drawing.Size(460, 28);
            this.SaveBibTeXToolStripMenuItem.Text = "Сохранить список публикаций в BibTeX файл";
            this.SaveBibTeXToolStripMenuItem.Click += new System.EventHandler(this.SaveBibTeXToolStripMenuItem_Click);
            // 
            // DistributionToolStripMenuItem
            // 
            this.DistributionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.YearsToolStripMenuItem,
            this.TypeToolStripMenuItem,
            this.KeywordsToolStripMenuItem,
            this.JournalYearToolStripMenuItem,
            this.ConferenceYearToolStripMenuItem,
            this.AuthorsYearsToolStripMenuItem});
            this.DistributionToolStripMenuItem.Name = "DistributionToolStripMenuItem";
            this.DistributionToolStripMenuItem.Size = new System.Drawing.Size(145, 27);
            this.DistributionToolStripMenuItem.Text = "Распределения";
            // 
            // YearsToolStripMenuItem
            // 
            this.YearsToolStripMenuItem.Name = "YearsToolStripMenuItem";
            this.YearsToolStripMenuItem.Size = new System.Drawing.Size(577, 28);
            this.YearsToolStripMenuItem.Text = "По годам издания";
            this.YearsToolStripMenuItem.Click += new System.EventHandler(this.YearsToolStripMenuItem_Click);
            // 
            // TypeToolStripMenuItem
            // 
            this.TypeToolStripMenuItem.Name = "TypeToolStripMenuItem";
            this.TypeToolStripMenuItem.Size = new System.Drawing.Size(577, 28);
            this.TypeToolStripMenuItem.Text = "По типу публикации";
            this.TypeToolStripMenuItem.Click += new System.EventHandler(this.TypeToolStripMenuItem_Click);
            // 
            // KeywordsToolStripMenuItem
            // 
            this.KeywordsToolStripMenuItem.Name = "KeywordsToolStripMenuItem";
            this.KeywordsToolStripMenuItem.Size = new System.Drawing.Size(577, 28);
            this.KeywordsToolStripMenuItem.Text = "По ключевым словам";
            this.KeywordsToolStripMenuItem.Click += new System.EventHandler(this.KeywordsToolStripMenuItem_Click);
            // 
            // JournalYearToolStripMenuItem
            // 
            this.JournalYearToolStripMenuItem.Name = "JournalYearToolStripMenuItem";
            this.JournalYearToolStripMenuItem.Size = new System.Drawing.Size(577, 28);
            this.JournalYearToolStripMenuItem.Text = "По количеству журналов и годам";
            this.JournalYearToolStripMenuItem.Click += new System.EventHandler(this.JournalYearToolStripMenuItem_Click);
            // 
            // ConferenceYearToolStripMenuItem
            // 
            this.ConferenceYearToolStripMenuItem.Name = "ConferenceYearToolStripMenuItem";
            this.ConferenceYearToolStripMenuItem.Size = new System.Drawing.Size(577, 28);
            this.ConferenceYearToolStripMenuItem.Text = "По количеству конференций и годам";
            this.ConferenceYearToolStripMenuItem.Click += new System.EventHandler(this.ConferenceYearToolStripMenuItem_Click);
            // 
            // AuthorsYearsToolStripMenuItem
            // 
            this.AuthorsYearsToolStripMenuItem.Name = "AuthorsYearsToolStripMenuItem";
            this.AuthorsYearsToolStripMenuItem.Size = new System.Drawing.Size(577, 28);
            this.AuthorsYearsToolStripMenuItem.Text = "По количественному составу авторского коллектива и годам";
            this.AuthorsYearsToolStripMenuItem.Click += new System.EventHandler(this.AuthorsYearsToolStripMenuItem_Click);
            // 
            // ViewToolStripMenuItem
            // 
            this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DiagrammToolStripMenuItem,
            this.ClearToolStripMenuItem});
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            this.ViewToolStripMenuItem.Size = new System.Drawing.Size(53, 27);
            this.ViewToolStripMenuItem.Text = "Вид";
            // 
            // DiagrammToolStripMenuItem
            // 
            this.DiagrammToolStripMenuItem.Name = "DiagrammToolStripMenuItem";
            this.DiagrammToolStripMenuItem.Size = new System.Drawing.Size(236, 28);
            this.DiagrammToolStripMenuItem.Text = "Диаграмма";
            this.DiagrammToolStripMenuItem.Click += new System.EventHandler(this.DiagrammToolStripMenuItem_Click);
            // 
            // ClearToolStripMenuItem
            // 
            this.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem";
            this.ClearToolStripMenuItem.Size = new System.Drawing.Size(236, 28);
            this.ClearToolStripMenuItem.Text = "Очистить таблицу";
            this.ClearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
            // 
            // SearchToolStripMenuItem
            // 
            this.SearchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowAllPublicationToolStripMenuItem,
            this.задатьПараметрыПоискаToolStripMenuItem});
            this.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem";
            this.SearchToolStripMenuItem.Size = new System.Drawing.Size(72, 27);
            this.SearchToolStripMenuItem.Text = "Поиск";
            // 
            // ShowAllPublicationToolStripMenuItem
            // 
            this.ShowAllPublicationToolStripMenuItem.Name = "ShowAllPublicationToolStripMenuItem";
            this.ShowAllPublicationToolStripMenuItem.Size = new System.Drawing.Size(299, 28);
            this.ShowAllPublicationToolStripMenuItem.Text = "Показать все публикации";
            this.ShowAllPublicationToolStripMenuItem.Click += new System.EventHandler(this.ShowAllPublicationToolStripMenuItem_Click);
            // 
            // задатьПараметрыПоискаToolStripMenuItem
            // 
            this.задатьПараметрыПоискаToolStripMenuItem.Name = "задатьПараметрыПоискаToolStripMenuItem";
            this.задатьПараметрыПоискаToolStripMenuItem.Size = new System.Drawing.Size(299, 28);
            this.задатьПараметрыПоискаToolStripMenuItem.Text = "Задать параметры поиска";
            this.задатьПараметрыПоискаToolStripMenuItem.Click += new System.EventHandler(this.задатьПараметрыПоискаToolStripMenuItem_Click);
            // 
            // Cur_status
            // 
            this.Cur_status.BackColor = System.Drawing.SystemColors.Control;
            this.Cur_status.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Cur_status.Enabled = false;
            this.Cur_status.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Cur_status.Name = "Cur_status";
            this.Cur_status.Size = new System.Drawing.Size(250, 27);
            this.Cur_status.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // currentStatus
            // 
            this.currentStatus.AutoSize = true;
            this.currentStatus.Location = new System.Drawing.Point(1131, 9);
            this.currentStatus.Name = "currentStatus";
            this.currentStatus.Size = new System.Drawing.Size(0, 20);
            this.currentStatus.TabIndex = 16;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 803);
            this.Controls.Add(this.currentStatus);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Science Direct Systematizer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveWordGOSTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem DistributionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem YearsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem KeywordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DiagrammToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AuthorsYearsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowAllPublicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem задатьПараметрыПоискаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveBibTeXToolStripMenuItem;
        public System.Windows.Forms.Label currentStatus;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripTextBox Cur_status;
        private System.Windows.Forms.ToolStripMenuItem JournalYearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConferenceYearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveWordIEEEToolStripMenuItem;
    }
}

