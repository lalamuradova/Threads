using CopyTextWithThreads.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyTextWithThreads.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public RelayCommand OpenFileCommand { get; set; }
        public RelayCommand Open2FileCommand { get; set; }
        public RelayCommand CopyCommand { get; set; }
        public RelayCommand PauseCommand { get; set; }
        public RelayCommand ResumeCommand { get; set; }
        public string Filename { get; set; }
        public string Filename2 { get; set; }
        public string Data { get; set; }
        public string CopyData { get; set; } = string.Empty;
        public int Count { get; set; }
        public OpenFileDialog Open { get; set; } = new OpenFileDialog();


        public MainViewModel(MainWindow mainWindow)
        {


            OpenFileCommand = new RelayCommand((sender) =>
            {
                Open.ShowDialog();
                Filename = Open.FileName;
                mainWindow.fromTxtBox.Text = Filename;
                Data = File.ReadAllText(Filename);
            });

            Open2FileCommand = new RelayCommand((sender) =>
            {
                Open.ShowDialog();
                Filename2 = Open.FileName;
                mainWindow.toTxtBox.Text = Filename2;
            });

            CopyCommand = new RelayCommand((sender) =>
            {
                if (Data != null)
                {
                    for (int i = 0; i < Data.Length; i++)
                    {
                        Count = i;
                       



                    }
                }
            });




        }

        public void Copy()
        {
            CopyData += Data[Count];
            File.WriteAllText(Open.FileName, CopyData);

        }

    }
}
