using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System;

namespace FlappyBird.Lib;

public class AudioManager
{
    private readonly Dictionary<string, Song> Songs = new Dictionary<string, Song>();

    private static AudioManager _instance;

    public string CurrentSong { get; private set; }

    public static AudioManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new AudioManager();
            }
            return _instance;
        }
    }

    public void LoadSong(string key, Song song)
    {
        if(!Songs.ContainsKey(key))
        {
            Songs.Add(key, song);
        }
    }

    public void Play(string song)
    {
        if(Songs.ContainsKey(song))
        {
            CurrentSong = song;
            Play(Songs[song]);
            CurrentSong = null;
        }
    }

    public bool IsPlaying => MediaPlayer.State == MediaState.Playing;

    public void Play(Song song)
    {
        if(MediaPlayer.State != MediaState.Stopped)
        {
            MediaPlayer.Stop();
        }

        MediaPlayer.Volume = 0.5f;

        MediaPlayer.Play(song);
    }

    public bool TryGetSong(string key, out Song song)
    {
        return Songs.TryGetValue(key, out song);
    }
}
