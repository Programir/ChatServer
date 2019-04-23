using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Server : IServer
    {
        private IEnumerable<User> _usersList;

        public Server(IEnumerable<User> usersList)
        {
            _usersList = usersList;
        }

        public bool CheckUser(string username, string password)
        {
            var result = _usersList.Any(u => u.UserName == username && u.Password == password);
            return result;
        }

        public void SendMessage(string text, string username)
        {

            var newMessage = new Message()
            {
                Text = text,
                Username = username,
                SentDateTime = DateTime.UtcNow
            };

            MainWindow.messages.Add(newMessage);
        }

        public IEnumerable<Message> GetNewMessages(DateTime newestMessageDate)
        {
            var newMessages = MainWindow.messages
                .Where(m => m.SentDateTime > newestMessageDate);

            return newMessages;
        }


    }
}
