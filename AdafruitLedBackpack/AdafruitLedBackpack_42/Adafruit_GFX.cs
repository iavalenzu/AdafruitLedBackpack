using System;
using Microsoft.SPOT;

namespace Gadgeteer.Modules.Efelunte
{
    public class Adafruit_GFX
    {

        private short _width, _height;
        private ushort rotation;

        public Adafruit_GFX()
        {
            _width = 0;
            _height = 0;
            rotation = 0;
        }

        public void setDimensions(short w, short h) {
            _width = w;
            _height = h;
        }

        // This MUST be defined by the subclass:
        public virtual void drawPixel(short x, short y, ushort color) { 
        }

        public ushort getRotation() {
            return rotation;    
        }

        public void setRotation(ushort _rotation)
        {
            rotation = _rotation;
        }

        public void swap(ref short x, ref short y)
        {
            short tmp;
            tmp = x;
            x = y;
            y = tmp;
        
        }

        public void drawBitmap(short x, short y, byte[] bitmap, short w, short h, ushort color) {

            byte tmp;
            int cond;
            short i, j, byteWidth = (short)((w + 7) / 8);

            for(j=0; j<h; j++) {
                for(i=0; i<w; i++ ) {

                    tmp = bitmap[j * byteWidth + i / 8];

                    cond = tmp & (128 >> (i & 7)); 

                    if(cond > 0){
                        drawPixel((short)(x + i), (short)(y + j), color);
                    }

                    /*
                     *   if(pgm_read_byte(bitmap + j * byteWidth + i / 8) & (128 >> (i & 7))) {
	                 *       drawPixel((short)(x+i), (short)(y+j), color);
                     *   }
                     *
                     */

                }
            }
        }


    }
}
