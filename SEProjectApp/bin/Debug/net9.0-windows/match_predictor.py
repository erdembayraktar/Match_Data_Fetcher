import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier
from sklearn.metrics import accuracy_score
import sys
import json


def preprocess_data(file_path):
    try:
        # CSV dosyasını oku
        data = pd.read_csv(file_path)
    except FileNotFoundError:
        return {"error": "CSV file not found"}
    except pd.errors.EmptyDataError:
        return {"error": "CSV file is empty"}

    # Beklenen sütunları kontrol et
    required_columns = {'date', 'league', 'home_team', 'away_team', 'score'}
    if not required_columns.issubset(data.columns):
        return {"error": f"CSV file must contain the following columns: {required_columns}"}

    try:
        # Skorları ayrıştır ve int'e çevir
        data[['home_score', 'away_score']] = data['score'].str.replace("–", "-").str.split('-', expand=True).astype(int)
    except ValueError:
        return {"error": "Score column contains invalid values"}

    # Sonuç sütunu ekle
    data['result'] = data.apply(lambda row: 'home_win' if row['home_score'] > row['away_score'] else
                                ('away_win' if row['away_score'] > row['home_score'] else 'draw'), axis=1)

    # Gerekli sütunları seç
    features = data[['home_team', 'away_team', 'home_score', 'away_score']].copy()
    target = data['result']

    # Takım adlarını sayısal değerlere dönüştür
    team_mapping = {team: idx for idx, team in enumerate(pd.concat([data['home_team'], data['away_team']]).unique())}
    features.loc[:, 'home_team'] = features['home_team'].map(team_mapping)
    features.loc[:, 'away_team'] = features['away_team'].map(team_mapping)

    return {"features": features, "target": target, "team_mapping": team_mapping}

def train_model(features, target):
    # Eğitim ve test verilerini ayır
    X_train, X_test, y_train, y_test = train_test_split(features, target, test_size=0.2, random_state=42)

    # Modeli eğit
    model = RandomForestClassifier(n_estimators=100, random_state=42)
    model.fit(X_train, y_train)

    # Test seti ile doğrulama yap
    predictions = model.predict(X_test)
    accuracy = accuracy_score(y_test, predictions)

    return model

def predict_match(model, team_mapping, home_team, away_team):
    home_team_id = team_mapping.get(home_team, -1)
    away_team_id = team_mapping.get(away_team, -1)

    # Takım isimlerini kontrol et
    if home_team_id == -1 or away_team_id == -1:
        return {
            "error": "Invalid team name(s). Please check the team names and try again.",
            "invalid_teams": {
                "home_team": home_team if home_team_id == -1 else None,
                "away_team": away_team if away_team_id == -1 else None
            }
        }

    # Modelin beklediği sütun isimleriyle uyumlu bir DataFrame oluştur
    input_data = pd.DataFrame([[home_team_id, away_team_id, 0, 0]],
                              columns=['home_team', 'away_team', 'home_score', 'away_score'])

    prediction = model.predict(input_data)[0]
    probabilities = model.predict_proba(input_data)[0]

    return {
        "prediction": prediction,
        "probabilities": {
            "home_win": probabilities[0],
            "draw": probabilities[1],
            "away_win": probabilities[2]
        }
    }


if __name__ == "__main__":
    if len(sys.argv) < 3:
        print(json.dumps({"error": "Please provide home and away team names"}))
        sys.exit(1)

    home_team = sys.argv[1]
    away_team = sys.argv[2]

    csv_path = "all_data.csv"
    preprocess_result = preprocess_data(csv_path)

    if "error" in preprocess_result:
        print(json.dumps(preprocess_result))
        sys.exit(1)

    features = preprocess_result["features"]
    target = preprocess_result["target"]
    team_mapping = preprocess_result["team_mapping"]

    model = train_model(features, target)

    # Tahmin ve doğruluk bilgilerini tek bir JSON nesnesinde birleştirin
    accuracy = accuracy_score(target, model.predict(features))
    result = predict_match(model, team_mapping, home_team, away_team)
    result["model_accuracy"] = accuracy

    # Sadece JSON döndür
    print(json.dumps(result))
