from tkinter import *
from Cell import *
from update_state import *

import time

class Simulation(Canvas):
    def __init__(self, root, rowNumber, columnNumber, cellSize, init_grid, pedestrian, target, distance_matrix, steps, *args,
                 **kwargs):
        Canvas.__init__(self, root, width=cellSize * columnNumber, height=cellSize * rowNumber, *args, **kwargs)

        self.grid = []
        self.steps = steps
        self.row_size = rowNumber
        self.col_size = columnNumber
        self.pedestrian = pedestrian
        self.target = target
        # self.grid = init_grid
        self.distance_matrix = distance_matrix

        for row in range(rowNumber):
            line = []
            for column in range(columnNumber):
                line.append(
                    Cell(self, column, row, cellSize) if init_grid == None else Cell(self, column, row, cellSize,
                                                                                     init_grid[row][column].state))

            self.grid.append(line)

        self.draw()

        self.bind("<Button-1>", self.handleMouseClick)

    def draw(self):
        for row in self.grid:
            for cell in row:
                cell.draw(cell.state)

    def handleMouseClick(self, event):
        for i in range(1, self.steps+1):
            self.update_step()
            print(i)
            time.sleep(0.1)
        
    def update_step(self):
        self.grid, self.pedestrian = update(self.grid, self.pedestrian, self.target, self.distance_matrix)
        self.draw()
        self.update()
        
