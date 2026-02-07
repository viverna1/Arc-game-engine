using Arc;
using Arc.Core;

class GameApp
{
    static void Main()
    {
        Application.Initialize(1280, 720, "My Game", "icon.png");
        GameScene.Setup(Scene.Instance);
        
        Application.Instance.Run();
    }
}