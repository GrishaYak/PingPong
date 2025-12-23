using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLib;
using MonoGameLib.Graphics;
using MonoGameLib.Objects;
using PingPong.Scenes;
using Gum.Forms;
using Gum.Forms.Controls;
using MonoGameGum;

namespace PingPong;

public class PingPong : Core
{
    public PingPong() : base("AeroPong", 900, 500, false) { }

    private void InitializeGum()
    {
        GumService.Default.Initialize(this, DefaultVisualsVersion.V2);

        GumService.Default.ContentLoader.XnaContentManager = Core.Content;

        FrameworkElement.KeyboardsForUiControl.Add(GumService.Default.Keyboard);

        FrameworkElement.GamePadsForUiControl.AddRange(GumService.Default.Gamepads);

        FrameworkElement.TabReverseKeyCombos.Add(new KeyCombo() { PushedKey = Keys.Up });
        FrameworkElement.TabReverseKeyCombos.Add(new KeyCombo() { PushedKey = Keys.W });

        FrameworkElement.TabKeyCombos.Add(new KeyCombo() { PushedKey = Keys.Down });
        FrameworkElement.TabKeyCombos.Add(new KeyCombo() { PushedKey = Keys.S });

        //GumService.Default.CanvasWidth = GraphicsDevice.PresentationParameters.BackBufferWidth / 2.0f;
        //GumService.Default.CanvasHeight = GraphicsDevice.PresentationParameters.BackBufferHeight / 2.0f;
        //GumService.Default.Renderer.Camera.Zoom = 2.0f;

    }

    protected override void Initialize()
    {
        base.Initialize();
        InitializeGum();
        ChangeScene(new TitleScene());
    }


}
