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

        //public List<User> UsersList = ReadUsers();
        public List<User> UsersList = ReadUsers();





        private void Create_User_Button_Click(object sender, RoutedEventArgs e)
        {
                        
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
            using (var db = new LiteDatabase(@"data.db"))
            {
                return (List<User>)db.GetCollection<User>().FindAll();
            }
        }

        void UpdateUser()
        {
           
            using (var db = new LiteDatabase(@"data.db"))
            {

            }
        }

        void CreateUser()
        {
            
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

        


    }
}
