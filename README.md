### Pathfinding implementation in Unity.

## Search Algorithm: Dijkstra

Elapsed Time: 40.5 sec
Distance Cost: 48.2m

Brief: Account for terrain cost. Before searching frontier nodes it prioritizes the nodes based on their weight and gives you the shortest path, 
gives us the balance between performance and time cost. Combining this algorithm with heuristics similar to greedy BFS make up A*.


## Search Algorithm: Greedy Breadth-First Search

Elapsed Time: 2.35 sec
Distance Cost: 84m

Brief: Based on Breadth-first search. Perform heuristics and go for nodes that have less distance from the goal node.
Which makes this algorithm very efficient regarding time and Performance but again doesn't account for Terrain cost.


## Search Algorithm: A-Star
Elapsed Time: 18.7 sec
Distance Cost: 48.2m

Brief: Industry-standard A* pathfinding, based on Dijkstra's algorithm and utilize Heuristics similar to Greedy BFS.
Guarantee the shortest path maintains a balance between time and distance cost.


## Search Algorithm: A-Star
Elapsed Time: 18.7 sec
Distance Cost: 48.2m

Brief: Industry-standard A* pathfinding, based on Dijkstra's algorithm and utilize Heuristics similar to Greedy BFS.
Guarantee the shortest path maintains a balance between time and distance cost.
