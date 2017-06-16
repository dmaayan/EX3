
$(document).ready(loadSettings);

function loadSettings() {
    $("#mazeName").val("mymaze");
    $("#mazeRows").val(localStorage.getItem("mazeRows"));
    $("#mazeCols").val(localStorage.getItem("mazeCols"));
    $("#searchAlgo").val(localStorage.getItem("searchAlgo"));
}

function startGame() {
    var url = "../api/GenerateMaze/" + $("#mazeName").val();
    $.get(url).done(function (maze) {
        console.log(maze);
        maze = JSON.parse(maze);
        var mazeData = {
            maze: maze.Maze,
            rows: maze.Rows,
            cols: maze.Cols
        };
        var playerImage = new Image();
        playerImage.src = "../Images/player.png";
        playerImage.onload = function () {
            var exitImage = new Image();
            exitImage.src = "../Images/key.jpg"
            exitImage.onload = function () {
                $("#mazeCanvas")
                $("#mazeCanvas").css({ "margin-right": "30px", "border": "solid thick" }).mazeBoard(
                    mazeData,
                    maze.Start.Row, maze.Start.Col,
                    maze.End.Row, maze.End.Col,
                    playerImage, exitImage, true,
                    function (direction, playerRow, playerCol) {
                        var canvas = $("#mazeCanvas")[0];
                        var context = canvas.getContext("2d");
                        var cellWidth = canvas.width / mazeData.cols;
                        var cellHeight = canvas.height / mazeData.rows;
                        context.fillStyle = "#ffffff";
                        switch (direction) {
                            //left
                            case 37: {
                                var mazePos = maze.Maze[playerRow * maze.Cols + playerCol - 1];
                                if ((playerCol > 0) && (mazePos != 1)) {
                                    context.fillRect(cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
                                    context.drawImage(playerImage, cellWidth * (playerCol - 1), cellHeight * playerRow, cellWidth, cellHeight);
                                    return { Row: playerRow, Col: (playerCol - 1) };
                                }
                                break;
                            }

                            //right
                            case 39: {
                                if ((playerCol < maze.Cols - 1) && (maze.Maze[playerRow * maze.Cols + playerCol + 1] != 1)) {
                                    context.fillRect(cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
                                    context.drawImage(playerImage, cellWidth * (playerCol + 1), cellHeight * playerRow, cellWidth, cellHeight);
                                    return { Row: playerRow, Col: (playerCol + 1) };
                                }
                                break;
                            }

                            //up
                            case 38: {
                                if ((playerRow > 0) && (maze.Maze[(playerRow - 1) * maze.Cols + playerCol] != 1)) {
                                    context.fillRect(cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
                                    context.drawImage(playerImage, cellWidth * playerCol, cellHeight * (playerRow - 1), cellWidth, cellHeight);
                                    return { Row: (playerRow - 1), Col: playerCol };
                                }
                                break;
                            }

                            //down
                            case 40: {
                                if ((playerRow < maze.Rows - 1) && (maze.Maze[(playerRow + 1) * maze.Cols + playerCol] != 1)) {
                                    context.fillRect(cellWidth * playerCol, cellHeight * playerRow, cellWidth, cellHeight);
                                    context.drawImage(playerImage, cellWidth * playerCol, cellHeight * (playerRow + 1), cellWidth, cellHeight);
                                    return { Row: (playerRow + 1), Col: playerCol };
                                }
                                break;
                            }

                            default: {
                                return { Row: playerRow, Col: playerCol };
                            }

                        }
                        return { Row: playerRow, Col: playerCol };
                    }
                );
            };
        };
        

        
    });
}

//{
//	"Name": "mymaze",
//	"Maze": "0000000001110111110101010000010101011111000100000111111111010001000001010101111101000000011111111111",
//	"Rows": 10,
//	"Cols": 10,
//	"Start": {
//		"Row": 2,
//		"Col": 0
//	},
//  "End": {
//  	"Row": 0,
//    "Col": 7
//  }
//}

//mazeData, // the matrix containing the maze cells
//    startRow, startCol, // initial position of the player
//    exitRow, exitCol, // the exit position
//    playerImage, // player's icon (of type Image)
//    exitImage, // exit's icon (of type Image)
//    enabled, // is the board enabled (i.e., player can move)
//    callback) {

function solve() {

}
