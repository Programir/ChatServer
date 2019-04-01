using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChatServer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        
        public MainWindow()
        {
            var users = ReadUsers();
            UsersList = users != null
                ? new ObservableCollection<User>(users)
                : new ObservableCollection<User>();
                
            InitializeComponent();
        }

        public ObservableCollection<User> UsersList { get; set; }

        public string userName { get { return SelectedUser.UserName; } set { SelectedUser.UserName = value; } }
        public string password { get { return SelectedUser.Password; } set { SelectedUser.Password = value; } }
        public string email { get { return SelectedUser.Email; } set { SelectedUser.Email = value; } }
        

        private User selectedUser;

         public User SelectedUser
        {
            get { return selectedUser; }
            set {
                selectedUser = value;
                OnPropertyChanged(nameof(userName));
                OnPropertyChanged(nameof(password));
                OnPropertyChanged(nameof(email));
                
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
            using (var db = new LiteDatabase(@"data.db"))
            {
                var LiteDBUsers = db.GetCollection<User>();
                if (LiteDBUsers == null)
                { LiteDBUsers.Insert(UsersList); MessageBox.Show("Сохранено"); }
                else
                {
                    LiteDBUsers.Update(UsersList);
                    //int length = LiteDBUsers.LongCount;
                    MessageBox.Show("Обновлено");
                }
            }
        }
        
        private void Delete_User_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {

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

            //using (var db = new LiteDatabase(@"data.db"))
            //{

            //}
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

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
