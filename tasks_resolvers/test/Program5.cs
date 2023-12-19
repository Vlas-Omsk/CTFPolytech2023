using System;
using System.Collections;
using System.Text;

namespace test
{
    internal sealed class Program5
    {
        private static void Main(string[] args)
        {
            var enc = File.ReadAllText(@"Z:\Users\melon\Downloads\long_bytes\out.txt");

            var flag = "tech";
            var flagNum = 0;

            for (var i = 0; i < flag.Length; i++)
            {
                flagNum = flagNum | ((byte)(flag[i]) << 7 * i);

                var bitsss = new BitArray(new int[] { flagNum });

                foreach (bool bit in bitsss)
                {
                    Console.Write(bit ? '1' : '0');
                }

                Console.WriteLine();
            }

            var bytes = new List<byte>();

            for (int i = 0; i < enc.Length - 1; i += 2)
            {
                var kk = enc[i..(i + 2)];

                var kkk = Convert.ToInt32(kk, 16);

                bytes.Add((byte)kkk);
            }

            //var flagout = Encoding.UTF8.GetString(bytes.Reverse<byte>().ToArray());

            var bits = new BitArray(bytes.Reverse<byte>().ToArray());

            //bits[^1] = true;

            foreach (bool bit in bits)
            {
                Console.Write(bit ? '1' : '0');
            }

            var c = (byte)'{';

            for (int i = 0; i < bits.Length - 7; i += 7)
            {
                var sbits = new BitArray(bits.OfType<bool>().Skip(i).Take(7).ToArray());

                var bytes2 = new byte[1];

                sbits.CopyTo(bytes2, 0);

                Console.Write((char)bytes2[0]);
            }
        }
    }
}
