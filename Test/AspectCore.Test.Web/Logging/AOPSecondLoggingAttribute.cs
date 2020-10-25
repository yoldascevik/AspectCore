using System;
using System.Threading.Tasks;
using AspectCore.Aspects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspectCore.Test.Web.Logging
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AOPSecondLoggingAttribute: AspectAttribute
    {        
        private ILogger<AOPLoggingAttribute> _logger;

        public override int Order => 2;

        public override void OnBefore(MethodExecutionArgs args)
        {
            _logger.LogInformation($"OnBefore method excecuting. Method Name : {args.Method.Name}, AttributeName = {nameof(AOPSecondLoggingAttribute)}, Order : {Order}");
        }

        public override Task OnBeforeAsync(MethodExecutionArgs args)
        {
            _logger.LogInformation($"OnBeforeAsync method excecuting. Method Name : {args.Method.Name}, AttributeName = {nameof(AOPSecondLoggingAttribute)}, Order : {Order}");
            return Task.CompletedTask;
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            _logger.LogInformation($"OnSuccess method excecuting. Method Name : {args.Method.Name}, AttributeName = {nameof(AOPSecondLoggingAttribute)}, Order : {Order}");
        }

        public override void OnAfter(MethodExecutionArgs args)
        {
            _logger.LogInformation($"OnAfter method excecuting. Method Name : {args.Method.Name}, AttributeName = {nameof(AOPSecondLoggingAttribute)} ,  Order : {Order}");
        }

        public override AspectAttribute LoadDependencies(IServiceProvider serviceProvider)
        {
            _logger ??= serviceProvider.GetService<ILogger<AOPLoggingAttribute>>();
            return this;
        }
    }
}