using System.Collections;
using System.Xml.Serialization;

namespace WpfApp1
{
    public class Position
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "role")]
        public string Role { get; set; }

        [XmlElement(ElementName = "skills")]
        public string Skills { get; set; }

        [XmlElement(ElementName = "keywords")]
        public string Keywords { get; set; }

        public int Score { get; set; }

        public ArrayList StringList { get; set; }
    }
}