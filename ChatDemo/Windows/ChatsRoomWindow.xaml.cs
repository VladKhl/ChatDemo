using ChatDemo.DB;
using ChatDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatDemo.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChatsRoomWindow.xaml
    /// </summary>
    public partial class ChatsRoomWindow : Window
    {
        public ChatsRoomWindow()
        {
            InitializeComponent();
            HelloLbl.Content += App.user.Username;
            Update();
        }

        private void Update()
        {
            var resLst = new List<ChatRoomAdditional>();

            var a = App.db.Employee_ChatRoom.Where(x => x.Id_Employee == App.user.Id).ToList();
            foreach (var item in a)
            {
                var lastMessages = App.db.ChatMessage.Where(x => x.Chatroom_Id == item.Id_ChatRoom);
                ChatMessage lastMessage = null;
                foreach (var lastmess in lastMessages)
                {
                    lastMessage = lastmess;
                }
                resLst.Add(new ChatRoomAdditional(item.Id, item.ChatRoom.Topic, lastMessage?.Date.ToString()));
            }

            ChatRoomsLstView.ItemsSource = resLst;
        }

        private void ChatRoomsLstView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var idEmployeeChatRoom = (ChatRoomsLstView.SelectedItem as ChatRoomAdditional).IdEmployeeChatRoom;
            var item = App.db.Employee_ChatRoom.Where(x => x.Id == idEmployeeChatRoom).FirstOrDefault();
            var window = new ChatWindow(item);
            window.Closed += (q, w) => Update();
            window.Show();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EmployeeFinderBtn_Click(object sender, RoutedEventArgs e)
        {
            new FindEmployeeWindow().Show();
        }
    }
}
