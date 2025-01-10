# Match Data Fetcher and Predictor

## 📋 Project Description
This project allows users to:
- Fetch football match data from various leagues (e.g., Bundesliga, Premier League).
- Save data into CSV format for analysis.
- Predict match outcomes using a machine learning model.

## ⚙️ Features
- Fetch data for specified years and leagues.
- Predict match outcomes with probabilities (Home Win, Draw, Away Win).
- Intuitive user interface.

## 🛠️ Requirements
- **Python 3.8+**
- Libraries: `pandas`, `requests`, `BeautifulSoup`, `scikit-learn`
- .NET Framework for the application.

## 🔧 Setup
1. Clone this repository:
   ```bash
   git clone https://github.com/erdembayraktar/MatchDataFetcher.git
2. Navigate to the project directory
   
   `cd MatchDataFetcher`
4. Install Python dependencies:
   
   `pip install -r requirements.txt`
6. Open the .sln file in Visual Studio to run the application or you can use exe file directly :
  
   SEProjectApp\SEProjectApp\bin\Release\net9.0-windows\SEProjectApp.exe

🚀 How to Use
- Open the application.
- Select a league from the dropdown menu (e.g., Bundesliga, Premier League).
- Choose a start year and an end year.
- Click Fetch All Data to fetch match data.
- Fetched data is automatically saved to a CSV file.

  Predicting Match Outcomes
- Open the application.
- Enter the home and away team names in the input fields.
- Click Predict to see the match outcome probabilities.

🌟 Future Enhancements
- Add more leagues.
- Provide a web-based interface.
- Include advanced machine learning models for predictions.
