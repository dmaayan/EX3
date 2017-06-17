
document.addEventListener("DOMContentLoaded", function (event) {
    var registerLi = "<li style=\"margin-left:100px\"><a href=\"RegisterPage.html\">Register</a></li>"
    var loginLi = "<li><a href=\"LoginPage.html\">Login</a></li>"
    if (sessionStorage.user) {
        registerLi = "<li style=\"margin-left:100px\"; width=\"100\"><a>Hello " + sessionStorage.user + "</a></li>";
        loginLi = "<li style=\"margin-left:30px\" onclick=\"logOff()\"><a>Log off</a></li>";
    }
    var navDivString = "<div id=\"nav_div\">" +
        "<nav class=\"navbar navbar-default\" role=\"navigation\">" +
        "<div class=\"navbar-header\">" +
        "<a class=\"navbar-brand\" href=\"#\">Maze</a>" +
        "</div >" +
        "<div>" +
        "<ul class=\"nav navbar-nav\">" +
        "<li class=\"active\"><a href=\"SinglePage.html\">Single Game</a></li>" +
        "<li><a href=\"MultiPage.html\">Multiplayer Game</a></li>" +
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

function logOff() {
    sessionStorage.removeItem("user");
    window.location.replace("../html/MainPage.html");
}