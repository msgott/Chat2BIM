using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat2BIM
{
    public class ChatbotHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}