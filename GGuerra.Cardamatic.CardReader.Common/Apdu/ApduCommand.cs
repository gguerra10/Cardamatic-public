using System.Text;

namespace GGuerra.Cardamatic.CardReader.Common.Apdu
{
    public class ApduCommand
    {
        /// <summary>
        /// Minimun size in bytes of an APDU command
        /// </summary>
        private const int APDU_MIN_LENGTH = 4;

        private readonly byte cla;
        private readonly byte ins;
        private readonly byte p1;
        private readonly byte p2;
        private readonly byte[] data;
        private byte le;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cla">Class byte</param>
        /// <param name="ins">Instruction byte</param>
        /// <param name="p1">Parameter P1 byte</param>
        /// <param name="p2">Parameter P2 byte</param>
        /// <param name="data">Data to send to the card if any, null if no data to send</param>
        /// <param name="le">Number of data expected, 0 if none</param>
        public ApduCommand(byte cla, byte ins, byte p1, byte p2, byte[] data, byte le)
        {
            this.cla = cla;
            this.ins = ins;
            this.p1 = p1;
            this.p2 = p2;
            this.data = data;
            this.le = le;
        }


        /// <summary>
        /// Class get property
        /// </summary>
        public byte Class => cla;


        /// <summary>
        /// Instruction get property
        /// </summary>
        public byte Ins => ins;


        /// <summary>
        /// Parameter P1 get property
        /// </summary>
        public byte P1 => p1;


        /// <summary>
        /// Parameter P2 get property
        /// </summary>
        public byte P2 => p2;



        /// <summary>
        /// Data get property
        /// </summary>
        public byte[] Data => data;


        /// <summary>
        /// Length expected get property
        /// </summary>
        public byte Le => le;


        /// <summary>
        /// Overrides the ToString method to format the APDUCommand object as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strData = null;

            if (data != null)
            {
                StringBuilder sb = new StringBuilder(data.Length * 2);

                for (int i = 0; i < data.Length; i++)
                {
                    sb.AppendFormat("{0:X02}", data[i]);
                }

                strData = "Data: " + sb.ToString();
            }

            StringBuilder sbApdu = new StringBuilder();
            sbApdu.AppendFormat("Cla: {0:X02} Ins: {1:X02} P1: {2:X02} P2: {3:X02} ", cla, ins, p1, p2);

            if (data != null)
            {
                var strlen = $"Lc: {data.Length:X02} ";
                sbApdu.Append(strlen);
                sbApdu.Append(strData);
            }

            if (le != 0xFF)
            {
                sbApdu.Append($" Le: {le:X02}");
            }

            return sbApdu.ToString();
        }

        /// <summary>
        /// ToArray method to return byte[] represented by the APDUCommand object
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            byte[] buffer = null;

            if (this.Data != null && this.Data.Length > 0)
            {
                if (this.Le != 0xFF)
                {
                    buffer = new byte[APDU_MIN_LENGTH + 1 + this.Data.Length + 1];
                    buffer[APDU_MIN_LENGTH + 1 + this.Data.Length] = this.Le;
                }
                else
                {
                    buffer = new byte[APDU_MIN_LENGTH + 1 + this.Data.Length];
                }

                buffer[APDU_MIN_LENGTH] = (byte)this.Data.Length;

                for (int i = 0; i < this.Data.Length; i++)
                {
                    buffer[APDU_MIN_LENGTH + 1 + i] = this.Data[i];
                }
            }
            else
            {
                if (this.Le != 0xFF)
                {
                    buffer = new byte[APDU_MIN_LENGTH + 1];
                    buffer[4] = this.Le;
                }
                else
                {
                    buffer = new byte[APDU_MIN_LENGTH];
                }
            }

            buffer[0] = this.Class;
            buffer[1] = this.Ins;
            buffer[2] = this.P1;
            buffer[3] = this.P2;

            return buffer;
        }
    }
}
