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
using System.Windows.Shapes;
using UI.ViewModels;
using UI.Share;
using System.Timers;
using System.Windows.Threading;

namespace UI.Views
{
    /// <summary>
    /// Логика взаимодействия для LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        private Config _config;
        public LogInWindow(Config config)
        {
            InitializeComponent();

            _config = config;
        }

        private void DragPlace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        int counterTryes = 0;
        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher dispatcher = Dispatcher;
            Timer timer = new Timer();
            timer.Interval = 10_000;
            counterTryes++;

            if (counterTryes == 3)
            {
                timer.Elapsed += new ElapsedEventHandler((x, y) =>
                {
                    counterTryes = 0;
                    dispatcher.BeginInvoke(new Action(() =>
                    {
                        errorCountsLabel.Visibility = Visibility.Hidden;
                    }));
                });
                timer.Start();

                errorCountsLabel.Content = "Превышен лимит попыток.";
                errorCountsLabel.Visibility = Visibility.Visible;
            }
            if (counterTryes > 3) return;
            var vm = DataContext as LogInViewModel;
            if (vm == null) return;

            string name = LoginField.Text;
            string password = PasswordField.Password;
            vm.LogIn(name, password, _config);

            if (_config.idSession == null)
            {
                errorLabel.Visibility = Visibility.Visible;
                return;
            }

            this.Close();
        }
    }
}
