using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace WeakSven
{
    class AudioSFX
    {
        public SoundEffect Sound { get; set; }
        private double startTime = 0.0;

        public bool FireAndForget()
        {
            return Sound.Play();
        }

        public bool Play(GameTime gameTime)
        {
            if (startTime == 0 ||
                gameTime.TotalGameTime.TotalSeconds - startTime >= Sound.Duration.TotalSeconds)
            {
                Sound.Play();
                startTime = gameTime.TotalGameTime.TotalSeconds;
                return true;
            }

            return false;
        }
    }
}