$("#waitingForPlayers").hide();
$("#gameDiv").hide();

$("#startGame").click(function () {
	$("#startDiv").hide();
	$("#waitingForPlayers").show();
});

let connection = new signalR.HubConnection('/chat');
connection.start();

//var userName = "@Model.UserName.ToString()";

connection.on('onSend', data => {
	$("#txtChat").append(data + "\n");
	scrollToBottom();
});

$("#btnSend").click(() => {
	let message = userName + ": " + $("#txtMessage").val();
	connection.invoke('Send', message);
	$("#txtMessage").val("");
});

//$("#txtChat").change(function () {
//	scrollToBottom();
//});

function scrollToBottom() {
	$('#txtChat').scrollTop($('#txtChat')[0].scrollHeight);
}