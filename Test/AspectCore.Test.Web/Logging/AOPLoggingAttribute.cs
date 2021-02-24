 using System;
using System.Threading.Tasks;
using AspectCore.Aspects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspectCore.Test.Web.Logging
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AOPLoggingAttribute: AspectAttribute
    {
        private ILogger<AOPLoggingAttribute> _logger;
        public override int Order => 1;

        public override void OnBefore(MethodExecutionArgs args)
        {
            LogExecutingMethodInfo("OnBefore", args);
        }

        public override Task OnBeforeAsync(MethodExecutionArgs args)
        {
            LogExecutingMethodInfo("OnBeforeAsync", args);
            return Task.CompletedTask;
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            LogExecutingMethodInfo("OnSuccess", args);
        }

        public override void OnAfter(MethodExecutionArgs args)
        {
            LogExecutingMethodInfo("OnAfter", args);
        }

        private void LogExecutingMethodInfo(string attributeMethodName, MethodExecutionArgs args)
        {
            _logger.LogInformation("{AttributeMethodName} method excecuting. " +
                                   "Method Name : {MethodName}, " +
                                   "AttributeName = {AttributeName}, " +
                                   "Order : {Order}", attributeMethodName, args.Method.Name, GetType().Name, Order);
        }

        public override AspectAttribute LoadDependencies(IServiceProvider serviceProvider)
        {
            _logger ??= serviceProvider.GetService<ILogger<AOPLoggingAttribute>>();
            return this;
        }
    }
}