namespace Chesser.Core;

public class Game
{
    public List<Move> MoveHistories { get; set; }
    public Board Board { get; set; }
    public GameState State { get; set; }

    public Game()
    {
        MoveHistories = new List<Move>();
        Board = Board.CreateBoardFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");
        State = GameState.WhiteMove;
    }

    public void Move(Move move)
    {
        if (IsEndGame())
        {
            throw new Exception("You can't walk because the game has already ended.");
        }

        IEnumerable<Move> moves = MoveGenerator.GetMoves(Board, GetCurrentColor());
        if (!moves.Contains(move))
        {
            throw new Exception("This move is not available. Please write again enter your move.");
        }

        Board.MakeMove(move);
        ReverseGameState();
    }

    public bool TryMove(Move move)
    {
        Check(); 

        if (IsEndGame())
        {
            return false;
        }

        IEnumerable<Move> moves = MoveGenerator.GetMoves(Board, GetCurrentColor());
        if (!moves.Contains(move))
        {
            return false;
        }

        Board.MakeMove(move);
        ReverseGameState();

        Check(); 
        
        return true;
    }

    public bool IsEndGame()
    {
        return State == GameState.WhiteVictory || State == GameState.BlackVictory;
    }

    public void Check()
    {
        if (Checker.IsCheckmate(Board, GetCurrentColor()))
        {
            if (State == GameState.WhiteMove)
            {
                State = GameState.BlackVictory;
            }
            if (State == GameState.BlackMove)
            {
                State = GameState.WhiteVictory;
            }
        }
    }

    public void ReverseGameState()
    {
        if (State == GameState.WhiteMove)
        {
            State = GameState.BlackMove;
        }
        else if (State == GameState.BlackMove)
        {
            State = GameState.WhiteMove;
        }
    }

    public Color GetCurrentColor()
    {
        if (State == GameState.WhiteMove || State == GameState.WhiteVictory) return Color.White;
        return Color.Black;
    }
}