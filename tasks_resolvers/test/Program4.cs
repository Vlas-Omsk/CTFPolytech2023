using SkiaSharp;
using System.Net.Sockets;
using System.Text;
using Tesseract;

namespace test
{
    internal sealed class Program4
    {
        public static void Main(string[] args)
        {
            using var tcpClient = new TcpClient("polytech2023.ru", 12346);
            var stream = tcpClient.GetStream();
            var buffer = new byte[9024];

            while (true)
            {
                var readedLength = stream.Read(buffer, 0, buffer.Length);

                var str = Encoding.UTF8.GetString(buffer, 0, readedLength);

                var lines = str.Split(new char[] { '\n' });

                var captchaLines = lines
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Where(x => x.Contains('█'))
                    .ToArray();

                using var engine = new TesseractEngine(@"Z:\Users\melon\Downloads", "eng");

                engine.SetVariable("tessedit_char_whitelist", "0123456789");

                if (captchaLines.Any())
                {
                    var imageInfo = new SKImageInfo(
                        width: 100,
                        height: 100,
                        colorType: SKColorType.Rgba8888,
                        alphaType: SKAlphaType.Premul);

                    var surface = SKSurface.Create(imageInfo);

                    var canvas = surface.Canvas;

                    var i = 0;
                    foreach (var line in captchaLines)
                    {
                        var j = 0;
                        foreach (var symbol in line)
                        {
                            if (symbol == '█')
                            {
                                //canvas.DrawPoint(new SKPoint(j * 2, i * 2), SKColors.Black);
                                //canvas.DrawPoint(new SKPoint(j * 2 + 1, i * 2), SKColors.Black);
                                //canvas.DrawPoint(new SKPoint(j * 2, i * 2 + 1), SKColors.Black);
                                //canvas.DrawPoint(new SKPoint(j * 2 + 1, i * 2 + 1), SKColors.Black);
                                canvas.DrawPoint(new SKPoint(j, i), SKColors.Black);
                            }

                            j++;
                        }

                        i++;
                    }

                    SKImage snapI = surface.Snapshot();
                    SKData pngImage = snapI.Encode();

                    var fullpath = "PicName.png";

                    File.WriteAllBytes(fullpath, pngImage.ToArray());

                    using var img = Pix.LoadFromFile(fullpath);
                    using var page = engine.Process(img);

                    var text = page.GetText().Trim();

                    stream.Write(Encoding.UTF8.GetBytes(text + "\n"));

                    //Console.WriteLine(str);
                    //Console.WriteLine(text);
                }
                else
                {

                }
            }
        }
    }
}
