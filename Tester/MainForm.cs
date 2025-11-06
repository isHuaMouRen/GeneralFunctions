using HuaZi.Library.Downloader;
using System.Threading.Tasks;

namespace Tester
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await Downloader.DownloadFileAsync("https://builds.dotnet.microsoft.com/dotnet/Sdk/9.0.306/dotnet-sdk-9.0.306-win-x64.exe", "ins.exe", ((p, s) => setProgress(p, s)));
        }

        public void setProgress(double progress,double speed)
        {
            label1.Text = $"{Math.Round(progress, 3)}  {Math.Round(speed / 1024, 3)}Mb/s";
        }
    }
}
