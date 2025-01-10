using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SEProjectApp
{
    public partial class MainForm : Form
    {
        private DataTable matchData = new DataTable();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            matchData.Columns.Add("Date", typeof(string));
            matchData.Columns.Add("Home Team", typeof(string));
            matchData.Columns.Add("Away Team", typeof(string));
            matchData.Columns.Add("Score", typeof(string));


            dataGridViewMatches.DataSource = matchData;



            comboBoxLeagues.Items.AddRange(new string[] { "Bundesliga", "Ligue 1", "Süper Lig", "Premier League", "La Liga", "Serie A" });
            comboBoxLeagues.SelectedIndex = 0;


            UpdateYearRanges();
        }

        private void UpdateYearRanges()
        {
            comboBoxStartYear.Items.Clear();
            comboBoxEndYear.Items.Clear();

            int startYear = 2000; // Varsayýlan baþlangýç yýlý
            if (comboBoxLeagues.SelectedItem?.ToString() == "Süper Lig")
            {
                startYear = 2013; // Süper Lig için farklý baþlangýç yýlý
            }

            int currentYear = DateTime.Now.Year;

            for (int year = startYear; year <= currentYear; year++)
            {
                comboBoxStartYear.Items.Add(year.ToString());
                comboBoxEndYear.Items.Add(year.ToString());
            }

            comboBoxStartYear.SelectedIndex = 0;
            comboBoxEndYear.SelectedIndex = comboBoxEndYear.Items.Count - 1;
        }


        private void comboBoxLeagues_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateYearRanges();
        }



        private void comboBoxStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStartYear.SelectedItem == null) return;

            int selectedStartYear = int.Parse(comboBoxStartYear.SelectedItem.ToString());
            int currentEndYear = DateTime.Now.Year;


            comboBoxEndYear.Items.Clear();
            for (int year = selectedStartYear + 1; year <= currentEndYear; year++)
            {
                comboBoxEndYear.Items.Add(year.ToString());
            }

            comboBoxEndYear.SelectedIndex = 0;
        }

        private async Task<string> RunPythonScriptAsync(string scriptPath, string arguments)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"{scriptPath} {arguments}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(start))
                {
                    string standardOutput = await process.StandardOutput.ReadToEndAsync();
                    string errorOutput = await process.StandardError.ReadToEndAsync();

                    await process.WaitForExitAsync();

                    if (!string.IsNullOrEmpty(errorOutput))
                    {
                        File.AppendAllText("error_log.txt", errorOutput); // Hata loguna yaz
                        MessageBox.Show("An error occurred while running the Python script. Check error_log.txt for details.");
                        return string.Empty;
                    }

                    return standardOutput.Trim();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("error_log.txt", $"Error: {ex.Message}");
                MessageBox.Show($"An error occurred. Check error_log.txt for details.");
                return string.Empty;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (matchData.DefaultView != null)
            {
                string filterText = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(filterText))
                {
                    matchData.DefaultView.RowFilter = $"[Date] LIKE '%{filterText}%' OR " +
                                                      $"[Home Team] LIKE '%{filterText}%' OR " +
                                                      $"[Away Team] LIKE '%{filterText}%' OR " +
                                                      $"[Score] LIKE '%{filterText}%'";
                }
                else
                {
                    matchData.DefaultView.RowFilter = string.Empty; // Tüm verileri göster
                }
            }
        }


        private async void btnFetchData_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedLeague = comboBoxLeagues.SelectedItem?.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(selectedLeague))
                {
                    MessageBox.Show("Please select a League!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string startYear = comboBoxStartYear.SelectedItem?.ToString() ?? string.Empty;
                string endYear = comboBoxEndYear.SelectedItem?.ToString() ?? string.Empty;

                if (string.IsNullOrEmpty(startYear) || string.IsNullOrEmpty(endYear))
                {
                    MessageBox.Show("Please select start-end dates!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data_fetcher.py");

                if (!File.Exists(scriptPath))
                {
                    MessageBox.Show($"Python script not found: {scriptPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string arguments = $"\"{selectedLeague}\" {startYear} {endYear}";

                progressBar.Visible = true;

                string output = await RunPythonScriptAsync(scriptPath, arguments);

                progressBar.Visible = false;

                if (string.IsNullOrWhiteSpace(output) || (!output.StartsWith("[") && !output.StartsWith("{")))
                {
                    MessageBox.Show("Invalid JSON output:\n" + output, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var jsonData = JArray.Parse(output);
                matchData.Clear();
                foreach (var match in jsonData)
                {
                    matchData.Rows.Add(
                        (string)match["date"],
                        (string)match["home_team"],
                        (string)match["away_team"],
                        (string)match["score"]
                    );
                }

                MessageBox.Show("The data was uploaded successfully!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                MessageBox.Show($"An error has occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnFetchAllData_Click(object sender, EventArgs e)
        {
            string startYear = comboBoxStartYear.SelectedItem?.ToString() ?? string.Empty;
            string endYear = comboBoxEndYear.SelectedItem?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(startYear) || string.IsNullOrEmpty(endYear))
            {
                MessageBox.Show("Please select a valid start and end year!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string outputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "all_data.csv");
            string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "all_data_generator.py");

            if (!File.Exists(scriptPath))
            {
                MessageBox.Show($"Python script not found: {scriptPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string arguments = $"{startYear} {endYear} \"{outputFilePath}\"";

            progressBarAllData.Visible = true;
            try
            {
                string output = await RunPythonScriptAsync(scriptPath, arguments);

                if (string.IsNullOrWhiteSpace(output))
                {
                    MessageBox.Show("No output from the Python script!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Data successfully fetched and saved to:\n{outputFilePath}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBarAllData.Visible = false;
            }
        }

        private async void button1_Click(object sender, EventArgs e) // Tahmin Butonu
        {
            string homeTeam = textBox1.Text.Trim();
            string awayTeam = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(homeTeam) || string.IsNullOrEmpty(awayTeam))
            {
                MessageBox.Show("Please enter both home and away team names!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "match_predictor.py");

            if (!File.Exists(scriptPath))
            {
                MessageBox.Show($"Python script not found: {scriptPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string arguments = $"\"{homeTeam}\" \"{awayTeam}\"";
            string output = await RunPythonScriptAsync(scriptPath, arguments);

            if (string.IsNullOrWhiteSpace(output))
            {
                MessageBox.Show("No output from prediction script!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Çýktýyý satýrlara böl
                var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                // JSON'u bul ve iþle
                var jsonLine = lines.FirstOrDefault(line => line.Trim().StartsWith("{") && line.Trim().EndsWith("}"));
                if (jsonLine == null)
                {
                    File.AppendAllText("output_log.txt", output); // Çýktýyý logla
                    MessageBox.Show("No valid JSON output found. Check output_log.txt for details.");
                    return;
                }

                var result = JObject.Parse(jsonLine);

                // Eðer bir hata dönerse
                if (result.ContainsKey("error"))
                {
                    string errorMessage = result["error"].ToString();
                    if (result.ContainsKey("invalid_teams"))
                    {
                        var invalidTeams = result["invalid_teams"];
                        if (invalidTeams["home_team"] != null)
                            errorMessage += $"\nInvalid Home Team: {invalidTeams["home_team"]}";
                        if (invalidTeams["away_team"] != null)
                            errorMessage += $"\nInvalid Away Team: {invalidTeams["away_team"]}";
                    }
                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tahmin sonuçlarýný göster
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Outcome", "Outcome");
                dataGridView1.Columns.Add("Probability (%)", "Probability (%)");

                dataGridView1.Rows.Add("Home Win", result["probabilities"]["home_win"]);
                dataGridView1.Rows.Add("Draw", result["probabilities"]["draw"]);
                dataGridView1.Rows.Add("Away Win", result["probabilities"]["away_win"]);
            }
            catch (Exception ex)
            {
                File.AppendAllText("error_log.txt", $"Error parsing JSON: {ex.Message}\nOutput: {output}");
                MessageBox.Show($"Error parsing JSON. Check error_log.txt for details.");
            }
        }




        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            comboBoxLeagues.SelectedIndex = 0; // "All" seçili
            UpdateYearRanges(); // Yýllarý güncelle
            matchData.Clear();
            MessageBox.Show("Filters have been cleaned!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
