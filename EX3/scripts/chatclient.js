debugger;
// Declare a proxy to reference the hub
var chat = $.connection.chatHub;
// Create a function that the hub can call to broadcast messages
chat.client.broadcastMessage = function (name, message) {
    // Add the message to the page
    $('#chat').append('<li><strong>' + name
    + '</strong>:&nbsp;&nbsp;' + message + '</li>');
};
// Get the user name and store it to prepend to messages
var username = prompt('Enter your name:');
// Set initial focus to message input box
$('#message').focus();
// Start the connection
$.connection.hub.start().done(function () {
    $('#btnSendMessage').click(function () {
        // Call the Send method on the hub
        chat.server.send(username, $('#message').val());
        // Clear text box and reset focus for next comment
        $('#message').val('').focus();
    });
});