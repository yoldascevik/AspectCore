using System;
using System.Reflection;

namespace AspectCore
{
    public class MethodExecutionArgs
    {
        public MethodExecutionArgs() { }

        public MethodExecutionArgs(
            MethodInfo method, 
            object[] arguments, 
            IServiceProvider serviceProvider)
        {
            Method = method;
            Arguments = arguments;
            ServiceProvider = serviceProvider;
        }
        
        /// <summary>
        /// Method info
        /// </summary>
        public MethodInfo Method { get; }
        
        /// <summary>
        /// Method arguments
        /// </summary>
        public object[] Arguments { get; }
        
        /// <summary>
        /// Error occurring within the method.
        /// </summary>
        public Exception Exception { get; internal set; }
        
        /// <summary>
        /// Method result
        /// </summary>
        public object ReturnValue { get; set; }
        
        /// <summary>
        /// Service provider for DI
        /// </summary>
        public IServiceProvider ServiceProvider { get; }
    }
}