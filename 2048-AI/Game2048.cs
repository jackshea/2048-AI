public class Game2048
{
    private int[,] board;
    private Random random;

    public Game2048()
    {
        board = new int[4, 4];
        random = new Random();
        GenerateNumber();
        GenerateNumber();
    }

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

    // 是否游戏结束
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

    public void MoveLeft()
    {
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
                        }
                        else if (board[i, j] == board[i, k])
                        {
                            board[i, j] *= 2;
                            board[i, k] = 0;
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
    }

    public void MoveRight()
    {
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
                        }
                        else if (board[i, j] == board[i, k])
                        {
                            board[i, j] *= 2;
                            board[i, k] = 0;
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
    }

    public void MoveUp()
    {
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
                        }
                        else if (board[i, j] == board[k, j])
                        {
                            board[i, j] *= 2;
                            board[k, j] = 0;
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
    }

    public void MoveDown()
    {
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
                        }
                        else if (board[i, j] == board[k, j])
                        {
                            board[i, j] *= 2;
                            board[k, j] = 0;
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
    }

    public void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                MoveLeft();
                break;
            case Direction.Right:
                MoveRight();
                break;
            case Direction.Up:
                MoveUp();
                break;
            case Direction.Down:
                MoveDown();
                break;
        }
    }

    // 打印游戏界面
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
    // 打印加网络的游戏界面
    public void PrintWithBorder()
    {
        Console.WriteLine("┌──────┬──────┬──────┬──────┐");
        for (int i = 0; i < 4; i++)
        {
            Console.Write("│");
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
                Console.Write("│");
            }
            Console.WriteLine();
            if (i != 3)
            {
                Console.WriteLine("├──────┼──────┼──────┼──────┤");
            }
        }
        Console.WriteLine("└──────┴──────┴──────┴──────┘");
    }

    // 开始2048游戏
    public void Start()
    {
        while (true)
        {
            Console.Clear();
            PrintWithBorder();
            if (IsGameOver())
            {
                Console.WriteLine("Game Over!");
                break;
            }

            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    Move(Direction.Left);
                    break;
                case ConsoleKey.RightArrow:
                    Move(Direction.Right);
                    break;
                case ConsoleKey.UpArrow:
                    Move(Direction.Up);
                    break;
                case ConsoleKey.DownArrow:
                    Move(Direction.Down);
                    break;
            }

            GenerateNumber();
        }
    }
}


public enum Direction
{
    Up,
    Down,
    Left,
    Right
}