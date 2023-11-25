using Chesser.Drawing;

namespace Chesser.App.UI.Views;

public class MenuGameView : IView
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }

    #region Logic
    private const int CountMenuItems = 2;
    private int selectedMenuItemIndex = 0;
    private void MoveDown()
    {
        if (selectedMenuItemIndex + 1 < CountMenuItems)
        {
            selectedMenuItemIndex++;
        }
        else
        {
            selectedMenuItemIndex = 0;
        }
    }
    private void MoveUp()
    {
        if (selectedMenuItemIndex - 1 >= 0)
        {
            selectedMenuItemIndex--;
        }
        else
        {
            selectedMenuItemIndex = CountMenuItems - 1;
        }
    }
    private void SelectingItem()
    {
        switch (selectedMenuItemIndex)
        {
            case 0: AppState.NavigationSerivce.Navigate(new SinglePlayerGameView()); break;
            case 1: AppState.NavigationSerivce.Navigate(new DoublesGameView()); break;

            default: throw new IndexOutOfRangeException(nameof(selectedMenuItemIndex));
        }
    }
    #endregion

    public void Input(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.Escape)
        {
            Application.Exit();
        }
        else if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            MoveUp();
        }
        else if (keyInfo.Key == ConsoleKey.DownArrow)
        {
            MoveDown();
        }
        else if (keyInfo.Key == ConsoleKey.Enter)
        {
            SelectingItem();
        }
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
        canvas.DrawFillRect(
            x: Position.x,
            y: Position.y,
            w: (int)(Size.x / 3f),
            h: Size.y,
            color: AppState.Colors.Background
        );

        RenderTitle(canvas);
        RenderMenuItems(canvas);
    }

    #region Render
    private void RenderTitle(Canvas canvas)
    {
        int widthMenuBar = (int)(Size.x / 3f);
        int headerHeightOffset = Position.y + Size.y / 3;

        canvas.DrawText(
            text: " ▄▄·  ▄ .▄▄▄▄ ..▄▄ · .▄▄ · ",
            x: Position.x + widthMenuBar / 2 - 27 / 2,
            y: headerHeightOffset - 2,
            foreground: AppState.Colors.ForegroundDark,
            background: AppState.Colors.Background,
            style: PixelStyle.StyleNone
        );

        canvas.DrawText(
            text: "▐█ ▌▪██▪▐█▀▄.▀·▐█ ▀. ▐█ ▀. ",
            x: Position.x + widthMenuBar / 2 - 27 / 2,
            y: headerHeightOffset - 1,
            foreground: AppState.Colors.ForegroundDark,
            background: AppState.Colors.Background,
            style: PixelStyle.StyleNone
        );

        canvas.DrawText(
            text: "██ ▄▄██▀▐█▐▀▀▪▄▄▀▀▀█▄▄▀▀▀█▄",
            x: Position.x + widthMenuBar / 2 - 27 / 2,
            y: headerHeightOffset,
            foreground: AppState.Colors.ForegroundDark,
            background: AppState.Colors.Background,
            style: PixelStyle.StyleNone
        );


        canvas.DrawText(
            text: "▐███▌██▌▐▀▐█▄▄▌▐█▄▪▐█▐█▄▪▐█",
            x: Position.x + widthMenuBar / 2 - 27 / 2,
            y: headerHeightOffset + 1,
            foreground: AppState.Colors.ForegroundDark,
            background: AppState.Colors.Background,
            style: PixelStyle.StyleNone
        );

        canvas.DrawText(
            text: "·▀▀▀ ▀▀▀ · ▀▀▀  ▀▀▀▀  ▀▀▀▀ ",
            x: Position.x + widthMenuBar / 2 - 27 / 2,
            y: headerHeightOffset + 2,
            foreground: AppState.Colors.ForegroundDark,
            background: AppState.Colors.Background,
            style: PixelStyle.StyleNone
        );

    }
    private void RenderMenuItems(Canvas canvas)
    {
        int widthMenuBar = (int)(Size.x / 3f);
        int heightOffset = Position.y + Size.y / 2;

        int menuItemWidth = 30;
        int menuItemHeight = 3;

        string[] menuItemsValue = new string[] {
            "New Single Player Game",
            "New Doubles Game",
        };

        for (int menuItemIndex = 0, menuItemPosition = 0; menuItemIndex < CountMenuItems; menuItemIndex++, menuItemPosition += menuItemHeight + 1)
        {
            Color foreground = AppState.Colors.BackgroundDark;
            Color background = AppState.Colors.ForegroundDark;

            if (menuItemIndex == selectedMenuItemIndex)
            {
                background = AppState.Colors.Foreground;
            }

            canvas.DrawFillRect(
                x: Position.x + widthMenuBar / 2 - menuItemWidth / 2,
                y: heightOffset + menuItemPosition,
                w: menuItemWidth,
                h: menuItemHeight,
                color: background
            );

            canvas.DrawText(
                text: menuItemsValue[menuItemIndex],
                x: Position.x + widthMenuBar / 2 - menuItemWidth / 2 + 3,
                y: heightOffset + menuItemPosition + 1,
                foreground: foreground,
                background: background,
                style: PixelStyle.StyleNone
            );
        }
    }
    #endregion
}