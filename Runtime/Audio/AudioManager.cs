using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace LumosLib
{
    public class AudioManager : MonoBehaviour, IPreInitialize, IAudioManager
    {
        #region >--------------------------------------------------- PROPERTIE

        
        public int PreInitOrder => (int)PreInitializeOrder.Audio;
        public bool PreInitialized { get; private set; }
        

        #endregion
        #region >--------------------------------------------------- FIELD
      
        
        private readonly Dictionary<int, AudioPlayer> _bgmPlayers = new();
        private readonly HashSet<AudioPlayer> _activePlayers = new();
        private readonly Dictionary<string, SoundAsset> _assetResources = new();
        
        private IPoolManager _poolManager;
        private AudioPlayer _playerPrefab;
        private AudioMixer _mixer;
        
        
        #endregion
        #region >--------------------------------------------------- UNITY

        protected virtual void Awake()
        {
            _poolManager = Global.GetInternal<IPoolManager>();
            
            var resourceManager = Global.GetInternal<IResourceManager>();
            var resources = resourceManager.LoadAll<SoundAsset>("");
            
            foreach (var resource in resources)
            {
                _assetResources[resource.name] = resource;
            }

            var preInitializeConfig = PreInitializer.Instance.Config;
            
            _playerPrefab = preInitializeConfig.AudioPlayerPrefab;
            if (_playerPrefab == null)
            {
                DebugUtil.LogError(" wrong audio player path ", " INIT FAIL ");
                return;
            }
            
            _mixer = preInitializeConfig.Mixer;
            
            Global.Register<IAudioManager>(this);
            
            DontDestroyOnLoad(gameObject);

            PreInitialized = true;
        }
        
        #endregion
        #region >--------------------------------------------------- GET & SET 
       

        public void SetVolume(string groupName, float volume)
        {
            _mixer.SetFloat(groupName, Mathf.Log10(volume) * 20f);
        }

        private AudioPlayer GetBGMPlayer(int bgmType)
        {
            return _bgmPlayers.GetValueOrDefault(bgmType);
        }

        private AudioPlayer CreateNewPlayer()
        {
            var player = _poolManager.Get(_playerPrefab);
            
            _activePlayers.Add(player);
            
            return player;
        }


        #endregion
        #region >--------------------------------------------------- PLAY


        public void PlayBGM(int bgmType, string assetName)
        {
            if (_assetResources.TryGetValue(assetName, out SoundAsset asset))
            {
                var bgmPlayer = GetBGMPlayer(bgmType) ?? CreateNewPlayer();

                _bgmPlayers.TryAdd(bgmType, bgmPlayer);
            
                bgmPlayer.Play(asset, _ =>
                {
                    OnStopBGM(bgmType, bgmPlayer);
                });
            }
        }

        public void PlaySFX(string assetName)
        {
            if (_assetResources.TryGetValue(assetName, out SoundAsset asset))
            {
                CreateNewPlayer().Play(asset, OnStopSFX);
            }
        }
        
        
        #endregion
        #region >--------------------------------------------------- STOP


        public void StopBGM(int bgmType)
        {
            GetBGMPlayer(bgmType)?.Stop();
        }
        
        public void StopSFXAll()
        {
            var temp = new List<AudioPlayer>(_activePlayers);
            
            foreach (var player in temp)
            {
                if (!_bgmPlayers.ContainsValue(player))
                {
                    player.Stop();
                }
            }
        }

        
        public void StopAll()
        {
            var temp = new List<AudioPlayer>(_activePlayers);
            
            foreach (var player in temp)
            {
                player.Stop();
            }
        }

        private void OnStopBGM(int bgmType, AudioPlayer player)
        {
            _poolManager.Release(player);

            _bgmPlayers.Remove(bgmType);
            
            if (_activePlayers.Contains(player))
            {
                _activePlayers.Remove(player);
            }
        }
        
        private void OnStopSFX(AudioPlayer player)
        {
            _poolManager.Release(player);
            
            if (_activePlayers.Contains(player))
            {
                _activePlayers.Remove(player);
            }
        }


        #endregion
        #region >--------------------------------------------------- PAUSE


        public void PauseBGM(int bgmType, bool enable)
        {
            GetBGMPlayer(bgmType)?.Pause(enable);
        }
        
        public void PauseSFXAll(bool enable)
        {
            var temp = new List<AudioPlayer>(_activePlayers);
            
            foreach (var player in temp)
            {
                if (!_bgmPlayers.ContainsValue(player))
                {
                    player.Pause(enable);
                }
            }
        }

        public void PauseAll(bool enable)
        {
            var temp = new List<AudioPlayer>(_activePlayers);
            
            foreach (var player in temp)
            {
                player.Pause(enable);
            }
        }


        #endregion
    }
}