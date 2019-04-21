using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ChatServer
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IChatServer
    {
        [OperationContract]
        void SendMessage(string text, string username);

        [OperationContract]
        IEnumerable<Message> GetNewMessages(DateTime lastMessageDate);
    }
}
