using System;
using System.Threading.Tasks;

namespace AspectCore.Test.Unit.Services
{
    public class AspectTestService1: IAspectTestService1
    {
        public InvokeMethod TestAspectMethodInvoke(InvokeMethod invokeMethod)
        {
            if (invokeMethod == InvokeMethod.OnException)
                throw new Exception("Test exception for OnException method invoke!");
            
            return InvokeMethod.Nothing;
        }
        
        public Task<InvokeMethod> TestAspectMethodInvokeAsync(InvokeMethod invokeMethod)
        {
            if (invokeMethod == InvokeMethod.OnExceptionAsync)
                throw new Exception("Test exception for OnExceptionAsync method invoke!");
            
            return Task.FromResult(InvokeMethod.Nothing);
        }

        public Type GetServiceType()
        {
            return this.GetType();
        }
    }
}