using ChatDemo.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChatDemo
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string configPath = Path.GetTempPath() + "config.ini";
        public static ChatDemoEntities db = new ChatDemoEntities();
        public static Employee user;
        public static Frame MainFrame { get; set; }
    }
}
