using System;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            XElement booksFromFile = XElement.Load(@"C:\Users\Asus\Desktop\books.xml");
            //Console.WriteLine(booksFromFile);

            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\Asus\Desktop\data.xml");
            XmlNodeList elemList = doc.DocumentElement.SelectNodes("/records/patient/name");
            foreach (XmlNode node in elemList)
            {
                if (!node.HasChildNodes)
                    Console.WriteLine(node.Attributes["name"].InnerText);
                else
                    Console.WriteLine(node.InnerText);
            }

            /*XmlReader xmlReader = XmlReader.Create("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Cube"))
                {
                    if (xmlReader.HasAttributes)
                        Console.WriteLine(xmlReader.GetAttribute("currency") + ": " + xmlReader.GetAttribute("rate"));
                }
            }*/

            /*Records records;

            using (FileStream file = new FileStream(@"C:\Users\Asus\Desktop\data.xml", FileMode.Open)) {
                XmlSerializer serial = new XmlSerializer(typeof(Records));
                records = (Records)serial.Deserialize(file);
            }

            var typek = records.Patients.First(p => p.Name == "john");
            Console.WriteLine(records.Patients[1].Name);
            Console.WriteLine(typek.Age);*/
        }
    }

    [XmlRoot(ElementName = "records")]
    public class Records
    {
        [XmlElement(ElementName = "patient")]
        public Patient[] Patients { get; set; }
    }

    // this assumes the 2nd example where properties
    // are child nodes and not attributes
    public class Patient
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "age")]
        public int Age { get; set; }

        [XmlElement(ElementName = "status")]
        public string Status { get; set; }
    }
}