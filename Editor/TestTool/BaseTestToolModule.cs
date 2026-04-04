using UnityEngine;

public abstract class BaseTestToolModule : ScriptableObject
{
    public string Title = "";
    public bool IsRunTimeOnly = false;
    
    public abstract void Init();
    public abstract void OnGUI();
}
