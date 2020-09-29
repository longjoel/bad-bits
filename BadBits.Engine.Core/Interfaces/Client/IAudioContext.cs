using System;
using System.Collections.Generic;
using System.Text;

namespace BadBits.Engine.Interfaces.Client
{
    public interface IAudioContext
    {
        void playSound(string soundName, double pan, double volume);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="musicName"></param>
        /// <param name="onFinished"></param>
        void startMusic(string musicName, Action onFinished);
        void startMusic(string musicName);
        void stopMusic();
        void setMusicVolume(double volume);
    }
}
