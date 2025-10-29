using UnityEngine;

public class TemplateSceneManager : GameSceneManager
{
    public override bool IsInitialized { get; protected set; }
    public override void Init()
    {
        IsInitialized = true;
    }
}