from flask import Blueprint, request, jsonify
from services.restaurant_recomandation_service import recommend_restaurant
restaurantRecommandation_bp = Blueprint('recommendRestaurants', __name__)

@restaurantRecommandation_bp.route('/recommendRestaurant', methods=['POST'])
def recommend():
    prefference=request.json.get('preference')
    recommandation = recommend_restaurant(prefference)
    return jsonify({'recommandation': recommandation})