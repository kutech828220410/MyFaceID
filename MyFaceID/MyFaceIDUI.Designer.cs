namespace MyFaceID
{
    partial class MyFaceIDUI
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.canvas = new HsBase.Canvas();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.AutoScroll = true;
            this.canvas.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.canvas.CanvasHeight = 300;
            this.canvas.CanvasWidth = 300;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Margin = new System.Windows.Forms.Padding(0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(514, 394);
            this.canvas.TabIndex = 2;
            // 
            // MyFaceIDUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.canvas);
            this.Name = "MyFaceIDUI";
            this.Size = new System.Drawing.Size(514, 394);
            this.ResumeLayout(false);

        }

        #endregion

        private HsBase.Canvas canvas;
    }
}
