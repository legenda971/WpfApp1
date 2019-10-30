using System.Xml.Serialization;

namespace WpfApp1
{
    [XmlRoot(ElementName = "henkeljobpositions")]
    public class HenkelJobPosition
    {
        [XmlElement(ElementName = "position")]
        public Position[] Position { get; set; }
    }
}