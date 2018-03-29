$("#waitingForPlayers").hide();
$("#gameDiv").hide();

$("#startGame").click(function () {
	$("#startDiv").hide();
	$("#waitingForPlayers").show();
});

let connection = new signalR.HubConnection('/chat');
connection.start();

connection.on('onSend', data => {
	$("#txtChat").append(data + "\n");
	scrollToBottom();
});

$("#btnSend").click(() => {
    let message = userName + ": " + $("#txtMessage").val();
    if ($("#txtMessage").val() != "") {
	    connection.invoke('Send', message);
	    $("#txtMessage").val("");
    }
});

let inputtxtMessage = document.getElementById("txtMessage");
inputtxtMessage.addEventListener("keyup", function (event) {
    event.preventDefault();
    if (event.keyCode === 13) {
        document.getElementById("btnSend").click();
    }
});


function scrollToBottom() {
	$('#txtChat').scrollTop($('#txtChat')[0].scrollHeight);
}