using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GGuerra.Cardamatic.Extensions
{
    public static class ByteArrayExtensions
    {
        private static bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        public static byte[] GetBits(this byte[] array, uint bitsLength)
        {
            var bits = new List<byte>();
            foreach (var b in array)
            {
                for (byte i = 0; i < bitsLength; i++)
                {
                    bits.Add((byte)(IsBitSet(b, (int)(bitsLength - 1 - i)) ? 1 : 0));
                }
            }
            return bits.ToArray();
        }

        public static byte[] GetBits(this byte[] array)
        {
            var bits = new List<byte>();
            foreach (var b in array)
            {
                for (byte i = 0; i < 8; i++)
                {
                    bits.Add((byte)(IsBitSet(b, 7 - i) ? 1 : 0));
                }
            }
            return bits.ToArray();
        }

        public static byte[] FromBits(this byte[] bitArray)
        {
            var bytes = new List<byte>();
            int i;
            for (i = 0; i < bitArray.Length; i += 8)
            {
                byte mByte = 0;
                for (int b = 0; b < 8; b++)
                {
                    mByte |= (byte)(bitArray[i + 8 - b - 1] << b);
                }
                bytes.Add(mByte);
            }
            return bytes.ToArray();
        }


        public static char GetChar(this byte[] array, uint startByte)
        {
            uint result = (char)0;
            uint init = startByte * 8;
            uint end = startByte * 8 + 8;
            for (uint i = init; i < end; i++)
            {
                result <<= 1;
                result |= array.GetBits()[i];
            }

            return (char)result;
        }

        public static int GetSubValue(this byte[] array, uint startByte, uint startBit, uint endByte, uint endBit)
        {
            int result = 0;
            uint init = startByte * 8 + startBit;
            uint end = endByte * 8 + endBit;
            for (uint i = init; i < end; i++)
            {
                result <<= 1;
                result |= array.GetBits()[i];
            }

            return result;
        }

        public static long GetSubValueLong(this byte[] array, uint startByte, uint startBit, uint endByte, uint endBit)
        {
            long result = 0;
            uint init = startByte * 8 + startBit;
            uint end = endByte * 8 + endBit;
            for (uint i = init; i < end; i++)
            {
                result <<= 1;
                result |= array.GetBits()[i];
            }

            return result;
        }

        public static int GetSubValue(this byte[] array, uint startByte, uint startBit, uint bitLength)
        {
            uint startInBits = startByte * 8 + startBit;
            uint startByteCorrected = startInBits / 8;
            uint startBitCorrected = startInBits % 8;


            uint endInBits = startInBits + bitLength;
            uint endByteCorrected = endInBits / 8;
            uint endBitCorrected = endInBits % 8;

            return array.GetSubValue(startByteCorrected, startBitCorrected, endByteCorrected, endBitCorrected);
        }

        public static uint GetSubValueUnsignedInteger(this byte[] array, uint startByte, uint startBit, uint bitLength)
        {
            uint startInBits = startByte * 8 + startBit;
            uint startByteCorrected = startInBits / 8;
            uint startBitCorrected = startInBits % 8;


            uint endInBits = startInBits + bitLength;
            uint endByteCorrected = endInBits / 8;
            uint endBitCorrected = endInBits % 8;

            return array.GetSubValueUnsignedInteger(startByteCorrected, startBitCorrected, endByteCorrected, endBitCorrected);
        }

        public static uint GetSubValueUnsignedInteger(this byte[] array, uint startByte, uint startBit, uint endByte, uint endBit)
        {
            uint result = 0;
            uint init = startByte * 8 + startBit;
            uint end = endByte * 8 + endBit;
            for (uint i = init; i < end; i++)
            {
                result <<= 1;
                result |= array.GetBits()[i];
            }

            return result;
        }

        public static ushort GetSubValueUnsignedShort(this byte[] array, uint startByte, uint startBit, uint bitLength)
        {
            uint startInBits = startByte * 8 + startBit;
            uint startByteCorrected = startInBits / 8;
            uint startBitCorrected = startInBits % 8;


            uint endInBits = startInBits + bitLength;
            uint endByteCorrected = endInBits / 8;
            uint endBitCorrected = endInBits % 8;

            return array.GetSubValueUnsignedShort(startByteCorrected, startBitCorrected, endByteCorrected, endBitCorrected);
        }

        public static ushort GetSubValueUnsignedShort(this byte[] array, uint startByte, uint startBit, uint endByte, uint endBit)
        {
            ushort result = 0;
            uint init = startByte * 8 + startBit;
            uint end = endByte * 8 + endBit;
            for (uint i = init; i < end; i++)
            {
                result <<= 1;
                result |= array.GetBits()[i];
            }

            return result;
        }

        public static short GetSubValueShort(this byte[] array, uint startByte, uint startBit, uint bitLength)
        {
            uint startInBits = startByte * 8 + startBit;
            uint startByteCorrected = startInBits / 8;
            uint startBitCorrected = startInBits % 8;


            uint endInBits = startInBits + bitLength;
            uint endByteCorrected = endInBits / 8;
            uint endBitCorrected = endInBits % 8;

            return (short)array.GetSubValueUnsignedShort(startByteCorrected, startBitCorrected, endByteCorrected, endBitCorrected);
        }



        public static long GetLongSubValue(this byte[] array, uint startByte, uint startBit, uint bitLength)
        {
            uint startInBits = startByte * 8 + startBit;
            uint startByteCorrected = startInBits / 8;
            uint startBitCorrected = startInBits % 8;


            uint endInBits = startInBits + bitLength;
            uint endByteCorrected = endInBits / 8;
            uint endBitCorrected = endInBits % 8;

            return array.GetSubValueLong(startByteCorrected, startBitCorrected, endByteCorrected, endBitCorrected);
        }

        public static string GetStringSubValue(this byte[] array, uint startByte, uint byteLength)
        {
            var result = new StringBuilder();
            for (uint i = startByte; i < startByte + byteLength; i++)
            {
                if (array[i] != 0)
                {
                    result.Append((char)array[i]);
                }
            }

            return result.ToString();
        }

        public static string ToHexString(this byte[] array)
        {
            return BitConverter.ToString(array).Replace("-", "");
        }

        public static byte[] RotateLeft(this byte[] input)
        {
            var output = new byte[input.Length];
            Array.Copy(input, 1, output, 0, input.Length - 1);
            output[input.Length - 1] = input[0];
            return output;
        }

        public static byte[] RotateRight(this byte[] input)
        {
            var output = new byte[input.Length];
            Array.Copy(input, 0, output, 1, input.Length - 1);
            output[0] = input[input.Length - 1];
            return output;
        }

        public static byte[] GetInitialBytes(this byte[] input, int length)
        {
            var output = new byte[length];
            if (length < input.Length)
            {
                Array.Copy(input, 0, output, 0, length);
            }
            else
            {
                output = input;
            }
            return output;
        }

        public static byte[] GetLastBytes(this byte [] input, int length)
        {
            var output = new byte[length];
            if (length < input.Length)
            {
                Array.Copy(input, input.Length - length, output, 0, length);
            }
            else
            {
                output = input;
            }
            return output;
        }

        private static byte[] Xor(this byte[] data, byte[] data2)
        {
            var len = data.Length > data2.Length ? data.Length : data2.Length;
            var result = new byte[len];
            for (int i = 0; i < len; i++)
            {
                if (i < data.Length && i < data2.Length)
                {
                    result[i] = (byte)(data[i] ^ data2[i]);
                }
                else if (i < data.Length)
                {
                    result[i] = data[i];
                }
                else
                {
                    result[i] = data2[i];
                }
            }
            return result;
        }

        public static byte[] GetRandomArray(int length)
        {
            var random = RandomNumberGenerator.Create();
            var rnd = new byte[length];
            random.GetBytes(rnd);
            return rnd;
        }
    }
}
