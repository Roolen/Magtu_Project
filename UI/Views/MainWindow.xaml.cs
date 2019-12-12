using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using Model;
using UI.Controls;
using UI.ViewModels;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ScrollViewer _content { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            _content = ContentMain;

            NavPanel.SelectZone.Margin = new Thickness(0, NavPanel.NewsButton.Margin.Top - 10, 0, 0);
            NavPanel.NewsButton.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x2A, 0x2C, 0x44));
        }

        

        private void DragPlace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ContentMain_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainViewModel;
            if (vm == null) return;

            Thread newThread = new Thread(new ParameterizedThreadStart(vm.PresentNews));
            newThread.Start(Dispatcher);

            NavPanel.CpecialsButtonClick += vm.PresentSpecialities;
            NavPanel.AboutCollegeButtonClick += vm.PresentAboutCollege;
        }
    }
}
