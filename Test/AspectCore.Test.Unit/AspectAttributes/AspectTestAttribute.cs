using AspectCore.Aspects;

namespace AspectCore.Test.Unit.AspectAttributes
{
    public class AspectTestAttribute: AspectAttribute
    {
        public override void OnBefore(MethodExecutionArgs args)
        {
            args.ReturnValue = true;
        }
    }
}