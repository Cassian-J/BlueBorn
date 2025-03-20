import json
import time
import RPi.GPIO as GPIO

# Configuration GPIO
joystick_pins = {
    "player1_up": 17,
    "player1_down": 27,
    "player1_left": 22,
    "player1_right": 23,
    "player2_up": 22,
    "player2_down": 23,
    "player2_left": 24,
    "player2_right": 25,
    "button1_p1": 5,
    "button2_p1": 6,
    "button1_p2": 13,
    "button2_p2": 19
}

GPIO.setmode(GPIO.BCM)
for pin in joystick_pins.values():
    GPIO.setup(pin, GPIO.IN, pull_up_down=GPIO.PUD_UP)

# Boucle principale
try:
    while True:
        # Lire l'état des joysticks et boutons
        data = {key: not GPIO.input(pin) for key, pin in joystick_pins.items()}  # 0 = Pressé

        # Sauvegarde en JSON
        with open("/home/pi/arcade_inputs.json", "w") as file:
            json.dump(data, file)

        time.sleep(0.05)

except KeyboardInterrupt:
    print("Arrêt du programme")
    GPIO.cleanup()