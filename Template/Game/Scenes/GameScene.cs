using Arc.Components;
using Arc.Components.UI;
using Arc.Core;
using SFML.System;
using System;

public static class GameScene
{
    public static void Setup(Scene scene)
    {
        GameObject canvas = new("Canvas");
        scene.AddGameObject(canvas);
        
        var cam = new GameObject("CameraControl");
        cam.AddComponent<CameraControl>();
        scene.AddGameObject(cam);

        var textObj = new GameObject("Text");
        var textRect = textObj.AddComponent<RectTransform>();
        var text = textObj.AddComponent<Text>();
        scene.AddGameObject(textObj);

        var obj1 = new GameObject("rect1");
        var rect1 = obj1.AddComponent<RectTransform>();
        var testComp = obj1.AddComponent<Test>();
        testComp.stateText = text;

        // testComp.Top = 10;
        // testComp.Right = 10;
        // testComp.Bottom = 10;
        // testComp.Left = 10;
        
        // testComp.AnchorMin = new Vector2f(0, 0);
        // testComp.AnchorMax = new Vector2f(0.5f, 0.5f);

        rect1.Pivot = new Vector2f(0, 0);

        obj1.AddComponent<Image>();

        var obj2 = new GameObject("rect1");
        var rect2 = obj2.AddComponent<RectTransform>();

        rect2.Position = new Vector2f(50, 50);
        rect2.Size = new Vector2f(250, 250);

        obj2.AddComponent<Image>().FillColor = SFML.Graphics.Color.Cyan;
        scene.AddGameObject(obj2);

        scene.AddGameObject(obj1);
        obj1.SetParent(obj2);
    }
}