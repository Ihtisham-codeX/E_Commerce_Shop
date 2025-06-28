namespace E_Commerce_Shop.UI
{
    partial class ProductTypes
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
            label10 = new Label();
            textBox1 = new TextBox();
            button10 = new Button();
            dataGridView1 = new DataGridView();
            button7 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Teal;
            label10.Location = new Point(319, 245);
            label10.Name = "label10";
            label10.Size = new Size(141, 27);
            label10.TabIndex = 3;
            label10.Text = "Product Type";
            label10.Click += label10_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.WhiteSmoke;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(495, 241);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(470, 31);
            textBox1.TabIndex = 4;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button10
            // 
            button10.BackColor = Color.LightGreen;
            button10.Font = new Font("Viner Hand ITC", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button10.Location = new Point(983, 217);
            button10.Name = "button10";
            button10.Size = new Size(205, 89);
            button10.TabIndex = 18;
            button10.Text = "ADD PRODUCT TYPE";
            button10.UseVisualStyleBackColor = false;
            button10.Click += button10_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(543, 293);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(407, 380);
            dataGridView1.TabIndex = 19;
            // 
            // button7
            // 
            button7.BackColor = Color.LightGreen;
            button7.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button7.Location = new Point(647, 713);
            button7.Name = "button7";
            button7.Size = new Size(205, 45);
            button7.TabIndex = 20;
            button7.Text = "Refresh";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // ProductTypes
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1289, 793);
            Controls.Add(button7);
            Controls.Add(dataGridView1);
            Controls.Add(button10);
            Controls.Add(textBox1);
            Controls.Add(label10);
            Name = "ProductTypes";
            Text = "ProductTypes";
            Load += ProductTypes_Load;
            Controls.SetChildIndex(label10, 0);
            Controls.SetChildIndex(textBox1, 0);
            Controls.SetChildIndex(button10, 0);
            Controls.SetChildIndex(dataGridView1, 0);
            Controls.SetChildIndex(button7, 0);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label10;
        private TextBox textBox1;
        private Button button10;
        private DataGridView dataGridView1;
        private Button button7;
    }
}