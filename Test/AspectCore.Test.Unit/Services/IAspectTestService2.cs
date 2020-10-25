using AspectCore.Test.Unit.AspectAttributes;

namespace AspectCore.Test.Unit.Services
{
    public interface IAspectTestService2: IAspectDecorated
    {
        [AspectTest]
        bool DoWork();
    }
}