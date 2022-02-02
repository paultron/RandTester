using XoRand.X64;

namespace RandTester
{
    public partial class XoroViewer256 : Form
    {
        readonly Xo256pp newrand = new();

        public XoroViewer256()
        {
            InitializeComponent();
        }

        private void UpdateStateFields()
        {
            // adds the current state and number to the history
            if (!string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                // sets the state from inputs
                newrand.StateX64 = new ulong[] {
                    ulong.Parse(textBox1.Text),
                    ulong.Parse(textBox2.Text),
                    ulong.Parse(textBox3.Text),
                    ulong.Parse(textBox4.Text),
                        };
                // update label
                label1.Text = newrand.NextuLong().ToString();
                if (StateBox.Items.Count == 20) StateBox.Items.RemoveAt(19);
                // add state and result to list
                StateBox.Items.Insert(0, $"{textBox1.Text,-20} | {textBox2.Text,-20} | {textBox3.Text,-20} | {textBox4.Text,-20} || {label1.Text,20}");

                textBox1.Text = newrand.StateX64[0].ToString();
                textBox2.Text = newrand.StateX64[1].ToString();
                textBox3.Text = newrand.StateX64[2].ToString();
                textBox4.Text = newrand.StateX64[3].ToString();
                label1.Text = newrand.NextuLong().ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            UpdateStateFields();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            StateBox.Items.Clear();

            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "1";

            label1.Text = "8388608";
        }

    }
}