using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace FlappyBird.Lib;

/// <summary>
/// Service responsible for persisting game-related information such as past scores.
/// Stores data in a simple JSON file inside the user's ApplicationData folder.
/// Thread-safe and resilient to IO errors.
/// </summary>
public sealed class GameDataService
{
    private readonly string _filePath;
    private readonly ReaderWriterLockSlim _lock = new();
    private List<int> _scores = new();

    public static GameDataService Instance { get; } = new GameDataService();

    private GameDataService()
    {
        try
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var folder = Path.Combine(appData, "FlappyBirdRebranded");
            Directory.CreateDirectory(folder);
            _filePath = Path.Combine(folder, "gamedata.json");

            Load();
        }
        catch
        {
            // If anything goes wrong during initialization, fall back to in-memory only.
            _filePath = Path.Combine(Path.GetTempPath(), "flappy_gamedata.json");
            _scores = new List<int>();
        }
    }

    /// <summary>
    /// Returns a read-only snapshot of all recorded scores (oldest first).
    /// </summary>
    public IReadOnlyList<int> GetAllScores()
    {
        _lock.EnterReadLock();
        try
        {
            return _scores.ToArray();
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    /// <summary>
    /// Adds a new score and persists the data.
    /// </summary>
    public void AddScore(int score)
    {
        _lock.EnterWriteLock();
        try
        {
            _scores.Add(score);
            Save();
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    /// <summary>
    /// Returns the highest recorded score, or zero if no scores exist.
    /// </summary>
    public int GetHighScore()
    {
        _lock.EnterReadLock();
        try
        {
            return _scores.Count == 0 ? 0 : _scores.Max();
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    /// <summary>
    /// Removes all stored scores (persists the empty state).
    /// </summary>
    public void ClearScores()
    {
        _lock.EnterWriteLock();
        try
        {
            _scores.Clear();
            Save();
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    private void Load()
    {
        _lock.EnterWriteLock();
        try
        {
            if (!File.Exists(_filePath))
            {
                _scores = new List<int>();
                return;
            }

            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                _scores = new List<int>();
                return;
            }

            try
            {
                var doc = JsonSerializer.Deserialize<GameDataDto>(json);
                _scores = doc?.Scores ?? new List<int>();
            }
            catch
            {
                // corrupted file: fallback to empty
                _scores = new List<int>();
            }
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    private void Save()
    {
        try
        {
            var dto = new GameDataDto { Scores = _scores };
            var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions { WriteIndented = true });
            // write atomically
            var tmp = _filePath + ".tmp";
            File.WriteAllText(tmp, json);
            File.Copy(tmp, _filePath, true);
            File.Delete(tmp);
        }
        catch
        {
            // Best effort only. Ignore IO errors to avoid crashing the game.
        }
    }

    private sealed class GameDataDto
    {
        public List<int> Scores { get; set; } = new();
    }
}
