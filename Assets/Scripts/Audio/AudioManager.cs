using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject instance;

    public Sound[] sounds;

    public List<Sound> SFX = new List<Sound>();


    float currentSongVolume;
    float currentSFXVolume;

    bool sfxToggle = true;
    bool musicToggle = true;

    [SerializeField] Slider songSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField] Toggle UIsongToggle;
    [SerializeField] Toggle UIsfxToggle;
    int sfxTog;
    int musicTog;


    private void Awake()
    {
        if (instance == null)
            instance = this.gameObject;
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        
        
        foreach(Sound s in sounds)
        {
            s.source =  gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            if (s.name.EndsWith("SFX"))
            {
                SFX.Add(s);
                s.source.volume = 0;
            }
            if(s.name.Equals("BGMBoss"))
            {
                s.source.Pause();
                Debug.Log("Sdad");
            }
        }

        
        PlaySound("BGMSong");
       
    }
    
    private void Start()
    {
        StopSound("BGMBoss");
        Load();

    }


    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();

        if (name == "BGMSong") s.source.loop = true;
        if (name == "BGMBoss") s.source.loop = true;
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Stop();
    }


    public void SetMusicVolume(float value)
    {
        Sound s = Array.Find(sounds, sound => sound.name == "BGMSong");
        s.source.volume = value;
        currentSongVolume = value;
        songSlider.value = value;

        Sound sB = Array.Find(sounds, soundB => soundB.name == "BGMBoss");
        sB.source.volume = value;
        currentSongVolume = value;
        songSlider.value = value;

    }


    public void ChangeSFXVolume()
    {
        foreach (Sound s in SFX)
        {
            s.source.volume = sfxSlider.value;
        }
        currentSFXVolume = sfxSlider.value;

        Save();
    }

    public void SetSFXVolume(float value)
    {
        currentSFXVolume = value;
        sfxSlider.value = currentSFXVolume;


        foreach (Sound s in SFX)
        {
            s.source.volume = currentSFXVolume;
        }

    }

    public void ToogleButtonSong(bool temp)
    {
        Sound s = Array.Find(sounds, sound => sound.name == "BGMSong");
        if(temp)
        {
            s.source.Play();
            songSlider.interactable = true;
        }
        else
        {
            s.source.Pause();
            songSlider.interactable = false;
        }


        musicToggle = temp;
        Save();
    }

    private void LoadButtonSong(bool temp)
    {
        Sound s = Array.Find(sounds, sound => sound.name == "BGMSong");
        if (temp)
        {
            s.source.Play();
            songSlider.interactable = true;
        }
        else
        {
            s.source.Pause();
            songSlider.interactable = false;
        }


        musicToggle = temp;

        if (musicToggle)
            musicTog = 1;
        else
            musicTog = 0;

    }

    public void ToogleSFXButton(bool temp)
    {
        if (temp)
        {
            foreach(Sound s in SFX)
            {
                s.source.mute = false;
            }
            sfxSlider.interactable = true;
        }
        else
        {
            foreach (Sound s in SFX)
            {
                s.source.mute = true;
            }
            sfxSlider.interactable = false;
        }

        sfxToggle = temp;
        Save();
    }

    void LoadSFXButton(bool temp)
    {
        if (temp)
        {
            foreach (Sound s in SFX)
            {
                s.source.mute = false;
            }
            sfxSlider.interactable = true;
        }
        else
        {
            foreach (Sound s in SFX)
            {
                s.source.mute = true;
            }
            sfxSlider.interactable = false;
        }

        sfxToggle = temp;

        if (sfxToggle)
            sfxTog = 1;
        else
            sfxTog = 0;
    }
    
    private void Save()
    {

        PlayerPrefs.SetFloat("SongVolume", currentSongVolume);
        PlayerPrefs.SetFloat("SFXvalue", currentSFXVolume);


        if (sfxToggle)
            sfxTog = 1;
        else
            sfxTog = 0;


        if (musicToggle)
            musicTog = 1;
        else
            musicTog = 0;

        PlayerPrefs.SetInt("SFXToggle", sfxTog);
        PlayerPrefs.SetInt("MusicToggle", musicTog);

        //Debug.Log("SFX: " + sfxTog + ",Music: " + musicTog);

        PlayerPrefs.Save();

    }

    private void Load()
    {

        float sfx = PlayerPrefs.GetFloat("SFXvalue");
        float music = PlayerPrefs.GetFloat("SongVolume");

        sfxTog = PlayerPrefs.GetInt("SFXToggle");
        musicTog = PlayerPrefs.GetInt("MusicToggle");

        if(!PlayerPrefs.HasKey("SFXvalue"))
        {
            sfxToggle = true;
            LoadSFXButton(true);
            UIsfxToggle.isOn = true;

            sfxToggle = true;
            LoadSFXButton(true);
            UIsfxToggle.isOn = true;

            SetMusicVolume(1);
            SetSFXVolume(1);

            return;
        }



        if (musicTog == 1)
        {
            musicToggle = true;
            LoadButtonSong(true);
            if (sfxTog == 1)
            {
                sfxToggle = true;
                LoadSFXButton(true);
                UIsfxToggle.isOn = true;
            }
            else
            {
                sfxToggle = false;
                UIsfxToggle.isOn = false;
                LoadSFXButton(false);
            }
            UIsongToggle.isOn = true;
        }
        else
        {
            musicToggle = false;
            LoadButtonSong(false);
            if (sfxTog == 1)
            {
                sfxToggle = true;
                LoadSFXButton(true);
                UIsfxToggle.isOn = true;
            }
            else
            {
                sfxToggle = false;
                UIsfxToggle.isOn = false;
                LoadSFXButton(false);
            }
            UIsongToggle.isOn = false;
        }


        SetMusicVolume(music);
        SetSFXVolume(sfx);



        //Debug.Log("SFX: " + PlayerPrefs.GetInt("SFXToggle") + ",Music: " + PlayerPrefs.GetInt("MusicToggle"));
    }
    

    public void StartBossSong()
    {
        Debug.Log("PlayBoss");
        PlaySound("BGMBoss");
        StopSound("BGMSong");
    }

    public void StopBossSong()
    {
        Debug.Log("StopBoss");
        PlaySound("BGMSong");
        StopSound("BGMBoss");
    }


}
