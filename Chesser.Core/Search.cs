namespace Chesser.Core;

public static class Search
{
    public static Move? GetBestMove(Board board, Color color)
    {
        IEnumerable<Move> moves = MoveGenerator.GetMoves(board, color);
        Move? bestMove = null;
        int bestValue = int.MinValue;

        foreach (Move move in moves)
        {
            board.MakeMove(move);
            int value = -Evaluator.Evaluate(board);
            board.UnmakeMove(move);
            if (value > bestValue)
            {
                bestValue = value;
                bestMove = move;
            }
        }

        return bestMove;
    }
}