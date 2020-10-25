using System;

namespace AspectCore.Test.Unit.Services
{
    public class DITestService: IDITestService
    {
        public Type GetServiceType()
        {
            return this.GetType();
        }
    }
}