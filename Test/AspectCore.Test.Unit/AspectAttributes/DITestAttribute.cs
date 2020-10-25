using System;
using AspectCore.Aspects;
using AspectCore.Test.Unit.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AspectCore.Test.Unit.AspectAttributes
{
    public class DITestAttribute: AspectAttribute
    {
        private IDITestService _diTestService;
        
        public override void OnBefore(MethodExecutionArgs args)
        {
            args.ReturnValue = _diTestService.GetType();
        }

        public override AspectAttribute LoadDependencies(IServiceProvider serviceProvider)
        {
            _diTestService ??= serviceProvider.GetRequiredService<IDITestService>();
            return base.LoadDependencies(serviceProvider);
        }
    }
}