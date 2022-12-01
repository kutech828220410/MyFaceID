namespace TestForm
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_GetFaceCount = new System.Windows.Forms.Button();
            this.myFaceIDUI_SRC = new MyFaceID.MyFaceIDUI();
            this.button_GetFaceFeature = new System.Windows.Forms.Button();
            this.myFaceIDUI_Clone = new MyFaceID.MyFaceIDUI();
            this.button_初始化 = new System.Windows.Forms.Button();
            this.button_產生離線文件 = new System.Windows.Forms.Button();
            this.textBox_序號 = new System.Windows.Forms.TextBox();
            this.button_離線激活 = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_GetFaceCount
            // 
            this.button_GetFaceCount.Location = new System.Drawing.Point(161, 855);
            this.button_GetFaceCount.Name = "button_GetFaceCount";
            this.button_GetFaceCount.Size = new System.Drawing.Size(104, 51);
            this.button_GetFaceCount.TabIndex = 6;
            this.button_GetFaceCount.Text = "GetFaceCount";
            this.button_GetFaceCount.UseVisualStyleBackColor = true;
            // 
            // myFaceIDUI_SRC
            // 
            this.myFaceIDUI_SRC.ActiveKey = "86A1-119W-L3X8-LGPB";
            this.myFaceIDUI_SRC.AppID = "JA4edUxKWWBLUFzC4hqenf8qbrMwQqyrMsko7uFYFUS2";
            this.myFaceIDUI_SRC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myFaceIDUI_SRC.CameraIndex = 0;
            this.myFaceIDUI_SRC.FrameHeight = 480;
            this.myFaceIDUI_SRC.FrameWidth = 640;
            this.myFaceIDUI_SRC.Horizontal = true;
            this.myFaceIDUI_SRC.ImagesFeatureList = ((System.Collections.Generic.List<ArcSoftFace.SDKModels.ASF_FaceFeature>)(resources.GetObject("myFaceIDUI_SRC.ImagesFeatureList")));
            this.myFaceIDUI_SRC.Location = new System.Drawing.Point(12, 12);
            this.myFaceIDUI_SRC.Name = "myFaceIDUI_SRC";
            this.myFaceIDUI_SRC.RotateType = MyFaceID.MyFaceIDUI.TxRotateType._0;
            this.myFaceIDUI_SRC.SdkKey = "DTL1LNqxkb7382ES8xxomgaGsFJf6gddwpQsaLBGXPm7";
            this.myFaceIDUI_SRC.Size = new System.Drawing.Size(333, 409);
            this.myFaceIDUI_SRC.TabIndex = 2;
            this.myFaceIDUI_SRC.Vertical = false;
            // 
            // button_GetFaceFeature
            // 
            this.button_GetFaceFeature.Location = new System.Drawing.Point(271, 855);
            this.button_GetFaceFeature.Name = "button_GetFaceFeature";
            this.button_GetFaceFeature.Size = new System.Drawing.Size(104, 51);
            this.button_GetFaceFeature.TabIndex = 7;
            this.button_GetFaceFeature.Text = "GetFaceFeature";
            this.button_GetFaceFeature.UseVisualStyleBackColor = true;
            this.button_GetFaceFeature.Click += new System.EventHandler(this.button_GetFaceFeature_Click);
            // 
            // myFaceIDUI_Clone
            // 
            this.myFaceIDUI_Clone.ActiveKey = "86A1-119W-L3X8-LGPB";
            this.myFaceIDUI_Clone.AppID = "JA4edUxKWWBLUFzC4hqenf8qbrMwQqyrMsko7uFYFUS2";
            this.myFaceIDUI_Clone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myFaceIDUI_Clone.CameraIndex = 0;
            this.myFaceIDUI_Clone.FrameHeight = 480;
            this.myFaceIDUI_Clone.FrameWidth = 640;
            this.myFaceIDUI_Clone.Horizontal = true;
            this.myFaceIDUI_Clone.ImagesFeatureList = ((System.Collections.Generic.List<ArcSoftFace.SDKModels.ASF_FaceFeature>)(resources.GetObject("myFaceIDUI_Clone.ImagesFeatureList")));
            this.myFaceIDUI_Clone.Location = new System.Drawing.Point(12, 427);
            this.myFaceIDUI_Clone.Name = "myFaceIDUI_Clone";
            this.myFaceIDUI_Clone.RotateType = MyFaceID.MyFaceIDUI.TxRotateType._0;
            this.myFaceIDUI_Clone.SdkKey = "DTL1LNqxkb7382ES8xxomgaGsFJf6gddwpQsaLBGXPm7";
            this.myFaceIDUI_Clone.Size = new System.Drawing.Size(333, 340);
            this.myFaceIDUI_Clone.TabIndex = 4;
            this.myFaceIDUI_Clone.Vertical = false;
            // 
            // button_初始化
            // 
            this.button_初始化.Location = new System.Drawing.Point(12, 785);
            this.button_初始化.Name = "button_初始化";
            this.button_初始化.Size = new System.Drawing.Size(180, 64);
            this.button_初始化.TabIndex = 8;
            this.button_初始化.Text = "初始化";
            this.button_初始化.UseVisualStyleBackColor = true;
            // 
            // button_產生離線文件
            // 
            this.button_產生離線文件.Location = new System.Drawing.Point(6, 21);
            this.button_產生離線文件.Name = "button_產生離線文件";
            this.button_產生離線文件.Size = new System.Drawing.Size(180, 64);
            this.button_產生離線文件.TabIndex = 9;
            this.button_產生離線文件.Text = "產生離線序號";
            this.button_產生離線文件.UseVisualStyleBackColor = true;
            // 
            // textBox_序號
            // 
            this.textBox_序號.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox_序號.Location = new System.Drawing.Point(3, 91);
            this.textBox_序號.Multiline = true;
            this.textBox_序號.Name = "textBox_序號";
            this.textBox_序號.Size = new System.Drawing.Size(458, 274);
            this.textBox_序號.TabIndex = 10;
            // 
            // button_離線激活
            // 
            this.button_離線激活.Location = new System.Drawing.Point(198, 785);
            this.button_離線激活.Name = "button_離線激活";
            this.button_離線激活.Size = new System.Drawing.Size(180, 64);
            this.button_離線激活.TabIndex = 11;
            this.button_離線激活.Text = "離線激活";
            this.button_離線激活.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "dat";
            this.openFileDialog.Filter = "dat File (*.dat)|*.dat;";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button_產生離線文件);
            this.groupBox1.Controls.Add(this.textBox_序號);
            this.groupBox1.Location = new System.Drawing.Point(374, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 368);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "步驟01";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "dat";
            this.saveFileDialog.Filter = "dat File (*.dat)|*.dat;";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(374, 386);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 149);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "步驟02";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "網址";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(75, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(368, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "https://www.arcsoft.com.cn/";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(75, 79);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(368, 22);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "bismagi@yahoo.com.tw";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "帳號";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.White;
            this.textBox3.Location = new System.Drawing.Point(75, 107);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(368, 22);
            this.textBox3.TabIndex = 5;
            this.textBox3.Text = "A82822040b";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "密碼";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(203, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "導出本地DAT註冊文件";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(28, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "登入網頁";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Location = new System.Drawing.Point(844, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 475);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "步驟03";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(17, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "選擇未註冊序號";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panel3);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(844, 498);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(534, 179);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "步驟03";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(17, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "選擇版本 V4.1";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.panel4);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Location = new System.Drawing.Point(844, 683);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(324, 179);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "步驟04";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(17, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(217, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "上傳文件後生成離線註冊文件";
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::TestForm.Properties.Resources._4;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel4.Location = new System.Drawing.Point(21, 54);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(267, 112);
            this.panel4.TabIndex = 17;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::TestForm.Properties.Resources._3;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel3.Location = new System.Drawing.Point(21, 54);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(479, 101);
            this.panel3.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::TestForm.Properties.Resources._2;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Location = new System.Drawing.Point(3, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(303, 405);
            this.panel2.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::TestForm.Properties.Resources._1;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(380, 541);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(455, 256);
            this.panel1.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1547, 952);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_離線激活);
            this.Controls.Add(this.button_初始化);
            this.Controls.Add(this.button_GetFaceFeature);
            this.Controls.Add(this.button_GetFaceCount);
            this.Controls.Add(this.myFaceIDUI_Clone);
            this.Controls.Add(this.myFaceIDUI_SRC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "FaceID 註冊機";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MyFaceID.MyFaceIDUI myFaceIDUI_SRC;
        private System.Windows.Forms.Button button_GetFaceCount;
        private System.Windows.Forms.Button button_GetFaceFeature;
        private MyFaceID.MyFaceIDUI myFaceIDUI_Clone;
        private System.Windows.Forms.Button button_初始化;
        private System.Windows.Forms.Button button_產生離線文件;
        private System.Windows.Forms.TextBox textBox_序號;
        private System.Windows.Forms.Button button_離線激活;
        protected System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        protected System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
    }
}

