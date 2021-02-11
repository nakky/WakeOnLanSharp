using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WakeOnLanSharp
{
    public class WakeOnLan
    {
        public  int PortNum { get; private set; }

        UDPSender sender;

        public WakeOnLan(int portNum)
        {
            PortNum = portNum;
            sender = new UDPSender(PortNum);
        }

        public async Task SendMagicPacket(byte[] macAddress, string ip, int times = 3, int intervalMilliSec = 0)
        {
            byte[] magic = BuildMagicPacket(macAddress);
            for(int i = 0 ; i < times ; i++)
            {
                await sender.Send(ip, magic.Length, magic);
                if(intervalMilliSec > 0) await Task.Delay(intervalMilliSec);
            }
        }

        public byte[] BuildMagicPacket(byte[] macAddress)
        {
            if (macAddress.Length != 6) throw new ArgumentException();

            List<byte> magic = new List<byte>();
            for (int i = 0; i < 6; i++)
            {
                magic.Add(0xff);
            }

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    magic.Add(macAddress[j]);
                }
            }
            return magic.ToArray();
        }

        public static byte[] ParseMacAddress(string text, char[] separator = null)
        {
            if (separator == null) separator = new char[] { ':', '-' };
            string[] tokens = text.Split(separator);

            byte[] bytes = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                bytes[i] = Convert.ToByte(tokens[i], 16);
            }
            return bytes;
        }
    }
}
