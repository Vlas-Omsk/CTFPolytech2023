using System.IO.Compression;

namespace test
{
    internal sealed class Program
    {
        private static void Main(string[] args)
        {


            while (true)
            {
                var files = Directory.GetFiles("ex").Where(x => x.EndsWith(".zip")).ToArray();

                if (files.Length != 1)
                    return;


                ZipFile.ExtractToDirectory(files[0], "ex2");

                Directory.Delete("ex", true);
                Directory.Move("ex2", "ex");
            }
        }
    }
}
