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

public class Ball : DynamicObject
{
    private enum Surface { Top, Bottom }
    public float Radius { get; set; }
    public override TextureRegion Texture
    {
        get => base.Texture; set
        {
            base.Texture = value;
            Origin = -value.Dimensions * 0.5f;
            Radius = value.Height * 0.5f;
        }
    }


    /// <summary>
    /// Constructs the object. Sets all properties to 0 or any other default values.
    /// </summary>
    /// <param name="textureRegion">The texture of an object</param>
    public Ball(TextureRegion textureRegion) : base(textureRegion)
    {
    }
    /// <summary>
    /// Constructs static object. 
    /// </summary>
    /// <param name="textureRegion">The texture of an object</param>
    /// <param name="position">Position of upper-left corner by default</param>
    /// <param name="origin">The vector that you want to add to the specified position so that it points to the upper-left corner.</param>
    /// <param name="rotation">Rotation of an object</param>
    /// <param name="scale">Scale of an obect that will be drawn. Influences object's dimensions.</param>
    public Ball(TextureRegion textureRegion, Vector2 position, Vector2 origin, float rotation=0f, float scale=1f) : base(textureRegion, position, origin, rotation, scale)
    {
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
        Vector2 dir = Direction;
        Bounce(normal, ref dir);
        Direction = dir;
    }

    /// <summary>
    /// Reflects given direction.
    /// </summary>
    /// <param name="normal"></param>
    /// <param name="direction"></param>
    private void Bounce(Vector2 normal, ref Vector2 direction)
    {
        direction.Normalize();
        Vector2 reflected = direction - 2f* Vector2.Dot(direction, normal) * normal;
        direction = reflected;
    }
    
    /// <summary>
    /// Realisticly bounce from top or bottom of the screen. It is not working at the moment.
    /// </summary>
    /// <param name="surface"></param>
    private void Bounce(Surface surface, Vector2 velocity)
    {
        if (surface == Surface.Bottom)
        {
            Vector2 normal = new(0, 1);
            float touchingY = ScreenHeight - Radius;
            float ratio = velocity.X / velocity.Y;
            float delta = touchingY - Position.Y;
            Vector2 toColision = new(delta * ratio, touchingY);
            Position += toColision;
            velocity -= toColision;

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
        float angle = (float)(Core.Random.NextDouble() * Math.PI * 2);
        dir.Rotate((float)Math.PI * 0.125f);
        Direction = dir;
        Speed = 0f;
    }

    public void Start()
    {
        Speed = 10f;
    }

    private void Move(Vector2? byVector=null)
    {
        Vector2 velocity = byVector ?? Velocity;
        Vector2 newPosition = Position + velocity;
        if (newPosition.Y + Radius > ScreenHeight && Velocity.Y > 0)
        {
            Bounce(Surface.Bottom, velocity);
            return;
        }
        if (newPosition.Y < Radius && Velocity.Y < 0)
        {
            Bounce(Surface.Top, velocity);
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

