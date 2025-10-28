using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace RFID_Demo
{
    public partial class MainForm : Form
    {
        private SerialPort serialPort;

        public MainForm()
        {
            InitializeComponent();
            InitializeSerialPort();
        }

        private void InitializeSerialPort()
        {
            try
            {
                serialPort = new SerialPort("COM5", 9600);
                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.Open();
                Invoke(() => logBox.AppendText("Đang kết nối COM5...\r\n"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối cổng COM: {ex.Message}");
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytes = serialPort.BytesToRead;
            byte[] buffer = new byte[bytes];
            serialPort.Read(buffer, 0, bytes);

            // Chuyển dữ liệu sang dạng HEX (chuỗi ASCII)
            string hex = BitConverter.ToString(buffer).Replace("-", " ");

            Invoke(() =>
            {
                logBox.AppendText($"{DateTime.Now:HH:mm:ss} → {hex}\r\n");
            });
        }
    }
}
