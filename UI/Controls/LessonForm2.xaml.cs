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
    /// Логика взаимодействия для LessonForm2.xaml
    /// </summary>
    public partial class LessonForm2 : UserControl
    {
        public LessonForm2(int lNumber, string lName1, string lTeacher1, string lLocation1,
            string lName2, string lTeacher2, string lLocation2)
        {
            InitializeComponent();
            LessonNumber.Content = lNumber;
            LessonName1.Content = lName1;
            LessonTeacher1.Content = lTeacher1;
            LessonLocation1.Content = lLocation1;

            LessonName2.Content = lName2;
            LessonTeacher2.Content = lTeacher2;
            LessonLocation2.Content = lLocation2;
        }
    }
}
