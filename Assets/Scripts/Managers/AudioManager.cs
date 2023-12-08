using System;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        public Sound[] musicSounds, sfxSounds;
        public AudioSource musicSource, sfxSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            PlayMusic("Background");
        }

        public void PlayMusic(string musicName)
        {
            Sound sound = Array.Find(musicSounds, x => x.soundName == musicName);
            if (sound == null)
            {
                Debug.LogError("Sound not found");
            }
            else
            {
                musicSource.clip = sound.clip;
                musicSource.Play();
            }
        }

        public void PlayEffect(string effectName)
        {
            Sound effect = Array.Find(sfxSounds, x => x.soundName == effectName);
            if (effect == null)
            {
                Debug.LogError("Effect not found");
            }
            else
            {
                sfxSource.PlayOneShot(effect.clip);
            }
        }

        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
        }
        public void ToggleSfx()
        {
            sfxSource.mute = !sfxSource.mute;
        }

        public void MusicVolume(float volume)
        {
            musicSource.volume = volume;
        }
        public void SfxVolume(float volume)
        {
            sfxSource.volume = volume;
        }
        
    }
    
}
