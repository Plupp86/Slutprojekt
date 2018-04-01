$("#knappDiv").hide();
$("#divGame").hide();
$("#divWaiting").hide();
$("#divConnecting").show();
$("#divWinner").hide();


let hubUrl = '/mathHub';
let httpConnection = new signalR.HttpConnection(hubUrl);
let mathConnection = new signalR.HubConnection(httpConnection);
var playerName = "";
var playerImage = "";
var opponentImage = "";
var hash = "#";
mathConnection.start();



//MumboJumbo to fins opponents and start game

$(document).ready(function () {
	setTimeout(function () {
		registerFunc();
		$("#divConnecting").hide();
		$("#knappDiv").show();
	}, 1000);

});

function registerFunc() {
	playerName = userName;
	mathConnection.invoke('RegisterPlayer', playerName);
};

mathConnection.on('opponentNotFound', data => {
	console.log("Opponent not found");
	console.log(data);
});


mathConnection.on('registrationComplete', data => {
	console.log("Registration complete");
});

mathConnection.on('opponentDisconnected', data => {
	console.log("opp disc")
});


$("#btnFindOpponentPlayer").click(function () {
	mathConnection.invoke('FindOpponent');
	$("#knappDiv").hide();
	$("#divWaiting").show();
});



//The actual MathGame
//$("#startMathBtn").click(function () {
//	mathConnection.invoke('FindOpponent');
//});

mathConnection.on('hello', data => {
	console.log("hello")
});

//mathConnection.on('startMathGame', data => {
//	console.log("hej hej")
//	console.log(data);
//});

mathConnection.on('winner', data => {
	console.log(data);
	setTimeout(function () {
		$("#divGame").hide();
		$("#divWinner").show();
		$("#divWinner").html(" ");
		$("#divWinner").append("<h3>Game is finished. The winner is " + data + "!</h3>");
	}, 1000);
});

mathConnection.on('startMathGame', data => {
	console.log("starting game");
	$("#divWaiting").hide();
	$("#knappDiv").hide();
	$("#divGame").show();
	$("#divGameBoard").html(" ");
	$("#divQuestion").html(" ");


	console.log(data);
	$("#divQuestion").append("<h3>" + data.player1.name + ": " + data.player1Score + " <br>")
	$("#divQuestion").append("<h3>" + data.player2.name + ": " + data.player2Score + " <br>")
	$("#divQuestion").append("<h3>" + data.questions[0].number1 + " x " + data.questions[0].number2 + " = </h3>")
	for (var i = 0; i < 9; i++) {
		$("#divGameBoard").append("<span class='marker' id='" + i + "' style='display:block;border:2px solid black;height:50px;width:50px;float:left;margin:5px;'><h4>" + data.questions[0].answers[i] + "</h4></span>");
	}
});

mathConnection.on('wrongAnswer', data => {
	console.log("wrong answer");
	$("#" + data).css('background-color', 'red');
});


mathConnection.on('correctAnswer', data => {
	console.log("correct answer");
	$(".marker").addClass('notAvailable');
	$("#" + data).css('background-color', 'green');
});

mathConnection.on('nextQuestion', data => {
	setTimeout(function () {
		var r = data.round;
		$("#divGameBoard").html(" ");
		$("#divQuestion").html(" ");
		$("#divQuestion").append("<h3>" + data.player1.name + ": " + data.player1Score + " <br>")
		$("#divQuestion").append("<h3>" + data.player2.name + ": " + data.player2Score + " <br>")
		$("#divQuestion").append("<h3>" + data.questions[r].number1 + " x " + data.questions[r].number2 + " = </h3> ")
		for (var i = 0; i < 9; i++) {
			$("#divGameBoard").append("<span class='marker' id='" + i + "' style='display:block;border:2px solid black;height:50px;width:50px;float:left;margin:5px;text-align:center;'><h4>" + data.questions[r].answers[i] + "</h4></span>");
		}
	}, 2000);

});

$(document).on('click', '.marker', function () {
	if ($(this).hasClass("notAvailable")) { //// Cell is already taken.
		return;
	}
	mathConnection.invoke('MakeAGuess', $(this)[0].id); //// Cell is valid, send details to hub.
});