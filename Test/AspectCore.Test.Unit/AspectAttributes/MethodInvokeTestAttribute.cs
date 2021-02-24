using System;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.Aspects;

namespace AspectCore.Test.Unit.AspectAttributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public class MethodInvokeTestAttribute: AspectAttribute
    {
        public override void OnBefore(MethodExecutionArgs args)
        {
            var invokeMethod = GetInvokeMethodArgument(args.Arguments);
            if (invokeMethod == InvokeMethod.OnBefore)
            {
                args.ReturnValue = InvokeMethod.OnBefore;
            }
        }

        public override Task OnBeforeAsync(MethodExecutionArgs args)
        {
            var invokeMethod = GetInvokeMethodArgument(args.Arguments);
            if (invokeMethod == InvokeMethod.OnBeforeAsync)
            {
                args.ReturnValue = InvokeMethod.OnBeforeAsync;
            }
            
            return Task.CompletedTask;
        }
        
        public override void OnSuccess(MethodExecutionArgs args)
        {
            var invokeMethod = GetInvokeMethodArgument(args.Arguments);
            if (invokeMethod == InvokeMethod.OnSuccess)
            {
                args.ReturnValue = InvokeMethod.OnSuccess;
            }
        }

        public override Task OnSuccessAsync(MethodExecutionArgs args)
        {
            var invokeMethod = GetInvokeMethodArgument(args.Arguments);
            if (invokeMethod == InvokeMethod.OnSuccessAsync)
            {
                args.ReturnValue = InvokeMethod.OnSuccessAsync;
            }
            
            return Task.CompletedTask;
        }
        
        public override void OnException(MethodExecutionArgs args)
        {
            var invokeMethod = GetInvokeMethodArgument(args.Arguments);
            if (invokeMethod == InvokeMethod.OnException)
            {
                args.ReturnValue = InvokeMethod.OnException;
            }
        }

        public override Task OnExceptionAsync(MethodExecutionArgs args)
        {
            var invokeMethod = GetInvokeMethodArgument(args.Arguments);
            if (invokeMethod == InvokeMethod.OnExceptionAsync)
            {
                args.ReturnValue = InvokeMethod.OnExceptionAsync;
            }
            
            return Task.CompletedTask;
        }
        
        public override void OnAfter(MethodExecutionArgs args)
        {
            var invokeMethod = GetInvokeMethodArgument(args.Arguments);
            if (invokeMethod == InvokeMethod.OnAfter)
            {
                args.ReturnValue = InvokeMethod.OnAfter;
            }
        }

        public override Task OnAfterAsync(MethodExecutionArgs args)
        {
            var invokeMethod = GetInvokeMethodArgument(args.Arguments);
            if (invokeMethod == InvokeMethod.OnAfterAsync)
            {
                args.ReturnValue = InvokeMethod.OnAfterAsync;
            }
            
            return Task.CompletedTask;
        }

        private InvokeMethod GetInvokeMethodArgument(object[] args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            var invokeMethodParam = (InvokeMethod)args.First(x => x is InvokeMethod);
            return invokeMethodParam;
        }
    }
}