from flask import Blueprint, request, jsonify
from services.museum_recomandation_service import recommend_museum
museumRecommandation_bp = Blueprint('recommendMuseums', __name__)

@museumRecommandation_bp.route('/recommendMuseum', methods=['POST'])
def recommend():
    prefference=request.json.get('preference')
    recommandation = recommend_museum(prefference)
    return jsonify({'recommandation': recommandation})