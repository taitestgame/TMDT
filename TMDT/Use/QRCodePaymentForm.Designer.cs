namespace TMDT.Use
{
    partial class QRCodePaymentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox pictureBoxQR;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblOrderID;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.Button btnCompleted;
        private System.Windows.Forms.Button btnCancel;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 650);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thanh toán QR Code";

            // lblTitle
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.lblTitle.Text = "Thanh Toán QR Code";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Location = new System.Drawing.Point(0, 20);
            this.lblTitle.Size = new System.Drawing.Size(500, 35);

            // lblOrderID
            this.lblOrderID = new System.Windows.Forms.Label();
            this.lblOrderID.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOrderID.Location = new System.Drawing.Point(100, 70);
            this.lblOrderID.Size = new System.Drawing.Size(300, 25);
            this.lblOrderID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // pictureBoxQR
            this.pictureBoxQR = new System.Windows.Forms.PictureBox();
            this.pictureBoxQR.Location = new System.Drawing.Point(100, 110);
            this.pictureBoxQR.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            // lblAmount
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAmount.ForeColor = System.Drawing.Color.Red;
            this.lblAmount.Location = new System.Drawing.Point(100, 425);
            this.lblAmount.Size = new System.Drawing.Size(300, 30);
            this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblInstruction
            this.lblInstruction = new System.Windows.Forms.Label();
            this.lblInstruction.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInstruction.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblInstruction.Location = new System.Drawing.Point(50, 470);
            this.lblInstruction.Size = new System.Drawing.Size(400, 60);
            this.lblInstruction.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblInstruction.Text = "Quét mã QR bằng ứng dụng ngân hàng\nhoặc ví điện tử để thanh toán";

            // btnCompleted
            this.btnCompleted = new System.Windows.Forms.Button();
            this.btnCompleted.BackColor = System.Drawing.Color.FromArgb(0, 153, 51);
            this.btnCompleted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompleted.FlatAppearance.BorderSize = 0;
            this.btnCompleted.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCompleted.ForeColor = System.Drawing.Color.White;
            this.btnCompleted.Location = new System.Drawing.Point(100, 550);
            this.btnCompleted.Size = new System.Drawing.Size(145, 40);
            this.btnCompleted.Text = "Đã thanh toán";
            this.btnCompleted.UseVisualStyleBackColor = false;
            this.btnCompleted.Click += new System.EventHandler(this.BtnCompleted_Click);

            // btnCancel
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCancel.FlatAppearance.BorderSize = 1;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnCancel.Location = new System.Drawing.Point(255, 550);
            this.btnCancel.Size = new System.Drawing.Size(145, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            // Add controls
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblOrderID);
            this.Controls.Add(this.pictureBoxQR);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.btnCompleted);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}

