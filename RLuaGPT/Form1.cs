using System;
using System.Windows.Forms;

namespace RLuaGPT
{
    public partial class Form1 : Form
    {
        AI.Save save = new AI.Save();
        public Form1() => InitializeComponent();

        private void button1_Click(object sender, EventArgs e) => OpenAI.Ask(textBox1.Text, richTextBox1, richTextBox2);

        private void button2_Click(object sender, EventArgs e)
        {
            Base.key = textBox2.Text;
            MessageBox.Show("OpenAI API key added!", "RLua GPT", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            save.key = textBox2.Text;
            save.Save();
            if (!string.IsNullOrEmpty(save.key)) button1.Enabled = true;
            else button1.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(save.key))
                button1.Enabled = true;
            textBox2.Text = save.key;
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hideToolStripMenuItem.Checked = !hideToolStripMenuItem.Checked;
            textBox2.UseSystemPasswordChar = hideToolStripMenuItem.Checked;
        }
    }
}