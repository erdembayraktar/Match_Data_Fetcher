import sys
import os
import csv
import requests
from bs4 import BeautifulSoup
import random
import time
from datetime import datetime

# User-agent listesi
user_agents = [
    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
    "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36"
]

# Lig URL şablonları
base_urls = {
    "Bundesliga": "https://fbref.com/en/comps/20/{}/schedule/{}-Bundesliga-Scores-and-Fixtures",
    "Ligue 1": "https://fbref.com/en/comps/13/{}/schedule/{}-Ligue-1-Scores-and-Fixtures",
    "Süper Lig": "https://fbref.com/en/comps/26/{}/schedule/{}-Super-Lig-Scores-and-Fixtures",
    "Premier League": "https://fbref.com/en/comps/9/{}/schedule/{}-Premier-League-Scores-and-Fixtures",
    "La Liga": "https://fbref.com/en/comps/12/{}/schedule/{}-La-Liga-Scores-and-Fixtures",
    "Serie A": "https://fbref.com/en/comps/11/{}/schedule/{}-Serie-A-Scores-and-Fixtures"
}

# HTML tabloyu parse etme
def scrape_table(url, league):
    headers = {
        "User-Agent": random.choice(user_agents),
        "Accept-Language": "en-US,en;q=0.9",
        "Accept-Encoding": "gzip, deflate, br",
        "Connection": "keep-alive",
        "Upgrade-Insecure-Requests": "1"
    }

    response = requests.get(url, headers=headers)
    response.raise_for_status()
    time.sleep(random.uniform(2, 5))

    soup = BeautifulSoup(response.text, "html.parser")
    rows = soup.select("table.stats_table tbody tr")
    data = []

    for row in rows:
        if "spacer" in row.get("class", []):
            continue

        date_cell = row.find("td", {"data-stat": "date"})
        home_team = row.find("td", {"data-stat": "home_team"})
        away_team = row.find("td", {"data-stat": "away_team"})
        score = row.find("td", {"data-stat": "score"})

        if date_cell and date_cell.text.strip():
            try:
                datetime.strptime(date_cell.text.strip(), "%Y-%m-%d")
            except ValueError:
                continue

            data.append({
                "date": date_cell.text.strip(),
                "league": league,
                "home_team": home_team.text.strip() if home_team else "",
                "away_team": away_team.text.strip() if away_team else "",
                "score": score.text.strip() if score else "",
            })

    return data

def fetch_data(start_year, end_year, output_file):
    all_data = []
    for league, url_template in base_urls.items():
        for year in range(int(start_year), int(end_year) + 1):
            season = f"{year}-{year + 1}"
            url = url_template.format(season, season)
            print(f"Fetching data for {league} season {season}...")
            try:
                all_data.extend(scrape_table(url, league))
            except Exception as e:
                print(f"Error fetching data for {league} season {season}: {e}")

    save_to_csv(all_data, output_file)


def save_to_csv(data, file_path):
    fieldnames = ["date", "league", "home_team", "away_team", "score"]
    directory = os.path.dirname(file_path)

    # Dizin belirtilmişse oluştur
    if directory:
        os.makedirs(directory, exist_ok=True)

    with open(file_path, "w", encoding="utf-8", newline="") as f:
        writer = csv.DictWriter(f, fieldnames=fieldnames)
        writer.writeheader()
        writer.writerows(data)

    print(f"Data saved to {file_path}")

if __name__ == "__main__":
    if len(sys.argv) != 4:
        print("Usage: python all_data_generator.py <start_year> <end_year> <output_file>")
        sys.exit(1)

    fetch_data(sys.argv[1], sys.argv[2], sys.argv[3])
