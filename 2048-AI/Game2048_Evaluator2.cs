using static System.Formats.Asn1.AsnWriter;

public class Game2048_Evaluator2 : IEvaluate
{
    private Game2048 _game2048;

    private const float SCORE_LOST_PENALTY = 200000.0f;
    private const float SCORE_MONOTONICITY_POWER = 4.0f;
    private const float SCORE_MONOTONICITY_WEIGHT = 47.0f;
    private const float SCORE_SUM_POWER = 3.5f;
    private const float SCORE_SUM_WEIGHT = 11.0f;
    private const float SCORE_MERGES_WEIGHT = 700.0f;
    private const float SCORE_EMPTY_WEIGHT = 270.0f;

    public Game2048_Evaluator2(Game2048 game2048)
    {
        _game2048 = game2048;
    }

    public double Evaluate()
    {
        double score = 0;
        var scoreLostPenalty = SCORE_LOST_PENALTY * 2;
        var scoreEmpty = CountEmptyCells() * 2 * SCORE_EMPTY_WEIGHT;
        var scoreMerges = MergeCount() * SCORE_MERGES_WEIGHT;
        var scoreMonotonicity = MonotonicityScore() * SCORE_MONOTONICITY_WEIGHT;
        var scoreSum = SumScore() * 2 * SCORE_SUM_WEIGHT;

        score = scoreLostPenalty + scoreEmpty + scoreMerges - scoreMonotonicity - scoreSum;
        //Console.WriteLine($"score={score}, scoreLostPenalty={scoreLostPenalty}, scoreEmpty={scoreEmpty}, scoreMerges={scoreMerges}, scoreMonotonicity={scoreMonotonicity}, scoreSum={scoreSum}");
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

    private double SumScore()
    {
        double score = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var v = FastLog2(_game2048.Board[i, j]);
                score += Math.Pow(v, SCORE_SUM_POWER);
            }
        }
        return score;
    }

    // 可合并数量
    private double MergeCount()
    {
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            int merges = 0;
            int prev = 0;
            int counter = 0;
            for (int j = 0; j < 4; j++)
            {
                int rank = _game2048.Board[i, j];
                if (rank != 0)
                {
                    if (prev == rank)
                    {
                        counter++;
                    }
                    else if (counter > 0)
                    {
                        merges += 1 + counter;
                        counter = 0;
                    }

                    prev = rank;
                }
            }
            if (counter > 0)
            {
                merges += 1 + counter;
            }
            count += merges;
        }

        for (int j = 0; j < 4; j++)
        {
            int merges = 0;
            int prev = 0;
            int counter = 0;
            for (int i = 0; i < 4; i++)
            {
                int rank = _game2048.Board[i, j];
                if (rank != 0)
                {
                    if (prev == rank)
                    {
                        counter++;
                    }
                    else if (counter > 0)
                    {
                        merges += 1 + counter;
                        counter = 0;
                    }

                    prev = rank;
                }
            }

            if (counter > 0)
            {
                merges += 1 + counter;
            }
            count += merges;
        }

        return count;
    }

    // 单调性
    private double MonotonicityScore()
    {
        double score = 0;
        for (int i = 0; i < 4; i++)
        {
            double left = 0;
            double right = 0;
            for (int j = 1; j < 4; j++)
            {
                if (_game2048.Board[i, j - 1] > _game2048.Board[i, j])
                {
                    left += Math.Pow(FastLog2(_game2048.Board[i, j - 1]), SCORE_MONOTONICITY_POWER) - Math.Pow(FastLog2(_game2048.Board[i, j]), SCORE_MONOTONICITY_POWER);
                }
                else
                {
                    right += Math.Pow(FastLog2(_game2048.Board[i, j]), SCORE_MONOTONICITY_POWER) - Math.Pow(FastLog2(_game2048.Board[i, j - 1]), SCORE_MONOTONICITY_POWER);
                }
            }
            score += Math.Min(left, right);
        }

        for (int j = 0; j < 4; j++)
        {
            double up = 0;
            double down = 0;
            for (int i = 1; i < 4; i++)
            {
                if (_game2048.Board[i - 1, j] > _game2048.Board[i, j])
                {
                    up += Math.Pow(FastLog2(_game2048.Board[i - 1, j]), SCORE_MONOTONICITY_POWER) - Math.Pow(FastLog2(_game2048.Board[i, j]), SCORE_MONOTONICITY_POWER);
                }
                else
                {
                    down += Math.Pow(FastLog2(_game2048.Board[i, j]), SCORE_MONOTONICITY_POWER) - Math.Pow(FastLog2(_game2048.Board[i - 1, j]), SCORE_MONOTONICITY_POWER);
                }
            }
            score += Math.Min(up, down);
        }

        return score;
    }

    private int FastLog2(int n)
    {
        if (n == 0)
        {
            return 0;
        }

        int log = 0;
        while ((n >>= 1) != 0)
        {
            log++;
        }
        return log;
    }
}