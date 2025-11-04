namespace LumosLib.Core
{
    public class AudioManager : BaseAudioManager, IAudioManager
    {
        #region >--------------------------------------------------- PROPERTIES


        public override int PreInitOrder => (int)PreInitializeOrder.Audio;
        
        
        #endregion
        #region >--------------------------------------------------- INIT


        public override void PreInit()
        {
            base.PreInit();
            
            Global.Register(this);
            
            PreInitialized = true;
        }
        

        #endregion
        #region >--------------------------------------------------- PLAY

        
        public void PlayBGM(int bgmType, int assetId)
        {
            if (_bgmPlayers.TryGetValue(bgmType, out var containsPlayer))
            {
                Play(assetId, containsPlayer);
                return;
            }
            
            var bgmPlayer = _poolManager.Get(_playerPrefab);
            
            _bgmPlayers[bgmType] = bgmPlayer;
            
            Play(assetId, bgmPlayer);
        }
        
        public void PlaySFX(int assetId)
        {
            Play(assetId, _poolManager.Get(_playerPrefab));
        }
        
        
        #endregion
        #region >--------------------------------------------------- STOP


        public void StopBGM(int bgmType)
        {
            if (_bgmPlayers.TryGetValue(bgmType, out var containsPlayer))
            {
                containsPlayer.Stop();
            }
        }
        
        public void StopAll()
        {
            foreach (var player in _activePlayers)
            {
                player.Stop();
            }
        }

        
        #endregion
        #region >--------------------------------------------------- PUASE

        
        public void PauseBGM(int bgmType, bool enable)
        {
            if (_bgmPlayers.TryGetValue(bgmType, out var containsPlayer))
            {
                containsPlayer.Pause(enable);
            }
        }
        
        public void PauseAll(bool enable)
        {
            foreach (var player in _activePlayers)
            {
                player.Pause(enable);
            }
        }
        
        
        #endregion
    }
}