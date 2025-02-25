using System;
using System.Management;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace AntiVirusSearcher
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartForm());
        }
    }

    public class CustomTitleBar : Panel
    {
        private Label lblTitle;
        private Button btnClose;
        private Button btnMinimize;
        private Form parentForm;
        private Point mouseDownLocation;

        public CustomTitleBar(Form parent)
        {
            parentForm = parent;
            this.Height = 30;
            this.Dock = DockStyle.Top;
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.MouseDown += TitleBar_MouseDown;
            this.MouseMove += TitleBar_MouseMove;

            lblTitle = new Label()
            {
                Text = "🛡️ AntiVirusSearcher",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Left = 10,
                Top = 7
            };

            btnMinimize = new Button()
            {
                Text = "_",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(50, 50, 50),
                Width = 30,
                Height = 30,
                Left = parent.Width - 70,
                FlatStyle = FlatStyle.Flat
            };
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.Click += (s, e) => parent.WindowState = FormWindowState.Minimized;

            btnClose = new Button()
            {
                Text = "X",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(150, 30, 30),
                Width = 30,
                Height = 30,
                Left = parent.Width - 40,
                FlatStyle = FlatStyle.Flat
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Application.Exit();

            this.Controls.Add(lblTitle);
            this.Controls.Add(btnMinimize);
            this.Controls.Add(btnClose);
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
            }
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                parentForm.Left += e.X - mouseDownLocation.X;
                parentForm.Top += e.Y - mouseDownLocation.Y;
            }
        }
    }
    public class StartForm : Form
    {
        
        private Button btnStart;
        private Label lblInfo;
        
        public StartForm()
        {
            this.Text = "🛡️ Antivirus Searcher - Start Page";
            this.Size = new Size(500, 300);
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(24, 24, 24);
            this.Region = CreateRoundedRegion(this.ClientRectangle, 20);
            var titleBar = new CustomTitleBar(this);
            this.Controls.Add(titleBar);
            lblInfo = new Label()
            {
                Left = 20,
                Top = 50,
                Width = 450,
                Text = "This tool will detect installed antivirus software on your system. \nClick 'Start' to begin.",
                ForeColor = Color.LightGreen,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                AutoSize = true
            };

            btnStart = new Button()
            {
                Left = 150,
                Top = 150,
                Width = 200,
                Height = 50,
                Text = "🚀 Start Search",
                BackColor = Color.DarkGray,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.Click += (sender, e) => { this.Hide(); new MainForm().Show(); };

            this.Controls.Add(lblInfo);
            this.Controls.Add(btnStart);
        }

        private Region CreateRoundedRegion(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return new Region(path);
        }
    }
    public class MainForm : Form
    {
        private Region CreateRoundedRegion(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return new Region(path);
        }
        private Label lblAntivirus;
        private Label lblSantivirus;
        private Button btnScreenshot;
        private ProgressBar progressBar;

        public MainForm()
        {
            this.Text = "🛡️ Antivirus Searcher";
            this.Size = new Size(500, 350);
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(24, 24, 24);
            this.Region = CreateRoundedRegion(this.ClientRectangle, 20);

            var titleBar = new CustomTitleBar(this);
            this.Controls.Add(titleBar);

            lblAntivirus = new Label() { Left = 20, Top = 50, Width = 450, Text = "🔍 Detecting antivirus...", ForeColor = Color.Cyan, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            lblSantivirus = new Label() { Left = 20, Top = 90, Width = 450, Text = "🛑 Checking for SAntivirus...", ForeColor = Color.Orange, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            progressBar = new ProgressBar()
            {
                Left = 20,
                Top = 130,
                Width = 450,
                Height = 10,
                Style = ProgressBarStyle.Continuous,
                ForeColor = Color.Green,
                BackColor = Color.Gray
            };

            btnScreenshot = new Button()
            {
                Left = 20,
                Top = 180,
                Width = 200,
                Height = 50,
                Text = "📸 Capture App Screenshot",
                BackColor = Color.FromArgb(55, 55, 55),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnScreenshot.FlatAppearance.BorderSize = 0;
            btnScreenshot.Click += (sender, e) => CaptureAppScreenshot();

            this.Controls.Add(lblAntivirus);
            this.Controls.Add(lblSantivirus);
            this.Controls.Add(progressBar);
            this.Controls.Add(btnScreenshot);

            DetectAntivirusAsync();
        }

        private bool SantivirusDetected()
        {
            return Directory.Exists("C:\\Program Files (x86)\\Digital Communications\\SAntivirus");
        }

        private async void DetectAntivirusAsync()
        {
            progressBar.Visible = true;
            await Task.Delay(2000);
            progressBar.Visible = false;

            try
            {
                string antivirusName = "⚠️ No antivirus detected!";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\SecurityCenter2", "SELECT * FROM AntiVirusProduct");
                foreach (ManagementObject obj in searcher.Get())
                {
                    antivirusName = obj["displayName"]?.ToString() ?? "Unknown Antivirus";
                    break;
                }
                lblAntivirus.Text = $"🛡️ Antivirus Detected: {antivirusName}";
            }
            catch (Exception ex)
            {
                lblAntivirus.Text = "❌ Error retrieving antivirus information.";
                MessageBox.Show($"Error: {ex.Message}", "Detection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            lblSantivirus.Text = $"🐍 SAntivirus installed? -> {SantivirusDetected()}";
        }

        private void CaptureAppScreenshot()
        {
            Bitmap screenshot = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(this.PointToScreen(Point.Empty), Point.Empty, this.Size);
            }
            screenshot.Save("AppScreenshot.jpg", ImageFormat.Jpeg);
            MessageBox.Show("📂 Screenshot saved as 'AppScreenshot.jpg'", "Screenshot Taken", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
