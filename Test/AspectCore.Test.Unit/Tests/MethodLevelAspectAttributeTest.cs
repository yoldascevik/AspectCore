using System;
using System.Threading.Tasks;
using AspectCore.Test.Unit.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AspectCore.Test.Unit.Tests
{
    public class MethodLevelAspectAttributeTest
    {
        [Theory]
        [InlineData(InvokeMethod.OnBefore)]
        [InlineData(InvokeMethod.OnSuccess)]
        [InlineData(InvokeMethod.OnException)]
        [InlineData(InvokeMethod.OnAfter)]
        public void TestAspectMethodInvoke_ShouldBeExpectedResult_WithSpecificInvokeMethod(InvokeMethod invokedMethod)
        {
            // Arrange
            var services = new ServiceCollection()
                .AddTransient<IAspectTestService1, AspectTestService1>()
                .DecorateWithAspect<IAspectTestService1>();
            
            var provider = services.BuildServiceProvider();
            var testService = provider.GetRequiredService<IAspectTestService1>();

            // Actual
            InvokeMethod methodResult = testService.TestAspectMethodInvoke(invokedMethod);

            // Assert
            Assert.Equal(invokedMethod, methodResult);    
        }
        
        [Theory]
        [InlineData(InvokeMethod.OnBeforeAsync)]
        [InlineData(InvokeMethod.OnSuccessAsync)]
        [InlineData(InvokeMethod.OnExceptionAsync)]
        [InlineData(InvokeMethod.OnAfterAsync)]
        public async Task TestAspectMethodInvokeAsync_ShouldBeExpectedResult_WithSpecificInvokeMethod(InvokeMethod invokedMethod)
        {
            // Arrange
            var services = new ServiceCollection()
                .AddTransient<IAspectTestService1, AspectTestService1>()
                .DecorateWithAspect<IAspectTestService1>();
            
            var provider = services.BuildServiceProvider();
            var testService = provider.GetRequiredService<IAspectTestService1>();

            // Actual
            InvokeMethod methodResult = await testService.TestAspectMethodInvokeAsync(invokedMethod);

            // Assert
            Assert.Equal(invokedMethod, methodResult);    
        }

        [Fact]
        public void LoadDependencies_GetServiceType_ShouldBeDITestService()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddTransient<IAspectTestService1, AspectTestService1>()
                .AddTransient<IDITestService, DITestService>()
                .DecorateWithAspect<IAspectTestService1>();
            
            var provider = services.BuildServiceProvider();
            var testService = provider.GetRequiredService<IAspectTestService1>();

            // Actual
            Type calledServiceType = testService.GetServiceType();

            // Assert
            Assert.Equal(typeof(DITestService), calledServiceType);    
        }
    }
}