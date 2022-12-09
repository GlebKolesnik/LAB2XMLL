using GroupClass;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab2XML
{
    
    public interface XMLStrategy
    {
        List<Groups> search(Groups group);
        public void setFilePath(string filepath);
    }
    public class DOMStrategy : XMLStrategy
    {
        private string filepath;
        public void setFilePath(string filepath)
        {
            this.filepath = filepath;
        }
        public List<Groups> search(Groups group)
        {
            //MessageBox.Show("DdD");
            List<Groups> results = new List<Groups>();
            XmlDocument xml = new XmlDocument();
            
            xml.Load(filepath);
           
            XmlNode xRoot = xml.DocumentElement; //root elem
            if (xRoot != null)
            {
                foreach (XmlNode xnode in xRoot.ChildNodes)
                {
                    string name = "";
                    string depart = "";
                    string bra = "";
                    string spec = "";
                    string beg = "";
                    string end = "";
                    string type = "";
                    foreach (XmlNode gr in xnode.ChildNodes)
                    {

                        if (gr.Name.Equals("name") && (gr.InnerText.Equals(group.Name) || group.Name.Equals(String.Empty)))
                        {
                            //MessageBox.Show("2");
                            name = gr.InnerText;
                        }
                        if (gr.Name.Equals("department") && (gr.InnerText.Equals(group.Department) || group.Department.Equals(String.Empty)))
                        {
                            //MessageBox.Show("2");
                            depart = gr.InnerText;
                        }
                        if (gr.Name.Equals("branch") && (gr.InnerText.Equals(group.Branch) || group.Branch.Equals(String.Empty)))
                        {
                            //MessageBox.Show("2");
                            bra = gr.InnerText;
                        }
                        if (gr.Name.Equals("speciality") && (gr.InnerText.Equals(group.Speciality) || group.Speciality.Equals(String.Empty)))
                        {
                            //MessageBox.Show("2");
                            spec = gr.InnerText;
                        }
                        if (gr.Name.Equals("begining") && (gr.InnerText.Equals(group.begining) || group.begining.Equals(String.Empty)))
                        {
                            //MessageBox.Show("2");
                            beg = gr.InnerText;
                        }
                        if (gr.Name.Equals("ending") && (gr.InnerText.Equals(group.ending) || group.ending.Equals(String.Empty)))
                        {
                            //MessageBox.Show("2");
                            end = gr.InnerText;

                        }
                        if (gr.Name.Equals("type") && (gr.InnerText.Equals(group.Type) || group.Type.Equals(String.Empty)))
                        {
                            //MessageBox.Show("2");
                            type = gr.InnerText;
                        }
                    }
                    if (name != "" && depart != "" && bra != "" && spec != "" && beg != "" && end != "" && type != "")
                    {
                        Groups new_group = new Groups();
                        new_group.Name = name;
                        new_group.Type = type;
                        new_group.ending = end;
                        new_group.begining = beg;
                        new_group.Department = depart;
                        new_group.Branch = bra;
                        new_group.Speciality = spec;
                        results.Add(new_group);
                    }

                }
                
            }
            return results;
        }
    }
    public class SAXStrategy : XMLStrategy
    {
        private string filepath;
        public void setFilePath(string filepath)
        {
            this.filepath = filepath;
        }
        public List<Groups> search(Groups group)
        {
            List<Groups> results = new List<Groups>(); 
            using (XmlReader xr = XmlReader.Create(filepath))
            {
                string name = "";
                string depart = "";
                string bra = "";
                string spec = "";
                string beg = "";
                string end = "";
                string type = "";
                string element = ""; // name of current element
                while(xr.Read())
                {

                    if(xr.NodeType == XmlNodeType.Element)
                    {
                        element = xr.Name; // name of current element 
                    }
                    else if (xr.NodeType == XmlNodeType.Text)
                    {
                        if(element == "name" && (xr.Value.Equals(group.Name) || group.Name.Equals(String.Empty)))
                        {
                            name = xr.Value;
                        }
                        if (element == "department" && (xr.Value.Equals(group.Department) || group.Department.Equals(String.Empty)))
                        {
                            depart = xr.Value;
                        }
                        if (element == "branch" && (xr.Value.Equals(group.Branch) || group.Branch.Equals(String.Empty)))
                        {
                            bra = xr.Value;
                        }
                        if (element == "speciality" && (xr.Value.Equals(group.Speciality) || group.Speciality.Equals(String.Empty)))
                        {
                            spec = xr.Value;
                        }
                        if (element == "begining" && (xr.Value.Equals(group.begining) || group.begining.Equals(String.Empty)))
                        {
                            beg = xr.Value;
                        }
                        if (element == "ending" && (xr.Value.Equals(group.ending) || group.ending.Equals(String.Empty)))
                        {
                            end = xr.Value;
                        }
                        if(element == "type" && (xr.Value.Equals(group.Type) || group.Type.Equals(String.Empty)))
                        {
                            type = xr.Value;
                        }
                    }
                    else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "group"))
                    {
                        if (name != "" && depart != "" && bra != "" && spec != "" && beg != "" && end != "" && type != "")
                        {
                            Groups new_group = new Groups();
                            new_group.Name = name;
                            new_group.Type = type;
                            new_group.ending = end;
                            new_group.begining = beg;
                            new_group.Department = depart;
                            new_group.Branch = bra;
                            new_group.Speciality = spec;
                            results.Add(new_group);
                        }
                        name = "";
                        depart = "";
                        bra = "";
                        spec = "";
                        beg = "";
                        end = "";
                        type = "";
                        element = "";
                    }
                }
                
            }
            return results;
        }
    }
    public class LINQtoXMLStrategy : XMLStrategy
    {
        private string filepath;
        public void setFilePath(string filepath)
        {
            this.filepath = filepath;
        }
        public List<Groups> search(Groups group)
        {
            List<Groups> results = new List<Groups>();
            XDocument xdoc = XDocument.Load(filepath);

            XElement xRoot = xdoc.Element("studentparliament");
            if (xRoot != null)
            {

                foreach (XElement gr in xRoot.Elements("group"))
                {
                    string name = "";
                    string depart = "";
                    string bra = "";
                    string spec = "";
                    string beg = "";
                    string end = "";
                    string type = "";
                    //MessageBox.Show(gr.Element("name").Value);
                    if (gr.Element("name").Value.Equals(group.Name) || group.Name.Equals(String.Empty))
                    {
                        name = gr.Element("name").Value;
                    }
                    if (gr.Element("department").Value.Equals(group.Department) || group.Department.Equals(String.Empty))
                    {
                        //MessageBox.Show("3");
                        depart = gr.Element("department").Value;
                    }
                    if (gr.Element("branch").Value.Equals(group.Branch) || group.Branch.Equals(String.Empty))
                    {
                        bra = gr.Element("branch").Value;
                    }
                    if (gr.Element("speciality").Value.Equals(group.Speciality) || group.Speciality.Equals(String.Empty))
                    {
                        spec = gr.Element("speciality").Value;
                    }
                    if (gr.Element("begining").Value.Equals(group.begining) || group.begining.Equals(String.Empty))
                    {
                        beg = gr.Element("begining").Value;
                    }
                    if (gr.Element("ending").Value.Equals(group.ending) || group.ending.Equals(String.Empty))
                    {
                        end = gr.Element("ending").Value;
                    }
                    if (gr.Element("type").Value.Equals(group.Type) || group.Type.Equals(String.Empty))
                    {
                        type = gr.Element("type").Value;
                    }
                    if (name != "" && depart != "" && bra != "" && spec != "" && beg != "" && end != "" && type != "")
                    {
                        Groups new_group = new Groups();
                        new_group.Name = name;
                        new_group.Type = type;
                        new_group.ending = end;
                        new_group.begining = beg;
                        new_group.Department = depart;
                        new_group.Branch = bra;
                        new_group.Speciality = spec;
                        results.Add(new_group);
                    }
                }

            }
            return results;
        }
    }
}
