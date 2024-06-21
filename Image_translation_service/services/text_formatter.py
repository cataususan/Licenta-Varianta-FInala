import re
from nltk.corpus import words
from nltk.tokenize import word_tokenize
import nltk

nltk.download('punkt')
nltk.download('words')
nltk.download('averaged_perceptron_tagger')

english_vocab = set(words.words())

def filter_words(token):
    return token in english_vocab or re.match(r"^[a-zA-Z]{2,}$", token)

def clean_text(text):
    text = text.lower()
    
    tokens = word_tokenize(text)
    
    filtered_tokens = [token for token in tokens if filter_words(token)]

    cleaned_text = ' '.join(filtered_tokens)
    
    return cleaned_text
