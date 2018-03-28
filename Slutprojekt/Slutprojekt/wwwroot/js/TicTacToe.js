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
};

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
	for (var i = 0; i < 9; i++) {
		$("#divGameBoard").append("<span class='marker' id=" + i + " style='display:block;border:2px solid black;height:100px;width:100px;float:left;margin:10px;'>" + i + "</span>");
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
	$('#divInfo').html("<br/><span><strong>Hey " + playerName + "! Your opponent disconnected or left the battle! You are the winner ! Hip Hip Hurray!!!</strong></span>");

});

hubConnection.on('waitingForMove', data => {
	$('#divInfo').html("<br/><span><strong> Your turn <i>" + playerName + "</i>! Make a winning move!</strong></span>");
});

hubConnection.on('moveMade', data => {
	if (data.image === playerImage) {
		$("#" + data.ImagePosition).addClass("notAvailable");
		$("#" + data.ImagePosition).css('background-image', 'url(' + data.Image + ')');
		console.log("dataimage == playerimage")
		$('#divInfo').html("<br/><strong>Waiting for <i>" + data.OpponentName + "</i> to make a move.</strong>");
	}
	else {
		if (data.opponentName === userName) {
			$("#" + data.imagePosition).css('background-color', 'red');
		}
		else {
			$("#" + data.imagePosition).css('background-color', 'blue');
		}

		$("#" + data.imagePosition).addClass("notAvailable");
		$('#divInfo').html("<br/><strong>Waiting for <i>" + data.OpponentName + "</i> to make a move.</strong>");
	}
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
};



$("#btnFindOpponentPlayer").click(function () {
	hubConnection.invoke('FindOpponent');
});

//// Triggers on clicking the grid cell.
$(document).on('click', '.marker', function () {
	if ($(this).hasClass("notAvailable")) { //// Cell is already taken.
		return;
	}
	console.log("Klick klick klick!!!");
	hubConnection.invoke('MakeAMove', $(this)[0].id); //// Cell is valid, send details to hub.
});