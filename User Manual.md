### **User Manual for Match Data Fetcher and Predictor**

## **1\. Introduction**

Welcome to the **Match Data Fetcher and Predictor** user manual\! This guide will walk you through every detail of the application, from its features and requirements to step-by-step instructions for fetching data and predicting match outcomes.

## **2\. Features**

### **Core Functionalities**

* **Data Fetching**: Retrieve match data from various football leagues (e.g., Bundesliga, Premier League, Süper Lig).  
* **Prediction**: Predict match outcomes with probabilities for Home Win, Draw, and Away Win using a machine learning model.  
* **CSV Export**: Save fetched data into a CSV file for further analysis.

### **User-Friendly Interface**

* Intuitive design with dropdown menus and input fields.  
* Progress bars for visual feedback during long-running tasks.

## **3\. Requirements**

* **Operating System**: Windows 10/11 (64-bit).  
* **Python Version**: Python 3.8+.  
* **Dependencies**:  
  * pandas  
  * requests  
  * BeautifulSoup  
  * scikit-learn

The application can be used either via:

1. **Direct Execution** of the provided .exe file.  
2. **Running the Application from Visual Studio** (for developers).

## 

## 

## 

## **4\. Application Overview**

### **Main Interface**

The main window contains:

1. **League Selection Dropdown**: Choose from leagues like Bundesliga, Süper Lig, etc.  
2. **Year Range Selection**: Specify the start and end years for data fetching.  
3. **Buttons**:  
   * Fetch All Data: Retrieve and save match data to a CSV file.  
   * Predict: Predict match outcomes based on team names.  
4. **Input Fields**:  
   * Home Team and Away Team: Enter the names of the teams for predictions.  
5. **Data Grid**:  
   * Displays fetched or predicted data in tabular format.

## **5\. How to Use**

### **Fetching Match Data**

1. **Open the Application**:  
   * Double-click the .exe file:  
      SEProjectApp\\SEProjectApp\\bin\\Release\\net9.0-windows\\SEProjectApp.exe  
2. **Select a League**:  
   * Choose a football league from the dropdown menu (e.g., Bundesliga, Süper Lig).  
3. **Choose Year Range**:  
   * Set the start and end years using the respective dropdowns.  
4. **Click Fetch All Data**:  
   * The application will fetch data for the specified league and years.  
   * Progress is displayed on a progress bar.  
5. **Data Export**:  
   * Fetched data is automatically saved to all\_data.csv in the application’s directory.

### **Predicting Match Outcomes**

1. **Open the Application**.  
2. **Enter Team Names**:  
   * Type the names of the home and away teams in the input fields.  
   * Ensure correct spelling of team names.  
3. **Click Predict**:  
   * The application will calculate probabilities for Home Win, Draw, and Away Win.  
4. **View Results**:  
   * The results are displayed in the table below the Predict button.

## **6\. Troubleshooting**

### **Issue: Python Script Errors**

* **Cause**: Missing dependencies.  
* **Solution**: Run the following command in your terminal to install required libraries:

	 ***pip install \-r requirements.txt***

### **Issue: Incorrect Team Names**

* **Cause**: Spelling errors in team names.  
* **Solution**: Ensure that team names match those available in the fetched data.

### **Issue: Missing .exe File**

* **Cause**: .exe file not found.  
* **Solution**: Verify that the executable file is located at: SEProjectApp\\SEProjectApp\\bin\\Release\\net9.0-windows\\SEProjectApp.exe

## **7\. Frequently Asked Questions**

### **Q1. How do I fetch data for multiple leagues at once?**

Currently, you can fetch data for one league at a time by selecting it from the dropdown menu.

### **Q2. Can I predict outcomes for past matches?**

Yes, as long as the team names and data match those fetched by the application.

### **Q3. Where is the fetched data saved?**

Fetched data is saved as a CSV file named all\_data.csv in the application’s directory.

### **Q4. What happens if the application crashes?**

Check the logs in the application’s directory for debugging. Ensure all required dependencies are installed.

## 

## 

## **8\. Future Enhancements**

* Adding more leagues and match types for data fetching and prediction.  
* A web-based version for easier accessibility.  
* Advanced machine learning models for higher prediction accuracy.

## **9\. Support and Feedback**

* For support or feedback, please open an issue in the GitHub repository: [https://github.com/erdembayraktar/Match\_Data\_Fetcher](https://github.com/erdembayraktar/MatchDataFetcher)  
* Alternatively, contact the project author via email:  
   stiuegroup@gmail.com

