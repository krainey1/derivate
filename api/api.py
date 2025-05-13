import os
from dotenv import load_dotenv
from flask import Flask, jsonify
import json
from flask_cors import CORS
from openai import OpenAI

app = Flask(__name__)
CORS(app)

load_dotenv() #grab my api key from the ether 
client = OpenAI(api_key = os.getenv("API_KEY"))

#only serving the most interesting content
@app.route("/")
def testpoint():
    return "bruh this on?"

@app.route("/generateConstant", methods = ['GET']) #omg I finally have a reason to use get over post, we arent sending stuff in the request!
def genCon():
    prompt = """
    Create a multiple choice question involving the derivative of a constant. 
    Include:
    - A question in the exact format: "d/dx (expression)?" where expression is random expression - pick a random number for the constant between 1 and 1000
    - Three answer choices (A-C), only one correct - ensure each choice is unique.
    - Clearly indicate the correct answer letter. Assign the correct answer to one of the three letter choices.

    Return it as JSON like:
    {
      "question": "...",
      "choices": ["...", "...", "..."],
      "correct": "B"
    } 
    
    """
    response = client.chat.completions.create(
        model="gpt-4",
        messages=[{"role": "user", "content": prompt}],
        temperature=0.9 #controls randomness
    )
    resp = json.loads(response.choices[0].message.content)
    return jsonify(resp) #the best func for making a json response

@app.route("/generateTore", methods = ['GET']) #omg I finally have a reason to use get over post, we arent sending stuff in the request!
def gentore():
    prompt = """
    Create a multiple choice question involving using just the derivative of a trig expression or the exponential function e^x (no product rule, quotient rule, chain rule, etc.). 
    Include:
    - A question in the exact format: "d/dx (expression)?" where expression is one of: sin(x), cos(x), tan(x), csc(x), sec(x), cot(x), e^x, ln(x) 
    - Three answer choices (A-C), only one correct - ensure each choice is unique.
    - Clearly indicate the correct answer letter. Assign the correct answer to one of the three letter choices.

    Return it as JSON like:
    {
      "question": "...",
      "choices": ["...", "...", "..."],
      "correct": "B"
    } 
    
    """
    response = client.chat.completions.create(
        model="gpt-4",
        messages=[{"role": "user", "content": prompt}],
        temperature=0.9 #controls randomness
    )
    resp = json.loads(response.choices[0].message.content)
    return jsonify(resp) #the best func for making a json response

@app.route("/generatePower", methods = ['GET']) #omg I finally have a reason to use get over post, we arent sending stuff in the request!
def genPow():
    prompt = """
    Create a multiple choice question involving the derivative of a an expression using just the power rule. 
    Include:
    - A question in the exact format: "d/dx (expression)?" where expression is a basic random expression ex: x^2, x, 3x^5, etc. where you can use the power rule to evaluate the derivative (no product rule, quotient rule, chain rule, etc.).
    Pick a power between (0 AND 100) and base between (1 and 500)
    - Three answer choices (A-C), only one correct - ensure each choice is unique.
    - Clearly indicate the correct answer letter. Assign the correct answer to one of the three letter choices.

    Return it as JSON like:
    {
      "question": "...",
      "choices": ["...", "...", "..."],
      "correct": "B"
    } 
    
    """
    response = client.chat.completions.create(
        model="gpt-4",
        messages=[{"role": "user", "content": prompt}],
        temperature=1.0 #controls randomness
    )
    resp = json.loads(response.choices[0].message.content)
    return jsonify(resp) #the best func for making a json response

#living for boilerplate 
if __name__=='__main__':
    app.run(debug=True)