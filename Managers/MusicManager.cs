using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Uranus.Managers
{
    public class MusicManager
    {
        public float maxVolume = 0.25f;
        public float currentVolume = 0.0001F;
        private SoundEffectInstance instance;

        public MusicManager(ContentManager content)
        {
            GameMusic = content.Load<SoundEffect>("To_the_Next_Destination");
        }

        public void PlayMenuMusic()
        {
            currentVolume = maxVolume;

            instance = GameMusic.CreateInstance();
            instance.Volume = currentVolume;
            instance.Play();
        }

        public void PlayGameMusic()
        {
            instance = GameMusic.CreateInstance();
            maxVolume = 0.4f;
            currentVolume = 0.050f;
            instance.Volume = currentVolume;
            instance.Play();
        }

        public void Stop()
        {
            instance.Stop();
        }

        public SoundEffect GameMusic { get; set; }

        public void UpdateGame(GameTime gameTime)
        {
            if(instance.State == SoundState.Stopped)
                instance.Play();
            if (currentVolume < maxVolume)
            {
                currentVolume += 0.0005F;
            }

            instance.Volume = currentVolume;
        }
    }
}
