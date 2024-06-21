import logging
from flask import Flask, request, jsonify
from PIL import Image
import pytesseract
import io
import cv2
import numpy as np
import base64
from .text_formatter import clean_text
from .translation_service import translate_text

api_key = "api_key"

def wrap_text(text, font, font_scale, thickness, max_width):
    words = text.split(' ')
    lines = []
    current_line = ''
    for word in words:
        size = cv2.getTextSize(current_line + word, font, font_scale, thickness)[0]
        if size[0] > max_width:
            lines.append(current_line)
            current_line = f"{word} " 
        else:
            current_line += f"{word} "
    lines.append(current_line)
    return lines

def extract_text_from_image(request):
    if 'image' not in request.files:
        return jsonify({'error': 'No image provided'}), 400
    
    image_file = request.files['image']
    image_bytes = image_file.read()
    image = Image.open(io.BytesIO(image_bytes))

    image_np = np.array(image)
    image_np = cv2.cvtColor(image_np, cv2.COLOR_RGB2BGR)

    font = cv2.FONT_HERSHEY_SIMPLEX
    font_scale = 1
    thickness = 2

    gray = cv2.cvtColor(image_np, cv2.COLOR_BGR2GRAY)
    blur = cv2.GaussianBlur(gray, (7, 7), 0)
    th3 = cv2.adaptiveThreshold(blur, 255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C,
                                cv2.THRESH_BINARY_INV, 11, 2)
    kernel = cv2.getStructuringElement(cv2.MORPH_RECT, (5, 5))
    dilate = cv2.dilate(th3, kernel, iterations=4)

    cnts, _ = cv2.findContours(dilate, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
    cnts = sorted(cnts, key=lambda x: cv2.boundingRect(x)[0])
    
    combined_text = ""
    for c in cnts:
        x, y, w, h = cv2.boundingRect(c)
        if h > 200 and w > 20:
            roi = image_np[y:y+h, x:x+w]
            roi_pil = Image.fromarray(cv2.cvtColor(roi, cv2.COLOR_BGR2RGB))
            text = pytesseract.image_to_string(roi_pil, lang='ron')
            translated_text = translate_text(text, api_key)
            cleaned_text = clean_text(translated_text)
            combined_text += cleaned_text + " "
    
    wrapped_text = wrap_text(combined_text, font, font_scale, thickness, image_np.shape[1])

    line_height = cv2.getTextSize('A', font, font_scale, thickness)[0][1] + 10
    text_block_height = line_height * len(wrapped_text)

    y_start = (image_np.shape[0] - text_block_height) // 2

    overlay = image_np.copy()
    for i, line in enumerate(wrapped_text):
        text_size = cv2.getTextSize(line, font, font_scale, thickness)[0]
        x_start = (image_np.shape[1] - text_size[0]) // 2
        y_pos = y_start + i * line_height

        rectangle_bgr = (255, 255, 255) 
        rectangle_opacity = 0.6
        sub_img = overlay[y_pos - text_size[1] - 10:y_pos + 10, x_start - 10:x_start + text_size[0] + 10]
        white_rect = np.ones(sub_img.shape, dtype=np.uint8) * 255
        res = cv2.addWeighted(sub_img, 1 - rectangle_opacity, white_rect, rectangle_opacity, 1.0)
        overlay[y_pos - text_size[1] - 10:y_pos + 10, x_start - 10:x_start + text_size[0] + 10] = res

        cv2.putText(overlay, line, (x_start, y_pos), font, font_scale, (0, 0, 0), thickness, cv2.LINE_AA)
    
    alpha = 0.8
    image_np = cv2.addWeighted(overlay, alpha, image_np, 1-alpha, 0)

    _, buffer = cv2.imencode('.jpg', image_np)
    image_base64 = base64.b64encode(buffer).decode('utf-8')

    return jsonify({'image': image_base64})

app = Flask(__name__)

@app.route('/extract-text', methods=['POST'])
def extract_text():
    return extract_text_from_image(request)

if __name__ == "__main__":
    app.run(debug=True)
