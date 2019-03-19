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

namespace ChatServer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        ListBox UserList = (ListBox) ReadUsers();
        

        private void Create_User_Button_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User()
            {
                //UserName;
            };
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

        public static IEnumerable<User> ReadUsers()
        {
            using (var db = new LiteDatabase(@"data.db"))
            {
                return db.GetCollection<User>().FindAll();
            }
        }

        void UpdateUser()
        {
            GoToChangeUserWindow();
            using (var db = new LiteDatabase(@"data.db"))
            {

            }
        }

        void CreateUser()
        {
            GoToChangeUserWindow();
            using (var db = new LiteDatabase(@"data.db"))
            {

            }
        }

        void DeleteUser()
        {
            using (var db = new LiteDatabase(@"data.db"))
            {

            }
        }

        private void GoToChangeUserWindow()
        {
            ChangeUserWindow changeUserWindow = new ChangeUserWindow();
            changeUserWindow.Show();
        }


    }
}
