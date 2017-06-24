(function ($) {
    $.fn.mazeBoard = function (
        mazeData, // the matrix containing the maze cells
        startRow, startCol, // initial position of the player
        exitRow, exitCol, // the exit position
        playerImage, // player's icon (of type Image)
        exitImage, // exit's icon (of type Image)
        enabled, // is the board enabled (i.e., player can move)
        callback) {
        var playerRow;
        var playerCol;
        var canvas = this[0];
        var context = canvas.getContext("2d");
        // set color
        context.fillStyle = "#000000";
        // clear the current canvas
        context.fillRect(0, 0, canvas.width, canvas.height);
        // calculate the size of each tile
        var size = (mazeData.rows >= mazeData.cols) ? canvas.height / mazeData.rows : canvas.width / mazeData.cols;
        var cellWidth = size;
        var cellHeight = size;

        // for each tile, color white if clear or black if wall
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
        // start location
        playerPos = { Row: startRow, Col: startCol };

        // draw the player and the exit images
        context.drawImage(playerImage, cellWidth * playerPos.Col, cellHeight * playerPos.Row, cellWidth, cellHeight);
        context.drawImage(exitImage, cellWidth * exitCol, cellHeight * exitRow, cellWidth, cellHeight);

        // add the keydown function to the window
        $(window).on("keydown", function (event) {
            // if enabled, move if possible
            if (enabled) {
                // get the new location
                playerPos = callback(event.which, playerPos.Row, playerPos.Col);
                // check if finished the game
                if ((playerPos.Row == exitRow) && (playerPos.Col == exitCol)) {
                    // disable the game
                    enabled = false;
                }
            }
        });
        return this;
    };
})(jQuery);