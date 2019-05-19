import RPi.GPIO as GPIO
import time
import temp
channel=21

GPIO.setmode(GPIO.BCM)
GPIO.setup(channel,GPIO.OUT)

state = GPIO.input(channel)

def heatOn():
	GPIO.output(21,GPIO.HIGH)

def heatOff():
	GPIO.output(21,GPIO.LOW)

def heating(t):
	while True:
            temperature = temp.read_temp()
            print(temperature)
            if temperature >= t:
                #return redirect(url_for('off'))
                heatOff()
                return 'heat off'
            time.sleep(2)
    