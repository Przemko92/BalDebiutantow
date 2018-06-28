namespace EventsEmmiter
{
    partial class Form1
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
            this.btnSystemEvent = new System.Windows.Forms.Button();
            this.btnUserEvent = new System.Windows.Forms.Button();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSystemEvent
            // 
            this.btnSystemEvent.Location = new System.Drawing.Point(12, 39);
            this.btnSystemEvent.Name = "btnSystemEvent";
            this.btnSystemEvent.Size = new System.Drawing.Size(131, 23);
            this.btnSystemEvent.TabIndex = 0;
            this.btnSystemEvent.Text = "Send system event";
            this.btnSystemEvent.UseVisualStyleBackColor = true;
            this.btnSystemEvent.Click += new System.EventHandler(this.btnSystemEvent_Click);
            // 
            // btnUserEvent
            // 
            this.btnUserEvent.Location = new System.Drawing.Point(149, 39);
            this.btnUserEvent.Name = "btnUserEvent";
            this.btnUserEvent.Size = new System.Drawing.Size(131, 23);
            this.btnUserEvent.TabIndex = 1;
            this.btnUserEvent.Text = "Send user event";
            this.btnUserEvent.UseVisualStyleBackColor = true;
            this.btnUserEvent.Click += new System.EventHandler(this.btnUserEvent_Click);
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(12, 13);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(268, 20);
            this.tbMessage.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 74);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.btnUserEvent);
            this.Controls.Add(this.btnSystemEvent);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSystemEvent;
        private System.Windows.Forms.Button btnUserEvent;
        private System.Windows.Forms.TextBox tbMessage;
    }
}

