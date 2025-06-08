namespace CarerExtensionTest;

internal class PrivateMemberTester(object target)
{
    private const BindingFlags TargetTypes =
        BindingFlags.GetProperty | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;

    public T Invoke<T>(string methodName, params object[] args) where T : class
    {
        var type = target.GetType();
        var result = type.InvokeMember(methodName, TargetTypes, null, target, args);
        return (T)(result ?? throw new ArgumentException(methodName));
    }
}
