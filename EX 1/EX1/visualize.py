from PIL import Image

def target_grid_to_image(target_distance_grids):
    """
    Creates a colored image based on the distance to the target stored in
    self.target_distance_gids.
    :param canvas: the canvas that holds the image.
    :param old_image_id: the id of the old grid image.
    """
    size = len(target_distance_grids)
    im = Image.new(mode="RGB", size=(size, size))
    pix = im.load()
    for x in range(size):
        for y in range(size):
            target_distance = min(target_distance_grids[x][y], 1e8)
            pix[y, x] = (max(0, min(255, int(10 * target_distance) - 0 * 255)),
                         max(0, min(255, int(10 * target_distance) - 1 * 255)),
                         max(0, min(255, int(10 * target_distance) - 2 * 255)))
            
    
    im = im.resize((1000, 1000), Image.NONE)
    im.show()
    