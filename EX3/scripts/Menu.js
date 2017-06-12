
document.addEventListener("DOMContentLoaded", function(event) {
    var div = document.getElementById("nav_div");
    div.innerHTML = "<nav class=\"navbar navbar-default\" role=\"navigation\">" +
        "<div class=\"navbar-header\">" +
        "<a class=\"navbar-brand\" href=\"#\">Maze</a>" +
        "</div >" +
        "<div>" +
        "<ul class=\"nav navbar-nav\">" +
        " <li class=\"active\"><a href=\"SingelPage.html\">Single Game</a></li>" +
        " <li><a href=\"MultiPage.html\">Multiplayer Game</a></li>" +
        "<li><a href=\"SettingsPage.html\">Settings</a></li>" +
        "<li><a href=\"RankingPage.html\">User Rankings</a></li>" +
        "<li style=\"margin-left:100px\"><a href=\"RegisterPage.html\">Register</a></li>" +
        "<li><a href=\"LoginPage.html\">Login</a></li>" +
        "</ul>" +
        "</div>" +
        "</nav >";
});
//document.onload = function(event) {
//    var div = document.getElementById("nav_div");
//    div.innerHTML = "<nav class=\"navbar navbar-default\" role=\"navigation\">" +
//        "<div class=\"navbar-header\">" +
//        "<a class=\"navbar-brand\" href=\"#\">Maze</a>" +
//        "</div >" +
//        "<div>" +
//        "<ul class=\"nav navbar-nav\">" +
//        " <li class=\"active\"><a href=\"SingelPage.html\">Single Game</a></li>" +
//        " <li><a href=\"MultiPage.html\">Multiplayer Game</a></li>" +
//        "<li><a href=\"SettingsPage.html\">Settings</a></li>" +
//        "<li><a href=\"RankingPage.html\">User Rankings</a></li>" +
//        "<li style=\"margin-left:100px\"><a href=\"RegisterPage.html\">Register</a></li>" +
//        "<li><a href=\"LoginPage.html\">Login</a></li>" +
//        "</ul>" +
//        "</div>" +
//        "</nav >";
//}