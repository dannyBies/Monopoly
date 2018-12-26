using System.Media;

namespace Monopoly.Audio
{
    public class AudioManager
    {
        private static SoundPlayer _clickAudio;
        private static SoundPlayer _rollDiceAudio;

        public static void PlayClickAudio()
        {
            if (_clickAudio == null)
            {
                _clickAudio = new SoundPlayer(@".\Data\Audio\Click.wav");
            }
            _clickAudio.Play();
        }

        public static void PlayRollDiceAudio()
        {
            if (_rollDiceAudio == null)
            {
                _rollDiceAudio = new SoundPlayer(@".\Data\Audio\RollDice.wav");
            }
            _rollDiceAudio.Play();
        }
    }
}
