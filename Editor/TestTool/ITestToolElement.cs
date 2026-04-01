namespace LLib.Editor
{
    public interface ITestToolElement
    {
        string Title { get; }
        int Priority { get; }
        bool IsRunTimeOnly { get; }
        void OnEnable(TestTool testTool);
        void OnGUI();
    }
}