using AspectCore.Test.Unit.AspectAttributes;

namespace AspectCore.Test.Unit.Services
{
    public interface IAspectTestService1: IAspectDecorated
    {
        [AspectTest]
        bool DoWork();
    }
}