class Cell():
    """
    Class which represents a Cell in the grid
    """
    EMPTY_COLOR_BG = "white"
    EMPTY_COLOR_BORDER = "black"

    COLOR_DICT = {'empty': 'white', 'pedestrian': 'red', 'target': 'yellow', 'obstacle': 'violet'}
    CELL_ENUM = {'empty': 0, 'pedestrian': 1, 'target': 2, 'obstacle': 3}
    CELL_STATE = ['empty', 'pedestrian', 'target', 'obstacle']

    def __init__(self, master, x, y, size, init_cell_state=None):
        """ Constructor of the object called by Cell(...) """

        self.master = master
        self.x = x
        self.y = y
        self.size = size

        if init_cell_state == None:
            self.enum_val = 0

        else:
            self.enum_val = Cell.CELL_ENUM[init_cell_state]

        self.state = Cell.CELL_STATE[self.enum_val]

    def _switch(self):
        """ Toggle Cell State """

        self.enum_val += 1

    def draw(self, state=None):
        """ order to the cell to draw its representation on the canvas """
        #
        if self.master != None:
            outline = Cell.EMPTY_COLOR_BORDER
            if state == None:

                self.state = Cell.CELL_STATE[self.enum_val % len(Cell.CELL_STATE)]
            else:
                self.state = state
            fill = Cell.COLOR_DICT[self.state]
            outline = Cell.EMPTY_COLOR_BORDER

            xmin = self.x * self.size
            xmax = xmin + self.size
            ymin = self.y * self.size
            ymax = ymin + self.size

            self.master.create_rectangle(xmin, ymin, xmax, ymax, fill=fill, outline=outline)