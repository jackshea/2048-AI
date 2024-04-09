/// <summary>
/// ÆÀ¹Àº¯Êý3
/// </summary>
/// <see cref="https://github.com/ovolve/2048-AI/blob/master/js/ai.js"/>
public class Game2048_Evaluator3 : IEvaluate
{
    private Game2048 _game2048;

    private const double smoothWeight = 0.1;
    private const double mono2Weight = 1.0;
    private const double emptyWeight = 2.7;
    private const double maxWeight = 1.0;


    public Game2048_Evaluator3(Game2048 game2048)
    {
        _game2048 = game2048;
    }

    public double Evaluate()
    {
        double score = 0;
        var countEmptyCells = CountEmptyCells();
        var scoreEmpty = countEmptyCells <= 0 ? -10000 : Math.Log(countEmptyCells) * emptyWeight;
        var scoreMonotonicity = MonotonicityScore() * mono2Weight;
        var scoreSmoothnessScore = GetSmoothness() * smoothWeight;
        var scoreMaxValue = GetMaxValue() * maxWeight;

        score = scoreEmpty + scoreMonotonicity + scoreSmoothnessScore + scoreMaxValue;
        //Console.WriteLine($"score={score}, scoreLostPenalty={scoreLostPenalty}, scoreEmpty={scoreEmpty}, scoreMerges={scoreMerges}, scoreMonotonicity={scoreMonotonicity}, scoreSum={scoreSum}");
        return score;
    }

    private double GetSmoothness()
    {
        double smoothness = 0;
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (_game2048.Board[x, y] != 0)
                {
                    var value = FastLog2(_game2048.Board[x, y]);

                    int targetX = Math.Min(x + 1, 3);
                    int targetY = y;


                    if (_game2048.Board[targetX, targetY] != 0)
                    {
                        var targetValue = FastLog2(_game2048.Board[targetX, targetY]);
                        smoothness -= Math.Abs(value - targetValue);
                    }

                    targetX = x;
                    targetY = Math.Min(y + 1, 3);
                    if (_game2048.Board[targetX, targetY] != 0)
                    {
                        var targetValue = FastLog2(_game2048.Board[targetX, targetY]);
                        smoothness -= Math.Abs(value - targetValue);
                    }
                }
            }
        }
        return smoothness;
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

    private int GetMaxValue()
    {
        int max = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (_game2048.Board[i, j] > max)
                {
                    max = _game2048.Board[i, j];
                }
            }
        }
        return max;
    }

    private double MonotonicityScore()
    {
        double[] totals = new double[4];
        for (int x = 0; x < 4; x++)
        {
            int current = 0;
            int next = current + 1;
            while (next < 4)
            {
                while (next < 4 && _game2048.Board[x, next] == 0)
                {
                    next++;
                }
                if (next >= 4) { next--; }
                double currentValue = _game2048.Board[x, current] != 0 ?
                    FastLog2(_game2048.Board[x, current]) :
                0;
                double nextValue = _game2048.Board[x, next] != 0 ?
                    FastLog2(_game2048.Board[x, next]) :
                0;
                if (currentValue > nextValue)
                {
                    totals[0] += nextValue - currentValue;
                }
                else if (nextValue > currentValue)
                {
                    totals[1] += currentValue - nextValue;
                }
                current = next;
                next++;
            }
        }

        for (int y = 0; y < 4; y++)
        {
            int current = 0;
            int next = current + 1;
            while (next < 4)
            {
                while (next < 4 && _game2048.Board[next, y] == 0)
                {
                    next++;
                }
                if (next >= 4) { next--; }
                double currentValue = _game2048.Board[current, y] != 0 ?
                    FastLog2(_game2048.Board[current, y]) :
                0;
                double nextValue = _game2048.Board[next, y] != 0 ?
                    FastLog2(_game2048.Board[next, y]) :
                0;
                if (currentValue > nextValue)
                {
                    totals[2] += nextValue - currentValue;
                }
                else if (nextValue > currentValue)
                {
                    totals[3] += currentValue - nextValue;
                }
                current = next;
                next++;
            }
        }

        return Math.Max(totals[0], totals[1]) + Math.Max(totals[2], totals[3]);
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
