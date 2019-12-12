using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using UI.Controls;

namespace UI.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public ObservableCollection<object> ContentPanel { get; set; }

        string _address = @"https://newlms.magtu.ru/";

        public MainViewModel()
        {
            ContentPanel = new ObservableCollection<object>();
        }

        public void PresentNews(object dispatcher)
        {

            var task = Task.Run(() => ParserHtml.ParseNews(_address));
            task.Wait();

            ((Dispatcher)dispatcher).BeginInvoke(new Action(() =>
            {
                News[] news = task.Result;
                StackPanel newsPanel = new StackPanel() { Orientation = Orientation.Vertical };
                for (int i = 0; i < news.Length; i++)
                {
                    NewsPost post = new NewsPost(news[i].Title, news[i].Author, news[i].Content.ToString());
                    newsPanel.Children.Add(post);
                }

                ContentPanel.Add(newsPanel);
            }));
            
        }

        public void PresentSpecialities()
        {
            ContentPanel.Clear();
            ContentPanel.Add(new Specialties());
        }

        public void PresentAboutCollege()
        {
            ContentPanel.Clear();
            ContentPanel.Add(new AboutCollege());
        }
    }
}
