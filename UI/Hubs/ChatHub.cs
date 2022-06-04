using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Hubs
{
    public class ChatHub :Hub
    {
        public async Task SendMessage(int SenderId, string message, string to ,int userid)
        {
            string txt = "";
            if (to=="To Admin")
            {
                //جايه من الشات فيو اللي في هوم كونترولر
                //يوزر باعت ماسدج للأدمن 
                //جايه بإسم اليوزر دا والماسدج بتاعته
                txt = "My text as user";
                await Clients.All.SendAsync("ReceiveMessage" , message,txt,SenderId,userid);
            }
            else
            {
                //جايه من الشات فيو اللي في ادمن كونترولر
                //باعته من الادمن ليوزر معين
                //جايه بإسم الادمن دا والماسدج بتاعته و اسم اليوزر اللي هبعتله
                txt = "My text as admin";
                await Clients.All.SendAsync("ReceiveMessage", message , txt, SenderId,userid);
            }
            
        }
    }
}
