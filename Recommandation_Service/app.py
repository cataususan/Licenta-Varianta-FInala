from flask import Flask
from flask_cors import CORS
from flask import jsonify
from loger import logger_config
from routes.recommendRestaurants import restaurantRecommandation_bp
from routes.recommendBars import barRecommandation_bp
from routes.recommendMuseums import museumRecommandation_bp
from routes.recommendEvents import eventRecommandation_bp
logger_config.setup_logging()

app = Flask(__name__)
CORS(app)

app.register_blueprint(restaurantRecommandation_bp)
app.register_blueprint(barRecommandation_bp)
app.register_blueprint(museumRecommandation_bp)
app.register_blueprint(eventRecommandation_bp)
if __name__ == '__main__':
    app.run(debug=True)