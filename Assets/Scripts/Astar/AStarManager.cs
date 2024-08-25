using SelfCollections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace AStarPathFinding
{
    public class AStarManager
    {
        private static AStarManager instance;

        public static AStarManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AStarManager();
                }
                return instance;
            }
        }

        private int startX;
        private int startY;

        private int mapWidth;
        private int mapHeight;

        public AStarCell[,] cells;

        public void InitMap(int startX, int endX, int startY, int endY)
        {
            this.startX = startX;
            this.startY = startY;

            mapWidth = endX - startX;
            mapHeight = endY - startY;

            cells = new AStarCell[mapWidth, mapHeight];

            // test
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    AStarCell cell = new AStarCell(i, j, 0);
                    cells[i, j] = cell;
                }
            }
        }

        public List<AStarCell> GetPath(Vector2Int startPos, Vector2Int endPos)
        {
            PriorityQueue<AStarCell, float> frontier = new PriorityQueue<AStarCell, float>();

            AStarCell start = GetCell(startPos);

            frontier.Enqueue(start, 0);

            Dictionary<AStarCell, AStarCell> cameFrom = new Dictionary<AStarCell, AStarCell>();
            Dictionary<AStarCell, float> costSoFar = new Dictionary<AStarCell, float>();
            cameFrom[start] = null;
            costSoFar[start] = 0;

            AStarCell current = null;

            while(frontier.Count != 0)
            {
                current = frontier.Dequeue();
                if(current == GetCell(endPos))
                {
                    break;
                }
                foreach (AStarCell next in GetNeighbours(current))
                {
                    float newCost = costSoFar[current] + Cost(current, next);
                    if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        float priority = newCost + Cost(next, GetCell(endPos));
                        frontier.Enqueue(next, priority);
                        cameFrom[next] = current;
                    }
                }
            }

            List<AStarCell> list = new List<AStarCell>();

            if (current == GetCell(endPos))
            {
                while (cameFrom[current] != null)
                {
                    list.Add(current);
                    current = cameFrom[current];
                }
            }

            return list;
        }

        public Vector2? GetNext(Vector2Int startPos, Vector2Int endPos)
        {

            List<AStarCell> cell = GetPath(startPos, endPos);
            if (cell.Count == 0)
            {
                return null;
            }
            return cell.Last().GetCenter() - new Vector2(startX, startY);
        }

        private AStarCell GetCell(Vector2Int pos)
        {
            return GetCell(pos.x, pos.y);
        }

        private AStarCell GetCell(int x, int y)
        {
            return cells[x - startX, y - startY];
        }

        private AStarCell GetCellAbs(Vector2Int pos)
        {
            return GetCellAbs(pos.x, pos.y);
        }

        private AStarCell GetCellAbs(int x, int y)
        {
            return cells[x, y];
        }

        private List<AStarCell> GetNeighbours(AStarCell cell)
        {
            List<AStarCell> neighbours = new List<AStarCell>();
            int[,] board = new int[8, 2] { { -1, 1 }, { 0, 1 }, { 1, 1 }, { -1, 0 }, { 1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 } };
            for (int i = 0; i < 8; i++)
            {
                if (cell.x + board[i, 0] < 0 || cell.x + board[i, 0] > mapWidth || cell.y + board[i, 1] < 0 || cell.y + board[i, 1] > mapHeight)
                {
                    continue;
                }
                neighbours.Add(GetCellAbs(cell.x + board[i, 0], cell.y + board[i, 1]));
            }
            return neighbours;
        }

        /// <summary>
        /// ����֮�侭�����и��ӵ�cost
        /// </summary>
        /// <param name="from">�������</param>
        /// <param name="to">�յ�����</param>
        /// <returns>cost</returns>
        private float Cost(Vector2 from, Vector2 to)
        {
            float cost = 0;
            Vector2Int fromInt = new Vector2Int((int)from.x, (int)from.y);
            Vector2Int toInt = new Vector2Int((int)to.x, (int)to.y);
            List<Vector2Int> list = Grid.GetTouchedPosBetweenTwoPoints(fromInt, toInt);
            foreach (Vector2Int item in list)
            {
                // Ӧ���ų�����񣨣������˴������ˣ���Ϊ������λ�õ�cost��0
                cost += GetCellAbs(item).CostHere;
            }
            return cost + Distance(from, to);
        }

        public float Cost(AStarCell start, AStarCell end)
        {
            Vector2 startPos = new Vector2(start.x, start.y);
            Vector2 endPos = new Vector2(end.x, end.y);
            return Cost(startPos, endPos);
        }

        public float Distance(Vector2 a, Vector2 b)
        {
            return (a - b).magnitude;
        }
    }

    // Class from https://blog.csdn.net/Vitens/article/details/107588013
    public static class Grid
    {
        public static List<Vector2Int> GetTouchedPosBetweenTwoPoints(Vector2Int from, Vector2Int to)
        {
            List<Vector2Int> touchedGrids = GetTouchedPosBetweenOrigin2Target(to - from);
            touchedGrids.Offset(from);
            return touchedGrids;
        }

        /// <summary>
        /// ����Ŀ��λ�õ�ԭ���������ĸ���
        /// </summary>
        static List<Vector2Int> GetTouchedPosBetweenOrigin2Target(Vector2Int target)
        {
            List<Vector2Int> touched = new List<Vector2Int>();
            bool steep = Mathf.Abs(target.y) > Mathf.Abs(target.x);
            int x = steep ? target.y : target.x;
            int y = steep ? target.x : target.y;

            //б��
            float tangent = (float)y / x;

            float delta = x > 0 ? 0.5f : -0.5f;

            for (int i = 1; i < 2 * Mathf.Abs(x); i++)
            {
                float tempX = i * delta;
                float tempY = tangent * tempX;
                bool isOnEdge = Mathf.Abs(tempY - Mathf.FloorToInt(tempY)) == 0.5f;

                //ż�� �����ڲ��ж�
                if ((i & 1) == 0)
                {
                    //�ڱ�Ե,�������������Ӷ���������
                    if (isOnEdge)
                    {
                        touched.AddUnique(new Vector2Int(Mathf.RoundToInt(tempX), Mathf.CeilToInt(tempY)));
                        touched.AddUnique(new Vector2Int(Mathf.RoundToInt(tempX), Mathf.FloorToInt(tempY)));
                    }
                    //���ڱ�Ե������������������
                    else
                    {
                        touched.AddUnique(new Vector2Int(Mathf.RoundToInt(tempX), Mathf.RoundToInt(tempY)));
                    }
                }

                //���� ���ӱ�Ե�ж�
                else
                {
                    //�ڸ��ӽ��㴦,����Ϊ�赲,����
                    if (isOnEdge)
                    {
                        continue;
                    }
                    //��������������������
                    else
                    {
                        touched.AddUnique(new Vector2Int(Mathf.CeilToInt(tempX), Mathf.RoundToInt(tempY)));
                        touched.AddUnique(new Vector2Int(Mathf.FloorToInt(tempX), Mathf.RoundToInt(tempY)));
                    }
                }
            }

            if (steep)
            {
                //����ת ���� X Y
                for (int i = 0; i < touched.Count; i++)
                {
                    Vector2Int v = touched[i];
                    v.x = v.x ^ v.y;
                    v.y = v.x ^ v.y;
                    v.x = v.x ^ v.y;

                    touched[i] = v;
                }
            }
            touched.Except(new List<Vector2Int>() { Vector2Int.zero, target });

            return touched;
        }

        //���Ԫ��(����Ѿ���������Ҫ�ظ����)
        public static void AddUnique(this List<Vector2Int> self, Vector2Int other)
        {
            if (!self.Contains(other))
            {
                self.Add(other);
            }
        }

        //���Ԫ��(����Ѿ���������Ҫ�ظ����)
        public static void AddUnique(this List<Vector2Int> self, List<Vector2Int> others)
        {
            if (others == null)
                return;

            for (int i = 0; i < others.Count; i++)
            {
                if (!self.Contains(others[i]))
                {
                    self.Add(others[i]);
                }
            }
        }

        //ƫ��
        public static void Offset(this List<Vector2Int> self, Vector2Int offset)
        {
            for (int i = 0; i < self.Count; i++)
            {
                self[i] += offset;
            }
        }

        //�Ƴ�����
        public static void Except(this List<Vector2Int> self, List<Vector2Int> other)
        {
            if (other == null)
                return;

            for (int i = 0; i < other.Count; i++)
            {
                if (self.Contains(other[i]))
                {
                    self.Remove(other[i]);
                }
            }
        }
    }
}