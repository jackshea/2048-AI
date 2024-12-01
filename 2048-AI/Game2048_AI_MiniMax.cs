// 极小化极大算法

public class Game2048_AI_MiniMax : IGame2048_AI
{
    private Game2048 _game2048;
    private readonly Game2048_Evaluator _game2048Evaluator;

    public Game2048_AI_MiniMax(Game2048 game2048)
    {
        _game2048 = game2048;
        _game2048Evaluator = new Game2048_Evaluator(_game2048);
    }

    public Direction GetBestMove()
    {
        double maxScore = int.MinValue;
        Direction bestMove = Direction.None;

        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (direction == Direction.None)
                continue;

            // 复制当前棋盘状态
            int[,] tempBoard = (int[,])_game2048.Board.Clone();
            bool hasMoved = _game2048.Move(direction);

            if (hasMoved)
            {
                // 使用极小化极大算法找到最佳移动
                double score = MiniMax(12, true);

                // 撤销移动
                _game2048.Board = tempBoard;

                if (score > maxScore)
                {
                    maxScore = score;
                    bestMove = direction;
                }
            }
        }

        return bestMove;
    }

    // 极小化极大算法
    private double MiniMax(int depth, bool isMax)
    {
        if (depth == 0)
        {
            return _game2048Evaluator.Evaluate();
        }

        if (isMax)
        {
            // 移动
            double maxEval = int.MinValue;
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (direction == Direction.None)
                    continue;

                // 复制当前棋盘状态
                int[,] tempBoard = (int[,])_game2048.Board.Clone();
                bool hasMoved = _game2048.Move(direction);

                if (hasMoved)
                {
                    double eval = MiniMax(depth - 1, false);

                    // 撤销移动
                    _game2048.Board = tempBoard;

                    maxEval = Math.Max(maxEval, eval);
                }
            }
            return maxEval;
        }
        else
        {
            // 生成数据
            double minEval = int.MaxValue;
            // 复制当前棋盘状态
            int[,] tempBoard = (int[,])_game2048.Board.Clone();
            _game2048.GenerateNumber2();
            double eval = MiniMax(depth - 1, true);
            // 还原
            _game2048.Board = tempBoard;
            minEval = Math.Min(minEval, eval);
            return minEval;
        }
    }
}