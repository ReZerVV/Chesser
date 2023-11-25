using Chesser.Drawing;

namespace Chesser.App.UI.Views;

public class WinMessageView : IView
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    private string message = string.Empty;

    public WinMessageView(Chesser.Core.Color winColor)
    {
        if (winColor == Core.Color.White)
        {
            message = "The white pieces won.";
        }
        else
        {
            message = "The black pieces won.";
        }
    }

    public void Input(ConsoleKeyInfo keyInfo)
    {
        AppState.NavigationSerivce.Navigate(new MenuGameView());
    }

    public void Update()
    {

    }

    public void Render(Canvas canvas)
    {
        canvas.DrawFillRect(
            x: Position.x,
            y: Position.y,
            w: Size.x,
            h: Size.y,
            color: AppState.Colors.BackgroundDark
        );

        canvas.DrawText(
            text: message,
            x: Position.x + Size.x / 2 - message.Length / 2,
            y: Position.y + Size.y / 2 - 1,
            foreground: AppState.Colors.Foreground,
            background: AppState.Colors.BackgroundDark,
            style: PixelStyle.StyleBold
        );
        
        canvas.DrawText(
            text: "Please press ESC to return to the main menu.",
            x: Position.x + Size.x / 2 - 44 / 2,
            y: Position.y + Size.y / 2 + 1,
            foreground: AppState.Colors.ForegroundDark,
            background: AppState.Colors.BackgroundDark,
            style: PixelStyle.StyleNone
        );
    }
}