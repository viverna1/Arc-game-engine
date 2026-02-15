namespace Arc.Core;

// Базовый класс для всех рендереров
public abstract class IRenderable : Component
{
    public abstract short ZLayer { get; set; }
    public abstract bool IsUI { get; set; }
    public abstract void Draw(SFML.Graphics.RenderWindow window);
}