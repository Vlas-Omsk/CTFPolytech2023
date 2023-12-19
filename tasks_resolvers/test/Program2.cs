namespace test
{
    internal class Program2
    {
        static void Main(string[] args)
        {
            var key = File.ReadAllText(@"Z:\Users\melon\Downloads\xor_with_ez\key.txt").Replace("\n", " ").Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt32(x, 16)).ToArray();
            int rounds;

            try
            {
                rounds = int.Parse(args[0]);
            }
            catch
            {
                rounds = 1;
            }

            using (StreamWriter outStream = new StreamWriter("flag.txt"))
            {
                string flag = File.ReadAllText(@"Z:\Users\melon\Downloads\xor_with_ez\out.txt");

                var d = new List<int>();

                for (int i = 0; i < flag.Length - 1; i += 2)
                {
                    var kk = flag[i..(i + 2)];

                    var kkk = Convert.ToInt32(kk, 16);

                    for (int o = 0; o < rounds; o++)
                    {
                        foreach (int k in key)
                        {
                            kkk = kkk ^ k;
                        }
                    }
                    
                    var dd = (char)kkk;

                    Console.Write(dd);

                    //outStream.Write(byteValue.ToString("X"));
                }
            }
        }
    }
}