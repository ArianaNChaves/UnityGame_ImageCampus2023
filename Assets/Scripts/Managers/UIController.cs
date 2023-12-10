using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIController : MonoBehaviour
    {
        public Slider musicSlider, sfxSlider;
        
        public void MusicVolume()
        {
            AudioManager.Instance.MusicVolume(musicSlider.value);
        }
        public void SfxVolume()
        {
            AudioManager.Instance.SfxVolume(sfxSlider.value);
        }
    }
}
