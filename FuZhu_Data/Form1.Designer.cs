namespace FuZhu_Data
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            textBox2 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            textBox3 = new TextBox();
            listBox1 = new ListBox();
            button2 = new Button();
            treeView1 = new TreeView();
            label4 = new Label();
            button3 = new Button();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(243, 79);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "开始抓取";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(54, 165);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(661, 23);
            textBox1.TabIndex = 1;
            textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(54, 131);
            label1.Name = "label1";
            label1.Size = new Size(143, 17);
            label1.TabIndex = 2;
            label1.Text = "authorization ： Bearer";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(76, 79);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 79);
            label2.Name = "label2";
            label2.Size = new Size(44, 17);
            label2.TabIndex = 4;
            label2.Text = "页码：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(36, 9);
            label3.Name = "label3";
            label3.Size = new Size(252, 17);
            label3.TabIndex = 5;
            label3.Text = "河南省2025年普通高校招生志愿填报辅助系统";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(54, 212);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(734, 23);
            textBox3.TabIndex = 6;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 17;
            listBox1.Location = new Point(54, 241);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(537, 225);
            listBox1.TabIndex = 7;
            // 
            // button2
            // 
            button2.Location = new Point(818, 411);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 8;
            button2.Text = "加载树";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // treeView1
            // 
            treeView1.Location = new Point(806, 21);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(333, 332);
            treeView1.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(818, 370);
            label4.Name = "label4";
            label4.Size = new Size(43, 17);
            label4.TabIndex = 10;
            label4.Text = "label4";
            // 
            // button3
            // 
            button3.Location = new Point(610, 443);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 11;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // panel1
            // 
            panel1.Location = new Point(54, 591);
            panel1.Name = "panel1";
            panel1.Size = new Size(1147, 148);
            panel1.TabIndex = 12;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1257, 751);
            Controls.Add(panel1);
            Controls.Add(button3);
            Controls.Add(label4);
            Controls.Add(treeView1);
            Controls.Add(button2);
            Controls.Add(listBox1);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private Label label1;
        private TextBox textBox2;
        private Label label2;
        private Label label3;
        private TextBox textBox3;
        private ListBox listBox1;
        private Button button2;
        private TreeView treeView1;
        private Label label4;
        private Button button3;
        private Panel panel1;
    }
}
