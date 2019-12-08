using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using UI.Controls;

namespace UI.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public ObservableCollection<StackPanel> NewsPanel { get; set; }

        string _address = @"https://newlms.magtu.ru/";

        public MainViewModel()
        {
            NewsPanel = new ObservableCollection<StackPanel>();
        }

        public void PresentNews()
        {

            var task = Task.Run(() => ParserHtml.ParseNews(_address));
            task.Wait();

            News[] news = task.Result;
            StackPanel newsPanel = new StackPanel() { Orientation = Orientation.Vertical };
            for (int i = 0; i < news.Length; i++)
            {
                NewsPost post = new NewsPost(news[i].Title, news[i].Author, news[i].Content.ToString());
                newsPanel.Children.Add(post);
            }

            NewsPanel.Add(newsPanel);
        }

        public void PresentSpecialities()
        {
            StackPanel specialties = new StackPanel();
            specialties.Children.Add(new Specialties());
            NewsPanel.Clear();
            NewsPanel.Add(specialties);
        }
    }
}
