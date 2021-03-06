﻿/*
 * File: Vector3.cs
 * Date: 21.2.2018,
 *
 * MIT License
 *
 * Copyright (c) 2018 JustAnotherVoiceChat
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace JustAnotherVoiceChat.Server.Wrapper.Math
{
    public class Vector3
    {

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        private const float EqualityThreshold = 0.01f;

        public Vector3()
        {
            X = 0f;
            Y = 0f;
            Z = 0f;
        }

        public Vector3(float coordinate)
        {
            X = coordinate;
            Y = coordinate;
            Z = coordinate;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static double Distance(Vector3 vector1, Vector3 vector2)
        {
            return (vector1 - vector2).Length();
        }

        public double Distance(Vector3 vector)
        {
            return Distance(this, vector);
        }

        public double Length()
        {
            return System.Math.Sqrt(
                X * X +
                Y * Y +
                Z * Z
            );
        }

        public override string ToString()
        {
            return $"[X: {X}, Y: {Y}, Z: {Z}]";
        }

        public override bool Equals(object obj)
        {
            var vector3 = obj as Vector3;
            if(vector3 != null)
                return this == vector3;
            return false;
        }

        public override int GetHashCode()
        {
            return (int) (X + Y + Z);
        }

        public static bool operator ==(Vector3 left, Vector3 right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }
            
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            return System.Math.Abs(left.X - right.X) < EqualityThreshold &&
                   System.Math.Abs(left.Y - right.Y) < EqualityThreshold &&
                   System.Math.Abs(left.Z - right.Z) < EqualityThreshold;
        }

        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return !(left == right);
        }

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        public static Vector3 operator *(Vector3 left, float right)
        {
            return new Vector3(left.X * right, left.Y * right, left.Z * right);
        }

        public static Vector3 operator /(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
        }

        public static Vector3 operator /(Vector3 left, float right)
        {
            return new Vector3(left.X / right, left.Y / right, left.Z / right);
        }

    }
}
