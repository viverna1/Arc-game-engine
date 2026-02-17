using Arc.Components.UI;
using Arc.System;
using Arc.Core;
using SFML.System;

class Test : Component
{
    public Text stateText = null!;
    public RectTransform rect = null!;
    
    public Vector2f AnchorMin = new(0.5f, 0.5f);
    public Vector2f AnchorMax = new(0.5f, 0.5f);
    
    public float Left { get; set; } = 0f;
    public float Top { get; set; } = 0f;
    public float Right { get; set; } = 0f;
    public float Bottom { get; set; } = 0f;
    
    public Vector2f Pivot = new(0.5f, 0.5f);

    public override void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
    }
    
    public override void Update(float deltaTime)
    {
        Pivot.X = ChangeVar(Pivot.X, SFML.Window.Keyboard.Key.D, SFML.Window.Keyboard.Key.A, 0.1f);
        Pivot.Y = ChangeVar(Pivot.Y, SFML.Window.Keyboard.Key.Right, SFML.Window.Keyboard.Key.Left, 0.1f);

        UpdateState();
        stateText.text = $"AnchorMin: {AnchorMin}\nAnchorMax: {AnchorMax}\nLeft: {Left}\nTop: {Top}\nRight: {Right}\nBottom: {Bottom}\nPivot: {Pivot}";
    }

    private float ChangeVar(float var, SFML.Window.Keyboard.Key keyPlus, SFML.Window.Keyboard.Key keyMinus, float step)
    {
        if (Input.IsKeyDown(keyPlus))
            var += step;
        if (Input.IsKeyDown(keyMinus))
            var -= step;
        return var;
    }

    private void UpdateState()
    {
        rect.Left = Left;
        rect.Top = Top;
        rect.Right = Right;
        rect.Bottom = Bottom;

        rect.AnchorMin = AnchorMin;
        rect.AnchorMax = AnchorMax;

        rect.Pivot = Pivot;
    }
}