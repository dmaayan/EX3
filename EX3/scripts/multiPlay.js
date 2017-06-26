
// do not let an unregistered user into multi games
if (!sessionStorage.user) {
    window.location.replace("../html/LoginPage.html");
}

// Declare a proxy to reference the hub
var server = $.connection.chatHub;
//define and load the image of the player
var playerImage = new Image();
playerImage.src = "../Images/player.png";
playerImage.onload = function () {
    playerImageLoaded = true;
}
//define and load the image of the exit point of the maze
var exitImage = new Image();
exitImage.src = "../Images/key.jpg"
exitImage.onload = function () {
    exitImageLoaded = true;
}
var maze = undefined;
var enabled = true;

var otherPos = undefined;
var playerImageLoaded = false;
var exitImageLoaded = false;
var mazeName = undefined;
var win = false;

// reciveMaze is a function that the hub can call to send the maze.
// the function get a data - maze and the maze's stat (1 if got a maze, 0 else)
// the function responsible to send to the mazeBoard the function that painting the maze
server.client.reciveMaze = function (data, stat) {
    var myCanvas = $("#myCanvas")[0];
    var myContext = myCanvas.getContext("2d");
    console.log("received maze")
    // got a maze
    if (stat == 1) {
        enabled = true;
        maze = data;
        $("#loader").hide();
        $("#myLabal").show();
        $("#otherLabal").show();
        $("#otherCanvas").show();
        $("#myCanvas").show();
        document.title = maze.Name;
        mazeName = maze.Name;
        // set the sizes depand on the maze size
        var size = (maze.Rows >= maze.Cols) ? myCanvas.height / maze.Rows :
                    myCanvas.width / maze.Cols;
        var cellWidth = size;
        var cellHeight = size;
        // set the players position
        otherPos = { Row: maze.Start.Row, Col: maze.Start.Col };
        // wait for images to load
        while (!exitImageLoaded || !playerImageLoaded);
        // set canvas style and draw my maze, by sending a function to the mazeBoard plugin
        $("#myCanvas").css({ "margin-right": "30px", "border": "1px solid #000000" }).mazeBoard(
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
                    if ((pos.Row >= maze.Rows) || (pos.Col >= maze.Col) ||
                        (pos.Row < 0) || (pos.Col < 0)) {
                        return { Row: playerRow, Col: playerCol };
                    }
                    // get maze tile and check
                    var mazePos = maze.Maze[pos.Row * maze.Cols + pos.Col];
                    if (mazePos != 1) {
                        // change position
                        playerPos = pos;
                        // set the color to white
                        myContext.fillStyle = "#ffffff";
                        // paint the last position of the player to white
                        myContext.fillRect(cellWidth * playerCol, cellHeight * playerRow,
                                           cellWidth, cellHeight);
                        // draw the new position of the player
                        myContext.drawImage(playerImage, cellWidth * pos.Col,
                                            cellHeight * pos.Row, cellWidth, cellHeight);
                        // in case of winning, end the game
                        if (playerPos.Row == maze.End.Row && playerPos.Col == maze.End.Col) {
                            $("#winOrLose").html("Congratulations, you won!");
                            win = true; 
                            endGame();

                        }
                        // send to the server that a move has been played, with the new position
                        server.server.playMove(JSON.stringify(pos));
                        return pos;
                    }
                }

                // return the current location
                return { Row: playerRow, Col: playerCol };
            }
        );
        // set canvas style and draw the other (rival) maze
        $("#otherCanvas").css({"margin-right": "30px", "border": "1px solid #000000"}).mazeBoard(
            { maze: maze.Maze, rows: maze.Rows, cols: maze.Cols },
            maze.Start.Row, maze.Start.Col,
            maze.End.Row, maze.End.Col,
            playerImage, exitImage, false, undefined);
    // didn't get a maze
    } else {
        alert("Problam occured");
        $("#join").show();
        $("#start").show();
        $("#loader").hide();
        document.title = 'Multi Player';
    }
};

// move is a function that the hub can call to send the other player move
// the function get a data -the new position, the stat (1 if got new position, 0 else)
server.client.move = function (data, stat) {
    var otherCanvas = $("#otherCanvas")[0];
    var otherContext = otherCanvas.getContext("2d");
    var size = (maze.Rows >= maze.Cols) ? otherCanvas.height / maze.Rows :
                                          otherCanvas.width / maze.Cols;
    var cellWidth = size;
    var cellHeight = size;
    // change position
    if (stat == 1) {
        var pos = JSON.parse(data);
        // set the color to white
        otherContext.fillStyle = "#ffffff";  
        // paint the last position of the other player to white
        otherContext.fillRect(cellWidth * otherPos.Col, cellHeight * otherPos.Row,
                              cellWidth, cellHeight);
        // draw the new position of the other player
        otherContext.drawImage(playerImage, cellWidth * pos.Col,
                               cellHeight * pos.Row, cellWidth, cellHeight);
        // in case of losing, end the game
        if (pos.Row == maze.End.Row && pos.Col == maze.End.Col) {
            $("#winOrLose").html("You Lost !");
            endGame();
        }
        otherPos = pos;
    }
};

// move is a function that the hub can call to send the list og games
// the function get a data, parse it to a list of games and update the "games" drop box 
server.client.getGames = function (data) {
    var games = JSON.parse(data);
    // clear the list
    $("#games").html('');
    // appand all the names of the games
    games.forEach(function (element) {
        $("#games").append("<option value=" + element + ">" + element + "</option>");
   
    });
};

// Start the connection
$.connection.hub.start().done(function () {
    console.log("connected to the server");
});

$(document).ready(loadSettings);
// loadSettings is a function that loading all the settings from the local storage,
// and give an answer to each button than has been clicked
function loadSettings() {
    //loading the settings from the local storage
    $("#mazeName").val("mymaze");
    $("#mazeRows").val(localStorage.getItem("mazeRows"));
    $("#mazeCols").val(localStorage.getItem("mazeCols"));
    $("#otherCanvas").hide();
    $("#myCanvas").hide();
    $("#myLabal").hide();
    $("#otherLabal").hide();

    $(window).ready(function () {
        // the start button has been clicked, update the page, and send request to the server
        $("#start").click(function () {
            if (($('#mazeName').val().trim().length > 0) && ($('#mazeRows').val() > 0)
                                                         && ($('#mazeCols').val() > 0)) {
                $(window).off("keydown");
                $("#loader").show();
                $("#join").hide();
                $("#start").hide();
                $("#winOrLose").html("");
                // Call the start method on the hub
                server.server.startGame($('#mazeName').val(), $('#mazeRows').val(),
                                        $('#mazeCols').val());
            } else {
                alert("Please fill all the fileds")
            }

        
        });
        // the join button has been clicked, update the page, and send request to the server
        $("#join").click(function () {
            $(window).off("keydown");
            $("#loader").show();
            $("#join").hide();
            $("#start").hide();
            $("#winOrLose").html("");
            // Call the JoinGame method on the hub
            server.server.joinGame($('#games').val());

        });
        // the "games" drop box has been clicked, send a request to the server
        $("#games").click(function () {
            server.server.getGames();
        });
    });
}
// endGame function updats the relevet buttons, update the Statistics and the server
function endGame()
{
    //updats the relevet buttons
    $("#join").show();
    $("#start").show();
    enabled = false;
    document.title = 'Multi Player';
    var statistics;
    var user = sessionStorage.user;
    // define url for post request
    var url = "../api/Statistics/" + user;
    var userData;
    if (win) {
        // if won, define stat plus 1 for wins
        userData = { UserName: user, Wins: 1, Losses: 0 };
    } else {
        // if lost, define stat plus 1 for losses
        userData = { UserName: user, Wins: 0, Losses: 1 };
        // inform the server that game have ended
        server.server.gameOver(mazeName);
    }
    // update the Statistics
    $.post(url, userData).fail(function (xhr) {
        // data base error
       console.log("Data base error");
    }).done(function () {
        // user have been saved
        console.log("statistics have been saved into database");
    });
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




