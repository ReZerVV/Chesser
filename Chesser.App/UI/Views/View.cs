using Chesser.Drawing;

namespace Chesser.App.UI.Views;

public interface IView
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public void Input(ConsoleKeyInfo keyInfo);
    public void Update();
    public void Render(Canvas canvas);
}