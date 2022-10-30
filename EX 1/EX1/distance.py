import numpy as np
import heapq
from scipy.spatial import distance
from typing import Union

class Node:
    def __init__(self, x, y):
        self.x = x
        self.y = y

        self.distance = 1e9
        self.visited = False
        self.blocked = False

    def __lt__(self, other):
        return self.distance < other.distance




class Distance:

    # Horizontal and VerticalDistance
    hVDistance = 1.0
    # Diagonal Distance
    dDistance = 1.4
    
    def __init__(self, method):
        self.method = method
        
    def get_distance(self, grid, targets):
        
        h, w = len(grid), len(grid[0])
        
        # Convert grid to boolean matrix with obstacles = 0
        matrix = np.ones((h, w), dtype=int)
        for i in range(h):
            for j in range(w):
                if grid[i][j].state == "obstacle":
                    matrix[i][j] = 0
                    
        # Check if input is single target or mutliple
        if isinstance(targets[0], int):
            targets = [targets]
            
        targets = np.array(targets, dtype=int)
                            
        if self.method == "euclidean":
            return self.euclidean(matrix, targets)
        elif self.method == "dijkstra":
            return self.dijkstra(matrix, targets)
        else:
            raise NotImplementedError()
        
    
    
    def euclidean(self, matrix, targets):
        """
        Euclidean distance for grid
                
        matrix: boolean matrix of size (n, n). 0 = Blocked, 1 = Not blocked
        targets : Array of tuples (i, j) with coordinates of each target
        return: matrix of size (n, n) with distances from each grid location to the closest target
        """

        size = len(matrix)
        distance_matrix = np.ones((size,size)) * np.inf

        for i in range(size):
            for j in range(size):
                if matrix[i][j] == 0:
                    continue
                for tar in targets:
                    
                    distance_matrix[i,j] = min(distance_matrix[i,j], distance.euclidean([i,j],tar))
                    
        return distance_matrix

    

    def dijkstra(self, matrix, targets):
        """
        Dijkstra for grid
        
        TODO : Update for multiple targets
        
        matrix: boolean matrix of size (n, n). 0 = Blocked, 1 = Not blocked
        si: x coordinate of target
        sj : y coordinate of target
        return: matrix of size (n, n) with distances from each grid location to target (si, sj)
        """
        
        size = len(matrix)
        gridArea = [[None for i in range(size)] for j in range(size)]
        
        # TODO : Only implemented for one target. Extend to multiple targets
        si, sj = targets[0]

        # Creating nodes and finding blocked cells in matrix and mapping accordingly to our grid
        for i in range(size):
            for j in range(size):
                gridArea[i][j] = Node(i, j)
                if matrix[i][j] == False:
                    gridArea[i][j].blocked = True

        # setting start distance to 0.
        # All other nodes will have infinity distance at the beginning
        start = gridArea[si][sj]
        start.distance = 0

        queueB = []
        heapq.heappush(queueB, start)
  

        while len(queueB) > 0:
            current = heapq.heappop(queueB)

            # Top
            if current.x - 1 >= 0:
                
                # Top Top
                t = gridArea[current.x - 1][current.y]

                if not t.visited and not t.blocked and t.distance > current.distance + Distance.hVDistance:
                    t.distance = current.distance + Distance.hVDistance
                    heapq.heappush(queueB, t)

                # Top Left
                if current.y - 1 > 0:
                    t = gridArea[current.x - 1][current.y - 1]
                    if not t.visited and not t.blocked and t.distance > current.distance + Distance.dDistance:
                        t.distance = current.distance + Distance.dDistance
                        heapq.heappush(queueB, t)

                # Top Right
                if current.y + 1 < size:
                    t = gridArea[current.x - 1][current.y + 1]
                    if not t.visited and not t.blocked and t.distance > current.distance + Distance.dDistance:
                        t.distance = current.distance + Distance.dDistance
                        heapq.heappush(queueB, t)

            # Left
            if current.y - 1 > 0:
                t = gridArea[current.x][current.y - 1]
                if not t.visited and not t.blocked and t.distance > current.distance + Distance.hVDistance:
                    t.distance = current.distance + Distance.hVDistance
                    heapq.heappush(queueB, t)

            # Right
            if current.y + 1 < size:
                t = gridArea[current.x][current.y + 1]
                if not t.visited and not t.blocked and t.distance > current.distance + Distance.hVDistance:
                    t.distance = current.distance + Distance.hVDistance
                    heapq.heappush(queueB, t)

            # Down
            if current.x + 1 < size:

                # Down Down
                t = gridArea[current.x + 1][current.y]

                if not t.visited and not t.blocked and t.distance > current.distance + Distance.hVDistance:
                    t.distance = current.distance + Distance.hVDistance
                    heapq.heappush(queueB, t)

                # Down Left
                if current.y - 1 >= 0:
                    t = gridArea[current.x + 1][current.y - 1]
                    if not t.visited and not t.blocked and t.distance > current.distance + Distance.dDistance:
                        t.distance = current.distance + Distance.dDistance
                        heapq.heappush(queueB, t)

                # Down Right
                if current.y + 1 < size:
                    t = gridArea[current.x + 1][current.y + 1]
                    if not t.visited and not t.blocked and t.distance > current.distance + Distance.dDistance:
                        t.distance = current.distance + Distance.dDistance
                        heapq.heappush(queueB, t)

            current.visited = True
            
        distance_matrix = np.array([[i.distance for i in j] for j in gridArea])

        return distance_matrix


