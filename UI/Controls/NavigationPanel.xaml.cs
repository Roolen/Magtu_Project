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

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для NavigationPanel.xaml
    /// </summary>
    public partial class NavigationPanel : UserControl
    {
        public event Action CpecialsButtonClick;
        public NavigationPanel()
        {
            InitializeComponent();
            
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void CpecialsButton_Click(object sender, RoutedEventArgs e)
        {
            CpecialsButtonClick.Invoke();
        }
    }
}
