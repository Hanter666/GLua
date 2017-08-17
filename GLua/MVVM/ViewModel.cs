using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GLua.Models;
using GLua.Helpers;
using AngleSharp;

namespace GLua.MVVM
{
    class ViewModel : ViewModelBase
    {
        Wiki wiki = new Wiki();
        Models.Windows WModel;
        RelayCommand closeCommand;
        RelayCommand minimizeCommand;
        RelayCommand maximizeCommand;

        public ViewModel()
        {
            //wiki.Get();
            wiki.LoadJsonObj();

            closeCommand = new RelayCommand((t) => {
                Application.Current.Shutdown();
            });
            minimizeCommand = new RelayCommand((t) => {
                if (t is Window w)
                {
                    switch (w.WindowState)
                    {
                        case WindowState.Normal:
                            w.WindowState = WindowState.Minimized;
                            break;
                        case WindowState.Maximized:
                            w.WindowState = WindowState.Minimized;
                            break;
                    }
                }
            });
            maximizeCommand = new RelayCommand((t) => {
                if (t is Window w)
                {
                    switch (w.WindowState)
                    {
                        case WindowState.Normal:
                            w.WindowState = WindowState.Maximized;
                            break;
                        case WindowState.Maximized:
                            w.WindowState = WindowState.Normal;
                            break;
                    }
                }
            });

            WModel = new Models.Windows();
            var wnd = WModel.GetWindow();
            wnd.StateChanged += (s,t) => 
            {
                OnPropertyChanged("WindowMargin");
            };
        }

        public int WindowMargin
        {
            get => WModel.Margin;
            set
            {
                int mrgn = WModel.Margin;
                SetProperty(ref mrgn,value);
            }
        }

        public List<Function> Wiki {
            get
            {
                return wiki.Functions;
            }
            set
            {
                if (Equals(wiki.Functions, value))
                {
                    return;
                }
                wiki.Functions = value;
                OnPropertyChanged("Wiki");
            }
        }

        public RelayCommand CloseCommand
        {
            get => closeCommand;
            set => SetProperty<RelayCommand>(ref closeCommand, value);
        }

        public RelayCommand MinimizeCommand
        {
            get => minimizeCommand;
            set => SetProperty<RelayCommand>(ref minimizeCommand, value);
        }

        public RelayCommand MaximizeCommand
        {
            get => maximizeCommand;
            set => SetProperty<RelayCommand>(ref maximizeCommand, value);
        }
    }
}
