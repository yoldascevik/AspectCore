using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspectCore.Aspects;
using AspectCore.DispatchProxy;

namespace AspectCore.Decorators
{
    public class AspectDecorator<TDecorated> : DispatchProxyAsync
    {
        private TDecorated _decorated;
        private IServiceProvider _serviceProvider;

        public static TDecorated Create(TDecorated decorated, IServiceProvider serviceProvider)
        {
            object proxy = Create<TDecorated, AspectDecorator<TDecorated>>();
            ((AspectDecorator<TDecorated>) proxy).SetParameters(decorated, serviceProvider);
            return (TDecorated) proxy;
        }

        public override object Invoke(MethodInfo method, object[] args)
        {
            List<AspectAttribute> aspects = GetAspectAttributesFromMethodInfo(method);
            if (!aspects.Any())
            {
                return method.Invoke(_decorated, args);
            }

            // OnBefore
            var methodExecutionArgs = new MethodExecutionArgs(method, args);
            aspects.ForEach(attribute => attribute
                .LoadDependencies(_serviceProvider)
                .OnBefore(methodExecutionArgs));

            try
            {
                methodExecutionArgs.ReturnValue ??= method.Invoke(_decorated, args);

                // OnSuccess
                aspects.ForEach(attribute => attribute
                    .LoadDependencies(_serviceProvider)
                    .OnSuccess(methodExecutionArgs));
            }
            catch (Exception exception)
            {
                // OnException
                methodExecutionArgs.Exception = exception;
                aspects.ForEach(attribute => attribute
                    .LoadDependencies(_serviceProvider)
                    .OnException(methodExecutionArgs));
            }
            finally
            {
                // OnAfter
                aspects.ForEach(attribute => attribute
                    .LoadDependencies(_serviceProvider)
                    .OnAfter(methodExecutionArgs));
            }

            return methodExecutionArgs.ReturnValue;
        }

        public override async Task InvokeAsync(MethodInfo method, object[] args)
        {
            List<AspectAttribute> aspects = GetAspectAttributesFromMethodInfo(method);
            if (!aspects.Any())
            {
                await (Task) method?.Invoke(_decorated, args);
                return;
            }

            // OnBefore
            var methodExecutionArgs = new MethodExecutionArgs(method, args);
            aspects.ForEach(async attribute => await attribute
                .LoadDependencies(_serviceProvider)
                .OnBeforeAsync(methodExecutionArgs));
            
            try
            {
                await (Task) method?.Invoke(_decorated, args);

                // OnSuccess
                aspects.ForEach(async attribute => await attribute
                    .LoadDependencies(_serviceProvider)
                    .OnSuccessAsync(methodExecutionArgs));
            }
            catch (Exception exception)
            {
                // OnException
                methodExecutionArgs.Exception = exception;
                aspects.ForEach(async attribute => await attribute
                    .LoadDependencies(_serviceProvider)
                    .OnExceptionAsync(methodExecutionArgs));
            }
            finally
            {
                // OnAfter
                aspects.ForEach(async attribute => await attribute
                    .LoadDependencies(_serviceProvider)
                    .OnAfterAsync(methodExecutionArgs));
            }
        }

        public override async Task<T> InvokeAsyncT<T>(MethodInfo method, object[] args)
        {
            List<AspectAttribute> aspects = GetAspectAttributesFromMethodInfo(method);
            if (!aspects.Any())
            {
                return await (Task<T>) method?.Invoke(_decorated, args);
            }

            // OnBefore
            var methodExecutionArgs = new MethodExecutionArgs(method, args);
            aspects.ForEach(async attribute => await attribute
                .LoadDependencies(_serviceProvider)
                .OnBeforeAsync(methodExecutionArgs));

            try
            {
                methodExecutionArgs.ReturnValue ??= await (Task<T>) method?.Invoke(_decorated, args);

                // OnSuccess
                aspects.ForEach(async attribute => await attribute
                    .LoadDependencies(_serviceProvider)
                    .OnSuccessAsync(methodExecutionArgs));
            }
            catch (Exception exception)
            {
                // OnException
                methodExecutionArgs.Exception = exception;
                aspects.ForEach(async attribute => await attribute
                    .LoadDependencies(_serviceProvider)
                    .OnExceptionAsync(methodExecutionArgs));
            }
            finally
            {
                // OnAfter
                aspects.ForEach(async attribute => await attribute
                    .LoadDependencies(_serviceProvider)
                    .OnAfterAsync(methodExecutionArgs));
            }

            return (T) methodExecutionArgs.ReturnValue;
        }
        
        private List<AspectAttribute> GetAspectAttributesFromMethodInfo(MethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));

            IEnumerable<AspectAttribute> aspectAttributes = method.GetCustomAttributes<AspectAttribute>(true);
            if (method.DeclaringType != null)
            {
                aspectAttributes = aspectAttributes.Concat(method.DeclaringType.GetCustomAttributes<AspectAttribute>(true));
            }

            return aspectAttributes.Distinct().OrderBy(attr => attr.Order).ToList();
        }

        private void SetParameters(TDecorated decorated, IServiceProvider serviceProvider)
        {
            _decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
    }
}