
class Node:
    """

    """
    def __init__(self, x, y):
        self.x = x
        self.y = y

        self.distance = 1e9
        self.visited = False
        self.blocked = False

    def __lt__(self, other):
        return self.distance < other.distance