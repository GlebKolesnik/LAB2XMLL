using GroupClass;
using Lab2XML;
using System;
using System.Globalization;
using System.Xml.Xsl;

namespace Lab2XML
{
    public partial class Form1 : Form
    {
        Data data;
        string filepath;
        public Form1()
        {
            InitializeComponent();
            radioButtonDOM.Checked = true;
            data = new Data(InformationField);
            data.enterStart();
            getStart();
        }
        public void getStart()
        {
            DialogResult result = MessageBox.Show("Select XML File", "Confirm selecting", MessageBoxButtons.OK, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    data.getFilePath(openFileDialog1.FileName);
                    filepath = openFileDialog1.FileName;
                    clearAll();
                    data.enterStart();
                    Groups gr = specify();
                    data.search(radioButtonSAX, radioButtonLINQ, gr);
                    this.Text = openFileDialog1.FileName + "XML";
                }

            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InformationField.Text = "";
            Groups gr = specify();
            data.search(radioButtonSAX, radioButtonLINQ, gr);
        }
        public Groups specify()
        {
            Groups group = new Groups();
            group.Name = textBox1.Text;
            group.Department = textBox2.Text;
            group.Branch = textBox3.Text;
            group.Speciality = textBox4.Text;
            group.begining = textBox5.Text;
            group.ending = textBox6.Text;
            group.Type = textBox7.Text;
            return group;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearAll();
        }
        public void clearAll()
        {
            InformationField.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                data.getFilePath(openFileDialog1.FileName);
                filepath = openFileDialog1.FileName;
                clearAll();
                data.enterStart();
                Groups gr = specify();
                data.search(radioButtonSAX, radioButtonLINQ, gr);
                this.Text = openFileDialog1.FileName + "XML";
            }
        }

        private void saveToHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XslCompiledTransform xsl = new XslCompiledTransform();
                xsl.Load(@"C:\Users\глеб\Desktop\KNU FKNK\Lab2Mike\Lab2XML\XSLTFile1.xsl");
                string f = saveFileDialog1.FileName;
                xsl.Transform(filepath, f);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirm Exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    XslCompiledTransform xsl = new XslCompiledTransform();
                    xsl.Load(@"D:\CSharp\Lab2XML\Lab2XML\XSLTFile1.xsl");
                    string f = saveFileDialog1.FileName;
                    xsl.Transform(filepath, f);
                }

            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            
        }
    }
}