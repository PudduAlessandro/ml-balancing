using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarBotV2 : MonoBehaviour
{
    public Tilemap tilemap; // Needed for setting up the grid of nodes
    public string targetTileName = ""; // What tile the bot is currently looking for
    public Vector3Int currentPos; // Where is the bot currently?
    public Vector3Int targetPos;
    public bool lockCurrentTargetType; // Don't change the tile until it reached its target 

    private Grid grid; // idk
    public Queue<Vector3Int> path;
    private List<Vector3Int> unreachableTiles; // Path to target
    
    // Compress tilemap bounds to actual tiles and get grid component
    public void SetupBot()
    {
        unreachableTiles = new List<Vector3Int>();
        tilemap.CompressBounds();
        grid = tilemap.transform.parent.GetComponent<Grid>();
    }

    // Find Path to closest tile of a certain type
    public Queue<Vector3Int> FindPath()
    {
        int loopCount = 0;
        
        do
        {
            Vector3Int targetTilePosition = FindClosestTargetTilePos();
        
            // Check if required tile is at the current position
            if (targetTilePosition == currentPos)
            {
                // Move one step in a random direction before returning to the current tile
                currentPos = RandomStep(targetTilePosition);
            }
            
            path = FindPath(currentPos, targetTilePosition);
            
            // If path returns null -> target tile probably unreachable -> add it to unreachable tiles list -> try pathfinding again with new tile
            if (path == null)
            {
                unreachableTiles.Add(targetTilePosition);
            }

            loopCount++;
        } while (path == null && loopCount < 36);

        if (loopCount >= 36)
        {
            //Debug.Log("Emergency break!");
        }
        
        if(path.Peek() == currentPos) path.Dequeue();
        return path;
    }

    // Find the closest tile of the needed type to the currentPosition 
    private Vector3Int FindClosestTargetTilePos()
    {
        Vector3Int startPosition = currentPos; // Start of the path
        Vector3Int closestTargetTile = Vector3Int.forward;
        float closestActualDistance = float.MaxValue; // Distance to target tile which will be reduced until shortest is found 

        // Check all tiles within tilemap
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if(unreachableTiles.Contains(pos)) continue;
            
            // Check if the current tile has the needed type
            if (tilemap.GetTile(pos).name.Contains(targetTileName) && !tilemap.GetTile(pos).name.Contains("Used") && pos != startPosition)
            {
                // Get distance from currentPosition to current target
                float distance = Vector3Int.Distance(startPosition, pos);

                // Check if the current distance is longer than the new distance
                if (distance < closestActualDistance)
                {
                    // Update target and distance to closest tile
                    closestActualDistance = distance;
                    closestTargetTile = pos;
                    targetPos = pos;
                }
            }
        }
        
        //Debug.Log($"Found {targetTileName} tile at {closestTargetTile}");
        return closestTargetTile;
    }
    
    private Queue<Vector3Int> FindPath(Vector3Int startPosition, Vector3Int targetTilePosition)
    {
        HashSet<Vector3Int> closedSet = new HashSet<Vector3Int>();
        HashSet<Vector3Int> openSet = new HashSet<Vector3Int>() { startPosition };
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        Dictionary<Vector3Int, float> gScore = new Dictionary<Vector3Int, float> { { startPosition, 0 } };
        Dictionary<Vector3Int, float> fScore = new Dictionary<Vector3Int, float> { { startPosition, HeuristicCostEstimate(startPosition, targetTilePosition) } };

        while (openSet.Count > 0)
        {
            Vector3Int current = GetLowestFScore(openSet, fScore);
            if (current == targetTilePosition)
                return ReconstructPath(cameFrom, targetTilePosition);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Vector3Int neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor) || !IsTileWalkable(neighbor, tilemap))
                    continue;
                
                float tentativeGScore = gScore[current] + 1; // Assuming all tile costs are the same (1)
                if (!openSet.Contains(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, targetTilePosition);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        if (targetTileName.Equals("Food"))
        {
            targetTileName = "WAdjacent";
        }
        
        return null;
    }
    
    static float HeuristicCostEstimate(Vector3Int start, Vector3Int goal)
    {
        return Vector3Int.Distance(start, goal);
    }

    static Vector3Int GetLowestFScore(HashSet<Vector3Int> openSet, Dictionary<Vector3Int, float> fScore)
    {
        float lowestFScore = float.MaxValue;
        Vector3Int lowestFScoreNode = Vector3Int.zero;

        foreach (Vector3Int node in openSet)
        {
            if (fScore.ContainsKey(node) && fScore[node] < lowestFScore)
            {
                lowestFScore = fScore[node];
                lowestFScoreNode = node;
            }
        }

        return lowestFScoreNode;
    }
    
    static Queue<Vector3Int> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current)
    {
        Queue<Vector3Int> path = new Queue<Vector3Int>();
        path.Enqueue(current);

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Enqueue(current);
        }

        Queue<Vector3Int> reversedPath= new Queue<Vector3Int>(path.Reverse());

        return new Queue<Vector3Int>(reversedPath);
    }
    
    static List<Vector3Int> GetNeighbors(Vector3Int current)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>
        {
            current + new Vector3Int(1, 0, 0), // Right
            current + new Vector3Int(-1, 0, 0), // Left
            current + new Vector3Int(0, 1, 0), // Up
            current + new Vector3Int(0, -1, 0) // Down
        };

        return neighbors;
    }
    
    static bool IsTileWalkable(Vector3Int position, Tilemap tilemap)
    {
        if (tilemap.GetTile(position) == null) return false;
        
        return !tilemap.GetTile(position).name.Contains("Wall") && !tilemap.GetTile(position).name.Contains("Water");
    }
    
    private Vector3Int RandomStep(Vector3Int targetTilePosition)
    {
        var possibleSteps = new List<Vector3Int>();
        
        foreach (var pos in GetNeighbors(targetTilePosition))
        {
            var tileName = tilemap.GetTile(pos).name;
            if(tileName.Contains("Wall") || tileName.Contains("Water")) continue;
            
            possibleSteps.Add(pos);
        }

        var rand = Random.Range(0, possibleSteps.Count - 1);

        return possibleSteps[rand];
    }
}
