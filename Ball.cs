using Gum.DataTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLib.Graphics;
using MonoGameLib.Objects;
using static PingPong.Window;
using MonoGameLib;

using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace PingPong;

public class Ball : PhysicalObject2
{
    private enum Surface { Top, Bottom }
    public float Radius { get; set; }
    public override TextureRegion MyTexture
    {
        get => base.MyTexture; set
        {
            base.MyTexture = value;
            Origin = value.Dimensions * 0.5f;
            Radius = value.Height * 0.5f;
        }
    }

    public Ball()
    {
        Reset();
    }
    public override void Update(GameTime gameTime)
    {
        Move();
    }

    /// <summary>
    /// Changes the Direction and plays the sound effect (potentially)
    /// </summary>
    /// <param name="normal"></param>
    private void Bounce(Vector2 normal)
    {
        Direction = Vector2.Reflect(Direction, normal);
    }
    /// <summary>
    /// Realisticly bounce from top or bottom of the screen
    /// </summary>
    /// <param name="surface"></param>
    private void Bounce(Surface surface, Vector2 velocity)
    {
        if (surface == Surface.Bottom)
        {
            Vector2 normal = new(0, 1);
            float delta = ScreenHeight - Position.Y - Radius;
            float ratio = delta / velocity.Y;
            Position = new(Position.X + velocity.X * ratio, delta + Position.Y);
            ratio = 1 - ratio;
            Bounce(normal);
            Move(velocity * ratio);
        }
        if (surface == Surface.Top)
        {
            Vector2 normal = new(0, -1);
            float delta = Position.Y - Radius;
            float ratio = delta / velocity.Y;
            Position = new(Position.X + velocity.X * ratio, Radius);
            ratio = 1 - ratio;
            Bounce(normal);
            Move(velocity * ratio);
        }

    }

    public void Reset()
    {
        Position = Screen * 0.5f;
        Vector2 dir = new (0, 1);
        dir.Rotate((float)(Core.Random.NextDouble() * Math.PI * 2));
        Direction = dir;
        Speed = 0f;
    }

    public void Start()
    {
        Speed = 5f;
    }

    private void Move(Vector2? byVector=null)
    {
        Vector2 velocity = byVector ?? Velocity;
        Vector2 newPosition = Position + velocity;
        if (newPosition.Y + Radius > ScreenHeight && Velocity.Y > 0)
        {
            Bounce(new(0,1));
            return;
        }
        if (newPosition.Y < Radius && Velocity.Y < 0)
        {
            Bounce(new(0,-1));
            return;
        }
        if (newPosition.X < 0 || newPosition.X > ScreenWidth)
        {
            Position = newPosition;
            OnOutOfScreen();
        }
        Position += velocity;
        
    }
    public event OutOfScreenEventHandler OutOfScreen;
    protected virtual void OnOutOfScreen()
    {
        OutOfScreen?.Invoke();
    }
    public delegate void OutOfScreenEventHandler();
}

