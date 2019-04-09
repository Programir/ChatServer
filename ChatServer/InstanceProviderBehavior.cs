using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class InstanceProviderBehavior<T> : IInstanceProvider, IContractBehavior
        where T : class
    {
        private readonly Func<T> m_instanceProvider;

        public InstanceProviderBehavior(Func<T> instanceProvider)
        {
            m_instanceProvider = instanceProvider;
        }

        // I think this method is more suitable to be an extension method of ServiceHost.
        // I put it here in order to simplify the code.
        public void AddToAllContracts(ServiceHost serviceHost)
        {
            foreach (var endpoint in serviceHost.Description.Endpoints)
            {
                endpoint.Contract.Behaviors.Add(this);
            }
        }

        #region IInstanceProvider Members

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return this.GetInstance(instanceContext);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            // Create a new instance of T
            return m_instanceProvider.Invoke();
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            try
            {
                var disposable = instance as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            catch { }
        }

        #endregion

        #region IContractBehavior Members

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}
