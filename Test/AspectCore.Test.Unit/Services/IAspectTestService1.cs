using System;
using System.Threading.Tasks;
using AspectCore.Test.Unit.AspectAttributes;

namespace AspectCore.Test.Unit.Services
{
    public interface IAspectTestService1: IAspectDecorated
    {
        [MethodInvokeTest]
        InvokeMethod TestAspectMethodInvoke(InvokeMethod invokeMethod);
        
        [MethodInvokeTest]
        Task<InvokeMethod> TestAspectMethodInvokeAsync(InvokeMethod invokeMethod);

        [DITest]
        public Type GetServiceType();
    }
}