using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Math
{
    public static class V2Plus
    {
        /// <summary>
        /// Will return point either inside box or on edge. If outside, will
        /// return closest point on edge of box.
        /// </summary>
        public static Vector2 ClampToBox(Vector2 point, Vector2 corner1, Vector2 corner2)
        {
            Vector2 center = Vector2.Lerp(corner1, corner2, 0.5f);
            float halfWidth = Mathf.Abs(corner1.x - corner2.x) / 2f;
            float halfHeight = Mathf.Abs(corner1.y - corner2.y) / 2f;

            float clampedX = Mathf.Clamp(point.x, center.x - halfWidth, center.x + halfWidth);
            float clampedY = Mathf.Clamp(point.y, center.y - halfHeight, center.y + halfHeight);

            Vector2 clamped = new Vector2(clampedX, clampedY);

            return clamped;
        }

        /// <summary>
        ///  Will clamp point to box formed by two corners
        /// </summary>
        /// <returns>True if point is within box already</returns>
        public static bool ClampToBox(ref Vector2 point, Vector2 corner1, Vector2 corner2) 
        {
            Vector2 clamped = V2Plus.ClampToBox(point, corner1, corner2);
            if (clamped == point) return true;
            else
            {
                point = clamped;
                return false;
            }
        }

    }
}
