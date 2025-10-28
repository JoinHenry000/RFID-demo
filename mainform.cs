using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace RFID_Demo
{
    public partial class MainForm : Form
    {
        SerialPort serialPort;
        int tagCount = 0;

        public MainForm()
        {
            InitializeComponent();
            InitSerialPort("COM5");
        }

        void InitSerialPort(string portName)
        {
            try
            {
                serialPort = new SerialPort(portName, 9600);
                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.Open();
                lblStatus.Text = $"Đang kết nối {portName}";
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Không thể mở {portName}: {ex.Message}";
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort.ReadLine().Trim();
            if (!string.IsNullOrEmpty(data))
            {
                Invoke(new Action(() =>
                {
                    tagCount++;
                    txtOutput.AppendText($"{tagCount}. {data}{Environment.NewLine}");
                }));
            }
        }
    }
}
