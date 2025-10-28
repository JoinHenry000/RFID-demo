using System;
using System.IO.Ports;
using System.Linq;
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
                // Tự tìm cổng COM nào đang có thiết bị RFID (ưu tiên COM có thể mở được)
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    try
                    {
                        serialPort = new SerialPort(port, 9600);
                        serialPort.Open();

                        if (serialPort.IsOpen)
                        {
                            logBox.AppendText($"✅ Đã mở {port}\r\n");
                            serialPort.DataReceived += SerialPort_DataReceived;
                            return; // kết nối thành công thì dừng
                        }
                    }
                    catch
                    {
                        continue; // thử COM khác nếu lỗi
                    }
                }

                logBox.AppendText("❌ Không tìm thấy thiết bị RFID nào\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối cổng COM: {ex.Message}");
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int bytes = serialPort.BytesToRead;
                if (bytes <= 0) return;

                byte[] buffer = new byte[bytes];
                serialPort.Read(buffer, 0, bytes);

                string data = BitConverter.ToString(buffer).Replace("-", " ");
                this.Invoke(new Action(() =>
                {
                    logBox.AppendText($"{DateTime.Now:HH:mm:ss} → {data}\r\n");
                    logBox.ScrollToCaret();
                }));
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    logBox.AppendText($"⚠️ Lỗi đọc dữ liệu: {ex.Message}\r\n");
                }));
            }
        }
    }
}
