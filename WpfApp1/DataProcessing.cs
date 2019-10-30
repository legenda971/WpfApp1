using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace WpfApp1
{
    public class DataProcessing
    {
        public HenkelJobPosition recordsProcessing(String path) { 
            using (FileStream file = new FileStream(@path, FileMode.Open))
            {
                XmlSerializer serial = new XmlSerializer(typeof(HenkelJobPosition));
                return ((HenkelJobPosition) serial.Deserialize(file));
            }
        }

        public void stringsProcessing(HenkelJobPosition records)
        {
            ArrayList stringList = new ArrayList();
            ArrayList wordList = new ArrayList();

            foreach (Position pos in records.Position)
            {
                string phrase = pos.Keywords;
                phrase = phrase.Trim(new Char[] { '\n' });

                string[] words = pos.Keywords.Split("; ");
                foreach (String word in words)
                    pos.StringList.Add(word);
            }
        }
    }
}
