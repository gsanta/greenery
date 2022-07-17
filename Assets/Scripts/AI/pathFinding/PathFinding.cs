using System.Collections.Generic;
using System.Linq;
using AI.GridSystem;
using UnityEngine;

namespace AI.pathFinding
{
    public class PathFinding
    {
        private const int MoveStraightCost = 10;
        private const int MoveDiagonalCost = 14;

        private readonly Grid<PathNode> _grid;
        private List<PathNode> _openList;
        private List<PathNode> _closedList;

        public PathFinding(Grid<PathNode> grid)
        {
            _grid = grid;
        }

        public List<PathNode> FindPath(PathNode startNode, PathNode endNode)
        {
            _openList = new List<PathNode> { startNode };
            _closedList = new List<PathNode>();
            
            var pathNodes = _grid.GetAllNodes();
            foreach (var pathNode in pathNodes)
            {
                pathNode.GCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.CameFromNode = null;    
            }

            startNode.GCost = 0;
            startNode.HCost = CalculateDistance(startNode, endNode);
            startNode.CalculateFCost();

            while (_openList.Count > 0)
            {
                var currentNode = GetLowestFCostNode(_openList);
                if (currentNode == endNode)
                {
                    return CalculatePath(endNode);
                }

                _openList.Remove(currentNode);
                _closedList.Add(currentNode);

                foreach (var neighbourNode in GetNeighbourList(currentNode))
                {
                    if (_closedList.Contains(neighbourNode))
                    {
                        continue;
                    }

                    if (!neighbourNode.IsWalkable)
                    {
                        _closedList.Add(neighbourNode);
                        continue;
                    }

                    var tentativeGCost = currentNode.GCost + CalculateDistance(currentNode, neighbourNode);

                    if (tentativeGCost < neighbourNode.GCost)
                    {
                        neighbourNode.CameFromNode = currentNode;
                        neighbourNode.GCost = tentativeGCost;
                        neighbourNode.HCost = CalculateDistance(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        if (!_openList.Contains(neighbourNode))
                        {
                            _openList.Add(neighbourNode);
                        }
                    }
                }
            }

            return null;
        }

        private List<PathNode> GetNeighbourList(PathNode currentNode)
        {
            List<PathNode> neighbourList = new List<PathNode>();

            var left = _grid.LeftNeighbour(currentNode.X, currentNode.Y);
            var right = _grid.LeftNeighbour(currentNode.X, currentNode.Y);
            var top = _grid.LeftNeighbour(currentNode.X, currentNode.Y);
            var bottom = _grid.LeftNeighbour(currentNode.X, currentNode.Y);

            return new List<PathNode>() {left, right, top, bottom}.FindAll(node => node != null).ToList();
        }

        private PathNode GetNode(int x, int y)
        {
            return _grid.GetGridObject(x, y);
        }

        private List<PathNode> CalculatePath(PathNode endNode)
        {
            List<PathNode> path = new List<PathNode>();
            path.Add(endNode);
            PathNode currentNode = endNode;

            while (currentNode.CameFromNode != null)
            {
                path.Add(currentNode.CameFromNode);
                currentNode = currentNode.CameFromNode;
            }
            path.Reverse();
            return path;
        }

        private int CalculateDistance(PathNode a, PathNode b)
        {
            int xDistance = Mathf.Abs(a.X - b.X);
            int yDistance = Mathf.Abs(a.Y - b.Y);
            int remaining = Mathf.Abs(xDistance - yDistance);

            return MoveDiagonalCost * Mathf.Min(xDistance, yDistance) + MoveStraightCost * remaining;
        }

        private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
        {
            PathNode lowestFCostNode = pathNodeList[0];
            for (int i = 0; i < pathNodeList.Count; i++)
            {
                if (pathNodeList[i].FCost < lowestFCostNode.FCost)
                {
                    lowestFCostNode = pathNodeList[i];
                }
            }
            return lowestFCostNode;
        }
    }
}
