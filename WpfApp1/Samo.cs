using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace WpfApp1
{
    class Samo
    {

        public Samo() {
        }

        
        public static string readPDF(string path){
            // zadaj cestu, vrati ti text z pdf


            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
                return text.ToString();
            }
        }

        public static int findNumStrInStr(string text, string findWord1, string findWord2) {
            // hlada prve slovo v texte a nasledne v jeho okoli hlada w2 ak ano ++

            int searchLen = 25;
            
            int numOfStrings = 0;
            int lastIdx = 0;
            int idW1 = 0;
            int idW2 = 0;

            string textLow = text.ToLower();
            string w1 = findWord1.ToLower();
            string w2 = findWord2.ToLower();

            if (textLow.Contains(w1)) {
                for (int i = 0; idW1 >= 0 & i < textLow.Length; i++) {

                    if ((idW1 = textLow.IndexOf(w1, lastIdx)) >= 0) {

                        lastIdx = idW1 + 1;
                        int startId = (idW1 - 20 > 0) ? idW1 - 20 : 0;
                        int endId = (idW1 + 20 < textLow.Length) ? idW2 + 20 : textLow.Length;
                        numOfStrings++;

                        if (textLow.Substring(startId, endId).Contains(w2)) {
                            // obsahuje v okoli w2
                            numOfStrings ++; // 2 body
                        }
                    }
                        ;
                }
            }

            return numOfStrings;
        }
        public static int findNumStrInStr(string text , string searchWord) {

            int numOfStrings = 0;
            int lastIdx = 0;
            int tmp=0;

            string wordLow = searchWord.ToLower();
            string textLow = text = text.ToLower();

            if (textLow.Contains(wordLow))
            {
                for (int i = 0; tmp >= 0 & i < textLow.Length; i++)
                    if ((tmp = textLow.IndexOf(wordLow, lastIdx)) >= 0)
                    {
                        numOfStrings++;
                        lastIdx = tmp + 1;
                    }
                    else
                    {
                        break;
                    }
            }


            return numOfStrings;
        }

        public static void calcScore(Position[] arrayPos, string text) {
            // das tomu pole positinos (ten obsahuje score a keywordList) a zadaj potom text z CV
        
            /*
             * do scere vlozi pocet vyskytov key words vyskytov v CV
            */
        
            for (int i = 0; i < arrayPos.Length; i++) {

                arrayPos[i].Score = 0;
                int lenStringList = arrayPos[i].StringList.Count;
                for (int j = 0; j < lenStringList; j++) {

                    arrayPos[i].Score += findNumStrInStr(text, arrayPos[i].StringList[j].ToString());
                    
                    
                }            
            }
        
        
        }


        public static void calcScore2(Position[] arrayPos, string text){
            
            for (int i = 0; i < arrayPos.Length; i++)
            {

                arrayPos[i].Score = 0;
                int lenStringList = arrayPos[i].StringList.Count;

                for (int j = 0; j < lenStringList; j++)
                {

                    
                    string keyWord = arrayPos[i].StringList[j].ToString();

                    if (keyWord.Contains(" ")){
                        // keyword obsahuje viac ako jedno slovo

                        int posSpace = keyWord.IndexOf(" ");
                        string w1 = keyWord.Substring(0, posSpace);
                        string w2 = keyWord.Substring(posSpace + 1, keyWord.Length);
                        // funguje to iba na 1 a dvojslovne keywords

                        arrayPos[i].Score += findNumStrInStr(text, w1, w2);

                    }
                    else {
                        // keyword je jedno slovo
                        arrayPos[i].Score += findNumStrInStr(text, keyWord);
                    }

                }
            }
        }
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
    }
}
