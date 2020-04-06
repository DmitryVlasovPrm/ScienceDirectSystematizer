namespace CourseWork
{
    partial class Filtration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Filtration));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.startYear = new System.Windows.Forms.TextBox();
            this.findJournal = new System.Windows.Forms.TextBox();
            this.findTitle = new System.Windows.Forms.TextBox();
            this.findKeywords = new System.Windows.Forms.TextBox();
            this.findAuthors = new System.Windows.Forms.TextBox();
            this.typeValue = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.sortCrit = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.sortValue = new System.Windows.Forms.ComboBox();
            this.search = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.endYear = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Поиск:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Год";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Журнал";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(375, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(290, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ключевые слова (через запятую)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Заголовок";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(375, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Тип";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(375, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(214, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Авторы (через запятую)";
            // 
            // startYear
            // 
            this.startYear.Location = new System.Drawing.Point(31, 73);
            this.startYear.Name = "startYear";
            this.startYear.Size = new System.Drawing.Size(86, 27);
            this.startYear.TabIndex = 7;
            // 
            // findJournal
            // 
            this.findJournal.Location = new System.Drawing.Point(31, 137);
            this.findJournal.Name = "findJournal";
            this.findJournal.Size = new System.Drawing.Size(255, 27);
            this.findJournal.TabIndex = 8;
            // 
            // findTitle
            // 
            this.findTitle.Location = new System.Drawing.Point(31, 202);
            this.findTitle.Name = "findTitle";
            this.findTitle.Size = new System.Drawing.Size(255, 27);
            this.findTitle.TabIndex = 10;
            // 
            // findKeywords
            // 
            this.findKeywords.Location = new System.Drawing.Point(379, 202);
            this.findKeywords.Name = "findKeywords";
            this.findKeywords.Size = new System.Drawing.Size(353, 27);
            this.findKeywords.TabIndex = 11;
            // 
            // findAuthors
            // 
            this.findAuthors.Location = new System.Drawing.Point(379, 137);
            this.findAuthors.Name = "findAuthors";
            this.findAuthors.Size = new System.Drawing.Size(353, 27);
            this.findAuthors.TabIndex = 12;
            // 
            // typeValue
            // 
            this.typeValue.FormattingEnabled = true;
            this.typeValue.Items.AddRange(new object[] {
            "Книга",
            "Журнал",
            "Конференция"});
            this.typeValue.Location = new System.Drawing.Point(379, 72);
            this.typeValue.Name = "typeValue";
            this.typeValue.Size = new System.Drawing.Size(158, 28);
            this.typeValue.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 252);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "Сортировка:";
            // 
            // sortCrit
            // 
            this.sortCrit.FormattingEnabled = true;
            this.sortCrit.Items.AddRange(new object[] {
            "Год",
            "Журнал",
            "Заголовок",
            "Количество авторов",
            "Количество ключевых слов"});
            this.sortCrit.Location = new System.Drawing.Point(31, 311);
            this.sortCrit.Name = "sortCrit";
            this.sortCrit.Size = new System.Drawing.Size(255, 28);
            this.sortCrit.TabIndex = 15;
            this.sortCrit.SelectedValueChanged += new System.EventHandler(this.sortCrit_SelectedValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 288);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "Критерий";
            // 
            // sortValue
            // 
            this.sortValue.FormattingEnabled = true;
            this.sortValue.Items.AddRange(new object[] {
            "По возрастанию",
            "По убыванию"});
            this.sortValue.Location = new System.Drawing.Point(379, 311);
            this.sortValue.Name = "sortValue";
            this.sortValue.Size = new System.Drawing.Size(217, 28);
            this.sortValue.TabIndex = 17;
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(31, 357);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(147, 41);
            this.search.TabIndex = 18;
            this.search.Text = "Поиск";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(197, 357);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(147, 41);
            this.cancel.TabIndex = 19;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // endYear
            // 
            this.endYear.Location = new System.Drawing.Point(144, 75);
            this.endYear.Name = "endYear";
            this.endYear.Size = new System.Drawing.Size(86, 27);
            this.endYear.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(123, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 20);
            this.label10.TabIndex = 21;
            this.label10.Text = "–";
            // 
            // Filtration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(767, 428);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.endYear);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.search);
            this.Controls.Add(this.sortValue);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.sortCrit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.typeValue);
            this.Controls.Add(this.findAuthors);
            this.Controls.Add(this.findKeywords);
            this.Controls.Add(this.findTitle);
            this.Controls.Add(this.findJournal);
            this.Controls.Add(this.startYear);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Filtration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox startYear;
        private System.Windows.Forms.TextBox findJournal;
        private System.Windows.Forms.TextBox findTitle;
        private System.Windows.Forms.TextBox findKeywords;
        private System.Windows.Forms.TextBox findAuthors;
        private System.Windows.Forms.ComboBox typeValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox sortCrit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox sortValue;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.TextBox endYear;
        private System.Windows.Forms.Label label10;
    }
}