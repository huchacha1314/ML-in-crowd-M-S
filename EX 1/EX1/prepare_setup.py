from distance import *


def prepare_setup(grid):
    
    pedestrians = []
    targets = []
    for row in range(len(grid.grid)):
        for col in range(len(grid.grid[0])):
            if grid.grid[row][col].state == 'pedestrian':
                pedestrians.append([row, col])
            elif grid.grid[row][col].state == 'target':
                targets.append([row, col])

    # Calculate distance matrix
    dist = Distance("dijkstra")
    distance_matrix = dist.get_distance(grid.grid, targets)
    return grid, pedestrians, targets, distance_matrix
