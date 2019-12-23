using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для NavigationPanel.xaml
    /// </summary>
    public partial class NavigationPanel : UserControl
    {
        public event Action LogInButtonClick;
        public event Action NewsButtonClick;
        public event Action CpecialsButtonClick;
        public event Action AboutCollegeButtonClick;

        public NavigationPanel()
        {
            InitializeComponent();
        }

        public void avatarChange(Image avatar)
        {
            AvatarImage.Background = new ImageBrush(avatar.Source);
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if(depObj != null)
                for(int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj);i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                        yield return (T)child;
                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
        }

        private void ChangeSelectedZone(Button button)
        {
            foreach(Button NavigationButtons in FindVisualChildren<Button>(this))
                NavigationButtons.Foreground = new SolidColorBrush(Color.FromRgb(227, 229, 236));
            SelectZone.Margin = new Thickness(0, button.Margin.Top - 10, 0, 0);
            button.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x2A, 0x2C, 0x44));
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            LogInButtonClick.Invoke();
        }

        private void NewsButton_Click(object sender, RoutedEventArgs e)
        {
            NewsButtonClick.Invoke();
            ChangeSelectedZone(NewsButton);
        }

        private void CpecialsButton_Click(object sender, RoutedEventArgs e)
        {
            CpecialsButtonClick.Invoke();
            ChangeSelectedZone(CpecialsButton);
        }

        private void AboutCollegeButton_Click(object sender, RoutedEventArgs e)
        {
            AboutCollegeButtonClick.Invoke();
            ChangeSelectedZone(AboutCollegeButton);
        }
    }
}
