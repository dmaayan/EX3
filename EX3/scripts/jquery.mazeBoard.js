(function ($) {
	$.fn.mazeBoard = function (
		mazeData, // the matrix containing the maze cells
		startRow, startCol, // initial position of the player
		exitRow, exitCol, // the exit position
		playerImage, // player's icon (of type Image)
		exitImage, // exit's icon (of type Image)
		enabled, // is the board enabled (i.e., player can move)
        callback) {
        $("#winLabel").html("");
        this.each(function (index, elem) {
            var playerRow;
            var playerCol;
			var canvas = elem;
			var context = canvas.getContext("2d");
			var cellWidth = canvas.width / mazeData.cols;
			var cellHeight = canvas.height / mazeData.rows;

			for (var i = 0; i < mazeData.rows; i++) {
				for (var j = 0; j < mazeData.cols; j++) {
                    if (mazeData.maze[i * mazeData.rows + j] == 1) {
                        context.fillStyle = "#000000";
                    }
                    else {
                        context.fillStyle = "#ffffff";
                    }
                    context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
				}
            }

            playerRow = startRow;
            playerCol = startCol;

            context.drawImage(playerImage, cellWidth * startCol, cellHeight * startRow, cellWidth, cellHeight);
            context.drawImage(exitImage, cellWidth * exitCol, cellHeight * exitRow,cellWidth, cellHeight);
            $(window).keydown(function (event) {
                if (enabled) {
                    var direction = event.which;
                    var change = callback(direction, playerRow, playerCol);
                    playerRow = change.Row;
                    playerCol = change.Col;
                    if ((playerRow == exitRow) && (playerCol == exitCol)) {
                        enabled = false;
                        $("#winLabel").html("Congratulaions, you won!");
                    }
                }
            });
			return this;
		});
	};
})(jQuery);