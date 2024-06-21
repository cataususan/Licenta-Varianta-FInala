import requests
def translate_text(text="Salut eu sunt catalin te iubesc", api_key="api_key", source_language="ro", target_language="en"):
    url = f"https://translation.googleapis.com/language/translate/v2?key={api_key}"
    data = {
        'q': text,
        'source': source_language,
        'target': target_language,
        'format': 'text'
    }
    response = requests.post(url, json=data)

    if response.status_code == 200:
        translated_text = response.json().get('data', {}).get('translations', [{}])[0].get('translatedText', '')
        return translated_text
    else:
        print(f"Error: {response.status_code}, Response Content: {response.content.decode('utf-8')}")
        return "Error: Unable to translate text."
