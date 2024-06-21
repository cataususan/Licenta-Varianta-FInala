from flask import Flask
from routes.extract_text import extract_text_bp
from flask_cors import CORS
from loger import logger_config
from flask import jsonify

app = Flask(__name__)

logger_config.setup_logging()

CORS(app)

app.register_blueprint(extract_text_bp)

if __name__ == '__main__':
    app.run(debug=True)