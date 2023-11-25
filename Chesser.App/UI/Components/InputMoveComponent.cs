using Chesser.Core;
using Chesser.Drawing;

namespace Chesser.App.UI.Components;

public class InputMoveComponent
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Vector2 Size { get; set; } = Vector2.Zero;

    private bool isFocus = false;
    public bool IsFocus
    {
        get
        {
            return isFocus;
        }
        set
        {
            isFocus = value;
            Start = null;
            HasStartPosition = false;
            Target = null;
            HasTargetPosition = false;
            buffer = string.Empty;
        }
    }
    public bool HasStartPosition { get; private set; }
    public Coord? Start { get; private set; } = null;
    public bool HasTargetPosition { get; private set; }
    public Coord? Target { get; private set; } = null;
    public Move? Move { get; private set; } = null;

    #region Text Input
    private int cursor;
    private string buffer;
    private void InsertText(string text)
    {
        if (buffer.Length <= 2)
        {
            buffer = (cursor < buffer.Length ? buffer.Substring(0, cursor) : buffer.Substring(0))
                + text
                + (cursor < buffer.Length ? buffer.Substring(cursor) : "");
            CursorMoveRight();
        }
    }
    private void RemoveText()
    {
        if (buffer == string.Empty)
        {
            return;
        }

        buffer = (cursor < buffer.Length ? buffer.Substring(0, cursor - 1) : buffer.Substring(0, buffer.Length - 1))
            + (cursor < buffer.Length ? buffer.Substring(cursor) : "");
        CursorMoveLeft();
    }
    private void CursorMoveLeft()
    {
        if (cursor - 1 >= 0)
        {
            cursor--;
        }
    }
    private void CursorMoveRight()
    {
        if (cursor + 1 <= buffer.Length)
        {
            cursor++;
        }
    }
    #endregion

    private void Enter()
    {
        if (!HasStartPosition)
        {
            if (buffer.Length == 2 && char.IsLetter(buffer[0]) && char.IsDigit(buffer[1]))
            {
                Start = new Coord(
                    file: (int)(buffer[0] - 'a'),
                    rank: (int)(buffer[1] - '0') - 1
                );
                HasStartPosition = true;
            }
            buffer = string.Empty;
            cursor = 0;
        }
        if (!HasTargetPosition)
        {
            if (buffer.Length == 2 && char.IsLetter(buffer[0]) && char.IsDigit(buffer[1]))
            {
                Target = new Coord(
                    file: (int)(buffer[0] - 'a'),
                    rank: (int)(buffer[1] - '0') - 1
                );
                HasTargetPosition = true;
                Move = new Move(Start, Target);
            }
            buffer = string.Empty;
            cursor = 0;
        }
    }

    public void Input(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.Escape)
        {
            isFocus = false;
        }
        else if (keyInfo.Key == ConsoleKey.Enter)
        {
            Enter();
        }
        else if (keyInfo.Key == ConsoleKey.Backspace)
        {
            RemoveText();
        }
        else if (keyInfo.Key == ConsoleKey.LeftArrow)
        {
            CursorMoveLeft();
        }
        else if (keyInfo.Key == ConsoleKey.RightArrow)
        {
            CursorMoveRight();
        }
        else
        {
            InsertText(keyInfo.KeyChar.ToString());
        }
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

        if (!IsFocus)
        {
            return;
        }

        if (!HasStartPosition)
        {
            canvas.DrawText(
                text: "From:",
                x: Position.x + 2,
                y: Position.y + Size.y / 2,
                foreground: AppState.Colors.Foreground,
                background: AppState.Colors.BackgroundDark,
                style: PixelStyle.StyleNone
            );
            canvas.DrawText(
                text: buffer,
                x: Position.x + 2 + "From:".Length + 1,
                y: Position.y + Size.y / 2,
                foreground: AppState.Colors.Foreground,
                background: AppState.Colors.BackgroundDark,
                style: PixelStyle.StyleNone
            );
            canvas.DrawSymbol(
                symbol: cursor < buffer.Length
                    ? buffer[cursor].ToString()
                    : " ",
                x: Position.x + 2 + "From:".Length + 1 + cursor,
                y: Position.y + Size.y / 2,
                foreground: AppState.Colors.BackgroundDark,
                background: AppState.Colors.Foreground,
                style: PixelStyle.StyleNone
            );
        }
        if (HasStartPosition)
        {
            canvas.DrawText(
                text: Start.ToString(),
                x: Position.x + 2,
                y: Position.y + Size.y / 2,
                foreground: AppState.Colors.Foreground,
                background: AppState.Colors.BackgroundDark,
                style: PixelStyle.StyleNone
            );
            canvas.DrawText(
                text: "To:",
                x: Position.x + 2 + Start.ToString().Length + 1,
                y: Position.y + Size.y / 2,
                foreground: AppState.Colors.Foreground,
                background: AppState.Colors.BackgroundDark,
                style: PixelStyle.StyleNone
            );
            canvas.DrawText(
                text: buffer,
                x: Position.x + 2 + Start.ToString().Length + 1 + "To:".Length + 1,
                y: Position.y + Size.y / 2,
                foreground: AppState.Colors.Foreground,
                background: AppState.Colors.BackgroundDark,
                style: PixelStyle.StyleNone
            );
            canvas.DrawSymbol(
                symbol: cursor < buffer.Length
                    ? buffer[cursor].ToString()
                    : " ",
                x: Position.x + 2 + Start.ToString().Length + 1 + "To:".Length + 1 + cursor,
                y: Position.y + Size.y / 2,
                foreground: AppState.Colors.BackgroundDark,
                background: AppState.Colors.Foreground,
                style: PixelStyle.StyleNone
            );
        }
    }
}