
using System.Runtime.InteropServices;
using System;
using System.Threading.Tasks;
using System.Diagnostics;

public class wallpaperChanger 
{
    
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
    [DllImport("user32.dll")]
    private static extern int MessageBox(IntPtr hWnd,string text,string caption,uint type);

    private const int SPI_SETDESKWALLPAPER = 20;
    private const int SPIF_UPDATEINIFILE = 0x01;
    private const int SPIF_SENDWININICHANGE = 0x02;

    static List<string> sprites = new List<string>();
    static int frameAmmount = 6572;
    static string folderLoaction = @""; //PUT PATH TO "frames" FOLDER HERE

    static void Main(string[] args)
    {
        PopulateList();
        Message("everything is ready","info");
        Play();
        Message("clip has finnished playing","info");
    }
    static void PopulateList()  //fills the sprites list with paths to images
    {
        for(int i = 0; i < frameAmmount;i++)
        {
            string name = "output_";
            
            string sframe = i.ToString();

            int count = 4 - sframe.Length;

            for(int y =0;y < count;y++)
            {
                name += "0";
            }
            name += sframe;

            string path =folderLoaction + name+".bmp";

            Console.WriteLine("Loaded : "+ path + "   " +i+"/"+frameAmmount);

            sprites.Add(path);
        }

        Console.WriteLine("LOADING FINNISHED");
    }

    static void Play()
    {
        var t = Task.Run(async delegate
              {
                 await Task.Delay(1);
                 return 42;
              });


        for(int frame = 0; frame < frameAmmount;frame++)
        {
            string path = sprites[frame];
            SetWallpaper(path);

            t.Wait();
        }
    }
    static void SetWallpaper(string path)
    {
        bool result = SystemParametersInfo(
            SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

    }
    static void Message(string message,string name)
    {
        MessageBox(new IntPtr(0),message,name,0);
    }





}



