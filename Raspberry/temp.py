import random
import time
from w1thermsensor import W1ThermSensor
sensor = W1ThermSensor()



def read_temp():
    temperature = sensor.get_temperature()
    print("The temperature is %s celsius" % temperature)
    return temperature