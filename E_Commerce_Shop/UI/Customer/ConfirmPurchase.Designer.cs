namespace E_Commerce_Shop.UI.Customer
{
    partial class ConfirmPurchase
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
            label3 = new Label();
            button4 = new Button();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(693, 324);
            label3.Name = "label3";
            label3.Size = new Size(140, 47);
            label3.TabIndex = 10;
            label3.Text = "50,000";
            label3.Click += label3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.Lime;
            button4.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button4.Location = new Point(687, 465);
            button4.Name = "button4";
            button4.Size = new Size(146, 52);
            button4.TabIndex = 11;
            button4.Text = "Pay Now";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // ConfirmPurchase
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1289, 793);
            Controls.Add(button4);
            Controls.Add(label3);
            Name = "ConfirmPurchase";
            Text = "ConfirmPurchase";
            Load += ConfirmPurchase_Load;
            Controls.SetChildIndex(label3, 0);
            Controls.SetChildIndex(button4, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Button button4;
    }
}