using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//挂载到Audio Source
//该类专门用于管理音乐和音效的播放
public class AudioManager : MonoBehaviour
{
   
    //音乐组件
    public AudioSource MusicPlayer;
    //音效组件
    public AudioSource SoundPlayer;

    //播放音乐
    public void PlayMusic(string name)
    {
        MusicPlayer.volume = GlobalVariable.BGMVolume;
        if (MusicPlayer.isPlaying == false)
        {
            //从Resources文件夹加载对应的音频文件
            AudioClip clip = Resources.Load<AudioClip>(name);
            MusicPlayer.loop = false;
            MusicPlayer.clip = clip;
            MusicPlayer.Play();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        SoundPlayer.volume = GlobalVariable.SoundVolume;
        SoundPlayer.PlayOneShot(clip);
    }


    public void StopMusic()
    {
        MusicPlayer.Stop();
    }

    public void PlaySound(string name)
    {
        SoundPlayer.volume = GlobalVariable.SoundVolume;
        AudioClip clip = Resources.Load<AudioClip>(name);
        SoundPlayer.PlayOneShot(clip);
    }

    public void PlayMusicLoop()
    {
        MusicPlayer.volume = GlobalVariable.BGMVolume;
        if (MusicPlayer.isPlaying == false)
        {
            //从Resources文件夹加载对应的音频文件
            AudioClip clip = Resources.Load<AudioClip>(name);
            MusicPlayer.clip = clip;
            MusicPlayer.loop = true;
            MusicPlayer.Play();
        }
    }

    public void PlayMusicLoop(string name)
    {
        MusicPlayer.volume = GlobalVariable.BGMVolume;
        //从Resources文件夹加载对应的音频文件
        AudioClip clip = Resources.Load<AudioClip>(name);
        MusicPlayer.clip = clip;
        MusicPlayer.loop = true;
        MusicPlayer.Play();
    }

}
