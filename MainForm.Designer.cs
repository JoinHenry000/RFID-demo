namespace RFID_Demo
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.txtOutput.Multiline = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Location = new System.Drawing.Point(12, 42);
            this.txtOutput.Size = new System.Drawing.Size(460, 300);

            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 15);
            this.lblStatus.Text = "Đang khởi động...";

            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.lblStatus);
            this.Text = "RFID Demo COM5";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
