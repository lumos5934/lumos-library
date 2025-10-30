using UnityEngine;

public abstract class BaseGameComponent : MonoBehaviour
{
    public abstract int Order { get; }
    public abstract bool IsInitialized { get; protected set; }
    public abstract void Init();
}