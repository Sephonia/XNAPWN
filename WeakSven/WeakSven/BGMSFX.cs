using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace WeakSven
{
    class BGMSFX
    {
        public Song Song { get; set; }
        public double startTime = 0.0;

        public bool Play(GameTime gameTime)
        {
            if (startTime == 0 ||
                gameTime.TotalGameTime.TotalSeconds - startTime >= Song.Duration.TotalSeconds)
            {
                MediaPlayer.Play(Song);
                startTime = gameTime.TotalGameTime.TotalSeconds;
                return true;
            }
            return false;
        }


    }
}