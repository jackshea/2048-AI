// 蒙特卡洛树搜索算法
public class Game2048_AI_Mcts : IGame2048_AI
{
    private Game2048 game;
    private int simulationsPerMove = 100; // 每次移动前的模拟次数

    public Game2048_AI_Mcts(Game2048 game)
    {
        this.game = game;
    }

    public Direction GetBestMove()
    {
        throw new NotImplementedException();
    }

    private Node SelectNode(Node node)
    {
        throw new NotImplementedException();
    }

    private Node ExpandNode(Node node)
    {
        throw new NotImplementedException();
    }

    private double Simulate(Node node)
    {
        throw new NotImplementedException();
    }

    private void Backpropagate(Node node, double result)
    {
        throw new NotImplementedException();
    }

}
