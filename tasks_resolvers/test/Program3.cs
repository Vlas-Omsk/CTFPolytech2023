using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    internal sealed class Program3
    {
        private static void Main()
        {
            using var tcpClient = new TcpClient("polytech2023.ru", 4000);
            var stream = tcpClient.GetStream();
            var buffer = new byte[1024];

            while (true)
            {
                var readedLength = stream.Read(buffer, 0, buffer.Length);

                var str = Encoding.UTF8.GetString(buffer, 0, readedLength);

                var lines = str.Split(new char[] { '\n' });

                var matrixLines = lines
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Where(x => x[0] == '⎡' || x[0] == '⎢' || x[0] == '⎣')
                    .Select(x => x[1..(x.Length - 1)])
                    .ToArray();

                if (matrixLines.Any())
                {
                    var matrix = MatrixCreate(matrixLines.Length, matrixLines.Length);

                    var j = 0;
                    foreach (var line in matrixLines)
                    {
                        var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        for (var i = 0; i < split.Length; i++)
                        {
                            matrix[j, i] = float.Parse(split[i]);
                        }

                        j++;
                    }
                    
                    var det = (int)Math.Round(DeterminantGaussElimination(matrix));

                    stream.Write(Encoding.UTF8.GetBytes(det + "\n"));
                }
                else if (!str.Contains("Правильный"))
                {
                    Console.WriteLine(str);
                }

                static double DeterminantGaussElimination(double[,] matrix)
                {
                    int n = int.Parse(System.Math.Sqrt(matrix.Length).ToString());
                    int nm1 = n - 1;
                    int kp1;
                    double p;
                    double det = 1;
                    for (int k = 0; k < nm1; k++)
                    {
                        kp1 = k + 1;
                        for (int i = kp1; i < n; i++)
                        {
                            p = matrix[i, k] / matrix[k, k];
                            for (int j = kp1; j < n; j++)
                                matrix[i, j] = matrix[i, j] - p * matrix[k, j];
                        }
                    }
                    for (int i = 0; i < n; i++)
                        det = det * matrix[i, i];
                    return det;

                }

                //Console.Write(str);

                //if (lines[^1].Contains('='))
                //{
                //    var answer = lines[^1].TrimEnd('=', ' ').EvalNumerical().Stringize();

                //    Console.WriteLine(answer);

                //    stream.Write(Encoding.UTF8.GetBytes(answer + "\n"));
                //}

                
            }
        }

        // --------------------------------------------------------------------------------------------------------------
        private static double[,] MatrixCreate(int rows, int cols)
        {
            // allocates/creates a matrix initialized to all 0.0. assume rows and cols > 0
            // do error checking here
            double[,] result = new double[rows, cols];
            return result;
        }

        // --------------------------------------------------------------------------------------------------------------
        
        
    }
}
