
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
        $("#mazeCanvas").css({ "margin-right": "30px", "border": "solid thick" }).mazeBoard(
            mazeData,
            maze.Start.Row, maze.Start.Col,
            maze.End.Row, maze.End.Col,
            null, null, true,
            //function (direction, playerRow, playerCol) {
            //    switch (direction) {
            //        case 0:
            //            pla
            //    }
            //});
            null);

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
