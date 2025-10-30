public class TemplateScene : BaseScene
{
    public override bool IsInitialized { get; protected set; }
    public override void Init()
    {
        IsInitialized = true;
    }
}