using GroupClass;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.Xml;

namespace Lab2XML
{
    public class Data
    {
        RichTextBox textBox;
        string filepath = "";

        public Data(RichTextBox textBox)
        {
            this.textBox = textBox;
        }
        public void enterStart()
        {
            textBox.Text = "This is program to process XML file`s information and to store it into HTML format.\n" +
                "Here is the example of valid XML file to upload for processing: \n" +
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<workgroup>\r\n\t<group>\r\n\t\t" +
                "<name>Gabriel</name>\r\n\t\t<department>HRT</department>\r\n\t\t<branch>ART</branch>\r\n\t\t" +
                "<speciality>PM</speciality>\r\n\t\t<begining>12:12</begining>\r\n\t\t<ending>13:15</ending>" +
                "\r\n\t\t<type>meeting</type>\r\n\t</group>\r\n\t<group>\r\n\t\t<name>Lora</name>\r\n\t\t" +
                "<department>NSB</department>\r\n\t\t<branch>CIA</branch>\r\n\t\t<speciality>QA</speciality>" +
                "\r\n\t\t<begining>09:24</begining>\r\n\t\t<ending>18:15</ending>\r\n\t\t<type>roll</type>\r\n\t" +
                "</group>\r\n</workgroup>" +
                "\n\n\n-----------------------------------------//--------------------------------------------------\n" +
                
                "Author: Hlib Kolesnichenko \n" + "2-nd Grade\n" +
         
                "----------------------------------------------//-------------------------------------------------\n" +
                "Your current opened File:";
        }
        public void getFilePath(string filepath)
        {
            this.filepath = filepath;
        }
        public void search(RadioButton radioButtonSAX, RadioButton radioButtonLINQ, Groups group)
        {
            List<Groups> temp = new List<Groups>();
            XMLStrategy strategy = new DOMStrategy();
            
            if(radioButtonSAX.Checked)
            {
                strategy = new SAXStrategy();
                textBox.Text += "\nSAX Method";
            }
            else if(radioButtonLINQ.Checked)
            {
                strategy = new LINQtoXMLStrategy();
                textBox.Text += "\nLINQ to XML Method";
            }
            else textBox.Text += "\nDOM Method";
            strategy.setFilePath(filepath);
            //MessageBox.Show("New XML file recieved!");
            if (filepath == "")
            {
                MessageBox.Show("Please open .xml file");
            }
            else
            {
                temp = strategy.search(group);
                printInfo(temp);
            }
        }
        public void printInfo(List<Groups> results)
        {
            foreach (Groups gr in results)
            {
                textBox.Text += "\nName: " + gr.Name + "\n";
                textBox.Text += "Department: " + gr.Department + "\n";
                textBox.Text += "Branch: " + gr.Branch + "\n";
                textBox.Text += "Speciality: " + gr.Speciality + "\n";
                textBox.Text += "Time of begining: " + gr.begining + "\n";
                textBox.Text += "Time of ending: " + gr.ending + "\n";
                textBox.Text += "Type: " + gr.Type + "\n\n";
                textBox.Text += "-----------------------//----------------------------\n";
            }
        }
    }
}
