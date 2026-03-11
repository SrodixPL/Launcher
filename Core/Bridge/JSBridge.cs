using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bridge
{
    public class JSBridge
    {
        public string Ping()
        {
            return "Pong";
        }

        public void ShowMessage(string message)
        {
            System.Windows.MessageBox.Show(message);
        }
    }
}
