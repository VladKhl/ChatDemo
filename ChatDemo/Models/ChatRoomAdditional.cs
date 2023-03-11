using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatDemo.Models
{
    public class ChatRoomAdditional
    {
        public int IdEmployeeChatRoom { get; set; }
        public string Topic { get; set; }
        public string LastMessageDate { get; set; }

        public ChatRoomAdditional(int idEmployeeChatRoom, string topic, string lastMessageDate)
        {
            IdEmployeeChatRoom = idEmployeeChatRoom;
            Topic = topic;
            LastMessageDate = lastMessageDate;
        }
    }
}
