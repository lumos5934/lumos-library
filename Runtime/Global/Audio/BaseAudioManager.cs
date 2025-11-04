using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace LumosLib.Core
{
    public abstract class BaseAudioManager : MonoBehaviour, IPreInitialize 
    {
        #region >--------------------------------------------------- PROPERTIE

        
        public int PreID => (int)PreInitializeOrder.Audio;
        public abstract int PreInitOrder { get; }
        public bool PreInitialized { get; protected set; }
        

        #endregion
        #region >--------------------------------------------------- FIELD
      
        protected AudioMixer _mixer;
        protected IPoolManager _poolManager;
        protected AudioPlayer _playerPrefab;
        protected Dictionary<int, AudioPlayer> _bgmPlayers = new();
        protected HashSet<AudioPlayer> _activePlayers = new();
        protected Dictionary<int, SoundAssetSO> _assetResources = new();
        
        
        #endregion
        #region >--------------------------------------------------- INIT
        
        
        public virtual void PreInit()
        {
            _poolManager = Global.Get<IPoolManager>();
            
            
            var resourceManager = Global.Get<IResourceManager>();
            var resources = resourceManager.LoadAll<SoundAssetSO>(Constant.Audio);
            
            foreach (var resource in resources)
            {
                _assetResources[resource.GetID()] = resource;
            }
        
            _playerPrefab = resourceManager.Load<AudioPlayer>(Constant.AudioPlayerPrefab);
            if (_playerPrefab == null)
            {
                DebugUtil.LogError(" wrong audio player path ", " INIT FAIL ");
            }
            
            
            Global.Register((IAudioManager)this);
        }
        
        
        #endregion
        #region >--------------------------------------------------- GET & SET
        
        
        public void SetMixer(AudioMixer mixer)
        {
            _mixer = mixer;
        }

        public void SetVolume(string groupName, float volume)
        {
            _mixer.SetFloat(groupName, Mathf.Log10(volume) * 20f);
        }
        
        
        #endregion
        #region >--------------------------------------------------- PLAY
        
        
        protected void Play(int assetId, AudioPlayer player)
        {
            if (_assetResources.TryGetValue(assetId, out SoundAssetSO asset))
            {
                player.Play(asset);
                
                _activePlayers.Add(player);
            }
        }
        
        
        #endregion
        #region >--------------------------------------------------- POOL

        
        public void ReleaseAudioPlayer(AudioPlayer player)
        {
            _poolManager.Release(player);

            if (_activePlayers.Contains(player))
            {
                _activePlayers.Remove(player);
            }
        }
        
        
        #endregion
    }
}