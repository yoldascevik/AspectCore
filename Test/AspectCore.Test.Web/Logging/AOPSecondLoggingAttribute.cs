using System;

namespace AspectCore.Test.Web.Logging
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AOPSecondLoggingAttribute : AOPLoggingAttribute
    {
        public override int Order => 2;
    }
}