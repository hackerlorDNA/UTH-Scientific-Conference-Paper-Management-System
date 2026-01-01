from flask import Flask, jsonify, request
import time

app = Flask(__name__)

@app.route('/check-grammar', methods=['POST'])
def check_grammar():
    # Giả lập xử lý AI
    data = request.json
    text = data.get('text', '')
    
    # Mock response
    return jsonify({
        "success": True,
        "original_text": text,
        "corrected_text": text + " (Checked by AI)",
        "suggestions": ["Consider rephrasing sentence 2."]
    })

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)