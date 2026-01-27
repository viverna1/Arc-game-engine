using Engine.Core;
using Engine.Components;
using Engine.System;
using SFML.Window;

class MainClass
{
    static void Main()
    {
        // SceneManager scene = new();
        // scene.Load();
        // return;
        var app = new Application(1200, 800);
        app.BindKey(Keyboard.Key.Escape, app.Window.Close);

        // app.window.MouseWheelScrolled += (sender, e) => 
        // {
        //     // Обработка зума колесом мыши
        //     float zoomFactor = (e.Delta > 0) ? 0.9f : 1.1f; // Delta > 0 - прокрутка вверх
            
        //     // Умножаем размер области видимости (отдаление/приближение)
        //     camera.Size = new Vector2f(camera.Size.X * zoomFactor, camera.Size.Y * zoomFactor);
            
        //     // Опционально: зум относительно курсора мыши
        //     // Для этого нужно преобразовать координаты мыши в мировые
        //     // Vector2f mouseWorldPos = window.MapPixelToCoords(new Vector2i(e.X, e.Y), camera);
        //     // camera.Zoom(zoomFactor); // Альтернативный метод
        //     // camera.Center = mouseWorldPos - (mouseWorldPos - camera.Center) * zoomFactor;
        // };
        
        var obj = new GameObject("Player");
        var player = obj.AddComponent<Player>();
        var transform = obj.AddComponent<Transform>();
        var renderer = obj.AddComponent<SpriteRenderer>();
        app.AddGameObject(obj);
        
        app.Run();
    }
}
