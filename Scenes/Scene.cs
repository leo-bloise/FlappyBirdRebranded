using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Net.Http.Headers;

namespace FlappyBird.Scenes;

public abstract class Scene : IDisposable
{
    public ContentManager Content { get; }

    public bool IsDisposed { get; private set; }

    public Scene()
    {
        Content = new ContentManager(MainGame.ContentManager.ServiceProvider);

        Content.RootDirectory = MainGame.ContentManager.RootDirectory;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            UnloadContent();
            Content.Dispose();
        }
        IsDisposed = true;
    }

    public virtual void Initialize()
    {
        LoadContent();
    }

    public virtual void LoadContent() { }
    
    public virtual void UnloadContent()
    {
        Content.Unload();
    }

    public virtual void Update(GameTime gameTime) { }

    public virtual void Draw(GameTime gameTime) { }

    ~Scene() => Dispose(true);
}
