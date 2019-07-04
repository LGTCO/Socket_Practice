namespace TCPClient
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_IP = new System.Windows.Forms.TextBox();
            this.Txt_Port = new System.Windows.Forms.TextBox();
            this.Btn_Connect = new System.Windows.Forms.Button();
            this.ListBox_Info = new System.Windows.Forms.ListBox();
            this.Txt_SendContent = new System.Windows.Forms.TextBox();
            this.Btn_Send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "端口";
            // 
            // Txt_IP
            // 
            this.Txt_IP.Location = new System.Drawing.Point(51, 10);
            this.Txt_IP.Name = "Txt_IP";
            this.Txt_IP.Size = new System.Drawing.Size(100, 21);
            this.Txt_IP.TabIndex = 2;
            // 
            // Txt_Port
            // 
            this.Txt_Port.Location = new System.Drawing.Point(220, 10);
            this.Txt_Port.Name = "Txt_Port";
            this.Txt_Port.Size = new System.Drawing.Size(100, 21);
            this.Txt_Port.TabIndex = 3;
            // 
            // Btn_Connect
            // 
            this.Btn_Connect.Location = new System.Drawing.Point(340, 8);
            this.Btn_Connect.Name = "Btn_Connect";
            this.Btn_Connect.Size = new System.Drawing.Size(75, 23);
            this.Btn_Connect.TabIndex = 4;
            this.Btn_Connect.Text = "连接";
            this.Btn_Connect.UseVisualStyleBackColor = true;
            this.Btn_Connect.Click += new System.EventHandler(this.Btn_Connect_Click);
            // 
            // ListBox_Info
            // 
            this.ListBox_Info.FormattingEnabled = true;
            this.ListBox_Info.ItemHeight = 12;
            this.ListBox_Info.Location = new System.Drawing.Point(40, 52);
            this.ListBox_Info.Name = "ListBox_Info";
            this.ListBox_Info.Size = new System.Drawing.Size(375, 220);
            this.ListBox_Info.TabIndex = 5;
            // 
            // Txt_SendContent
            // 
            this.Txt_SendContent.Location = new System.Drawing.Point(40, 292);
            this.Txt_SendContent.Name = "Txt_SendContent";
            this.Txt_SendContent.Size = new System.Drawing.Size(280, 21);
            this.Txt_SendContent.TabIndex = 6;
            // 
            // Btn_Send
            // 
            this.Btn_Send.Location = new System.Drawing.Point(340, 292);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(75, 23);
            this.Btn_Send.TabIndex = 7;
            this.Btn_Send.Text = "发送";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.Btn_Send);
            this.Controls.Add(this.Txt_SendContent);
            this.Controls.Add(this.ListBox_Info);
            this.Controls.Add(this.Btn_Connect);
            this.Controls.Add(this.Txt_Port);
            this.Controls.Add(this.Txt_IP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_IP;
        private System.Windows.Forms.TextBox Txt_Port;
        private System.Windows.Forms.Button Btn_Connect;
        private System.Windows.Forms.ListBox ListBox_Info;
        private System.Windows.Forms.TextBox Txt_SendContent;
        private System.Windows.Forms.Button Btn_Send;
    }
}