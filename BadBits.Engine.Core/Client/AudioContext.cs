using BadBits.Engine.Interfaces.Client;
using BadBits.Engine.Interfaces.Services;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadBits.Engine.Client
{
    public class AudioContext : IAudioContext
    {
        private IResourceManager _resourceManager;
        private SoundEffectInstance _music;
        private Action _onFinished;

        public AudioContext(IResourceManager resourceManager) {
            _resourceManager = resourceManager;
            _music = null;
        }
        public void playSound(string soundName, double pan, double volume)
        {
            _resourceManager.SoundEffectCache[soundName].Play((float)volume, 0, (float)pan);
        }

        public void setMusicVolume(double volume)
        {
            if (_music != null) {
                _music.Volume = (float)volume;
            }
        }

        public void startMusic(string musicName, Action onFinished)
        {
            stopMusic();

            _music = _resourceManager.SoundEffectCache[musicName].CreateInstance();
            _music.Play();
            _music.IsLooped =true;
            _onFinished = onFinished;
        }

        public void startMusic(string musicName)
        {
            startMusic(musicName, null);
        }

        public void stopMusic()
        {
            if (_music != null) {
                _music.Stop(true);
                if (_onFinished != null) {
                    _onFinished.Invoke();
                }
                _music.Dispose();
                _music = null;
            }

        }
    }
}
