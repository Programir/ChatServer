using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
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
    }
}
