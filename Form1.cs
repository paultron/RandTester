using XoRand;

namespace RandTester
{
    public partial class Form1 : Form
    {

        public XoRand.XoRand256 newrand = new();

        public Form1()
        {
            InitializeComponent();
            
            newrand.StateX64 = new ulong[] { 1, 2, 3, 4 };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = newrand.NextLong().ToString();
            label1.Text = newrand.StateX64[0].ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}