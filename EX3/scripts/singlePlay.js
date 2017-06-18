
$("#loader").hide();


$(document).ready(loadSettings);

function loadSettings() {
    $("#mazeName").val("mymaze");
    $("#mazeRows").val(localStorage.getItem("mazeRows"));
    $("#mazeCols").val(localStorage.getItem("mazeCols"));
    $("#searchAlgo").val(localStorage.getItem("searchAlgo"));

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

        var maze = undefined;
        var canvas = $("#mazeCanvas")[0];
        var context = canvas.getContext("2d");
        var cellWidth = undefined;
        var cellHeight = undefined;

        var enabled = true;
        var playerPos;
        var animating = false;
        var interval;

        $("#start").click(function startGame() {
            $("#loader").show();
            if (animating) {
                clearInterval(interval);
                animating = false;
            }
            enabled = true;
            var url = "../api/GenerateMaze/" + $("#mazeName").val() + "/" + $("#mazeRows").val() + "/" + $("#mazeCols").val();
            $.get(url).fail(function () {
                alert("Failed to get maze from server");
            }).done(function (data) {
                $("#loader").hide();
                console.log(data);
                maze = data;
                localStorage.removeItem(maze.Name);

                var size = (maze.Rows >= maze.Cols) ? canvas.height / maze.Rows : canvas.width / maze.Cols;
                cellWidth = size;
                cellHeight = size;
                playerPos = { Row: maze.Start.Row, Col: maze.Start.Col };
                while (!exitImageLoaded || !playerImageLoaded);
                $("#mazeCanvas").css({ "margin-right": "30px", "border": "1px solid #000000" }).mazeBoard(
                    { maze: maze.Maze, rows: maze.Rows, cols: maze.Cols },
                    maze.Start.Row, maze.Start.Col,
                    maze.End.Row, maze.End.Col,
                    playerImage, exitImage, enabled,
                    function (direction, playerRow, playerCol) {
                        var pos = changeInKeyDirection(direction, playerRow, playerCol);
                        if (enabled) {

                            var mazePos = maze.Maze[pos.Row * maze.Cols + pos.Col];
                            if (mazePos != 1) {
                                playerPos = pos;
                                context.fillStyle = "#ffffff";
                                context.fillRect(cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
                                context.drawImage(playerImage, cellWidth * pos.Col, cellHeight * pos.Row, cellWidth, cellHeight);
                                return pos;
                            }
                        }
                        return { Row: playerRow, Col: playerCol };
                    }
                );
            });
        });

        $("#solve").click(function solve() {
            if (maze == undefined) {
                console.log("Please start a game first!");
                return;
            }
            if (animating) {
                return;
            }
            if ((playerPos.Col == maze.End.Col) && (playerPos.Row == maze.End.Row)) {
                alert("You already won by yourself");
                return;
            }
            animating = true;
            var url = "../api/GenerateMaze/" + maze.Name + "/" + $("#searchAlgo").val();

            var solveAnimationFunc = function (solution) {
                enabled = false;
                localStorage.setItem(maze.Name, JSON.stringify(solution));
                var directions = solution.Solution;

                var playerRow = maze.Start.Row;
                var playerCol = maze.Start.Col;

                context.fillStyle = "#ffffff";
                context.fillRect(cellWidth * playerPos.Col, cellHeight * playerPos.Row, cellWidth, cellHeight);
                context.drawImage(playerImage, cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);

                var i = 0;
                interval = setInterval(function () {
                    if (i >= directions.length) {
                        context.drawImage(playerImage, cellWidth * playerPos.Col, cellHeight * playerPos.Row, cellWidth, cellHeight);
                        context.drawImage(exitImage, cellWidth * maze.End.Col, cellHeight * maze.End.Row, cellWidth, cellHeight);
                        clearInterval(interval);
                        enabled = true;
                        animating = false;
                        return;
                    }
                    var change = changeInCharDirection(directions[i], playerRow, playerCol);
                    context.fillRect(cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
                    playerRow = change.Row;
                    playerCol = change.Col;
                    context.drawImage(playerImage, cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
                    ++i;
                }, 250);
            };

            var solutioinInLocal = localStorage.getItem(maze.Name);
            if (solutioinInLocal != undefined) {
                solveAnimationFunc(JSON.parse(solutioinInLocal));
            } else {
                $.get(url).fail(function () {
                    alert("Failed to get solution from server");
                }).done(solveAnimationFunc);
            }
        });
    });
}


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

