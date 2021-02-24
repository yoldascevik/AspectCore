using System.Threading.Tasks;
using AspectCore.Test.Unit.AspectAttributes;

namespace AspectCore.Test.Unit.Services
{
    [MethodInvokeTest]
    public interface IInterfaceLevelAspectTestService
    {
        InvokeMethod TestAspectMethodInvoke(InvokeMethod invokeMethod);
        
        Task<InvokeMethod> TestAspectMethodInvokeAsync(InvokeMethod invokeMethod);
    }
}