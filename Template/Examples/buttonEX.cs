using Arc.Core;
using Arc.Components.UI;
using SFML.System;
using System;



var canvas = new GameObject("Canvas");
Scene.Instance.AddGameObject(canvas);


var button = new GameObject("Button");
button.SetParent(canvas);

var rectPanel = button.AddComponent<RectTransform>();
rectPanel.Position = new Vector2f(-100, -25);
rectPanel.Size = new Vector2f(200, 50);
rectPanel.AnchorMin = new Vector2f(0.5f, 0.5f);
rectPanel.AnchorMax = new Vector2f(0.5f, 0.5f);

var buttonImage = button.AddComponent<Image>();
buttonImage.FillColor = new SFML.Graphics.Color(100, 100, 100, 200);

var cursorEvents = button.AddComponent<CursorEvents>();
cursorEvents.OnMouseEnter += () => Console.WriteLine("enter");
cursorEvents.OnMouseExit += () => { 
    buttonImage.FillColor = SFML.Graphics.Color.White;
    Console.WriteLine("exit");
};
cursorEvents.OnMouseHover += () => buttonImage.FillColor = SFML.Graphics.Color.Blue;
cursorEvents.OnMousePressed += () => buttonImage.FillColor = SFML.Graphics.Color.Green;
cursorEvents.OnMouseReleased += () => Console.WriteLine("released");
cursorEvents.OnClick += () => Console.WriteLine("click");

Scene.Instance.AddGameObject(button);