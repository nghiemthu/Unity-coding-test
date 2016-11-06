# Unity-coding-test
This question has several parts, do them in order: 

part 1 ) Make a square grid in unity, 50 wide by 50 high (ideally generated in code). Make it so that a click or tap on a a grid cell adds it to the user's selection, with a maximum of 2 selected points. The selected cells should be clearly visualized The oldest point in the list should be removed when a third point is selected. 

part 2 ) When two cells are selected, visualize the shortest route between the two points. The grid cells have a Von Neumann neighborhood, so no diagonals. The visualization can be whatever, e.g. a small sphere on each point in the path.

part 3 ) Change the application so that on initialization, each grid cell is given a random height value between 0 and 127. Use the weight to set the cells color. When two points are selected display the path cost for the route in a UI label, where going uphill costs (e.g. height 24 ­> 54 has a cost of 30) but going downhill is free (e.g. 54­>24 has 0 cost)

part 4) Alter the shortest route finding algorithm from part 2, to find the route between the two selected cells that minimizes climbing as defined in part 3. Imagine this is for a game, so it has to be able to find a route within several milliseconds on an average mobile device
