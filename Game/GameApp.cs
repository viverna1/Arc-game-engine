using Engine;
using Engine.Core;

class GameApp
{
    static void Main()
    {
        Application.Initialize(1200, 800, "My Game");
        Scene scene = new Scene("Main Scene");
        GameScene.Setup(scene);
        
        Application.Instance.LoadScene(scene);
        Application.Instance.Run();
    }
} 