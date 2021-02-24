using System.Threading.Tasks;
using AspectCore.Test.Unit.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AspectCore.Test.Unit.Tests
{
    public class InterfaceLevelAspectAttributeTest
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
                .AddTransient<IInterfaceLevelAspectTestService, InterfaceLevelAspectTestService>()
                .DecorateWithAspect<IInterfaceLevelAspectTestService>();
            
            var provider = services.BuildServiceProvider();
            var testService = provider.GetRequiredService<IInterfaceLevelAspectTestService>();

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
                .AddTransient<IInterfaceLevelAspectTestService, InterfaceLevelAspectTestService>()
                .DecorateWithAspect<IInterfaceLevelAspectTestService>();
            
            var provider = services.BuildServiceProvider();
            var testService = provider.GetRequiredService<IInterfaceLevelAspectTestService>();

            // Actual
            InvokeMethod methodResult = await testService.TestAspectMethodInvokeAsync(invokedMethod);

            // Assert
            Assert.Equal(invokedMethod, methodResult);    
        }
        
        [Theory]
        [InlineData(InvokeMethod.OnBeforeAsync)]
        [InlineData(InvokeMethod.OnSuccessAsync)]
        [InlineData(InvokeMethod.OnExceptionAsync)]
        [InlineData(InvokeMethod.OnAfterAsync)]
        public async Task TestAspectMethodInvokeAsync_ForInheritence_ShouldBeExpectedResult_WithSpecificInvokeMethod(InvokeMethod invokedMethod)
        {
            // Arrange
            var services = new ServiceCollection()
                .AddTransient<IInterfaceLevelAspectTestService, InheritenceAspectTestService>()
                .DecorateWithAspect<IInterfaceLevelAspectTestService>();
            
            var provider = services.BuildServiceProvider();
            var testService = provider.GetRequiredService<IInterfaceLevelAspectTestService>();

            // Actual
            InvokeMethod methodResult = await testService.TestAspectMethodInvokeAsync(invokedMethod);

            // Assert
            Assert.Equal(invokedMethod, methodResult);    
        }
    }
}