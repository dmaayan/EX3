
function validateInputs() {
    // check the inputs
    var userNameIn = document.getElementById("userName").value;
    var userPasswordIn = document.getElementById("password").value;
    if ((userNameIn.length < 1) || (userPasswordIn.length < 1)) {
        alert("Input error!");
        return;
    }
    // send message to te server for login
    var url = "../api/Users/" + userNameIn + "/" + userPasswordIn;
    // get user info from the server
    $.get(url).fail(function (xhr) {
        if (xhr.status == 404 || xhr.status == 400) {
            alert("Input information is incorrect. please try again");
        } else {
            alert("Failed to connect to server");
        }
    }).done(function (user) {
        // keep user name
        console.log("Connected!");
        sessionStorage.user = user.userName;
        // change window location
        window.location.replace("../html/MainPage.html");
    });
}