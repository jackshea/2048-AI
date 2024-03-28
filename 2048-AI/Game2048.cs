using _2048_AI;

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

    private IGame2048_AI _ai;

    public int[,] Board
    {
        get { return board;}
        set { board = value; }
    }

    public Game2048()
    {
        board = new int[4, 4];
        random = new Random();
        GenerateNumber();
        GenerateNumber();
    }

    public void SetAI(IGame2048_AI ai)
    {
        _ai = ai;
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
        int step = 1;
        while (true)
        {
            Console.Clear();
            PrintWithBorder();
            if (IsGameOver())
            {
                Console.WriteLine("Game Over!");
                Console.WriteLine($"��{step}��");
                Console.ReadKey();
                break;
            }
            var direction = _ai.GetBestMove();
            Console.WriteLine($"��{step}����{GetArrow(direction)}");
            //ConsoleKeyInfo key = Console.ReadKey();
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
                step++;
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