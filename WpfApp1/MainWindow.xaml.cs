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
    public partial class MainWindow : Window {
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
                System.Console.WriteLine("Browser patch");
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "PDF files (*.pdf)|*pdf";
                openFileDialog.ShowDialog();
                TextBlock.Text = (string)(openFileDialog.FileName);
                //System.Console.WriteLine("Path : " + openFileDialog.FileName);
                string pdfText = Samo.readPDF(openFileDialog.FileName);
                processingPDF(records.Position ,pdfText);


            }
            else {
                MessageBox.Show("GDPR ?");
            }
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://eugdpr.org");
            e.Handled = true;
        }

        private void processingPDF(Position[] position, string pdfText) {

            Samo.calcScore(position, pdfText);


        }
    }

}
