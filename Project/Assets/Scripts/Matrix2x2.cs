using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public struct Matrix2x2
    {
        public float m00;
        public float m10;
        public float m01;
        public float m11;
        public Matrix2x2(Vector2 column0, Vector2 column1)
        {
            m00 = column0.x;
            m10 = column0.y;
            m01 = column1.x;
            m11 = column1.y;
        }
        public static Vector2 operator *(Matrix2x2 lhs, Vector2 vector)
        {
            return new Vector2(lhs.m00, lhs.m10) * vector.x + new Vector2(lhs.m01, lhs.m11) * vector.y;
        }
    }
}
