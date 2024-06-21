from flask import Blueprint, request, jsonify
from services.bar_recomandation_service import recommend_bar
barRecommandation_bp = Blueprint('recommendBars', __name__)

@barRecommandation_bp.route('/recommendBar', methods=['POST'])
def recommend():
    prefference=request.json.get('preference')
    recommandation = recommend_bar(prefference)
    return jsonify({'recommandation': recommandation})