
function validateInputs() {
    var userNameIn = document.getElementById("userName").value;
    var userPasswordIn = document.getElementById("password").value;
    if ((userNameIn.length < 1) || (userPasswordIn.length < 1)) {
        alert("Input error!");
        return;
    }
    var url = "../api/Users/" + userNameIn + "/" + userPasswordIn;
    $.get(url).fail(function (xhr) {
        if (xhr.status == 404 || xhr.status == 400) {
            alert("Input information is incorrect. please try again");

        } else {
            alert("Failed to connect to server");
        }
    }).done(function (user) {
        console.log("Connected!");
        sessionStorage.user = user.userName;
    });
}