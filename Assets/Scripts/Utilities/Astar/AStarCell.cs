using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AStarPathFinding
{
    /// <summary>
    /// A* node class.
    /// </summary>
    public class AStarCell
    {
        public readonly int x;
        public readonly int y;
        public float Cost;

        public AStarCell(int x, int y, float costOfCell)
        {
            this.x = x;
            this.y = y;
            Cost = costOfCell;
        }

        public bool ContainsPoint(float x, float y)
        {
            return (x >= this.x) && (y >= this.y) && (x <= this.x + 1) && (y <= this.y + 1);
        }

        public bool ContainsPoint(Vector2 point)
        {
            return ContainsPoint(point.x, point.y);
        }

        public bool ContainsPointWithoutBoard(float x, float y)
        {
            return (x > this.x) && (y > this.y) && (x < this.x + 1) && (y < this.y + 1);
        }

        public bool ContainsPointWithoutBoard(Vector2 point)
        {
            return ContainsPointWithoutBoard(point.x, point.y);
        }

        public Vector2 GetBottomLeft()
        {
            return new Vector2 (x, y);
        }

        public Vector2 GetBottomRight()
        {
            return new Vector2(x, y + 1);
        }

        public Vector2 GetTopLeft()
        {
            return new Vector2 (x + 1, y);
        }

        public Vector2 GetTopRight()
        {
            return new Vector2 (x + 1, y + 1);
        }

        public Vector2 GetCenter()
        {
            return new Vector2 (x + 0.5f, y + 0.5f);
        }
    }

    public enum AStarCellType
    {
        Normal,
        Block
    }
}