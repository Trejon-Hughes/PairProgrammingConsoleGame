using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public class Audio
    {
        //These are all the audio resources with methods to run them at the appropriate times
        //Must be WAV files. File locations can be stored anywhere, but easiest to import them
        //into resources and use Properties.Resources.file_name to make it portable
        System.Media.SoundPlayer shuffle = new System.Media.SoundPlayer(Properties.Resources.SINGLE_SHUFFLE);
        System.Media.SoundPlayer win = new System.Media.SoundPlayer(Properties.Resources.YAY);
        System.Media.SoundPlayer lose = new System.Media.SoundPlayer(Properties.Resources.Drop_This_Is_Good_News);
        System.Media.SoundPlayer bet = new System.Media.SoundPlayer(Properties.Resources.CHA_CHING);
        public void ShuffleSound()
        {
            shuffle.Play();
        }

        public void WinSound()
        {
            win.Play();
        }

        public void LoseSound()
        {
            lose.Play();
        }
        public void BetSound()
        {
            bet.Play();
        }
    }
}
