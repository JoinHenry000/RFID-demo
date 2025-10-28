using System;
using System.IO.Ports;
using System.Drawing;
using System.Windows.Forms;

namespace RFID_Demo
{
    public partial class MainForm : Form
    {
        private SerialPort? serialPort;
        private TextBox logBox;

        public MainForm()
        {
            InitializeComponent();
            InitializeUI();
            InitializeSerialPort();
        }

        private void InitializeUI()
        {
            // Cấu hình form
            this.Text = "RFID Demo COM5";
            this.Size = new Size(600, 400);
            this.BackColor = Color.FromArgb(30, 30, 30);

            // Tạo textbox log
            logBox = new TextBox();
            logBox.Multiline = true;
            logBox.ReadOnly = true;
            logBox.ScrollBars = ScrollBars.Vertical;
            logBox.Dock = DockStyle.Fill;
            logBox.BackColor = Color.Black;
            logBox.ForeColor = Color.Lime;
            logBox.Font = new Font("Consolas", 10);

            // Thêm vào form
            this.Controls.Add(logBox);
        }

        private void InitializeSerialPort()
        {
            try
            {
                serialPort = new SerialPort("COM5", 9600);
                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.Open();
                logBox.AppendText("Đang kết nối COM5...\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối cổng COM: {ex.Message}");
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort == null) return;

            int bytes = serialPort.BytesToRead;
            byte[] buffer = new byte[bytes];
            serialPort.Read(buffer, 0, bytes);

            // Chuyển dữ liệu sang dạng HEX (chuỗi ASCII)
            string hex = BitConverter.ToString(buffer).Replace("-", " ");

            this.Invoke(new Action(() =>
            {
                logBox.AppendText($"{DateTime.Now:HH:mm:ss} → {hex}\r\n");
                logBox.SelectionStart = logBox.Text.Length;
                logBox.ScrollToCaret();
            }));
        }
    }
}
