using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskManager.Model;

namespace TaskManager
{
    /// <summary>
    /// Логика взаимодействия для LogginWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        public List<MetaData> LogData { get; private set; }

        public LogWindow(List<MetaData> datas)
        {
            InitializeComponent();
            LogData = datas;
            ListLogs.ItemsSource = LogData;
        }
    }
}
