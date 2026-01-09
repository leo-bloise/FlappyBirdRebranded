using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Lib;

public class TextureAtlas
{
    private readonly Dictionary<string, TextureRegion> _regions = new();
    private readonly Dictionary<string, Animation> _animations = new();
    private readonly Texture2D _texture;

    public TextureAtlas(Texture2D texture)
    {
        _texture = texture;
    }
    
    public void AddRegion(string name, int x, int y, int width, int height)
    {
        _regions.Add(name, new TextureRegion(_texture, x, y, width, height));
    }

    public TextureRegion GetRegion(string name)
    {
        if (!_regions.TryGetValue(name, out TextureRegion region))
        {
            throw new Exception("TextureRegion not found");    
        }
        
        return region;
    }

    public void AddAnimation(string name, Animation animation)
    {
        _animations.Add(name, animation);
    }

    public Animation GetAnimation(string name)
    {
        return _animations[name]; 
    }

    public Sprite CreateSprite(string regionName)
    {
        TextureRegion region = GetRegion(regionName);
        return new Sprite(region);
    }

    public AnimatedSprite CreateAnimatedSprite(string animationName)
    {
        Animation animation = GetAnimation(animationName);

        return new AnimatedSprite(animation);
    }

    public static TextureAtlas FromFile(ContentManager content, string fileName)
    {
        #nullable enable
        
        string filePath = Path.Combine(content.RootDirectory, fileName);
        
        if (!Path.Exists(filePath)) throw new Exception($"TextureAtlas file {filePath} does not exists in {filePath}");
        
        using Stream stream = TitleContainer.OpenStream(filePath);
        using XmlReader reader = XmlReader.Create(stream);
        
        XDocument doc = XDocument.Load(reader);
        XElement? root = doc.Root;
        
        if(root == null) throw new Exception("Invalid XML - No root found");

        string? texturePath = root.Element("Texture")?.Value;
        
        if(texturePath == null) throw new Exception("Invalid XML - No texture path found");
                
        Texture2D texture = content.Load<Texture2D>(texturePath);
                
        IEnumerable<XElement>? regions = root.Element("Regions")?.Elements("Region");
                
        TextureAtlas atlas = new TextureAtlas(texture);
                
        if (regions == null)
        {
            return atlas;
        }

        foreach (XElement region in regions)
        {
            string? name = region.Attribute("name")?.Value;
            int x = int.Parse(region.Attribute("x")?.Value ?? "0");
            int y = int.Parse(region.Attribute("y")?.Value ?? "0");
            int width = int.Parse(region.Attribute("width")?.Value ?? "0");
            int height = int.Parse(region.Attribute("height")?.Value ?? "0");

            if (string.IsNullOrEmpty(name)) throw new Exception("Region declared without name");
            
            atlas.AddRegion(name, x, y, width, height);
        }

        IEnumerable<XElement>? animations = root.Element("Animations")?.Elements("Animation");

        if(animations == null) return atlas;

        foreach(XElement animation in animations)
        {
            string? animationName = animation.Attribute("name")?.Value;

            if (animationName == null) throw new Exception("Animation name was null");

            int delay = int.Parse(animation.Attribute("delay")?.Value ?? "0");

            IEnumerable<XElement>? frames = animation.Elements("Frame"); 

            if(frames == null) throw new Exception("Animation defined, but no frames detected");


            List<TextureRegion> animationFrames = frames.Select(frame =>
            {
                string? region = frame.Attribute("region")?.Value;

                if (region == null) throw new Exception("No region defined for frame");

                return atlas.GetRegion(region);
            }).ToList();

            atlas.AddAnimation(animationName, new Animation(animationFrames, TimeSpan.FromMilliseconds(delay)));
        }

        return atlas;
    }
}