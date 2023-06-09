﻿using ChatDemo.DB;
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
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private Employee_ChatRoom eChatRoom;
        List<Employee> lst;
        public ChatWindow(Employee_ChatRoom item)
        {
            InitializeComponent();
            eChatRoom = item;
            Update();
            MessagesLstView.ItemsSource = App.db.ChatMessage.Where(x => x.Chatroom_Id == eChatRoom.Id_ChatRoom).ToList();
        }

        private void Update()
        {
            lst = new List<Employee>();
            foreach (var chatrooms in App.db.Employee_ChatRoom.Where(x => x.Id_ChatRoom == eChatRoom.Id_ChatRoom))
            {
                lst.Add(App.db.Employee.Where(x => x.Id == chatrooms.Id_Employee).FirstOrDefault());
            }
            EmployesLstView.ItemsSource = lst;
        }

        private void LeaveChatBtn_Click(object sender, RoutedEventArgs e)
        {
            var a = App.db.Employee_ChatRoom.Where(x => x.Id_ChatRoom == eChatRoom.Id_ChatRoom && x.Id_Employee == App.user.Id).FirstOrDefault();
            App.db.Employee_ChatRoom.Remove(a);
            App.db.SaveChanges();
            MessageBox.Show("Вы вышли из чата");
            this.Close();
        }

        private void ChangeTopicBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddUserWindow(lst, eChatRoom.Id_ChatRoom);
            window.Closing += (q, s) => Update();
            window.Show();
        }

        private void SendMessageBtn_Click(object sender, RoutedEventArgs e)
        {
            ChatMessage msg = new ChatMessage
            {
                Sender_Id = App.user.Id,
                Chatroom_Id = eChatRoom.Id_ChatRoom,
                Date = DateTime.Now,
                Message = tbMessage.Text
            };
            App.db.ChatMessage.Add(msg);
            App.db.SaveChanges();
            MessagesLstView.ItemsSource = App.db.ChatMessage.Where(x => x.Chatroom_Id == eChatRoom.Id_ChatRoom).ToList();
        }
    }
}
