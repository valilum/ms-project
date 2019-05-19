from flask import Flask, url_for, jsonify, redirect
from flask import request
from flask_api import status
from multiprocessing import Process
import temp
import time
import heater
import json
import shutdown

app = Flask(__name__)

@app.route('/')
def home():
    return 'hello'

@app.route('/read',methods=['GET'])
def read():
	if request.method == 'GET':
		temperature = temp.read_temp()
		
		return jsonify(temperature=str(temperature))

@app.route('/on',methods=['POST'])
def on():
    
    if request.method == 'POST':
        
        data = request.get_json()
        x = int((data['temperature']))
        print(x)

        heater.heatOn()   
        time.sleep(1) 
        #heater.heating(x)
        global p
        p = Process(target=heater.heating, args=(x,))
        p.start()
        p.join()
        return 'heating...'


@app.route('/off',methods=['GET','POST'])
def off():
    
    if request.method == 'POST':
        global p
        p.terminate()
        heater.heatOff()
        return 'turning off'

@app.route('/shutdown', methods=['POST'])
def shut():
    if request.method == 'POST':
        shutdown.shutdown_server()
        #shutdown.random()
        return 'Server shutting down...'
    

with app.test_request_context():
    print(url_for('read'))
    print(url_for('off'))
    
if __name__ == '__main__':
    app.run(host="0.0.0.0",port=80,debug=True, threaded=True)

