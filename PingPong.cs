using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLib;
using PingPong.Scenes;
using Gum.Forms;
using Gum.Forms.Controls;
using MonoGameGum;
using static PingPong.Window;
using MonoGameLib.Text;

namespace PingPong;

public class PingPong : Core
{
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
        GumService.Default.CanvasWidth = GraphicsDevice.PresentationParameters.BackBufferWidth / BufferMultiplyer;
        GumService.Default.CanvasHeight = GraphicsDevice.PresentationParameters.BackBufferHeight / BufferMultiplyer;
        GumService.Default.Renderer.Camera.Zoom = BufferMultiplyer;

    }

    protected override void Initialize()
    {
        base.Initialize();
        InitializeGum();
        ChangeScene(new TitleScene());
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        TextManager.Load(Core.Content, "en");
    }


}
