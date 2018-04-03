$("#waitingForPlayers").hide();
$("#gameDiv").hide();

$("#startGame").click(function () {
    $("#startDiv").hide();
    $("#waitingForPlayers").show();
});

let chat = new signalR.HubConnection('/chat');

chat.start();
//.done(function () {
//	chat.invoke('GetOnlineUsers')
//		.done(function () { chat.invoke('RegisterChatUser', userName) });	
//});



chat.on('listOfChatUsers', data => {
    $("#onlineUsers").empty();
    data.forEach(function (element) {
        $("#onlineUsers").append(element.userName)
        var br = document.createElement('br');
        $("#onlineUsers").append(br);
    })
    //$("#onlineUsers").append(data[0].UserName);
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

$(document).ready(function () {
    setTimeout(function () { chat.invoke('GetOnlineUsers'); }, 500);
});

$(document).ready(function () {
    setTimeout(function () { chat.invoke('RegisterChatUser', userName); }, 1000);
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

