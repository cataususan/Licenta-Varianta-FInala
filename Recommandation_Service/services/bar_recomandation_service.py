import pandas as pd
import numpy as np
from sklearn.neighbors import NearestNeighbors
from sklearn.preprocessing import OneHotEncoder
import logging
import requests

def recommend_bar(prefference):
    logging.info('Starting recommendation process')
    logging.debug(f'Received preferences: {prefference}')

    url = 'http://localhost:8080/api/Bars'

    response = requests.get(url)
    data = response.json()
    logging.debug(f'Received data: {data}')
    data = [data] if isinstance(data, dict) else data
    data = {
        'name': [item['name'] for item in data],
        'priceRange': [item['features']['priceRange'] for item in data],
        'barAmbiance': [item['features']['barAmbiance'] for item in data],
        'barDrinkSpecialties': [item['features']['barDrinkSpecialties'] for item in data],
        'barEvent': [item['features']['barEvent'] for item in data],
        'barFoodOptions': [item['features']['barFoodOptions'] for item in data]
        
    }
    logging.debug(f'Data: {data}')
    try:
        df = pd.DataFrame(data)
        
        df_features = df.drop(columns='name')

        encoder = OneHotEncoder()
        df_encoded = encoder.fit_transform(df_features)

        user_prefferences = pd.DataFrame([prefference])

        user_prefs_encoded = encoder.transform(user_prefferences)

        nbrs = NearestNeighbors(n_neighbors=1, algorithm='auto').fit(df_encoded)
        distances, indices = nbrs.kneighbors(user_prefs_encoded)

        best_match_index = indices[0][0]
        best_match = df.iloc[best_match_index]

        best_match_dict = best_match.to_dict()
    except Exception as e:
        logging.error(f'Error in recommendation process: {e}', exc_info=True)
    logging.info('Recommending finished successfully')
    return best_match_dict