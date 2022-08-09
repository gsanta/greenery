using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace game.scene.grid.path
{
    public class PathFinding
    {
        private const int MoveStraightCost = 10;
        private const int MoveDiagonalCost = 14;

        private readonly GridGraph<PathNode> _grid;
        private List<PathNode> _openList;
        private List<PathNode> _closedList;

        public PathFinding(GridGraph<PathNode> grid)
        {
            _grid = grid;
        }

        public List<Vector2> FindPath(Vector2 startWorldPosition, Vector2 endWorldPosition, out List<PathNode> nodes)
        {
            nodes = new List<PathNode>();
            var start = _grid.GetGridPosition(startWorldPosition);
            var end = _grid.GetGridPosition(endWorldPosition);

            if (!start.HasValue || !end.HasValue)
            {
                return null;
            }

            var startNode = _grid.GetNode(start.Value.x, start.Value.y);
            var endNode = _grid.GetNode(end.Value.x, end.Value.y);

            if (startNode == null || endNode == null)
            {
                return null;
            }
            
            var path = FindPath(startNode, endNode);

            if (path != null)
            {
                nodes = path;
            }

            return path?.Select((node) => _grid.GetWorldPosition(node.X, node.Y)).ToList();
        }

        public List<PathNode> FindPath(PathNode startNode, PathNode endNode)
        {
            _openList = new List<PathNode> { startNode };
            _closedList = new List<PathNode>();
            
            InitPath(startNode, endNode);

            while (_openList.Count > 0)
            {
                var currentNode = GetLowestFCostNode(_openList);
                if (currentNode == endNode)
                {
                    return CalculatePath(endNode);
                }

                _openList.Remove(currentNode);
                _closedList.Add(currentNode);

                UpdateNeighbourCosts(currentNode, endNode);
            }

            return null;
        }

        private void InitPath(PathNode startNode, PathNode endNode)
        {
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
        }

        private void UpdateNeighbourCosts(PathNode currentNode, PathNode endNode)
        {
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

        private List<PathNode> GetNeighbourList(PathNode currentNode)
        {
            var left = _grid.LeftNeighbour(currentNode.X, currentNode.Y);
            var right = _grid.RightNeighbour(currentNode.X, currentNode.Y);
            var top = _grid.TopNeighbour(currentNode.X, currentNode.Y);
            var bottom = _grid.BottomNeighbour(currentNode.X, currentNode.Y);

            return new List<PathNode>() {left, right, top, bottom}.FindAll(node => node != null).ToList();
        }

        private PathNode GetNode(int x, int y)
        {
            return _grid.GetNode(x, y);
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
            var xDistance = Mathf.Abs(a.X - b.X);
            var yDistance = Mathf.Abs(a.Y - b.Y);
            var remaining = Mathf.Abs(xDistance - yDistance);

            return MoveDiagonalCost * Mathf.Min(xDistance, yDistance) + MoveStraightCost * remaining;
        }

        private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
        {
            var lowestFCostNode = pathNodeList[0];
            foreach (var t in pathNodeList)
            {
                if (t.FCost < lowestFCostNode.FCost)
                {
                    lowestFCostNode = t;
                }
            }
            return lowestFCostNode;
        }
    }
}
