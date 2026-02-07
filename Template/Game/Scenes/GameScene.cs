using Arc.Components;
using Arc.Components.UI;
using Arc.Core;
using Arc.System;
using SFML.System;

public static class GameScene
{
    public static void Setup(Scene scene)
    {
        // Объект
        GameObject obj = new("Object");
        obj.AddComponent<Transform>();
        obj.AddComponent<SpriteRenderer>();
        scene.AddGameObject(obj);


        var canvas = new GameObject("Canvas");
        Scene.Instance.AddGameObject(canvas);

        var panelObject = new GameObject("Button");
        panelObject.SetParent(canvas);
        
        var rectPanel = panelObject.AddComponent<RectTransform>();
        rectPanel.Position = new Vector2f(-100, -25);
        rectPanel.Size = new Vector2f(200, 50);
        rectPanel.AnchorMin = new Vector2f(0.5f, 0.5f);
        rectPanel.AnchorMax = new Vector2f(0.5f, 0.5f);
        
        var panelImage = panelObject.AddComponent<Image>();
        panelImage.FillColor = new SFML.Graphics.Color(100, 100, 100, 200);

        panelObject.AddComponent<Button>();
        Scene.Instance.AddGameObject(panelObject);


        var temp = new  GameObject("temp");
        obj.AddComponent<Temp>();
        scene.AddGameObject(temp);
    }
}