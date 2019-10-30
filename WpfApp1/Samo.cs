﻿using System;
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

        public static int findNumStrInStr(string text , string searchWord) {

            int numOfStrings = 0;
            int lastIdx = 0;
            int tmp=0;

            string wordLow = searchWord.ToLower();
            string textLow = text = text.ToLower();

            if (textLow.Contains(wordLow))
            {
                for (int i = 0; tmp >= 0 & i < textLow.Length; i++)
                    if ((tmp = text.IndexOf(textLow, lastIdx)) >= 0)
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
    }
}
