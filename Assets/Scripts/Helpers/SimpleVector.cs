using System;
using UnityEngine;

namespace Helpers
{
    [Serializable]
    public class SimpleVector
    {
        public float X;
        public float Y;
        public float SqrMagnitude => (X * X + Y * Y);

        public static SimpleVector Zero => new(0f, 0f);
        public static SimpleVector One => new(1f, 1f);
        public static SimpleVector Up => new(0f, 1f);
        public static SimpleVector Down => new(0f, -1f);
        public static SimpleVector Left => new(-1f, 0f);
        public static SimpleVector Right => new(1f, 0f);

        public static SimpleVector operator +(SimpleVector a) => a;
        public static SimpleVector operator -(SimpleVector a)
        {
            var value = -a.ToVector2();
            return new SimpleVector(value.x, value.y);
        }

        public static SimpleVector operator +(SimpleVector a, SimpleVector b)
        {
            var value = a.ToVector2() + b.ToVector2();
            return new SimpleVector(value.x + value.x, value.y + value.y);
        }

        public static SimpleVector operator -(SimpleVector a, SimpleVector b)
        {
            var value = a.ToVector2() - b.ToVector2();
            return new SimpleVector(value.x + value.x, value.y + value.y);
        }

        public static SimpleVector operator *(SimpleVector a, SimpleVector b)
        {
            var value = a.ToVector2() * b.ToVector2();
            return new SimpleVector(value.x + value.x, value.y + value.y);
        }

        public static SimpleVector operator /(SimpleVector a, SimpleVector b)
        {
            var value = a.ToVector2() / b.ToVector2();
            return new SimpleVector(value.x + value.x, value.y + value.y);
        }

        public SimpleVector(){}

        public SimpleVector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public SimpleVector(Vector2 vector)
        {
            X = vector.x;
            Y = vector.y;
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }

        public static float SignedAngle(SimpleVector from, SimpleVector to)
        {
            float num = Angle(from, to);
            float num2 = Sign(from.X * to.Y - from.Y * to.X);
            return num * num2;
        }

        public static float Angle(SimpleVector from, SimpleVector to)
        {
            float num = (float)Math.Sqrt(from.SqrMagnitude * to.SqrMagnitude);
            if (num < 1E-15f)
            {
                return 0f;
            }

            float num2 = Clamp(Dot(from, to) / num, -1f, 1f);
            return (float)Math.Acos(num2) * 57.29578f;
        }

        public static float Distance(SimpleVector a, SimpleVector b)
        {
            float num = a.X - b.X;
            float num2 = a.Y - b.Y;
            return (float)Math.Sqrt(num * num + num2 * num2);
        }

        private static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }

            return value;
        }

        private static float Dot(SimpleVector lhs, SimpleVector rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y;
        }

        private static float Sign(float f)
        {
            return (f >= 0f) ? 1f : (-1f);
        }
    }
}
