using HuaZi.Library.Downloader;
using HuaZi.Library.Logger;
using System.Reflection;
using System.Threading.Tasks;

namespace Tester
{
    public partial class MainForm : Form
    {
        Logger logger = new Logger
        {
            LogDirectory = $"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs")}",
            ShowDate = false,
            ShowCallerInfo = false
        };

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            logger.Info("INfo");
            logger.Warn("WArn");
            logger.Error("erRor");
            logger.Fatal("fATal");
            logger.Debug("dEBug");
        }
    }
}
