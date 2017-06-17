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
            context.fillStyle = "#000000";
            context.fillRect(0, 0, canvas.width, canvas.height);
            var size = (mazeData.rows >= mazeData.cols) ? canvas.height / mazeData.rows : canvas.width / mazeData.cols;
			var cellWidth = size;
			var cellHeight = size;

			for (var i = 0; i < mazeData.rows; i++) {
				for (var j = 0; j < mazeData.cols; j++) {
				    if (mazeData.maze[i * mazeData.cols + j] == 1) {
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
            context.drawImage(exitImage, cellWidth * exitCol, cellHeight * exitRow, cellWidth, cellHeight);
            var keyDownFunc = function (event) {
                if (enabled) {
                    var direction = event.which;
                    var change = callback(direction, playerRow, playerCol);
                    playerRow = change.Row;
                    playerCol = change.Col;
                    if ((playerRow == exitRow) && (playerCol == exitCol)) {
                        enabled = false;
                        $("#winLabel").html("Congratulations, you won!");
                    }
                }
            };

            $(window).on("keydown", keyDownFunc);
            var exitFunc = function () {
                $(window).off("keydown", keyDownFunc);
                $("#start").off("click", exitFunc);
            };
            $("#start").on("click", exitFunc);
			return this;
		});
	};
})(jQuery);