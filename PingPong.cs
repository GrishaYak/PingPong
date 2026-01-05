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
    public static string WindowName { get; } = "AeroPong";
    public static int ScreenWidth { get; } = 900;
    public static int ScreenHeight { get; } = 500;
    public static bool IsFullScreen { get; } = false;
    public PingPong() : base(WindowName, ScreenWidth, ScreenHeight, IsFullScreen) { }

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
        float multiplyer = 3.0f;
        GumService.Default.CanvasWidth = GraphicsDevice.PresentationParameters.BackBufferWidth / multiplyer;
        GumService.Default.CanvasHeight = GraphicsDevice.PresentationParameters.BackBufferHeight / multiplyer;
        GumService.Default.Renderer.Camera.Zoom = multiplyer;

    }

    protected override void Initialize()
    {
        base.Initialize();
        InitializeGum();
        ChangeScene(new TitleScene());
    }


}
