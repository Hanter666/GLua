using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GLua.Models
{
    class Windows
    {
        int margin = 10;
        Window w;

        public Windows()
        {
            w = Application.Current.MainWindow;
        }

        public Window GetWindow()
        {
            return w;
        }

        public int Margin
        {
            get
            {
                return w.WindowState == WindowState.Maximized ? margin : 0;
            }
            set => margin = value;
        }
    }
}
