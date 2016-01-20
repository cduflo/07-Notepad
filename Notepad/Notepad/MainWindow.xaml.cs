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

namespace Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title = "QuikNote";
            textBoxMain.AcceptsReturn = true;
        }

        //global variables for use in this class
        public string filename = "New Document";
        bool textChanged = false;
        
        

        //Methods for key actions in this program
        public void quickSave()
        {
            if (filename == "New Document")
            {
                SaveDiag();
            }
            else
            {
                StreamWriter writer = new StreamWriter(filename);
                writer.Write(textBoxMain.Text);
                writer.Close();
                textChanged = false;
            }
        }

        public void SaveDiag()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = filename;
            dlg.DefaultExt = ".text";
            dlg.Filter = "Text documents (.txt)|*.txt|CSharp File (.cs)|*.cs";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filename = dlg.FileName;
                File.WriteAllText(filename, textBoxMain.Text);
                textChanged = false;
            }
        }

        public void openDiag()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt|CSharp File (.cs)|*.cs";

            Nullable<bool> result = dlg.ShowDialog();

            filename = dlg.FileName;

            if (result == true)
            {
                textBoxMain.Text = File.ReadAllText(filename);
                textChanged = false;
            }
        }

        public void warningMessage()
        {
            if (textChanged)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Unsaved changes. Do you want to save your work first?", "Unsaved Work", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    SaveDiag();
                }
            }
        }

        public void toggleSpellCheck()
        {
            if (MenuSpellCheck.IsChecked)
            {
                textBoxMain.SpellCheck.IsEnabled = true;
            }
            else
            {
                textBoxMain.SpellCheck.IsEnabled = false;
            }
        }

        //Event Handlers
        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            warningMessage();
            openDiag();
        }

        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            quickSave();
        }

        private void MenuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveDiag();
        }

        public void textBoxMain_TextChanged(object sender, TextChangedEventArgs e)
        {
            textChanged = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            warningMessage();
        }

        private void Clear_Doc_Click(object sender, RoutedEventArgs e)
        {
            warningMessage();
            textBoxMain.Text = "";
        }

        private void MenuSpellCheck_Click(object sender, RoutedEventArgs e)
        {
            toggleSpellCheck();
        }
    }
}
