$("#divRegister").show();
$("#divFindOpponentPlayer").hide();
$("#divFindingOpponentPlayer").hide();
$("#divGame").hide();
$("#divGameInformation").hide();
$("#divPreviewImage").hide();
$("#divOpponentPlayer").hide();

let hubUrl = '/gameHub';
let httpConnection = new signalR.HttpConnection(hubUrl);
let hubConnection = new signalR.HubConnection(httpConnection);
var playerName = "";
var playerImage = "";
var opponentImage = "";
var hash = "#";
hubConnection.start();

$("#image").change(function () {
	readURL(this);
});

function readURL(input) {
	if (input.files && input.files[0]) {
		var reader = new FileReader();
		reader.onload = imageIsLoaded;
		reader.readAsDataURL(input.files[0]);
	}
}

function imageIsLoaded(e) {
	if (e.target.result) {
		$('#previewImage').attr('src', e.target.result);
		$("#divPreviewImage").show();
	}
}

hubConnection.on('registrationComplete', data => {
	$("#divRegister").hide();
	$('#divOpponentPlayer').hide();
	$("#divFindOpponentPlayer").show();
});

hubConnection.on('waitingForOpponent', data => {
	$('#divInfo').html("<br/><span><strong> Waiting for <i>" + data + "</i> to make a move.</strong></span>");
});

hubConnection.on('opponentFound', (data, image) => {
	$('#divFindOpponentPlayer').hide();
	$('#divFindingOpponentPlayer').hide();
	$('#divGame').show();
	$('#divGameInformation').show();
	$('#divOpponentPlayer').show();
	opponentImage = image;
	$('#opponentImage').attr('src', opponentImage);
	$('#divGameInfo').html("<br/><span><strong> Hey " + playerName + "! You are playing against <i>" + data + "</i></strong></span>");
	$("#divGameBoard").html(" ");
	for (var i = 0; i < 9; i++) {
        $("#divGameBoard").append("<span class='marker' id=" + i + " style='display:block;border:2px solid #493F0B;border-radius:5px;height:50px;width:50px;float:left;margin:5px;'> </span>");
	}
});

hubConnection.on('opponentNotFound', data => {
	$('#divFindOpponentPlayer').hide();
	$('#divFindingOpponentPlayer').show();
});

hubConnection.on('opponentDisconnected', data => {
	$("#divRegister").hide();
	$('#divGame').hide();
	$('#divGameInfo').html(" ");
	$('#divInfo').html("<br/><span><strong>Your opponent disconnected or left the battle! You are the winner ! Hip Hip Hurray!!!</strong></span>");
	setTimeout(function () { $('#divFindOpponentPlayer').show(); }, 2000);
});

hubConnection.on('waitingForMove', data => {
	$('#divInfo').html("<br/><span><strong> Your turn <i>" + playerName + "</i>! Make a winning move!</strong></span>");
});

hubConnection.on('moveMade', data => {
	if (data.opponentName === userName) {
		$("#" + data.imagePosition).css('background-image', 'url("/Images/cross.png")');
	}
	else {
		$("#" + data.imagePosition).css('background-image', 'url("/Images/circle.png")');
	}

	$("#" + data.imagePosition).addClass("notAvailable");
	$('#divInfo').html("<br/><strong>Waiting for <i>" + data.OpponentName + "</i> to make a move.</strong>");
});

hubConnection.on('gameOver', data => {
	$('#divGame').hide();
	$('#divInfo').html("<br/><span><strong>Hey " + playerName + "! " + data + " </strong></span>");
	$('#divGameBoard').html(" ");
	$('#divGameInfo').html(" ");
	$('#divOpponentPlayer').hide();
});


$(document).ready(function () {
	setTimeout(function () { registerFunc(); }, 1000);
});


function registerFunc() {
	playerName = userName;
	playerImage = $('#previewImage').attr('src');
	var data = playerName.concat(hash, playerImage);
	hubConnection.invoke('RegisterPlayer', data);
}



$("#btnFindOpponentPlayer").click(function () {
	hubConnection.invoke('FindOpponent');
});

//// Triggers on clicking the grid cell.
$(document).on('click', '.marker', function () {
	if ($(this).hasClass("notAvailable")) { //// Cell is already taken.
		return;
	}
	hubConnection.invoke('MakeAMove', $(this)[0].id); //// Cell is valid, send details to hub.
});




