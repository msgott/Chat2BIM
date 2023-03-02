using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat2BIM
{
    public class ChatbotHub : Hub
    {
        const string fileName = "./Assets/LargeBuilding.ifc";

        
        public void Receive(string message)
        {
            string UserId = Context.ConnectionId;
            Clients.Client(UserId).ClientOwnReceive(DateTime.Now.TimeOfDay.ToString(@"hh\:mm"), message);
            System.Threading.Thread.Sleep(2000);
            Clients.Client(UserId).ClientReceive(DateTime.Now.TimeOfDay.ToString(@"hh\:mm"), "Nachricht empfangen: " + message);
        }
    }
}