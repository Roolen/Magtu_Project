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
using UI.Views;
using UI.Share;

namespace UI.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public ObservableCollection<object> ContentPanel { get; set; }
        private bool IsLoading = false;
        private LogInWindow LogInWindow = null;
        private Config config = new Config();

        string _address = @"https://newlms.magtu.ru/";

        public MainViewModel()
        {
            ContentPanel = new ObservableCollection<object>();
        }

        public void LogIn()
        {
            if (LogInWindow != null) return;

            LogInWindow = new LogInWindow(config);
            LogInWindow.Show();

            LogInWindow.Closed += new EventHandler((sender, e) =>
            {
                LogInWindow = null;
            });
        }

        public void PresentNews(object dispatcher)
        {
            IsLoading = true;

            ((Dispatcher)dispatcher).BeginInvoke(new Action(() =>
            {
                Loading loading = new Loading();
                ContentPanel.Clear();
                ContentPanel.Add(loading);
            }));

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

                if (IsLoading == false) return;

                ContentPanel.Clear();
                ContentPanel.Add(newsPanel);
                IsLoading = false;
            }));
        }

        public void PresentSpecialities()
        {
            IsLoading = false;
            ContentPanel.Clear();
            ContentPanel.Add(new Specialties());
        }

        public void PresentAboutCollege()
        {
            IsLoading = false;
            ContentPanel.Clear();
            ContentPanel.Add(new AboutCollege());
        }
    }
}
