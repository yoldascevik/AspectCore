using System;

namespace AspectCore.Test.Unit.Services
{
    public class AspectTestService2: IAspectTestService2
    {
        public InvokeMethod TestAspectMethodInvoke(InvokeMethod invokeMethod)
        {
            if (invokeMethod == InvokeMethod.OnException)
                throw new Exception("Test exception for OnException method invoke!");
            
            return InvokeMethod.Nothing;
        }
    }
}