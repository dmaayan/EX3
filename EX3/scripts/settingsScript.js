// load the default data id available
$(document).ready(function () {
    if (localStorage.mazeRows) {
        $("#mazeRows").val(localStorage.mazeRows);
    }
    if (localStorage.mazeCols) {
        $("#mazeCols").val(localStorage.mazeCols);
    }
    if (localStorage.searchAlgo) {
        $("#searchAlgo").val(localStorage.searchAlgo);
    }
});

function saveSettings() {
    // save the settings
    localStorage.setItem("mazeRows", $("#mazeRows").val());
    localStorage.setItem("mazeCols", $("#mazeCols").val());
    localStorage.setItem("searchAlgo", $("#searchAlgo").val());
    window.location = "../html/MainPage.html";
}