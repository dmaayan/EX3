
document.addEventListener("DOMContentLoaded", function (event) {
    // decide which register and login lines to use
    var registerLi = "<li style=\"margin-left:100px\"><a href=\"RegisterPage.html\">Register</a></li>"
    var loginLi = "<li><a href=\"LoginPage.html\">Login</a></li>"
    // change the lines if user is connected
    if (sessionStorage.user) {
        registerLi = "<li style=\"margin-left:100px\"; width=\"100\"><a>Hello " + sessionStorage.user + "</a></li>";
        loginLi = "<li style=\"margin-left:30px\" onclick=\"logOff()\"><a>Log off</a></li>";
    }
    // create menu bar
    var navDivString = "<div id=\"nav_div\">" +
        "<nav class=\"navbar navbar-default\" role=\"navigation\">" +
        "<div class=\"navbar-header\">" +
        "<a class=\"navbar-brand\" href=\"MainPage.html\">Maze</a>" +
        "</div >" +
        "<div>" +
        "<ul class=\"nav navbar-nav\">" +
        "<li><a href=\"SinglePage.html\">Single Game</a></li>" +
        "<li onclick=\"multiClick()\"><a href=\"#\">Multiplayer Game</a></li>" +
        "<li><a href=\"SettingsPage.html\">Settings</a></li>" +
        "<li><a href=\"RankingPage.html\">User Rankings</a></li>" +
        registerLi + loginLi +
        "</ul>" +
        "</div>" +
        "</nav >" +
        "</div>" +
        "<h1 style=\"margin-left:10px\">" + $(document).find("title").text() + "</h1>";
    $("body").prepend(navDivString);
});

// log of the user
function logOff() {
    sessionStorage.removeItem("user");
    window.location.replace("../html/MainPage.html");
}

// if clicked on multiplayer game without login
function multiClick() {
    if (sessionStorage.user) {
        window.location = "../html/MultiPage.html";
    } else {
        window.location = "../html/LoginPage.html";
    }
}

