namespace SEProjectApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox comboBoxLeagues;
        private System.Windows.Forms.ComboBox comboBoxStartYear;
        private System.Windows.Forms.ComboBox comboBoxEndYear;
        private System.Windows.Forms.Button btnFetchData;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.DataGridView dataGridViewMatches;
        private System.Windows.Forms.Label lblLeague;
        private System.Windows.Forms.Label lblStartYear;
        private System.Windows.Forms.Label lblEndYear;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnExportToCSV;



        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblSearch = new Label();
            txtSearch = new TextBox();
            progressBar = new ProgressBar();
            comboBoxLeagues = new ComboBox();
            comboBoxStartYear = new ComboBox();
            comboBoxEndYear = new ComboBox();
            btnFetchData = new Button();
            btnClearFilters = new Button();
            dataGridViewMatches = new DataGridView();
            lblLeague = new Label();
            lblStartYear = new Label();
            lblEndYear = new Label();
            label1 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label2 = new Label();
            btnFetchAllData = new Button();
            progressBarAllData = new ProgressBar();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMatches).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(23, 140);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(45, 15);
            lblSearch.TabIndex = 9;
            lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(100, 137);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(150, 23);
            txtSearch.TabIndex = 10;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(20, 470);
            progressBar.MarqueeAnimationSpeed = 30;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(404, 20);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 11;
            progressBar.Visible = false;
            // 
            // comboBoxLeagues
            // 
            comboBoxLeagues.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLeagues.FormattingEnabled = true;
            comboBoxLeagues.Location = new Point(100, 20);
            comboBoxLeagues.Name = "comboBoxLeagues";
            comboBoxLeagues.Size = new Size(150, 23);
            comboBoxLeagues.TabIndex = 0;
            comboBoxLeagues.SelectedIndexChanged += comboBoxLeagues_SelectedIndexChanged;
            // 
            // comboBoxStartYear
            // 
            comboBoxStartYear.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxStartYear.FormattingEnabled = true;
            comboBoxStartYear.Location = new Point(100, 60);
            comboBoxStartYear.Name = "comboBoxStartYear";
            comboBoxStartYear.Size = new Size(150, 23);
            comboBoxStartYear.TabIndex = 1;
            comboBoxStartYear.SelectedIndexChanged += comboBoxStartYear_SelectedIndexChanged;
            // 
            // comboBoxEndYear
            // 
            comboBoxEndYear.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEndYear.FormattingEnabled = true;
            comboBoxEndYear.Location = new Point(100, 100);
            comboBoxEndYear.Name = "comboBoxEndYear";
            comboBoxEndYear.Size = new Size(150, 23);
            comboBoxEndYear.TabIndex = 2;
            // 
            // btnFetchData
            // 
            btnFetchData.Location = new Point(270, 20);
            btnFetchData.Name = "btnFetchData";
            btnFetchData.Size = new Size(100, 30);
            btnFetchData.TabIndex = 3;
            btnFetchData.Text = "Fetch Data";
            btnFetchData.UseVisualStyleBackColor = true;
            btnFetchData.Click += btnFetchData_Click;
            // 
            // btnClearFilters
            // 
            btnClearFilters.Location = new Point(270, 60);
            btnClearFilters.Name = "btnClearFilters";
            btnClearFilters.Size = new Size(100, 30);
            btnClearFilters.TabIndex = 4;
            btnClearFilters.Text = "Clear Filter";
            btnClearFilters.UseVisualStyleBackColor = true;
            btnClearFilters.Click += btnClearFilters_Click;
            // 
            // dataGridViewMatches
            // 
            dataGridViewMatches.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMatches.Location = new Point(20, 186);
            dataGridViewMatches.Name = "dataGridViewMatches";
            dataGridViewMatches.Size = new Size(404, 273);
            dataGridViewMatches.TabIndex = 5;
            // 
            // lblLeague
            // 
            lblLeague.AutoSize = true;
            lblLeague.Location = new Point(20, 23);
            lblLeague.Name = "lblLeague";
            lblLeague.Size = new Size(48, 15);
            lblLeague.TabIndex = 6;
            lblLeague.Text = "League:";
            // 
            // lblStartYear
            // 
            lblStartYear.AutoSize = true;
            lblStartYear.Location = new Point(20, 63);
            lblStartYear.Name = "lblStartYear";
            lblStartYear.Size = new Size(59, 15);
            lblStartYear.TabIndex = 7;
            lblStartYear.Text = "Start Year:";
            // 
            // lblEndYear
            // 
            lblEndYear.AutoSize = true;
            lblEndYear.Location = new Point(20, 103);
            lblEndYear.Name = "lblEndYear";
            lblEndYear.Size = new Size(55, 15);
            lblEndYear.TabIndex = 8;
            lblEndYear.Text = "End Year:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.Location = new Point(640, 9);
            label1.Name = "label1";
            label1.Size = new Size(164, 21);
            label1.TabIndex = 12;
            label1.Text = "MATCH PREDICTION";
            // 
            // textBox1
            // 
            textBox1.ForeColor = SystemColors.WindowFrame;
            textBox1.Location = new Point(564, 65);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(141, 23);
            textBox1.TabIndex = 14;
            // 
            // textBox2
            // 
            textBox2.ForeColor = SystemColors.WindowFrame;
            textBox2.Location = new Point(787, 65);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(144, 23);
            textBox2.TabIndex = 15;
            // 
            // button1
            // 
            button1.Location = new Point(484, 103);
            button1.Name = "button1";
            button1.Size = new Size(100, 30);
            button1.TabIndex = 16;
            button1.Text = "Predict";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(484, 186);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(447, 179);
            dataGridView1.TabIndex = 17;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(484, 68);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 18;
            label3.Text = "Home Team:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(711, 68);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 19;
            label4.Text = "Away Team:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(484, 35);
            label5.Name = "label5";
            label5.Size = new Size(463, 15);
            label5.TabIndex = 20;
            label5.Text = "(You must enter the team names correctly.for example: \"Bayern Munich\" , \"Gladbach\")";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label2.Location = new Point(486, 386);
            label2.Name = "label2";
            label2.Size = new Size(318, 21);
            label2.TabIndex = 21;
            label2.Text = "Fetch and download to csv file for all data:";
            // 
            // btnFetchAllData
            // 
            btnFetchAllData.Location = new Point(810, 383);
            btnFetchAllData.Name = "btnFetchAllData";
            btnFetchAllData.Size = new Size(137, 30);
            btnFetchAllData.TabIndex = 22;
            btnFetchAllData.Text = "Fetch All Data";
            btnFetchAllData.UseVisualStyleBackColor = true;
            btnFetchAllData.Click += btnFetchAllData_Click;
            // 
            // progressBarAllData
            // 
            progressBarAllData.Location = new Point(722, 444);
            progressBarAllData.MarqueeAnimationSpeed = 30;
            progressBarAllData.Name = "progressBarAllData";
            progressBarAllData.Size = new Size(225, 20);
            progressBarAllData.Style = ProgressBarStyle.Marquee;
            progressBarAllData.TabIndex = 23;
            progressBarAllData.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(484, 444);
            label6.Name = "label6";
            label6.Size = new Size(232, 15);
            label6.TabIndex = 24;
            label6.Text = "This process may take about half an hour...";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(985, 500);
            Controls.Add(label6);
            Controls.Add(progressBarAllData);
            Controls.Add(btnFetchAllData);
            Controls.Add(label2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(lblSearch);
            Controls.Add(txtSearch);
            Controls.Add(progressBar);
            Controls.Add(lblEndYear);
            Controls.Add(lblStartYear);
            Controls.Add(lblLeague);
            Controls.Add(dataGridViewMatches);
            Controls.Add(btnClearFilters);
            Controls.Add(btnFetchData);
            Controls.Add(comboBoxEndYear);
            Controls.Add(comboBoxStartYear);
            Controls.Add(comboBoxLeagues);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Match Data Fetcher";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewMatches).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private DataGridView dataGridView1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label2;
        private Button btnFetchAllData;
        private ProgressBar progressBarAllData;
        private Label label6;
    }
}
