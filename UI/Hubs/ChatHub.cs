using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Models;
using BLL.ViewModel;


namespace UI.Hubs
{
    public class ChatHub :Hub
    {

        readonly WonderHardwareContext _wonder;

        public ChatHub(WonderHardwareContext wonder)
        {
            _wonder = wonder;
        }
        public async Task SendMessage(int SenderId, string message, string to ,int userid)
        {
            string txt = "";
            if (to=="To Admin")
            {
                Message obj = new Message();
                obj.UserId = SenderId;
                obj.MessageText = message;
                obj.AdminOrNot = false;
                obj.Time = DateTime.Now;
                _wonder.Messages.Add(obj);
                _wonder.SaveChanges();
                txt = "My text as user";
                await Clients.All.SendAsync("ReceiveMessage" , message,txt,SenderId,userid, DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture));
            }
            else
            {
                Message obj = new Message();
                obj.UserId = userid;
                obj.AdminId = SenderId;
                obj.MessageText = message;
                obj.AdminOrNot = true;
                obj.Time = DateTime.Now;
                _wonder.Messages.Add(obj);
                _wonder.SaveChanges();
                txt = "My text as admin";
                await Clients.All.SendAsync("ReceiveMessage", message , txt, SenderId,userid, DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture));
            }
            
        }
    }
}
