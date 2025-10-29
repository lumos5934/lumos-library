using System.Collections;
using UnityEngine;

public abstract class GameSceneManager : GameManagerComponent
{
    #region  --------------------------------------------------- PROPERTIES

    
    public override int Order { get; protected set; } = 0;
    
    
    #endregion
    #region  --------------------------------------------------- UNITY

    
    private void Awake()
    {
        StartCoroutine(InitAsync());
    }

    private void OnDestroy()
    {
        GameManager.Instance?.Unregister(this);
    }
    
    
    #endregion
    #region  --------------------------------------------------- INIT
    
    
    private IEnumerator InitAsync()
    {
        var gameMgr = GameManager.Instance;
        
        yield return new WaitUntil(() => gameMgr.IsInitialized);

        gameMgr.Register(this);
        
        Init();
    }
    
    
    #endregion
}