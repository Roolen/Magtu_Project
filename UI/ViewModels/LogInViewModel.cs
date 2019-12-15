using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace UI.ViewModels
{
    class LogInViewModel: BaseViewModel
    {
        public void LogIn(string name, string password)
        {
            Task task = Task.Factory.StartNew(async () => { await ParserHtml.Authorize(name, password); });
            
        }
    }
}
