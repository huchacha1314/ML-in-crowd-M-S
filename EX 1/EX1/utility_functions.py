import numpy as np

def utility(pedestrian_loc, distance_matrix):
    """
    :param pedestrian_loc: coordinates of pedestrian
    :param distance_matrix: matrix with distance to closest target
    :return: utility value scalar indicative of how favourable pedestrian location is to targets
    """
    x, y = pedestrian_loc
    grid_size = len(distance_matrix)
    r_2 = distance_matrix[x,y]**2
    r_max2 = 2 * grid_size**2

    return np.exp(1.0/(r_2-r_max2)) if r_2<r_max2 else 0

def check_grid_bound(location, grid_size_x, grid_size_y):
    """
    :param location: tuple of coordinate values (x, y) to be checked whether it lies inside the grid or not
    :param grid_size_x: Size of Grid in x direction (Number of Columns)
    :param grid_size_y: Size of Grid in y direction (Number of Rows)
    :return: bool value indicating whether cell lies inside (true) or not (false)
    """
    if location[0] <= 0 or location[0] > grid_size_x or location[1] <= 0 or location[1] > grid_size_y:
        return False
    return True