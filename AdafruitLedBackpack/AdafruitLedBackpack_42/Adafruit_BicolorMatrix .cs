using System;
using Microsoft.SPOT;

namespace Gadgeteer.Modules.Efelunte
{
    public class Adafruit_BicolorMatrix  : Adafruit_LEDBackpack
    {

        public Adafruit_BicolorMatrix(Socket _socket) : base(_socket)
        {
            this.setDimensions(8,8);   
        }


        public void drawPixel(short x, short y, ushort color)
        {

            if ((y < 0) || (y >= 8)) return;
            if ((x < 0) || (x >= 8)) return;

            switch (getRotation())
            {
                case 1:
                    swap(ref x, ref y);
                    x = (short)(8 - x - 1);
                    break;
                case 2:
                    x = (short)(8 - x - 1);
                    y = (short)(8 - y - 1);
                    break;
                case 3:
                    swap(ref x, ref y);
                    y = (short)(8 - y - 1);
                    break;
            }

            if (color == Adafruit_LEDBackpack.LED_GREEN)
            {
                // Turn on green LED.
                displaybuffer[y] |= (ushort)(1 << x);
                // Turn off red LED.
                displaybuffer[y] &= (ushort)~(1 << (x + 8));
            }
            else if (color == Adafruit_LEDBackpack.LED_RED)
            {
                // Turn on red LED.
                displaybuffer[y] |= (ushort)(1 << (x + 8));
                // Turn off green LED.
                displaybuffer[y] &= (ushort)~(1 << x);
            }
            else if (color == Adafruit_LEDBackpack.LED_YELLOW)
            {
                // Turn on green and red LED.
                displaybuffer[y] |= (ushort)((1 << (x + 8)) | (1 << x));
            }
            else if (color == Adafruit_LEDBackpack.LED_OFF)
            {
                // Turn off green and red LED.
                displaybuffer[y] &= (ushort)(~(1 << x) & ~(1 << (x + 8)));
            }
        }

    }
}
