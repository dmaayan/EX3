
function validateInputs() {
    // validate all inputs
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
    // request to register user to data base
    var url = "../api/Users/";
    var userData = {
        userName: userNameIn,
        password: userPasswordIn,
        email: userEmailIn,
        StatisticsUserName: { UserName: userNameIn, Wins: 0, Losses: 0 }
    };
    $.post(url, userData).fail(function (xhr) {
        // user name taken error
        if (xhr.responseJSON == "UserName") {
            alert("User name already taken!");
        } else if (xhr.responseJSON == "Db") {
            // data base error
            console.log("Data base error");
        }
    }).done(function () {
        // user have been saved
        console.log("User have been saved into database");
        window.location.replace("../html/LoginPage.html");
    });
}