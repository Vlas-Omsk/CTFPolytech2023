using AngouriMath.Extensions;
using System.Net.Sockets;
using System.Text;

namespace EasterEggResolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var tcpClient = new TcpClient("polytech2023.ru", 12345);
            var stream = tcpClient.GetStream();
            var buffer = new byte[1024];

            while (true)
            {
                var readedLength = stream.Read(buffer, 0, buffer.Length);

                var str = Encoding.UTF8.GetString(buffer, 0, readedLength);

                var lines = str.Split(new char[] { '\n' });
                
                Console.Write(str);

                if (lines[^1].Contains('='))
                {
                    var answer = lines[^1].TrimEnd('=', ' ').EvalNumerical().Stringize();

                    Console.WriteLine(answer);

                    stream.Write(Encoding.UTF8.GetBytes(answer + "\n"));
                }
            }
        }
    }
}