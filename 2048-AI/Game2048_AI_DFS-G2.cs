using _2048_AI;

// 第二代算法
public class Game2048_AI_DFS_G2 : IGame2048_AI
{
    private Game2048 _game2048;
    // 最大值位置的分数权重
    private int[,] weights = new int[4, 4]
    {
        { 32, 16, 8, 4 },
        { 16, 8, 4, 2 },
        { 8, 4, 2, 1 },
        { 4, 2, 1, 0 }
    };

    public Game2048_AI_DFS_G2(Game2048 game2048)
    {
        _game2048 = game2048;
    }

    public Direction GetBestMove()
    {
        //EvaluateBoard(true);
        int maxScore = int.MinValue;
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
                _game2048.GenerateNumber2();
                // 使用DFS和剪枝算法找到最佳移动
                int score = DFS(10, maxScore);

                //Console.Write($"D:{_game2048.GetArrow(direction)}, score：{score}, maxScore:{maxScore}\t");
                //EvaluateBoard(true);

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

    private int DFS(int depth, int maxScore)
    {
        if (depth == 0)
        {
            return EvaluateBoard();
        }

        int maxEval = EvaluateBoard();
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (direction == Direction.None)
                continue;

            // 复制当前棋盘状态
            int[,] tempBoard = (int[,])_game2048.Board.Clone();
            bool hasMoved = _game2048.Move(direction);

            if (hasMoved)
            {
                _game2048.GenerateNumber2();
                var score = DFS(depth - 1, maxEval);

                // 撤销移动
                _game2048.Board = tempBoard;

                if (score > maxEval)
                {
                    maxEval = score;

                    // 剪枝
                    if (maxEval > maxScore)
                    {
                        break;
                    }
                }
            }
        }

        return maxEval;
    }

    public int EvaluateBoard(bool print = false)
    {
        int score = 0;

        int maxVal = _game2048.Board.Cast<int>().Max() * 2;

        score += maxVal;

        // 最大值位置
        var maxPositionVal = EvaluateByPosition();
        score += maxPositionVal;

        // 空格数量
        var emptyCellsVal = CountEmptyCells() * 100;
        score += emptyCellsVal;

        // 单调性
        var monotonicVal = 0;
        for (int i = 0; i < 4; i++)
        {
            if (IsMonotonic(GetRow(_game2048.Board, i)) || IsMonotonic(GetColumn(_game2048.Board, i)))
            {
                monotonicVal += 1000;
            }
        }
        score += monotonicVal;

        //// 平滑度
        var calculateSmoothness = CalculateSmoothness() / 1;
        score -= calculateSmoothness;
        if (print)
        {
            Console.WriteLine($"score: {score}, maxVal: {maxVal}, maxPositionVal: {maxPositionVal}, emptyCellsVal: {emptyCellsVal}, monotonicVal: {monotonicVal}, calculateSmoothness: {calculateSmoothness}");
        }
        return score;
    }

    private int CountEmptyCells()
    {
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (_game2048.Board[i, j] == 0)
                {
                    count++;
                }
            }
        }
        return count;
    }

    private int[] GetRow(int[,] board, int row)
    {
        return Enumerable.Range(0, board.GetLength(1))
            .Select(x => board[row, x])
            .ToArray();
    }

    private int[] GetColumn(int[,] board, int column)
    {
        return Enumerable.Range(0, board.GetLength(0))
            .Select(x => board[x, column])
            .ToArray();
    }

    private bool IsMonotonic(int[] array)
    {
        return IsNonDecreasing(array) || IsNonIncreasing(array);
    }

    private bool IsNonDecreasing(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i] > array[i + 1])
            {
                return false;
            }
        }
        return true;
    }

    private bool IsNonIncreasing(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i] < array[i + 1])
            {
                return false;
            }
        }
        return true;
    }

    private int CalculateSmoothness()
    {
        int smoothness = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i < 3) // 计算与下方单元格的差值
                {
                    smoothness += Math.Abs(_game2048.Board[i, j] - _game2048.Board[i + 1, j]);
                }
                if (j < 3) // 计算与右侧单元格的差值
                {
                    smoothness += Math.Abs(_game2048.Board[i, j] - _game2048.Board[i, j + 1]);
                }
            }
        }
        return smoothness;
    }

    // 根据棋盘位置评估分数
    private int EvaluateByPosition()
    {
        int score = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                score += weights[i, j] * _game2048.Board[i, j];
            }
        }

        return score;
    }
}