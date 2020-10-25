using System;
using System.Threading.Tasks;

namespace AspectCore.Aspects
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class AspectAttribute: Attribute, IAspect, IAspectAsync
    {
        public virtual void OnBefore(MethodExecutionArgs args) { }
        public virtual Task OnBeforeAsync(MethodExecutionArgs args) 
            => Task.Run(() => OnBefore(args));

        public virtual void OnSuccess(MethodExecutionArgs args) { }
        public virtual Task OnSuccessAsync(MethodExecutionArgs args)
            => Task.Run(() => OnSuccess(args));
        
        public virtual void OnException(MethodExecutionArgs args) { }
        public virtual Task OnExceptionAsync(MethodExecutionArgs args)
            => Task.Run(() => OnException(args));

        public virtual void OnAfter(MethodExecutionArgs args) { }
        public virtual Task OnAfterAsync(MethodExecutionArgs args)
            => Task.Run(() => OnAfter(args));

        public virtual AspectAttribute LoadDependencies(IServiceProvider serviceProvider)
            => this;
    }
}