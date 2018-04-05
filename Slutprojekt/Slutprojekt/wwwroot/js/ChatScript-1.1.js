$("#waitingForPlayers").hide();
$("#gameDiv").hide();

$("#startGame").click(function () {
    $("#startDiv").hide();
    $("#waitingForPlayers").show();
});

let chat = new signalR.HubConnection('/chat');

chat.start();

$(document).ready(function () {
	setTimeout(function () { chat.invoke('GetOnlineUsers'); }, 1000);
});

$(document).ready(function () {
	setTimeout(function () {
		var userAndGame = userName + " - " + gameName;
		chat.invoke('RegisterChatUser', userAndGame);
	}, 1500);
});

chat.on('listOfChatUsers', data => {
    $("#onlineUsers").empty();
    data.forEach(function (element) {
        $("#onlineUsers").append(element.userName)
        var br = document.createElement('br');
        $("#onlineUsers").append(br);
    })
});


chat.on('newChatUser', data => {
    $("#onlineUsers").append('\n');
    $("#onlineUsers").append(data + '\n');
});
//var userName = "@Model.UserName.ToString()";

chat.on('onSend', data => {
    $("#txtChat").append(data + "\n");
    scrollToBottom();
});

$("#btnSend").click(() => {
    let message = userName + ": " + $("#txtMessage").val();
    if ($("#txtMessage").val() !== "") {
        chat.invoke('Send', message);
        $("#txtMessage").val("");
    }
});



$("#listbtn").click(() => {
    chat.invoke('GetOnlineUsers');
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

