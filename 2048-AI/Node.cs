public class Node
{
    public Node Parent { get; set; }
    public List<Node> Children { get; set; }
    public double Wins { get; set; }
    public int Visits { get; set; }
    public Direction Move { get; set; } // 假设Move是一个表示移动的类
    public GameState State { get; set; } // 假设GameState是一个表示游戏状态的类

    public Node(Node parent, Direction move, GameState state)
    {
        Parent = parent;
        Move = move;
        State = state;
        Children = new List<Node>();
        Wins = 0;
        Visits = 0;
    }

    // 添加子节点的方法
    public void AddChild(Node child)
    {
        Children.Add(child);
    }

    // 其他方法，比如选择子节点、更新节点信息等
}

public class GameState
{
    // 游戏状态的属性和方法
}