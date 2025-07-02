namespace E_Commerce_Shop.UI.Admin
{
    partial class AdminDasboard
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
            dataGridView1 = new DataGridView();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(-2, -6);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1290, 154);
            dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            button1.BackColor = Color.RosyBrown;
            button1.Font = new Font("Showcard Gothic", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(-2, -6);
            button1.Name = "button1";
            button1.Size = new Size(226, 154);
            button1.TabIndex = 1;
            button1.Text = "VIEW ALL USERS";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaption;
            button2.Font = new Font("Showcard Gothic", 14F, FontStyle.Bold);
            button2.Location = new Point(220, -6);
            button2.Name = "button2";
            button2.Size = new Size(226, 154);
            button2.TabIndex = 2;
            button2.Text = "VIEW ALL PRODUCTS";
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.RosyBrown;
            button3.Font = new Font("Showcard Gothic", 14F, FontStyle.Bold);
            button3.Location = new Point(443, -6);
            button3.Name = "button3";
            button3.Size = new Size(226, 154);
            button3.TabIndex = 3;
            button3.Text = "VIEW DETAILS OF MERCHANTS";
            button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ActiveCaption;
            button4.Font = new Font("Showcard Gothic", 14F, FontStyle.Bold);
            button4.Location = new Point(665, -6);
            button4.Name = "button4";
            button4.Size = new Size(226, 154);
            button4.TabIndex = 4;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            button5.BackColor = Color.RosyBrown;
            button5.Font = new Font("Showcard Gothic", 14F, FontStyle.Bold);
            button5.Location = new Point(871, -6);
            button5.Name = "button5";
            button5.Size = new Size(241, 154);
            button5.TabIndex = 5;
            button5.Text = "button5";
            button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.BackColor = SystemColors.ActiveCaption;
            button6.Font = new Font("Showcard Gothic", 14F, FontStyle.Bold);
            button6.Location = new Point(1077, -6);
            button6.Name = "button6";
            button6.Size = new Size(211, 154);
            button6.TabIndex = 6;
            button6.Text = "button6";
            button6.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.Screenshot_2025_07_02_063100;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Location = new Point(253, 189);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(903, 546);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // AdminDasboard
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1289, 793);
            Controls.Add(pictureBox1);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "AdminDasboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AdminDasboard";
            Load += AdminDasboard_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private PictureBox pictureBox1;
    }
}