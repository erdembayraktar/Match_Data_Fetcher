import sys

import requests
from bs4 import BeautifulSoup
import json
import random
import time
from datetime import datetime

user_agents = [
    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
    "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36"
    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36",
    "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36",
    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36",
    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1.1 Safari/605.1.15",
    "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36",
    "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36",
    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36",
    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36 Edg/91.0.864.59",
    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36",
    "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36",
    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
]

base_urls = {
    "Bundesliga": "https://fbref.com/en/comps/20/{}/schedule/{}-Bundesliga-Scores-and-Fixtures",
    "Ligue 1": "https://fbref.com/en/comps/13/{}/schedule/{}-Ligue-1-Scores-and-Fixtures",
    "Süper Lig": "https://fbref.com/en/comps/26/{}/schedule/{}-Super-Lig-Scores-and-Fixtures",
    "Premier League": "https://fbref.com/en/comps/9/{}/schedule/{}-Premier-League-Scores-and-Fixtures",
    "La Liga": "https://fbref.com/en/comps/12/{}/schedule/{}-La-Liga-Scores-and-Fixtures",
    "Serie A": "https://fbref.com/en/comps/11/{}/schedule/{}-Serie-A-Scores-and-Fixtures"
}

def scrape_table(url, league=None, start_year=None, end_year=None):
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

    soup = BeautifulSoup(response.text, 'html.parser')
    rows = soup.select("table.stats_table tbody tr")
    data = []
    current_date = None

    for row in rows:

        if "spacer" in row.get("class", []):
            continue

        date_cell = row.find("td", {"data-stat": "date"})
        home_team = row.find("td", {"data-stat": "home_team"})
        away_team = row.find("td", {"data-stat": "away_team"})
        score = row.find("td", {"data-stat": "score"})


        if date_cell and date_cell.text.strip():
            current_date = date_cell.text.strip()


        if current_date:
            try:
                match_year = datetime.strptime(current_date, "%Y-%m-%d").year
                if start_year and end_year and not (int(start_year) <= match_year <= int(end_year)):
                    continue
            except ValueError:
                print(f"Geçersiz tarih formatı: {current_date}")
                continue


        data.append({
            "date": current_date if current_date else "",
            "home_team": home_team.text.strip() if home_team else "",
            "away_team": away_team.text.strip() if away_team else "",
            "score": score.text.strip() if score else "",
        })

    return data


def fetch_data(league, start_year, end_year):
    results = []


    if league == "All":
        for lg, url_template in base_urls.items():
            for year in range(int(start_year), int(end_year) + 1):
                season = f"{year}-{str(year + 1)}"
                url = url_template.format(season, season)
                results.extend(scrape_table(url, lg, start_year, end_year))
    else:

        if league not in base_urls:
            raise ValueError(f"Lig bulunamadı: {league}")

        for year in range(int(start_year), int(end_year) + 1):
            season = f"{year}-{str(year + 1)}"
            url = base_urls[league].format(season, season)
            results.extend(scrape_table(url, league, start_year, end_year))

    return results


if __name__ == "__main__":

    league = sys.argv[1]
    start_year = sys.argv[2]
    end_year = sys.argv[3]
    data = fetch_data(league, start_year, end_year)

    print(json.dumps(data, ensure_ascii=False))
