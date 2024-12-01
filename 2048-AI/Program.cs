// See https://aka.ms/new-console-template for more information

var game2048 = new Game2048();
game2048.SetAI(new Game2048_AI_DFS_G2(game2048));
game2048.Start();