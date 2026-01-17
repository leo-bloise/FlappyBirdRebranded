using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace FlappyBird.Lib;

public class AudioManager
{
    private readonly Dictionary<string, Song> Songs = new Dictionary<string, Song>();

    private static AudioManager _instance;

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
            Play(Songs[song]);
        }
    }

    public void Play(Song song)
    {
        if(MediaPlayer.State != MediaState.Stopped)
        {
            MediaPlayer.Stop();
        }

        MediaPlayer.Volume = 0.5f;

        MediaPlayer.Play(song);
    }
}
