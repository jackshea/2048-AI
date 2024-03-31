// DFS�ڶ����㷨

public class Game2048_AI_DFS_G2 : IGame2048_AI
{
    private Game2048 _game2048;
    private readonly Game2048_Evaluator _game2048Evaluator;

    public Game2048_AI_DFS_G2(Game2048 game2048)
    {
        _game2048 = game2048;
        _game2048Evaluator = new Game2048_Evaluator(_game2048);
    }

    public Direction GetBestMove()
    {
        //EvaluateBoard(true);
        double maxScore = int.MinValue;
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
                _game2048.GenerateNumber2();
                // ʹ��DFS�ͼ�֦�㷨�ҵ�����ƶ�
                double score = DFS(10, maxScore);

                //Console.Write($"D:{_game2048.GetArrow(direction)}, score��{score}, maxScore:{maxScore}\t");
                //EvaluateBoard(true);

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

    private double DFS(int depth, double maxScore)
    {
        if (depth == 0)
        {
            return _game2048Evaluator.EvaluateBoard();
        }

        double maxEval = _game2048Evaluator.EvaluateBoard();
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (direction == Direction.None)
                continue;

            // ���Ƶ�ǰ����״̬
            int[,] tempBoard = (int[,])_game2048.Board.Clone();
            bool hasMoved = _game2048.Move(direction);

            if (hasMoved)
            {
                _game2048.GenerateNumber2();
                var score = DFS(depth - 1, maxEval);

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
}