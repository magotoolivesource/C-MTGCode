namespace ChatServer
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.InputTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.IPText = new System.Windows.Forms.TextBox();
            this.PortText = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InputTextBox
            // 
            this.InputTextBox.Location = new System.Drawing.Point(12, 322);
            this.InputTextBox.Multiline = true;
            this.InputTextBox.Name = "InputTextBox";
            this.InputTextBox.Size = new System.Drawing.Size(501, 21);
            this.InputTextBox.TabIndex = 0;
            this.InputTextBox.Text = "가나다";
            this.InputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputTextBox_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(536, 322);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 21);
            this.button1.TabIndex = 1;
            this.button1.Text = "클릭01";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 48);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(642, 268);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "내용들추가";
            // 
            // IPText
            // 
            this.IPText.Location = new System.Drawing.Point(12, 12);
            this.IPText.Name = "IPText";
            this.IPText.Size = new System.Drawing.Size(143, 21);
            this.IPText.TabIndex = 3;
            this.IPText.Text = "127.0.0.1";
            // 
            // PortText
            // 
            this.PortText.Location = new System.Drawing.Point(182, 12);
            this.PortText.Name = "PortText";
            this.PortText.Size = new System.Drawing.Size(143, 21);
            this.PortText.TabIndex = 4;
            this.PortText.Text = "25000";
            this.PortText.TextChanged += new System.EventHandler(this.PortText_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(349, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 21);
            this.button2.TabIndex = 5;
            this.button2.Text = "서버시작";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 355);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.PortText);
            this.Controls.Add(this.IPText);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.InputTextBox);
            this.Name = "Form1";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InputTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox IPText;
        private System.Windows.Forms.TextBox PortText;
        private System.Windows.Forms.Button button2;
    }
}

