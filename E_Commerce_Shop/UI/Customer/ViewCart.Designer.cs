﻿namespace E_Commerce_Shop.UI.Customer
{
    partial class ViewCart
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
            dataGridView4 = new DataGridView();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).BeginInit();
            SuspendLayout();
            // 
            // dataGridView4
            // 
            dataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView4.Location = new Point(285, 164);
            dataGridView4.Name = "dataGridView4";
            dataGridView4.RowHeadersWidth = 62;
            dataGridView4.Size = new Size(1005, 557);
            dataGridView4.TabIndex = 5;
            dataGridView4.CellContentClick += dataGridView4_CellContentClick;
            // 
            // button4
            // 
            button4.BackColor = Color.Lime;
            button4.Location = new Point(1123, 727);
            button4.Name = "button4";
            button4.Size = new Size(154, 54);
            button4.TabIndex = 10;
            button4.Text = "CheckOut";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // ViewCart
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1289, 793);
            Controls.Add(button4);
            Controls.Add(dataGridView4);
            Name = "ViewCart";
            Text = "ViewCart";
            Load += ViewCart_Load;
            Controls.SetChildIndex(dataGridView4, 0);
            Controls.SetChildIndex(button4, 0);
            ((System.ComponentModel.ISupportInitialize)dataGridView4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView4;
        private Button button4;
    }
}