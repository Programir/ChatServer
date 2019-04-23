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

    public partial class MainWindow : Window, INotifyPropertyChanged, IChatServer
    {
        private ServiceHost _chatServer;
        
        internal static List<Message> messages = new List<Message>(1000);

        public bool StartButtonEnabled
        {
            get { return _chatServer == null || _chatServer.State == CommunicationState.Closed; }
        }
        public bool StopButtonEnabled
        {
            get { return _chatServer != null && _chatServer.State == CommunicationState.Opened; }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void Create_User_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateUser();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Apply_Changes_Button_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                ApplyUsers();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex)
        {
            string message = ex.Message;
            string caption = "Ошибка";
            var image = MessageBoxImage.Error;
            if (ex is WarningException)
            {
                caption = "Внимание";
                image = MessageBoxImage.Warning;
            }
            else
            {
                message = $"{message}\nStackTrace:\n{ex.StackTrace}";
            }

            MessageBox.Show(message, caption, MessageBoxButton.OK, image);
        }

        private void ApplyUsers()
        {
            var hasDuplicates = ControlDuplicates();
            if (hasDuplicates)
            {
                throw new WarningException("В списке пользователей обнаружены повторяющиеся имена");
            }

            try
            {
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
            catch (Exception ex)
            {
                throw new Exception("Не удается записать изменения в базу данных. Проверьте права доступа на файл data.db, атрибут \"Только чтение\".", ex);
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
            OnPropertyChanged(nameof(StartButtonEnabled));
            OnPropertyChanged(nameof(StopButtonEnabled));
        }


        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            if(_chatServer != null)
                _chatServer.Close();

            OnPropertyChanged(nameof(StartButtonEnabled));

            OnPropertyChanged(nameof(StopButtonEnabled));
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
        }

       
        void CreateUser()
        {
            UsersList.Add(new User() { UserName = "" });
            OnPropertyChanged(nameof(UsersList));

        }

        private void UpdateList()
        {
            OnPropertyChanged(nameof(UsersList));
            CollectionViewSource.GetDefaultView(UsersList).Refresh();
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

        public void SendMessage(string text, string username)
        {
            
            var newMessage = new Message()
            {
                Text = text,
                Username = username,
                SentDateTime = DateTime.Now
            };

            messages.Add(newMessage);
        }

        public IEnumerable<Message> GetNewMessages(DateTime newestMessageDate)
        {
            var newMessages = messages
                .Where(m => m.SentDateTime > newestMessageDate);

            return newMessages;
        }


    }
}
