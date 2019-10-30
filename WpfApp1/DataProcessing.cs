using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace WpfApp1
{
    public class DataProcessing
    {
        public HenkelJobPosition recordsProcessing(String path)
        {
            using (FileStream file = new FileStream(@path, FileMode.Open))
            {
                XmlSerializer serial = new XmlSerializer(typeof(HenkelJobPosition));
                return ((HenkelJobPosition)serial.Deserialize(file));
            }
        }

        public void stringsProcessing(HenkelJobPosition records)
        {
            foreach (Position pos in records.Position)
            {
                string phrase = pos.Keywords;
                phrase = phrase.Trim(new Char[] { '\n' });

                string[] words = phrase.Split(new string[] { "; ", ", " }, StringSplitOptions.None);

                foreach (String word in words)
                    pos.StringList.Add(word);
            }
        }

        public Position bestPosition(Position[] position)
        {
            int ind = 0, maxScore = 0;

            for (int i = 0; i < position.Length; i++)
            {
                if (position[i].Score > maxScore)
                {
                    maxScore = position[i].Score;
                    ind = i;
                }
            }

            return position[ind];
        }

        public List<MainWindow.TodoItem> bestItem(Position[] position, string path)
        {
            List<MainWindow.TodoItem> list = new List<MainWindow.TodoItem>();
            MainWindow.TodoItem item = new MainWindow.TodoItem();
            /*Position posBest = bestPosition(position);

            item.nameFile = Path.GetFileName(path);
            item.patch = path;
            item.role = posBest.Title;
            item.info = posBest.Role;*/

            Array.Sort(position, delegate (Position pos1, Position pos2)
            {
                return pos2.Score.CompareTo(pos1.Score);
            });

            for (int i = 0; i < 1; i++)
            {
                item.nameFile = Path.GetFileName(path);
                item.patch = path;
                item.role = position[i].Title;
                item.info = position[i].Role;
                item.score = position[i].Score;
                list.Add(item);
            }

            return list;
        }
    }
}
