
var jqueryTable = $("#rankTable");
jqueryTable.ready(function () {
    var url = "../api/Statistics"
    $.get(url).fail(function () {
        alert("Failed to get ranking from server");
    }).done(function (data) {
        jqueryTable.append("<tr><th>Position</th><th>User Name</th><th>Wins</th><th>Losses</th></tr>");
        var tdClass = ["evenTd", "oddTd"];
        for (var i = 0; i < data.length; i++) {
            var stat = data[i];
            var tr = "<tr class=\"" + tdClass[i % 2] + "\"><td>" + (i + 1) + "</td><td>" + stat.UserName + "</td><td>" + stat.Wins + "</td><td>" + stat.Losses + "</td></tr>";
            jqueryTable.append(tr);
        }
    })
})