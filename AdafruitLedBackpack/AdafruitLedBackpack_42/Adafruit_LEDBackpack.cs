using System;
using Microsoft.SPOT;

namespace Gadgeteer.Modules.Efelunte
{
    public class Adafruit_LEDBackpack : Adafruit_GFX
    {

        public const byte HT16K33_BLINK_CMD = 0x80;
        public const byte HT16K33_BLINK_DISPLAYON = 0x01;

        public const byte HT16K33_BLINK_OFF = 0;
        public const short HT16K33_BLINK_2HZ = 1;
        public const short HT16K33_BLINK_1HZ = 2;
        public const short HT16K33_BLINK_HALFHZ = 3;

        public const short HT16K33_CMD_BRIGHTNESS = 0xE0;

        public const ushort LED_ON = 1;
        public const ushort LED_OFF = 0;

        public const ushort LED_RED = 1;
        public const ushort LED_YELLOW = 2;
        public const ushort LED_GREEN = 3;

        private Wire wire;
        private byte i2c_addr;
        private Socket socket;

        protected ushort[] displaybuffer;


        public Adafruit_LEDBackpack(Socket _socket, byte _addr = 0x70)
        {
            i2c_addr = _addr;
            socket = _socket;

            displaybuffer = new ushort[8];

            wire = new Wire(socket, i2c_addr);

            clear();

        }

        public void setBrightness(byte b) {

            if (b > 15) b = 15;
            wire.beginTransmission();
            wire.write((byte)(HT16K33_CMD_BRIGHTNESS | b));
            wire.endTransmission();  
        }

        public void blinkRate(byte b) {

            wire.beginTransmission();
    
            if (b > 3) b = 0; // turn off if not sure
  
            wire.write((byte)(HT16K33_BLINK_CMD | HT16K33_BLINK_DISPLAYON | (b << 1))); 
            wire.endTransmission();

        }

        public void turnOnOscillator() {

            wire.beginTransmission();
            wire.write(0x21);  // turn on oscillator
            wire.endTransmission();
        }

        public void begin() {

            turnOnOscillator();

            blinkRate(HT16K33_BLINK_OFF);
  
            setBrightness(15); // max brightness

        }

        public void printDisplay()
        {
            for (byte i = 0; i < 8; i++)
            {
                Debug.Print("Buffer[" + i + "]:" + displaybuffer[i]);
            }
        }

        public void writeDisplay()
        {

            wire.beginTransmission();

            wire.write((byte)0x00); // start at address $00

            for (byte i = 0; i < 8; i++)
            {
                wire.write((byte)(displaybuffer[i] & 0xFF));
                wire.write((byte)(displaybuffer[i] >> 8));
            }

            wire.endTransmission();

        }

        public void clear()
        {
            for (byte i = 0; i < 8; i++)
            {
                displaybuffer[i] = 0;
            }
        }

    }
}
