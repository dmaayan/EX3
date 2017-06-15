(function ($) {
	$.fn.mazeBoard = function (
		mazeData, // the matrix containing the maze cells
		startRow, startCol, // initial position of the player
		exitRow, exitCol, // the exit position
		playerImage, // player's icon (of type Image)
		exitImage, // exit's icon (of type Image)
		enabled, // is the board enabled (i.e., player can move)
		callback) {
		this.each(function (index, elem) {
			var canvas = elem;
			var context = canvas.getContext("2d");
			var cellWidth = canvas.width / mazeData.cols;
			var cellHeight = canvas.height / mazeData.rows;

			for (var i = 0; i < mazeData.rows; i++) {
				for (var j = 0; j < mazeData.cols; j++) {
					if (mazeData.maze[i * mazeData.rows + j] == 1) {
						context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
					}
				}
			}
			return this;
		});
	};
})(jQuery);