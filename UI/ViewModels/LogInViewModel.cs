using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using UI.Share;

namespace UI.ViewModels
{
    class LogInViewModel: BaseViewModel
    {
        public void LogIn(string name, string password, Config config)
        {
            var task = Task.Run(() => ParserHtml.Authorize(name, password));
            //task.Wait();
            config.idSession = task.Result;

            if (config.idSession == null) return;
            var taskAv = Task.Run(() => ParserHtml.GetAvatarAddress(config.idSession));
            config.AvatarAddress = taskAv.Result;
        }
    }
}
