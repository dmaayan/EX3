﻿
$("#loader").hide();
$(document).ready(loadSettings);

// Declare a proxy to reference the hub
var server = $.connection.chatHub;

var maze = undefined;
var mazeRecived = 0;

// Create a function that the hub can call to broadcast messages
server.client.reciveMaze = function (data, stat) {
    // TODO נקודת כשל אופציונלית
    // got a maze
    if (state == 1) {
        maze = data;
        mazeRecived = 1;
    }
};


// Start the connection
$.connection.hub.start().done(function () {
    $('#btnSendMessage').click(function () {
        // Call the Send method on the hub
        chat.server.send(username, $('#message').val());
        // Clear text box and reset focus for next comment
        $('#message').val('').focus();
    });
});

function loadSettings() {
    $("#mazeName").val("mymaze");
    $("#mazeRows").val(localStorage.getItem("mazeRows"));
    $("#mazeCols").val(localStorage.getItem("mazeCols"));

    var playerImageLoaded = false;
    var exitImageLoaded = false;
    var playerImage = new Image();
    playerImage.src = "../Images/player.png";
    playerImage.onload = function () {
        playerImageLoaded = true;
    }
    var exitImage = new Image();
    exitImage.src = "../Images/key.jpg"
    exitImage.onload = function () {
        exitImageLoaded = true;
    }

    $(window).ready(function () {

        var otherMaze = undefined;
        var myCanvas = $("#myCanvas")[0];
        var otherCanvas = $("#otherCanvas")[0];
        var myContext = myCanvas.getContext("2d");
        var otherContext = otherCanvas.getContext("2d");
        var cellWidth = undefined;
        var cellHeight = undefined;

        var enabled = true;
        var myPos;
        var otherPos;

        $("#start").click(function startGame() {
            $("#loader").show();
            // Call the start method on the hub
            chat.server.send($('#mazeName').val(), $('#mazeRows').val(), $('#mazeCols').val());
            if (mazeRecived == 1) {
                $("#loader").hide();
                var size = (maze.Rows >= maze.Cols) ? myCanvas.height / maze.Rows : myCanvas.width / maze.Cols;
                cellWidth = size;
                cellHeight = size;
                myPos = { Row: maze.Start.Row, Col: maze.Start.Col };
                // if the Images didn't load, wait
                while (!exitImageLoaded || !playerImageLoaded);

                // set canvas style and draw the maze
                $("#mazeCanvas").css({ "margin-right": "30px", "border": "1px solid #000000" }).mazeBoard(
                    { maze: maze.Maze, rows: maze.Rows, cols: maze.Cols },
                    maze.Start.Row, maze.Start.Col,
                    maze.End.Row, maze.End.Col,
                    playerImage, exitImage, enabled,
                    function (direction, playerRow, playerCol) {
                        // change pos in the direction of movement
                        var pos = changeInKeyDirection(direction, playerRow, playerCol);
                        // if game is enabled, try to move
                        if (enabled) {
                            // check position boundaries
                            if ((pos.Row >= maze.Rows) || (pos.Col >= maze.Col) || (pos.Row < 0) || (pos.Col < 0)) {
                                return { Row: playerRow, Col: playerCol };
                            }
                            // get maze tile and check
                            var mazePos = maze.Maze[pos.Row * maze.Cols + pos.Col];
                            if (mazePos != 1) {
                                // change position
                                playerPos = pos;
                                context.fillStyle = "#ffffff";
                                context.fillRect(cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
                                context.drawImage(playerImage, cellWidth * pos.Col, cellHeight * pos.Row, cellWidth, cellHeight);
                                return pos;
                            }
                        }
                        // return the current location
                        return { Row: playerRow, Col: playerCol };
                    }
                );

            } else {
                alert("Failed to get maze from server");
            }             
         });
    });
}

// return a json of location after change to the right direction
function changeInCharDirection(direction, playerRow, playerCol) {
    switch (direction) {
        case '0': {
            // left
            return { Row: playerRow, Col: (playerCol - 1) };
        }
        case '1': {
            // right
            return { Row: playerRow, Col: (playerCol + 1) };
        }
        case '2': {
            // up
            return { Row: (playerRow - 1), Col: playerCol };
        }
        case '3': {
            // down
            return { Row: (playerRow + 1), Col: playerCol };
        }
    }
}

// return a json of location after change to the right direction
function changeInKeyDirection(direction, playerRow, playerCol) {
    switch (direction) {
        case 37: {
            // left
            return { Row: playerRow, Col: (playerCol - 1) };
        }
        case 39: {
            // right
            return { Row: playerRow, Col: (playerCol + 1) };
        }
        case 38: {
            // up
            return { Row: (playerRow - 1), Col: playerCol };
        }
        case 40: {
            // down
            return { Row: (playerRow + 1), Col: playerCol };
        }
    }
}




