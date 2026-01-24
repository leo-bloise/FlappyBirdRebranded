using System;
using System.Collections.Generic;
using System.Threading;

namespace FlappyBird.Lib;

public sealed class AudioThread : IDisposable
{
    private readonly Queue<string> _songRequests = new();
    private readonly object _lock = new();
    private readonly AutoResetEvent _signal = new(false);

    private Thread? _thread;
    private CancellationTokenSource? _cts;

    public void Enqueue(string songKey)
    {
        if (songKey == null) throw new ArgumentNullException(nameof(songKey));

        lock (_lock)
        {
            _songRequests.Enqueue(songKey);
        }

        _signal.Set();
    }

    public void Start()
    {
        if (_thread != null && _thread.IsAlive)
            return;

        _cts = new CancellationTokenSource();
        _thread = new Thread(() => Worker(_cts.Token))
        {
            IsBackground = true,
            Name = "FlappyBird-AudioThread"
        };

        _thread.Start();
    }

    private void Worker(CancellationToken ct)
    {
        try
        {
            while (!ct.IsCancellationRequested)
            {
                _signal.WaitOne();

                if (ct.IsCancellationRequested)
                    break;
                
                while (true)
                {
                    string? songKey = null;
                    
                    lock (_lock)
                    {
                        if (_songRequests.Count > 0)
                            songKey = _songRequests.Dequeue();
                        else
                            break;

                        if (songKey != null)
                        {
                            AudioManager.Instance.Play(songKey);
                        }

                        if (ct.IsCancellationRequested)
                            break;
                    }
                }
            }
        }
        catch (ThreadInterruptedException)
        {
        }
    }

    public void Stop(int joinTimeoutMs = 500)
    {
        if (_cts == null)
            return;

        try
        {
            _cts.Cancel();
            
            _signal.Set();

            if (_thread != null && !_thread.Join(joinTimeoutMs))
            {
                try { _thread.Interrupt(); } catch { }
            }
        }
        finally
        {
            _cts.Dispose();
            _cts = null;
            _thread = null;
        }
    }

    public void Dispose()
    {
        Stop();

        lock (_lock)
        {
            _songRequests.Clear();
        }

        _signal.Dispose();
    }
}
