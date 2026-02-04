using Arc;
using Arc.Core;

class GameApp
{
    static void Main()
    {
        Application.Initialize(1280, 704, "My Game");
        GameScene.Setup(Scene.Instance);
        
        Application.Instance.Run();
    }
}