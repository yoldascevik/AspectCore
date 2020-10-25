using AspectCore.Test.Unit.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AspectCore.Test.Unit
{
    public class ServiceCollectionExtensionTest
    {
        [Fact]
        public void DecorateWithAspect_ExpectedTrue()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddTransient<IAspectTestService1, AspectTestService1>()
                .DecorateWithAspect<IAspectTestService1>();
            
            var provider = services.BuildServiceProvider();

            // Actual
            var testService = provider.GetRequiredService<IAspectTestService1>();
            bool doWork = testService.DoWork();

            // Assert
            Assert.True(doWork);
        }
        
        [Fact]
        public void DecorateAllInterfacesImplemented_ExpectedTrue()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddTransient<IAspectTestService1, AspectTestService1>()
                .AddTransient<IAspectTestService2, AspectTestService2>()
                .DecorateAllInterfacesImplemented<IAspectDecorated>(typeof(IAspectTestService1).Assembly);
            
            var provider = services.BuildServiceProvider();

            // Actual
            var testService1 = provider.GetRequiredService<IAspectTestService1>();
            var testService2 = provider.GetRequiredService<IAspectTestService2>();
            var doWork1 = testService1.DoWork();
            var doWork2 = testService2.DoWork();

            // Assert
            Assert.True(doWork1);
            Assert.True(doWork2);
        }
        
        [Fact]
        public void DecorateAllInterfacesUsingAspect_ExpectedTrue()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddTransient<IAspectTestService1, AspectTestService1>()
                .AddTransient<IAspectTestService2, AspectTestService2>()
                .DecorateAllInterfacesUsingAspect(typeof(IAspectTestService1).Assembly);
            
            var provider = services.BuildServiceProvider();

            // Actual
            var testService1 = provider.GetRequiredService<IAspectTestService1>();
            var testService2 = provider.GetRequiredService<IAspectTestService2>();
            var doWork1 = testService1.DoWork();
            var doWork2 = testService2.DoWork();

            // Assert
            Assert.True(doWork1);
            Assert.True(doWork2);
        }
    }
}