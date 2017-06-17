var maze = undefined;
var canvas = undefined;
var context = undefined;
var cellWidth = undefined;
var cellHeight = undefined;

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

var enabled = true;
var playerPos;
var animating = false;
var interval;

$(document).ready(loadSettings);

function loadSettings() {
    $("#mazeName").val("mymaze");
    $("#mazeRows").val(localStorage.getItem("mazeRows"));
    $("#mazeCols").val(localStorage.getItem("mazeCols"));
    $("#searchAlgo").val(localStorage.getItem("searchAlgo"));
    maze = undefined;
    canvas = $("#mazeCanvas")[0];
    context = canvas.getContext("2d");
}

function startGame() {
    if (animating) {
        clearInterval(interval);
        animating = false;
    }
    enabled = true;
    var url = "../api/GenerateMaze/" + $("#mazeName").val() + "/" + $("#mazeRows").val() + "/" + $("#mazeCols").val();
    $.get(url).done(function (data) {
        console.log(data);
        maze = JSON.parse(data);
        localStorage.removeItem(maze.Name);
        var mazeData = {
            maze: maze.Maze,
            rows: maze.Rows,
            cols: maze.Cols
        };
        var size = (mazeData.rows >= mazeData.cols) ? canvas.height / mazeData.rows : canvas.width / mazeData.cols;
        cellWidth = size;
        cellHeight = size;
        playerPos = { Row: maze.Start.Row, Col: maze.Start.Col };
        while (!exitImageLoaded || !playerImageLoaded);
        $("#mazeCanvas").css({ "margin-right": "30px", "border": "1px solid #000000" }).mazeBoard(
            mazeData,
            maze.Start.Row, maze.Start.Col,
            maze.End.Row, maze.End.Col,
            playerImage, exitImage, enabled,
            function (direction, playerRow, playerCol) {
                if (!enabled) {
                    return { Row: playerRow, Col: playerCol };
                }
                context.fillStyle = "#ffffff";
                switch (direction) {
                    //left
                    case 37: {
                        var mazePos = maze.Maze[playerRow * maze.Cols + playerCol - 1];
                        if ((playerCol > 0) && (mazePos != 1)) {
                            context.fillRect(cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
                            context.drawImage(playerImage, cellWidth * (playerCol - 1), cellHeight * playerRow, cellWidth, cellHeight);
                            playerPos = { Row: playerRow, Col: (playerCol - 1) };
                            return playerPos;
                        }
                        break;
                    }

                        //right
                    case 39: {
                        if ((playerCol < maze.Cols - 1) && (maze.Maze[playerRow * maze.Cols + playerCol + 1] != 1)) {
                            context.fillRect(cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
                            context.drawImage(playerImage, cellWidth * (playerCol + 1), cellHeight * playerRow, cellWidth, cellHeight);
                            playerPos = { Row: playerRow, Col: (playerCol + 1) };
                            return playerPos;
                        }
                        break;
                    }

                        //up
                    case 38: {
                        if ((playerRow > 0) && (maze.Maze[(playerRow - 1) * maze.Cols + playerCol] != 1)) {
                            context.fillRect(cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
                            context.drawImage(playerImage, cellWidth * playerCol, cellHeight * (playerRow - 1), cellWidth, cellHeight);
                            playerPos = { Row: (playerRow - 1), Col: playerCol };
                            return playerPos;
                        }
                        break;
                    }

                        //down
                    case 40: {
                        if ((playerRow < maze.Rows - 1) && (maze.Maze[(playerRow + 1) * maze.Cols + playerCol] != 1)) {
                            context.fillRect(cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
                            context.drawImage(playerImage, cellWidth * playerCol, cellHeight * (playerRow + 1), cellWidth, cellHeight);
                            playerPos = { Row: (playerRow + 1), Col: playerCol };
                            return playerPos;
                        }
                        break;
                    }

                    default: {
                        playerPos = { Row: playerRow, Col: playerCol };
                        return playerPos;
                    }

                }
                return { Row: playerRow, Col: playerCol };
            }
        );
    });
}

function solve() {
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
    var url = "../api/GenerateMaze/" + $("#searchAlgo").val();
    context.fillStyle = "#ffffff";

    var solveAnimationFunc = function (solution) {
        enabled = false;
        localStorage.setItem(maze.Name, solution);
        solution = JSON.parse(solution);
        directions = solution.Solution;

        var playerRow = maze.Start.Row;
        var playerCol = maze.Start.Col;
        var i = 0;

        context.fillRect(cellWidth * playerPos.Col, cellHeight * playerPos.Row, cellWidth, cellHeight);
        context.drawImage(playerImage, cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
        
        enabled = false;
        interval = setInterval(function () {
            if (i >= directions.length) {
                context.drawImage(playerImage, cellWidth * playerPos.Col, cellHeight * playerPos.Row, cellWidth, cellHeight);
                context.drawImage(exitImage, cellWidth * maze.End.Col, cellHeight * maze.End.Row, cellWidth, cellHeight);
                clearInterval(interval);
                enabled = true;
                animating = false;
                return;
            }
            var change = changeInDirection(directions[i], playerRow, playerCol);
            context.fillRect(cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
            playerRow = change.Row;
            playerCol = change.Col;
            context.drawImage(playerImage, cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
            ++i;
        }, 250);
    };

    if (localStorage.getItem(maze.Name) != undefined) {
        console.log("found in local");
        solveAnimationFunc(localStorage.getItem(maze.Name));
    } else {
        $.get(url).done(solveAnimationFunc);
    }
}

function changeInDirection(direction, playerRow, playerCol) {
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