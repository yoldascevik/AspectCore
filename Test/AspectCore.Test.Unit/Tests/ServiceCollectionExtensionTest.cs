using AspectCore.Test.Unit.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AspectCore.Test.Unit.Tests
{
    public class ServiceCollectionExtensionTest
    {
        [Fact]
        public void DecorateWithAspect_ExpectedMethodInvoke()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddTransient<IAspectTestService1, AspectTestService1>()
                .DecorateWithAspect<IAspectTestService1>();
            
            var provider = services.BuildServiceProvider();
            var testService = provider.GetRequiredService<IAspectTestService1>();
            var expectedMethodInvoke = InvokeMethod.OnBefore;

            // Actual
            InvokeMethod actualMethodInvoke = testService.TestAspectMethodInvoke(expectedMethodInvoke);

            // Assert
            Assert.Equal(expectedMethodInvoke, actualMethodInvoke);
        }
        
        [Fact]
        public void DecorateAllInterfacesImplemented_ExpectedMethodInvoke()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddTransient<IAspectTestService1, AspectTestService1>()
                .AddTransient<IAspectTestService2, AspectTestService2>()
                .DecorateAllInterfacesImplemented<IAspectDecorated>(typeof(IAspectTestService1).Assembly);
            
            var provider = services.BuildServiceProvider();
            var testService1 = provider.GetRequiredService<IAspectTestService1>();
            var testService2 = provider.GetRequiredService<IAspectTestService2>();
            var expectedMethodInvoke = InvokeMethod.OnBefore;

            // Actual
            InvokeMethod actualMethodInvoke1 = testService1.TestAspectMethodInvoke(expectedMethodInvoke);
            InvokeMethod actualMethodInvoke2 = testService2.TestAspectMethodInvoke(expectedMethodInvoke);
            
            // Assert
            Assert.Equal(expectedMethodInvoke, actualMethodInvoke1);
            Assert.Equal(expectedMethodInvoke, actualMethodInvoke2);
        }
        
        [Fact]
        public void DecorateAllInterfacesUsingAspect_ExpectedMethodInvoke()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddTransient<IAspectTestService1, AspectTestService1>()
                .AddTransient<IAspectTestService2, AspectTestService2>()
                .DecorateAllInterfacesUsingAspect(typeof(IAspectTestService1).Assembly);
            
            var provider = services.BuildServiceProvider();
            var testService1 = provider.GetRequiredService<IAspectTestService1>();
            var testService2 = provider.GetRequiredService<IAspectTestService2>();
            var expectedMethodInvoke = InvokeMethod.OnBefore;

            // Actual
            InvokeMethod actualMethodInvoke1 = testService1.TestAspectMethodInvoke(expectedMethodInvoke);
            InvokeMethod actualMethodInvoke2 = testService2.TestAspectMethodInvoke(expectedMethodInvoke);
            
            // Assert
            Assert.Equal(expectedMethodInvoke, actualMethodInvoke1);
            Assert.Equal(expectedMethodInvoke, actualMethodInvoke2);
        }
        
        [Fact]
        public void DecorateAllInterfacesUsingAspect_ForInterfaceLevel_ExpectedMethodInvoke()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddTransient<IInterfaceLevelAspectTestService, InterfaceLevelAspectTestService>()
                .DecorateAllInterfacesUsingAspect(typeof(IInterfaceLevelAspectTestService).Assembly);
            
            var provider = services.BuildServiceProvider();
            var testService = provider.GetRequiredService<IInterfaceLevelAspectTestService>();
            var expectedMethodInvoke = InvokeMethod.OnBefore;

            // Actual
            InvokeMethod actualMethodInvoke = testService.TestAspectMethodInvoke(expectedMethodInvoke);
            
            // Assert
            Assert.Equal(expectedMethodInvoke, actualMethodInvoke);
        }
    }
}