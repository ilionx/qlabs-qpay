import nxppy
import time
import json
import urllib.request
import datetime
import os.path
import RPi.GPIO as GPIO

# Make sure this points towards your own hosted API
url = "http://192.168.1.10:58938/api/Terminal"

card = nxppy.Mifare()
bool = 'false'  # json returns a string

currentCard = 'none'

GPIO.setmode(GPIO.BOARD)
GPIO.setwarnings(False)

# pin = pin number based on the board (pin40 = gpio pin 21 etc.)
def singleBlinkLed(pin, timer):
    GPIO.setup(pin, GPIO.OUT)
    GPIO.output(pin, 1)
    time.sleep(timer)
    GPIO.output(pin, 0)
    time.sleep(timer)


def duoBlinkLed(pin1, pin2, timer):
    GPIO.setup(pin1, GPIO.OUT)
    GPIO.setup(pin2, GPIO.OUT)
    GPIO.output(pin1, 1)
    GPIO.output(pin2, 1)
    time.sleep(timer)
    GPIO.output(pin1, 0)
    GPIO.output(pin2, 0)
    time.sleep(timer)


def getSerial():
    cpuserial = "0000000000000000"
    try:
        f = open('/proc/cpuinfo', 'r')
        for line in f:
            if line[0:6] == 'Serial':
                cpuserial = line[10:26]
        f.close()
    except:
        cpuserial = "ERROR000000000"
    return cpuserial


def writeToLog(writeToLogData):
    logPath = '/home/pi/QPayReader/logs/scanLog.txt'
    if os.path.exists(logPath) == True:
        with open(logPath, 'a') as writer:  # if log exists we append the data to the log file
            writer.write('\n' + writeToLogData)
            writer.close()
    else:  # Log does not exists so we create it
        with open(logPath, 'w') as writer:
            writer.write(writeToLogData)
            writer.close()


def PostToApi(DataToPost):
    req = urllib.request.Request(url)
    req.add_header('Content-Type', 'application/json; chatset=utf-8')
    jsonData = json.dumps(DataToPost)
    jsonDataAsBytes = jsonData.encode('utf-8')
    req.add_header('Content-Length', len(jsonDataAsBytes))
    response = urllib.request.urlopen(req, jsonDataAsBytes)
    returnMessage = response.read().decode('utf-8')
    return json.loads(returnMessage)


while True:
    try:
        cardUid = card.select()
        raspberryUid = getSerial()

        print('cardUid: ' + cardUid)
        print('raspberryUid: ' + raspberryUid)
        if currentCard == 'none':
            message = {
                'CardUid': cardUid,
                'DeviceUid': raspberryUid
            }

            try:
                responseData = PostToApi(message)
                statusCode = responseData['item1']
                statusMessage = responseData['item2']
                print('Statuscode: ' + str(statusCode))
                print('Message: ' + statusMessage)
                currentCard = cardUid

                # build log string
                dateTime = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
                logData = cardUid + '\t' + dateTime

                if statusCode == 1:  # scanOk
                    # green
                    singleBlinkLed(37, 2)
                    print('scanOK')
                elif statusCode == 2:  # notEnoughBalance
                    # red
                    singleBlinkLed(40, 2)
                    print('notEnoughBalance')
                elif statusCode == 3:  # cardNotFound
                    # green + red 2sec
                    duoBlinkLed(40, 37, 2)
                    print('cardNotFound')
                elif statusCode == 4:  # terminalNotFound
                    # blink red
                    singleBlinkLed(40, 1)
                    singleBlinkLed(40, 1)
                    print('terminalNotFound')
                elif statusCode == 5:  # invalidInput
                    # blink red + green
                    duoBlinkLed(40, 37, 1)
                    duoBlinkLed(40, 37, 1)
                    print('invalidInput')
                else:
                    # Can't connect to api
                    print('checkConnection')

            except:
                print('Connection with API failed')
        else:
            print('scannedCard not reset yet')
            pass

    except nxppy.SelectError:
        currentCard = 'none'
        print('No card detected')
        pass
    time.sleep(0.5)
