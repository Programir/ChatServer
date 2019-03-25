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

        //public List<User> UsersList = ReadUsers();
        public ObservableCollection<User> UsersList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Create_User_Button_Click(object sender, RoutedEventArgs e)
        {
            CreateUser();
        }

        private void Change_User_Button_Click(object sender, RoutedEventArgs e)
        {

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

        public static List<User> ReadUsers()
        {
            var result = new List<User>();

            for (int i = 0; i < 100; i++)
            {
                result.Add(new User()
                {
                    UserName = $"UserName{i.ToString()}",
                    Email = $"user.name.{i.ToString()}@test.com"
                });
            }

            return result;

            //using (var db = new LiteDatabase(@"data.db"))
            //{
            //    return (List<User>)db.GetCollection<User>().FindAll();
            //}
        }

        void UpdateUser()
        {
           
            using (var db = new LiteDatabase(@"data.db"))
            {

            }
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

    }
}
