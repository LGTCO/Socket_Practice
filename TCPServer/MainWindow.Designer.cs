namespace TCPServer
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
            this.Txt_IP = new System.Windows.Forms.TextBox();
            this.Txt_Port = new System.Windows.Forms.TextBox();
            this.Btn_StartMonitor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox_Info = new System.Windows.Forms.ListBox();
            this.Txt_SendContent = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.EndPointBox = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Txt_IP
            // 
            this.Txt_IP.Location = new System.Drawing.Point(57, 18);
            this.Txt_IP.Name = "Txt_IP";
            this.Txt_IP.Size = new System.Drawing.Size(132, 21);
            this.Txt_IP.TabIndex = 0;
            // 
            // Txt_Port
            // 
            this.Txt_Port.Location = new System.Drawing.Point(262, 17);
            this.Txt_Port.Name = "Txt_Port";
            this.Txt_Port.Size = new System.Drawing.Size(94, 21);
            this.Txt_Port.TabIndex = 1;
            // 
            // Btn_StartMonitor
            // 
            this.Btn_StartMonitor.Location = new System.Drawing.Point(377, 15);
            this.Btn_StartMonitor.Name = "Btn_StartMonitor";
            this.Btn_StartMonitor.Size = new System.Drawing.Size(75, 23);
            this.Btn_StartMonitor.TabIndex = 2;
            this.Btn_StartMonitor.Text = "开始监听";
            this.Btn_StartMonitor.UseVisualStyleBackColor = true;
            this.Btn_StartMonitor.Click += new System.EventHandler(this.Btn_StartMonitor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "端口";
            // 
            // listBox_Info
            // 
            this.listBox_Info.FormattingEnabled = true;
            this.listBox_Info.ItemHeight = 12;
            this.listBox_Info.Location = new System.Drawing.Point(57, 66);
            this.listBox_Info.Name = "listBox_Info";
            this.listBox_Info.Size = new System.Drawing.Size(299, 196);
            this.listBox_Info.TabIndex = 5;
            // 
            // Txt_SendContent
            // 
            this.Txt_SendContent.Location = new System.Drawing.Point(57, 283);
            this.Txt_SendContent.Name = "Txt_SendContent";
            this.Txt_SendContent.Size = new System.Drawing.Size(299, 21);
            this.Txt_SendContent.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(377, 283);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "发送";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // EndPointBox
            // 
            this.EndPointBox.FormattingEnabled = true;
            this.EndPointBox.Location = new System.Drawing.Point(377, 66);
            this.EndPointBox.Name = "EndPointBox";
            this.EndPointBox.Size = new System.Drawing.Size(95, 20);
            this.EndPointBox.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(377, 124);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "移除客户端";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Btn_RemoveClient_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.EndPointBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Txt_SendContent);
            this.Controls.Add(this.listBox_Info);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_StartMonitor);
            this.Controls.Add(this.Txt_Port);
            this.Controls.Add(this.Txt_IP);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Txt_IP;
        private System.Windows.Forms.TextBox Txt_Port;
        private System.Windows.Forms.Button Btn_StartMonitor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox_Info;
        private System.Windows.Forms.TextBox Txt_SendContent;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox EndPointBox;
        private System.Windows.Forms.Button button2;
    }
}