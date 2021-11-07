# Pathfinding implementation in Unity.

<img align="left" alt="Zeeshan Qaswar" width="600px" src="https://user-images.githubusercontent.com/7692061/140657159-210f2ad2-4136-4ab3-92c3-08dde70505d0.gif" />
<br />
## Search Algorithm: Dijkstra

Elapsed Time: 40.5 sec
Distance Cost: 48.2m

Brief: Account for terrain cost. Before searching frontier nodes it prioritizes the nodes based on their weight and gives you the shortest path, 
gives us the balance between performance and time cost. Combining this algorithm with heuristics similar to greedy BFS make up A*.

<img align="left" alt="Zeeshan Qaswar" width="600px" src="https://user-images.githubusercontent.com/7692061/140657147-1f7d9567-d4d8-4161-a56b-5b9b5d22070b.gif" />
<br />
## Search Algorithm: Greedy Breadth-First Search

Elapsed Time: 2.35 sec
Distance Cost: 84m

Brief: Based on Breadth-first search. Perform heuristics and go for nodes that have less distance from the goal node.
Which makes this algorithm very efficient regarding time and Performance but again doesn't account for Terrain cost.

<img align="left" alt="Zeeshan Qaswar" width="600px" src="https://user-images.githubusercontent.com/7692061/140657067-278ddde7-8401-4d49-b10d-42150309a278.gif" />
<br />
## Search Algorithm: A-Star
Elapsed Time: 18.7 sec
Distance Cost: 48.2m

Brief: Industry-standard A* pathfinding, based on Dijkstra's algorithm and utilize Heuristics similar to Greedy BFS.
Guarantee the shortest path maintains a balance between time and distance cost.

<img align="left" alt="Zeeshan Qaswar" width="600px" src="https://user-images.githubusercontent.com/7692061/140657098-8e6884bd-d062-4954-a887-c3dc07acf4f5.gif" />
<br />
## Search Algorithm: Breadth-First Search
Elapsed Time: 42.1 sec
Distance Cost: 88.6m

Brief: Basic search algorithm, doesn't account for terrain cost. Give us a path but don't guarantee the shortest path.
Don't use any heuristics and search all the neighbor nodes so costly in terms of time and performance.
