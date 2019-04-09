using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IServer
    {
        [OperationContract]
        bool CheckUser(string username, string password);
    }
}
