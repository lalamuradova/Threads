using CopyTextWithThreads.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

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
        public int Count { get; set; } = 0;
        public OpenFileDialog Open { get; set; } = new OpenFileDialog();
        public Thread thread1;

        public DispatcherTimer DT { get; set; } = new DispatcherTimer();
        public bool isSuspend { get; set; } = false;
        public bool IsPause { get; set; } = false;
        public MainWindow MainWindow { get; set; }
        public int maxLength { get; set; }
        public MainViewModel(MainWindow mainWindow)
        {
            
            MainWindow = mainWindow;
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
               
            maxLength = Data.Length;
                 thread1 = new Thread(() =>
                  {
                if (Data != null)
                {
                     DT.Interval = TimeSpan.FromSeconds(1);
                          DT.Tick += DT_Tick;
                          DT.Start();
                          for (int i = 0; i < Data.Length;i++)
                          {
                              Count = i;
                            CopyData += Data[i];
                            File.WriteAllText(Open.FileName, CopyData);  

                          }
                    
                }
                 });

                thread1.Start();
            });


            PauseCommand = new RelayCommand((sender) =>
            {
                try
                {
                    thread1.Suspend();
                    DT.Stop();
                    isSuspend = true;                  
                }
                catch (Exception)
                {                   
                }
                
            });


            ResumeCommand = new RelayCommand((sender) =>
            {
                try
                {
                    thread1.Resume();
                    DT.Start();
                    isSuspend = false;
                }
                catch (Exception)
                {                    
                }
                
            });

        }

        private void DT_Tick(object sender, EventArgs e)
        {
            if (!isSuspend)
            {
                MainWindow.Progress.Maximum = maxLength;
                MainWindow.Progress.Value += Count;

                //double value = maxLength;
                //Duration duration = new Duration(TimeSpan.FromSeconds(15));
                //DoubleAnimation doubleanimation = new DoubleAnimation(value, duration);
                //MainWindow.Progress.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
            }
           
        }
    }
}
