from flask import Blueprint, request, jsonify
from services.text_extraction_service import extract_text_from_image

extract_text_bp = Blueprint('extract_text', __name__)

@extract_text_bp.route('/extract-text', methods=['POST'])
def extract_text():
    return extract_text_from_image(request)
