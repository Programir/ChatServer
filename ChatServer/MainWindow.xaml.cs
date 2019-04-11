using ChatServer;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ChatServer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _secondRun;
        private ServiceHost _chatServer;

        public MainWindow()
        {
            var users = ReadUsers();
            UsersList = users != null
                ? new ObservableCollection<User>(users)
                : new ObservableCollection<User>();
                
            InitializeComponent();
        }

        public ObservableCollection<User> UsersList { get; set; }
        List<User> RemovedUsers = new List<User>();
        public string UserName
        {
            get { return SelectedUser?.UserName; }
            set
            {
                SelectedUser.UserName = value;
                UpdateList();
            }
        }
        public string Password
        {
            get { return SelectedUser?.Password; }
            set { SelectedUser.Password = value; }
        }
        public string Email
        {
            get { return SelectedUser?.Email; }
            set { SelectedUser.Email = value; }
        }
        
        private User selectedUser;

        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(UserName));
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(Email));
            }
        }

       
        /*public void PasswordBoxLoaded(RoutedEventArgs e)
        {   // Получаем ссылку на PasswordBox, т.к. привязаться напрямую к свойству Password нельзя
            if (e == null || e.OriginalSource == null)
                return;

            var passwordBox = e.OriginalSource as PasswordBox;
            if (passwordBox == null)
                return;

            _passwordBox = passwordBox;
        }*/

        public event PropertyChangedEventHandler PropertyChanged;

        private void Create_User_Button_Click(object sender, RoutedEventArgs e)
        {
            CreateUser();
        }

        private void Apply_Changes_Button_Click(object sender, RoutedEventArgs e)
        {
            var hasDuplicates = ControlDuplicates();
            if (hasDuplicates)
            {
                MessageBox.Show("В списке пользователей обнаружены повторяющиеся имена");
                return;
            }

            using (var db = new LiteDatabase(@"data.db"))
            {
                var liteDBUsers = db.GetCollection<User>();
                if (RemovedUsers.Count > 0)
                {
                    foreach (var user in RemovedUsers)
                    {
                        liteDBUsers.Delete(user.UserId);
                    }
                }

               liteDBUsers.Upsert(UsersList);
            }
        }
        
        private void Delete_User_Button_Click(object sender, RoutedEventArgs e)
        {
            User user = SelectedUser;
            UsersList.Remove(user);
            OnPropertyChanged(nameof(UsersList));
            RemovedUsers.Add(user);
        }

        private bool ControlDuplicates()
        {
            var hasDuplicates = UsersList
                .GroupBy(u => u.UserName)
                .Any(g => g.Count() > 1);

            return hasDuplicates;
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            var server = new Server(UsersList);
            _chatServer = new ServiceHost(server);

            _chatServer.ManualFlowControlLimit = Int32.MaxValue;
            _chatServer.Open();
        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            if(_chatServer != null)
                _chatServer.Close();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public List<User> ReadUsers()
        {
            using (var db = new LiteDatabase(@"data.db"))
            {
                var collection = db.GetCollection<User>().FindAll();
                if (collection == null)
                    return null;

                return collection.ToList();
            }
            
      /*    for (int i = 0; i < 100; i++)
            {
                result.Add(new User()
                {
                    UserName = $"UserName{i.ToString()}",
                    Password = $"P@ssword{i.ToString()}",
                    Email = $"user.name.{i.ToString()}@test.com"
                });
            }*/
            //return result;
        }

        void UpdateUser()
        {
            
        }

        void CreateUser()
        {
            UsersList.Add(new User() { UserName = "NewUser" });
            OnPropertyChanged(nameof(UsersList));

        }

        private void UpdateList()
        {
            OnPropertyChanged(nameof(UsersList));
            CollectionViewSource.GetDefaultView(UsersList).Refresh();
        }

        void DeleteUser()
        {
            using (var db = new LiteDatabase(@"data.db"))
            {

            }
        }

        private void Input_TextBox_Email(object sender, RoutedEventArgs e)
        {

        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Input_TextBox_Email(object sender, TextChangedEventArgs e)
        {

        }
    }
}
