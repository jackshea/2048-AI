public class Game2048
{
    /// <summary>
    /// 2048��Ϸ������
    /// </summary>
    private int[,] board;

    /// <summary>
    /// �������������
    /// </summary>
    private Random random;

    public Game2048()
    {
        board = new int[4, 4];
        random = new Random();
        GenerateNumber();
        GenerateNumber();
    }

    /// <summary>
    /// ������������һ���µ�����
    /// </summary>
    public void GenerateNumber()
    {
        while (true)
        {
            int row = random.Next(0, 4);
            int col = random.Next(0, 4);

            if (board[row, col] == 0)
            {
                board[row, col] = random.Next(0, 2) == 0 ? 2 : 4;
                break;
            }
        }
    }

    // �Ƿ���Ϸ����
    public bool IsGameOver()
    {
        if (board.Cast<int>().Any(x => x == 0))
        {
            return false;
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i > 0 && board[i, j] == board[i - 1, j])
                {
                    return false;
                }

                if (i < 3 && board[i, j] == board[i + 1, j])
                {
                    return false;
                }

                if (j > 0 && board[i, j] == board[i, j - 1])
                {
                    return false;
                }

                if (j < 3 && board[i, j] == board[i, j + 1])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool MoveLeft()
    {
        bool hasMoved = false;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = j + 1; k < 4; k++)
                {
                    if (board[i, k] > 0)
                    {
                        if (board[i, j] <= 0)
                        {
                            board[i, j] = board[i, k];
                            board[i, k] = 0;
                            hasMoved = true;
                        }
                        else if (board[i, j] == board[i, k])
                        {
                            board[i, j] *= 2;
                            board[i, k] = 0;
                            hasMoved = true;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        return hasMoved;
    }

    public bool MoveRight()
    {
        bool hasMoved = false;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 3; j >= 0; j--)
            {
                for (int k = j - 1; k >= 0; k--)
                {
                    if (board[i, k] > 0)
                    {
                        if (board[i, j] <= 0)
                        {
                            board[i, j] = board[i, k];
                            board[i, k] = 0;
                            hasMoved = true;
                        }
                        else if (board[i, j] == board[i, k])
                        {
                            board[i, j] *= 2;
                            board[i, k] = 0;
                            hasMoved = true;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        return hasMoved;
    }

    public bool MoveUp()
    {
        bool hasMoved = false;
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int k = i + 1; k < 4; k++)
                {
                    if (board[k, j] > 0)
                    {
                        if (board[i, j] <= 0)
                        {
                            board[i, j] = board[k, j];
                            board[k, j] = 0;
                            hasMoved = true;
                        }
                        else if (board[i, j] == board[k, j])
                        {
                            board[i, j] *= 2;
                            board[k, j] = 0;
                            hasMoved = true;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        return hasMoved;
    }

    public bool MoveDown()
    {
        bool hasMoved = false;
        for (int j = 0; j < 4; j++)
        {
            for (int i = 3; i >= 0; i--)
            {
                for (int k = i - 1; k >= 0; k--)
                {
                    if (board[k, j] > 0)
                    {
                        if (board[i, j] <= 0)
                        {
                            board[i, j] = board[k, j];
                            board[k, j] = 0;
                            hasMoved = true;
                        }
                        else if (board[i, j] == board[k, j])
                        {
                            board[i, j] *= 2;
                            board[k, j] = 0;
                            hasMoved = true;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        return hasMoved;
    }

    /// <summary>
    /// �ƶ�
    /// </summary>
    /// <param name="direction">����</param>
    /// <returns></returns>
    public bool Move(Direction direction)
    {
        bool hasMoved = false;
        switch (direction)
        {
            case Direction.Left:
                hasMoved = MoveLeft();
                break;
            case Direction.Right:
                hasMoved = MoveRight();
                break;
            case Direction.Up:
                hasMoved = MoveUp();
                break;
            case Direction.Down:
                hasMoved = MoveDown();
                break;
        }
        return hasMoved;
    }

    // ��ӡ��Ϸ����
    public void Print()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Console.Write(board[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
    // ��ӡ���������Ϸ����
    public void PrintWithBorder()
    {
        Console.WriteLine("���������������Щ������������Щ������������Щ�������������");
        for (int i = 0; i < 4; i++)
        {
            Console.Write("��");
            for (int j = 0; j < 4; j++)
            {
                if (board[i, j] == 0)
                {
                    Console.Write("      ");
                }
                else
                {
                    Console.Write(board[i, j].ToString().PadLeft(6));
                }
                Console.Write("��");
            }
            Console.WriteLine();
            if (i != 3)
            {
                Console.WriteLine("���������������੤�����������੤�����������੤������������");
            }
        }
        Console.WriteLine("���������������ة������������ة������������ة�������������");
    }

    /// <summary>
    /// ��ʼ2048��Ϸ
    /// </summary>
    public void Start()
    {
        while (true)
        {
            Console.Clear();
            PrintWithBorder();
            if (IsGameOver())
            {
                Console.WriteLine("Game Over!");
                Console.ReadKey();
                break;
            }
            var direction = GetBestMove();
            Console.WriteLine(GetArrow(direction));
            ConsoleKeyInfo key = Console.ReadKey();
            //var direction = Direction.None;
            //switch (key.Key)
            //{
            //    case ConsoleKey.LeftArrow:
            //        direction = Direction.Left;
            //        break;
            //    case ConsoleKey.RightArrow:
            //        direction = Direction.Right;
            //        break;
            //    case ConsoleKey.UpArrow:
            //        direction = Direction.Up;
            //        break;
            //    case ConsoleKey.DownArrow:
            //        direction = Direction.Down;
            //        break;
            //}

            if (Move(direction))
            {
                GenerateNumber();
            }
        }
    }

    // ��ȡ�����ͷ
    public string GetArrow(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                return "��";
            case Direction.Right:
                return "��";
            case Direction.Up:
                return "��";
            case Direction.Down:
                return "��";
        }

        return string.Empty;
    }


    #region AI
    public Direction GetBestMove()
    {
        int maxScore = int.MinValue;
        Direction bestMove = Direction.None;

        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (direction == Direction.None)
                continue;

            // ���Ƶ�ǰ����״̬
            int[,] tempBoard = (int[,])board.Clone();
            bool hasMoved = Move(direction);

            if (hasMoved)
            {
                // ʹ��DFS�ͼ�֦�㷨�ҵ�����ƶ�
                int score = DFS(10, maxScore);

                // �����ƶ�
                board = tempBoard;

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
            int[,] tempBoard = (int[,])board.Clone();
            bool hasMoved = Move(direction);

            if (hasMoved)
            {
                int score = DFS(depth - 1, maxEval);

                // �����ƶ�
                board = tempBoard;

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
        if (board[0, 0] == board.Cast<int>().Max())
        {
            score += 1000;
        }

        // ������
        for (int i = 0; i < 4; i++)
        {
            if (IsMonotonic(GetRow(board,i)) || IsMonotonic(GetColumn(board,i)))
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
                if (board[i, j] == 0)
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
                    smoothness += Math.Abs(board[i, j] - board[i + 1, j]);
                }
                if (j < 3) // �������Ҳ൥Ԫ��Ĳ�ֵ
                {
                    smoothness += Math.Abs(board[i, j] - board[i, j + 1]);
                }
            }
        }
        return smoothness;
    }

    #endregion
}

/// <summary>
/// ��ʾ�ƶ��ķ���
/// </summary>
public enum Direction
{
    None,
    Up,
    Down,
    Left,
    Right
}