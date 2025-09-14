using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Audio;

//DONT DUPLICATE!!! FROM TRIAS DEVELOPER
namespace Nitzz.Utility
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public SoundData[] musicSounds, sfxSounds;
        public string mixerGroupName = "SFX";
        public AudioSource musicSource;
        public GameObject GameobjectSfx;
        public int sfxPoolSize = 10; 
        [SerializeField]private List<AudioSource> sfxSources = new List<AudioSource>(); 

        private void Awake()
        {
            instance = this;

            // Initialize the SFX pool
            for (int i = 0; i < sfxPoolSize; i++)
            {
                AudioSource sfxSource = GameobjectSfx.AddComponent<AudioSource>();
                sfxSource.playOnAwake = false;
                sfxSource.volume = 0.4f;
                sfxSources.Add(sfxSource);
            }

            if (PlayerPrefs.HasKey("VolumeMusic"))
            {
                MusicVolume(PlayerPrefs.GetFloat("VolumeMusic"));
            }

            if (PlayerPrefs.HasKey("VolumeSFX"))
            {
                SFXVolume(PlayerPrefs.GetFloat("VolumeSFX"));
            }
        }

        public void PlayMusic(string name)
        {
            SoundData s = Array.Find(musicSounds, x => x.name == name);

            if (s == null)
            {
                Debug.Log("Sound Not Found");
            }
            else
            {
                musicSource.clip = s.clip;
                musicSource.Play();
            }
        }

        public void PlayMusicWith(AudioClip musicData)
        {
            if (musicData == null)
            {
                Debug.Log("Sound is Null");
            }
            else
            {
                musicSource.clip = musicData;
                musicSource.Play();
            }
        }

        public void PlaySFX(string name)
        {
            SoundData s = Array.Find(sfxSounds, x => x.name == name);

            if (s == null)
            {
                Debug.Log("SFX Not Found");
            }
            else
            {
                // Get an available AudioSource from the pool
                AudioSource sfxSource = GetAvailableSFXSource();
                if (sfxSource != null)
                {
                    sfxSource.PlayOneShot(s.clip);
                }
            }
        }

        public void PlaySFXWith(AudioClip sfxData)
        {
            if (sfxData == null)
            {
                Debug.Log("SFX is Null");
            }
            else
            {
                // Get an available AudioSource from the pool
                AudioSource sfxSource = GetAvailableSFXSource();
                if (sfxSource != null)
                {
                    sfxSource.PlayOneShot(sfxData);
                }
            }
        }

        private AudioSource GetAvailableSFXSource()
        {
            // Find an AudioSource in the pool that is not currently playing
            foreach (AudioSource source in sfxSources)
            {
                if (!source.isPlaying)
                {
                    return source;
                }
            }

            AudioSource sfxSource = GameobjectSfx.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            sfxSources.Add(sfxSource);
            return sfxSource;

            Debug.LogWarning("All SFX sources are busy, consider increasing the pool size.");
            return null;
        }

        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
        }

        public void ToggleSFX()
        {
            foreach (AudioSource source in sfxSources)
            {
                source.mute = !source.mute;
            }
        }

        public void MusicVolume(float volume)
        {
            musicSource.volume = volume;
        }

        public void SFXVolume(float volume)
        {
            foreach (AudioSource source in sfxSources)
            {
                source.volume = volume;
            }
        }
    }

    [System.Serializable]
    public class SoundData
    {
        public string name;
        public AudioClip clip;
    }
}
