using Chesser.App.UI.Components;
using Chesser.Core;
using Chesser.Core.Pieces;
using Chesser.Drawing;

using Color = Chesser.Drawing.Color;

namespace Chesser.App.UI.Views;

public class SinglePlayerGameView : IView
{
    private Vector2 position = Vector2.Zero;
    public Vector2 Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
            inputMoveComponent.Size = new Vector2(
                x: (int)(Size.x / 4.8) - 4,
                y: 3
            );
            inputMoveComponent.Position = new Vector2(
                x: Position.x + 2,
                y: Position.y + Size.y - 1 - inputMoveComponent.Size.y
            );
            listOfMove.Position = new Vector2(
                x: Position.x + 2,
                y: Position.y + 2
            );
        }
    }

    private Vector2 size = Vector2.Zero;
    public Vector2 Size
    {
        get
        {
            return size;
        }
        set
        {
            size = value;
            inputMoveComponent.Size = new Vector2(
                x: (int)(Size.x / 4.8) - 4,
                y: 3
            );
            inputMoveComponent.Position = new Vector2(
                x: Position.x + 2,
                y: Position.y + Size.y - 1 - inputMoveComponent.Size.y
            );
            listOfMove.Size = new Vector2(
                x: (int)(Size.x / 4.8) - 4,
                y: Size.y - inputMoveComponent.Size.y - 4
            );
        }
    }

    private Game game { get; set; } = new();
    private AnimationComponent playerAnimationProcessMove = new AnimationComponent
    {
        Frames = new string[] { "◜", "◝", "◞", "◟" },
        Delay = 5,
    };
    private InputMoveComponent inputMoveComponent = new InputMoveComponent
    {

    };
    private ListComponent listOfMove = new ListComponent
    {

    };

    public void Input(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.Escape)
        {
            AppState.NavigationSerivce.Navigate(new MenuGameView());
        }
        else if (inputMoveComponent.IsFocus)
        {
            inputMoveComponent.Input(keyInfo);
        }
    }

    public void Update()
    {
        if (game.IsEndGame())
        {
            AppState.NavigationSerivce.Navigate(new WinMessageView(game.GetCurrentColor()));
            return;
        }

        playerAnimationProcessMove.Foregroung = AppState.Colors.Green;
        playerAnimationProcessMove.Backgroung = AppState.Colors.BackgroundDark;
        playerAnimationProcessMove.Update();

        if (game.State == GameState.WhiteMove &&
            !inputMoveComponent.IsFocus)
        {
            inputMoveComponent.IsFocus = true;
        }
        if (game.State == GameState.BlackMove)
        {
            game.TryMove(Search.GetBestMove(game.Board, game.GetCurrentColor()));
        }

        if (inputMoveComponent.HasTargetPosition)
        {
            if (game.TryMove(inputMoveComponent.Move))
            {
                listOfMove.Items.Add($"{DateTime.UtcNow.ToShortTimeString()}: {inputMoveComponent.Move}");
            }
            else
            {
                listOfMove.Items.Add($"{DateTime.UtcNow.ToShortTimeString()}: [Error] You have entered an illegal move. Please re-enter again.");
            }
            inputMoveComponent.IsFocus = false;
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

        RenderLeftLane(canvas);
        inputMoveComponent.Render(canvas);
        listOfMove.Render(canvas);

        RenderBoard(canvas);
        RenderSuperiorityIndicator(canvas);
        RenderPlayersName(canvas);
        RenderMoveIndicator(canvas);
    }

    #region Render
    private void RenderLeftLane(Canvas canvas)
    {
        int widthLeftLane = (int)(Size.x / 4.8);

        canvas.DrawFillRect(
            x: Position.x,
            y: Position.y,
            w: widthLeftLane,
            h: Size.y,
            color: AppState.Colors.Background
        );
    }
    private void RenderBoard(Canvas canvas)
    {
        int widthLeftLane = (int)(Size.x / 4.8);
        int widthRightLane = Size.x - widthLeftLane;

        int verticalCenter = Position.y + Size.y / 2;
        int horizontalCenter = Position.x + widthRightLane / 2;

        int cellWidth = 6;
        int cellHeight = 3;

        int gapWidth = 0;
        int gapHeight = 0;

        int yStartPosition = verticalCenter - cellHeight * 4 + (int)(gapHeight * 3.5);
        int xStartPosition = widthLeftLane + horizontalCenter - cellWidth * 4 + (int)(gapWidth * 3.5);

        IEnumerable<Coord> moves;
        if (inputMoveComponent.HasStartPosition &&
            !game.Board.Empty(inputMoveComponent.Start))
        {
            moves = MoveGenerator.GetMoves(game.Board, inputMoveComponent.Start)
                .Select(move => move.Target);
        }
        else
        {
            moves = new List<Coord>();
        }

        for (int rank = Board.Height - 1, yPosition = yStartPosition; rank >= 0; rank--, yPosition += cellHeight + gapHeight)
        {
            for (int file = 0, xPosition = xStartPosition; file < Board.Height; file++, xPosition += cellWidth + gapWidth)
            {
                Color color = AppState.Colors.BoardLight;
                if ((rank + file) % 2 == 0)
                {
                    color = AppState.Colors.BoardDark;
                }

                if (moves.Contains(new Coord(file, rank)))
                {
                    color = new Color(145, 199, 136);
                    if ((rank + file) % 2 == 0)
                    {
                        color = new Color(82, 115, 77);
                    }
                }

                canvas.DrawFillRect(
                    x: xPosition,
                    y: yPosition,
                    w: cellWidth,
                    h: cellHeight,
                    color: color
                );

                canvas.DrawText(
                    text: GetAsciiSymbolFromPiece(game.Board[file, rank]),
                    x: xPosition + 2,
                    y: yPosition + 1,
                    foreground: game.Board[file, rank] != null
                        ? game.Board[file, rank].Color == Core.Color.White
                            ? Color.Black
                            : Color.White
                        : color,
                    background: game.Board[file, rank] != null
                        ? game.Board[file, rank].Color == Core.Color.White
                            ? Color.White
                            : Color.Black
                        : color,
                    style: PixelStyle.StyleBold
                );
            }
        }

        for (int rank = Board.Height - 1, yPosition = yStartPosition; rank >= 0; rank--, yPosition += cellHeight + gapHeight)
        {
            canvas.DrawText(
                text: (rank + 1).ToString(),
                x: xStartPosition - 3,
                y: yPosition + 1,
                foreground: AppState.Colors.Foreground,
                background: AppState.Colors.BackgroundDark,
                style: PixelStyle.StyleNone
            );

            canvas.DrawText(
                text: (rank + 1).ToString(),
                x: xStartPosition + cellWidth * 8 + (int)(gapWidth * 3.5) + 2,
                y: yPosition + 1,
                foreground: AppState.Colors.Foreground,
                background: AppState.Colors.BackgroundDark,
                style: PixelStyle.StyleNone
            );
        }

        for (int file = 0, xPosition = xStartPosition; file < Board.Width; file++, xPosition += cellWidth + gapWidth)
        {
            canvas.DrawText(
                text: GetSymbolFromFile(file),
                x: xPosition + 2,
                y: yStartPosition - 2,
                foreground: AppState.Colors.Foreground,
                background: AppState.Colors.BackgroundDark,
                style: PixelStyle.StyleNone
            );

            canvas.DrawText(
                text: GetSymbolFromFile(file),
                x: xPosition + 2,
                y: yStartPosition + cellHeight * 8 + (int)(gapHeight * 3.5) + 1,
                foreground: AppState.Colors.Foreground,
                background: AppState.Colors.BackgroundDark,
                style: PixelStyle.StyleNone
            );
        }
    }
    private void RenderSuperiorityIndicator(Canvas canvas)
    {
        int widthLeftLane = (int)(Size.x / 4.8);
        int widthRightLane = Size.x - widthLeftLane;

        int verticalCenter = Position.y + Size.y / 2;
        int horizontalCenter = Position.x + widthRightLane / 2;

        int cellWidth = 6;
        int cellHeight = 3;

        int gapWidth = 0;
        int gapHeight = 0;

        int yStartPosition = verticalCenter - cellHeight * 4 + (int)(gapHeight * 3.5);
        int xStartPosition = widthLeftLane + horizontalCenter - cellWidth * 4 + (int)(gapWidth * 3.5) - 8;

        int superiorityIndicatorWidth = 2;
        int superiorityIndicatorHeight = cellHeight * 8 + (int)(gapHeight * 3.5);

        int superiorityIndicatorCenter = cellHeight * 4 + (int)(gapHeight * 3.5);

        canvas.DrawFillRect(
            x: xStartPosition,
            y: yStartPosition,
            w: superiorityIndicatorWidth,
            h: superiorityIndicatorHeight,
            color: AppState.Colors.BoardLight
        );

        canvas.DrawFillRect(
            x: xStartPosition,
            y: yStartPosition,
            w: superiorityIndicatorWidth,
            h: superiorityIndicatorCenter - (int)(Evaluator.Evaluate(game.Board) / 100),
            color: AppState.Colors.BoardDark
        );
    }
    private void RenderPlayersName(Canvas canvas)
    {
        int widthLeftLane = (int)(Size.x / 4.8);
        int widthRightLane = Size.x - widthLeftLane;

        int verticalCenter = Position.y + Size.y / 2;
        int horizontalCenter = Position.x + widthRightLane / 2;

        int cellWidth = 6;
        int cellHeight = 3;

        int gapWidth = 0;
        int gapHeight = 0;

        int yStartPosition = verticalCenter - cellHeight * 4 + (int)(gapHeight * 3.5);
        int xStartPosition = widthLeftLane + horizontalCenter - cellWidth * 4 + (int)(gapWidth * 3.5);

        canvas.DrawSymbol(
            symbol: "●",
            x: xStartPosition,
            y: yStartPosition - 5,
            foreground: AppState.Colors.Green,
            background: AppState.Colors.BackgroundDark,
            style: PixelStyle.StyleNone
        );
        canvas.DrawText(
            text: "BOT",
            x: xStartPosition + 2,
            y: yStartPosition - 5,
            foreground: AppState.Colors.Yellow,
            background: AppState.Colors.BackgroundDark,
            style: PixelStyle.StyleNone
        );
        canvas.DrawText(
            text: "Player 2",
            x: xStartPosition + 6,
            y: yStartPosition - 5,
            foreground: AppState.Colors.Foreground,
            background: AppState.Colors.BackgroundDark,
            style: PixelStyle.StyleNone
        );

        if (game.State == GameState.WhiteMove)
        {
            playerAnimationProcessMove.Position = new Vector2(
                x: xStartPosition,
                y: yStartPosition + cellHeight * 8 + (int)(gapHeight * 3.5) + 4
            );
            playerAnimationProcessMove.Render(canvas);
        }
        canvas.DrawText(
            text: "Player 1",
            x: xStartPosition + 2,
            y: yStartPosition + cellHeight * 8 + (int)(gapHeight * 3.5) + 4,
            foreground: AppState.Colors.Foreground,
            background: AppState.Colors.BackgroundDark,
            style: PixelStyle.StyleNone
        );
    }
    private void RenderMoveIndicator(Canvas canvas)
    {
        int widthLeftLane = (int)(Size.x / 4.8);
        int widthRightLane = Size.x - widthLeftLane;

        int verticalCenter = Position.y + Size.y / 2;
        int horizontalCenter = Position.x + widthRightLane / 2;

        int cellWidth = 6;
        int cellHeight = 3;

        int gapWidth = 0;
        int gapHeight = 0;

        int yStartPosition = verticalCenter - cellHeight * 4 + (int)(gapHeight * 3.5) - 7;
        int xStartPosition = widthLeftLane + horizontalCenter - cellWidth * 4 + (int)(gapWidth * 3.5);

        Color foreground;
        Color background;

        if (game.GetCurrentColor() == Core.Color.White)
        {
            foreground = AppState.Colors.Background;
            background = AppState.Colors.Foreground;
        }
        else
        {
            foreground = AppState.Colors.Foreground;
            background = AppState.Colors.Background;
        }

        canvas.DrawText(
            text: $" {game.GetCurrentColor()} To Move ",
            x: xStartPosition,
            y: yStartPosition,
            foreground: foreground,
            background: background,
            style: PixelStyle.StyleNone
        );
    }
    #endregion

    private string GetChessSymbolFromPiece(PieceBase piece)
    {
        switch (piece)
        {
            case Pawn: return piece.Color == Core.Color.White ? "♙" : "♟";
            case Knight: return piece.Color == Core.Color.White ? "♘" : "♞";
            case Bishop: return piece.Color == Core.Color.White ? "♗" : "♝";
            case Rook: return piece.Color == Core.Color.White ? "♖" : "♜";
            case Queen: return piece.Color == Core.Color.White ? "♕" : "♛";
            case King: return piece.Color == Core.Color.White ? "♔" : "♚";
            default: return " ";
        }
    }

    private string GetAsciiSymbolFromPiece(PieceBase piece)
    {
        switch (piece)
        {
            case Pawn: return piece.Color == Core.Color.White ? "p" : "P";
            case Knight: return piece.Color == Core.Color.White ? "n" : "N";
            case Bishop: return piece.Color == Core.Color.White ? "b" : "B";
            case Rook: return piece.Color == Core.Color.White ? "r" : "R";
            case Queen: return piece.Color == Core.Color.White ? "q" : "Q";
            case King: return piece.Color == Core.Color.White ? "k" : "K";
            default: return " ";
        }
    }

    private string GetSymbolFromFile(int file)
    {
        return ((char)('a' + file)).ToString();
    }
}
