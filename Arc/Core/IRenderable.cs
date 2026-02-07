namespace Arc.Core;

// Базовый класс для всех рендереров
public abstract class Renderer : Component
{
    public abstract short ZLayer { get; set; }
    
    public abstract void Draw(SFML.Graphics.RenderWindow window);
}