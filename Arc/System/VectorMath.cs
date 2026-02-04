using SFML.System;
using System;

namespace Arc.System;

public static class VectorMath
{
    // Длина вектора
    public static float Length(Vector2f v)
    {
        return MathF.Sqrt(v.X * v.X + v.Y * v.Y);
    }

    // Нормализация
    public static Vector2f Normalize(Vector2f v)
    {
        float length = Length(v);
        return length > 0 ? new Vector2f(v.X / length, v.Y / length) : v;
    }

    // Расстояние между точками
    public static float Distance(Vector2f a, Vector2f b)
    {
        return Length(b - a);
    }

    // Угол вектора в радианах
    public static float Angle(Vector2f v)
    {
        return MathF.Atan2(v.Y, v.X);
    }

    // Угол между двумя точками в радианах
    public static float AngleBetween(Vector2f from, Vector2f to)
    {
        return Angle(to - from);
    }

    // Угол между двумя точками в градусах
    public static float AngleBetweenDegrees(Vector2f from, Vector2f to)
    {
        return Angle(to - from) * 180f / MathF.PI;
    }

    // Скалярное произведение
    public static float Dot(Vector2f a, Vector2f b)
    {
        return a.X * b.X + a.Y * b.Y;
    }

    // Поворот вектора на угол (радианы)
    public static Vector2f Rotate(Vector2f v, float radians)
    {
        float cos = MathF.Cos(radians);
        float sin = MathF.Sin(radians);
        return new Vector2f(v.X * cos - v.Y * sin, v.X * sin + v.Y * cos);
    }

    // Линейная интерполяция
    public static Vector2f Lerp(Vector2f a, Vector2f b, float t)
    {
        return a + (b - a) * t;
    }
}