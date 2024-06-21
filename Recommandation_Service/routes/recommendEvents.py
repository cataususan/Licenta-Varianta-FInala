from flask import Blueprint, request, jsonify
from services.event_recomandation_service import recommend_event
eventRecommandation_bp = Blueprint('recommendEvents', __name__)

@eventRecommandation_bp.route('/recommendEvent', methods=['POST'])
def recommend():
    prefference=request.json.get('preference')
    recommandation = recommend_event(prefference)
    return jsonify({'recommandation': recommandation})