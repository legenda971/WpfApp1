using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Navigation;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {

        HenkelJobPosition records;
        DataProcessing data;

        public MainWindow()
        {

            data = new DataProcessing();

            records = data.recordsProcessing("..\\..\\..\\henkel.xml");
            data.stringsProcessing(records);


            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxGDPR.IsChecked == true)
            {
                CheckBoxGDPR.IsEnabled = false;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "PDF files (*.pdf)|*pdf";

                if (openFileDialog.ShowDialog()==true)
                {

                    foreach (string pathPDF in openFileDialog.FileNames)
                    {
                        string pdfText = Samo.readPDF(pathPDF);
                        processingPDF(records.Position, pdfText, pathPDF);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pre pokračovanie musite súhlasiť s podmienkami.");
            }
        }

        private void otvoritPDF_Click(object sender, RoutedEventArgs e) {
            TodoItem item = pdfList.SelectedItem as TodoItem;
            if (item == null)
                return;

            System.Diagnostics.Process.Start("explorer.exe", item.patch);
        }

        private void pdfList_Change(object sender, RoutedEventArgs e){
            TodoItem item = pdfList.SelectedItem as TodoItem;
            if (item == null)
                return;
            textBlockNameRole.Text = item.role;
            textBlockRoleKappa.Text = item.info;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://www.henkel.sk/spolocnost/riadenie-a-sulad-s-legislativou/spracuvanie-osobnych-udajov?fbclid=IwAR1alwPhX5n7AyrSo39nhJ87hhJNICfbTgJOgKmGAcMsP-91Q4Y2Cx2eHlg");
            e.Handled = true;
        }

        private void processingPDF(Position[] position, string pdfText, string path)
        {
            Samo.calcScore(position, pdfText);
            List<TodoItem> temp = pdfList.ItemsSource as List<TodoItem>;
           
            if (temp == null)
            {
                pdfList.ItemsSource = data.bestItem(records.Position, path);
            }
            else
            {
                List<TodoItem> newList = new List<TodoItem>(temp);
                newList.Add(data.bestItem(records.Position, path)[0]);
                pdfList.ItemsSource = newList;
            }

     
        }

        public class TodoItem
        {
            public string nameFile { get; set; }
            public string patch { get; set; }
            public string role { get; set; }
            public string info { get; set; }
        }
    }

}