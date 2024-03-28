using _2048_AI;

public class Game2048_AI_DFS: IGame2048_AI
{
    private Game2048 _game2048;

    public Game2048_AI_DFS(Game2048 game2048)
    {
        _game2048 = game2048;
    }

    public Direction GetBestMove()
    {
        int maxScore = int.MinValue;
        Direction bestMove = Direction.None;

        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (direction == Direction.None)
                continue;

            // ���Ƶ�ǰ����״̬
            int[,] tempBoard = (int[,])_game2048.Board.Clone();
            bool hasMoved = _game2048.Move(direction);

            if (hasMoved)
            {
                // ʹ��DFS�ͼ�֦�㷨�ҵ�����ƶ�
                int score = DFS(10, maxScore);

                // �����ƶ�
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

        int maxEval = int.MinValue;

        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (direction == Direction.None)
                continue;

            // ���Ƶ�ǰ����״̬
            int[,] tempBoard = (int[,])_game2048.Board.Clone();
            bool hasMoved = _game2048.Move(direction);

            if (hasMoved)
            {
                int score = DFS(depth - 1, maxEval);

                // �����ƶ�
                _game2048.Board = tempBoard;

                if (score > maxEval)
                {
                    maxEval = score;

                    // ��֦
                    if (maxEval > maxScore)
                    {
                        break;
                    }
                }
            }
        }

        return maxEval;
    }

    private int EvaluateBoard()
    {
        int score = 0;

        // ���ֵλ��
        if (_game2048.Board[0, 0] == _game2048.Board.Cast<int>().Max())
        {
            score += 1000;
        }

        // ������
        for (int i = 0; i < 4; i++)
        {
            if (IsMonotonic(GetRow(_game2048.Board, i)) || IsMonotonic(GetColumn(_game2048.Board, i)))
            {
                score += 200;
            }
        }

        // �ո�����
        score += CountEmptyCells() * 500;

        // ƽ����
        score -= CalculateSmoothness() * 100;

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
                if (i < 3) // �������·���Ԫ��Ĳ�ֵ
                {
                    smoothness += Math.Abs(_game2048.Board[i, j] - _game2048.Board[i + 1, j]);
                }
                if (j < 3) // �������Ҳ൥Ԫ��Ĳ�ֵ
                {
                    smoothness += Math.Abs(_game2048.Board[i, j] - _game2048.Board[i, j + 1]);
                }
            }
        }
        return smoothness;
    }
}