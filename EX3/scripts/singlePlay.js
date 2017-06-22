
$(document).ready(loadSettings);

// load the default settings and define all others
function loadSettings() {
    $("#mazeName").val("mymaze");
    $("#mazeRows").val(localStorage.getItem("mazeRows"));
    $("#mazeCols").val(localStorage.getItem("mazeCols"));
    $("#searchAlgo").val(localStorage.getItem("searchAlgo"));

    // load the images
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

    // when window is ready
    $(window).ready(function () {
        // define maze and canvas variables
        var maze = undefined;
        var canvas = $("#mazeCanvas")[0];
        var context = canvas.getContext("2d");
        var cellWidth = undefined;
        var cellHeight = undefined;

        // define game play variables
        var enabled = true;
        var playerPos;
        var animating = false;
        var interval;

        // add click event to the start button
        $("#start").click(function startGame() {
            // clear the win label
            $("#winLabel").html("");
            // disable previeus keydown events
            $(window).off("keydown");
            // show the loader
            $("#loader").fadeIn();
            // if currently animating solution
            if (animating) {
                clearInterval(interval);
                animating = false;
            }
            enabled = true;
            // set get url request
            var url = "../api/GenerateMaze/" + $("#mazeName").val() + "/" + $("#mazeRows").val() + "/" + $("#mazeCols").val();
            // send get to server
            $.get(url).fail(function () {
                alert("Failed to get maze from server");
            }).done(function (data) {
                // hide the loader
                $("#loader").hide();
                maze = data;
                // delete previeus maze solutions
                localStorage.removeItem(maze.Name);

                // calculate maze size
                var size = (maze.Rows >= maze.Cols) ? canvas.height / maze.Rows : canvas.width / maze.Cols;
                cellWidth = size;
                cellHeight = size;
                playerPos = { Row: maze.Start.Row, Col: maze.Start.Col };
                // wait for images to load
                while (!exitImageLoaded || !playerImageLoaded);
                // set canvas style and draw the maze
                $("#mazeCanvas").css({ "margin-right": "30px", "border": "1px solid #000000" }).mazeBoard(
                    { maze: maze.Maze, rows: maze.Rows, cols: maze.Cols },
                    maze.Start.Row, maze.Start.Col,
                    maze.End.Row, maze.End.Col,
                    playerImage, exitImage, enabled,
                    function (direction, playerRow, playerCol) {
                        // if game is enabled, try to move
                        if (enabled) {
                            // change pos in the direction of movement
                            var pos = changeInKeyDirection(direction, playerRow, playerCol);
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
                                if (playerPos.Row == maze.End.Row && playerPos.Col == maze.End.Col) {
                                    $("#winLabel").html("Congratulations, you won!");
                                }
                                return pos;
                            }
                        }
                        // return the current location
                        return { Row: playerRow, Col: playerCol };
                    }
                );
            });
        });
        // add a click event to solve
        $("#solve").click(function solve() {
            // if there is no maze
            if (maze == undefined) {
                console.log("Please start a game first!");
                return;
            }
            // if already animating
            if (animating) {
                return;
            }
            // if game have ended without solve
            if ((playerPos.Col == maze.End.Col) && (playerPos.Row == maze.End.Row)) {
                alert("You already won by yourself");
                return;
            }
            animating = true;
            var url = "../api/GenerateMaze/" + maze.Name + "/" + $("#searchAlgo").val();

            // define a solve animation function
            var solveAnimationFunc = function (solution) {
                enabled = false;
                // save the solution
                localStorage.setItem(maze.Name, JSON.stringify(solution));
                // the directions of the solution
                var directions = solution.Solution;
                // position of player at animation
                var PosAtAnimation = {Row: maze.Start.Row, Col: maze.Start.Col};
                // define style and starting location
                context.fillStyle = "#ffffff";
                context.fillRect(cellWidth * playerPos.Col, cellHeight * playerPos.Row, cellWidth, cellHeight);
                context.drawImage(playerImage, cellWidth * PosAtAnimation.Col, cellHeight * PosAtAnimation.Row, cellWidth, cellHeight);

                var i = 0;
                // define function for intervals
                interval = setInterval(function () {
                    // do until got to end location
                    if (i >= directions.length) {
                        // return the player to its original location and reset
                        context.drawImage(playerImage, cellWidth * playerPos.Col, cellHeight * playerPos.Row, cellWidth, cellHeight);
                        context.drawImage(exitImage, cellWidth * maze.End.Col, cellHeight * maze.End.Row, cellWidth, cellHeight);
                        clearInterval(interval);
                        enabled = true;
                        animating = false;
                        return;
                    }
                    // change the player location on animation
                    context.fillRect(cellWidth * PosAtAnimation.Col, cellHeight * PosAtAnimation.Row, cellWidth, cellHeight);
                    PosAtAnimation = changeInCharDirection(directions[i], PosAtAnimation.Row, PosAtAnimation.Col);
                    context.drawImage(playerImage, cellWidth * PosAtAnimation.Col, cellHeight * PosAtAnimation.Row, cellWidth, cellHeight);
                    ++i;
                }, 250);
            };
            // find solution in local machine
            var solutioinInLocal = localStorage.getItem(maze.Name);
            // if found solution
            if (solutioinInLocal != undefined) {
                // solve with it
                solveAnimationFunc(JSON.parse(solutioinInLocal));
            } else {
                // get the solution from the server
                $.get(url).fail(function () {
                    alert("Failed to get solution from server");
                }).done(solveAnimationFunc);
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
            return { Row: playerRow, Col: (playerCol + 1)};
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
        default: {
            // none direction key
            return { Row: playerRow, Col: playerCol };
        }
    }
}

