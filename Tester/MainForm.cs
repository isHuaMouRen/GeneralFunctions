using HuaZi.Library.Downloader;
using HuaZi.Library.Logger;
using System.Threading.Tasks;

namespace Tester
{
    public partial class MainForm : Form
    {
        Logger logger = new Logger();

        public Downloader downloader = null!;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            logger.Info("我是日志，我被输出了");

            downloader = new Downloader
            {
                Url = "https://builds.dotnet.microsoft.com/dotnet/Sdk/9.0.306/dotnet-sdk-9.0.306-win-x64.exe",
                SavePath = $"{Directory.GetCurrentDirectory()}\\inst.exe",
                Progress = ((p, s) => setProgress(p, s)),
                Completed = ((s, e) =>
                {
                    if (s)
                        MessageBox.Show($"Done");
                    else
                        MessageBox.Show($"Error: {e}");
                })
            };

            downloader.StartDownload();
            
        }

        public void setProgress(double progress,double speed)
        {
            if (progress > 50)
            {
                downloader.StopDownload();
                return;
            }
                
            label1.Text = $"{Math.Round(progress, 1)}%  {Math.Round(speed / 1024, 1)}Mb/s";
        }
    }
}
