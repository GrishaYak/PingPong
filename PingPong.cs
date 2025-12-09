using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLib;
using MonoGameLib.Graphics;
using MonoGameLib.Objects;
using PingPong.Scenes;

namespace PingPong;

public class PingPong : Core
{
    public PingPong() : base("AeroPong", 900, 500, false) { }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
        ChangeScene(new TitleScene());
    }


}
