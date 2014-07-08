using System;
using System.Collections;

using Microsoft.SPOT;

using GTI = Gadgeteer.Interfaces;

namespace Gadgeteer.Modules.Efelunte
{
    public class Wire
    {
        private GTI.I2CBus i2c;

        private Socket socket;
        private byte i2c_addr;
        private int clockRateKhz;
        private int timeout;

        private ArrayList buffer;

        public Wire(Socket _socket, byte _i2c_addr, int _clockRateKhz = 50, int _timeout = 100)
        {
            socket = _socket;
            i2c_addr = _i2c_addr;
            clockRateKhz = _clockRateKhz;
            timeout = _timeout;

            buffer = new ArrayList();

            i2c = new GTI.I2CBus(socket, i2c_addr, clockRateKhz, null); 
        }

        public void beginTransmission()
        {
            /*
             * Clear buffer
             */

            buffer.Clear();

        }

        public void write(byte p)
        {
            /*
             * Add p to buffer
             */

            buffer.Add(p);
        }

        public void endTransmission()
        {
            /*
             * Write buffer on i2c
             */

            byte[] data = (byte[])buffer.ToArray(typeof(byte));

            int n = i2c.Write(data, timeout);

            Debug.Print("Enviados: " + n);

        }
    }
}
