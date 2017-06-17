/// <reference path="C:\Users\nweichse\Source\Repos\EX3\EX3\html/LoginPage.html" />

function validateInputs() {
    var userNameIn = document.getElementById("userName").value;
    var userEmailIn = document.getElementById("email").value;
    var userValidPassIn = document.getElementById("validatePassword").value;
    var userPasswordIn = document.getElementById("password").value;
    if ((userNameIn.length < 1)
        || (!(userPasswordIn == userValidPassIn))
        || (!userEmailIn.includes('@'))
        || (userEmailIn.length < 3)) {
        alert("Input error!");
        return;
    }
    var url = "../api/Users/" + userNameIn;
    $.get(url).fail(function (xhr) {
        if (xhr.status == 404) {
            $.post("../api/Users",
                {
                    userName: userNameIn,
                    password: userPasswordIn,
                    email: userEmailIn,
                    StatisticsUserName: { UserName: userNameIn, Wins: 0, Losses: 0 }
                }).fail(function (xhr1) {
                    console.log("Failed to add user")
                }).done(function () {
                    console.log("User have been saved into database");
                    window.location.replace("../html/LoginPage.html");
                });
        } else {
            alert("Failed to connect to server");
        }
    }).done(function () {
        alert("Name already taken");
    });
}