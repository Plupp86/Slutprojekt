

$("#divRegister").show();
$("#divFindOpponentPlayer").hide();
$("#divFindingOpponentPlayer").hide();
$("#divGame").hide();
$("#divGameInformation").hide();
$("#divPreviewImage").hide();
$("#divOpponentPlayer").hide();

let hubUrlMemory = '/gameHubMemory';
let httpConnectionMemory = new signalR.HttpConnection(hubUrlMemory);
let hubConnectionMemory = new signalR.HubConnection(httpConnectionMemory);
var playerName = "";
var playerImage = "";
var opponentImage = "";
var hash = "#";
let positionsArray = [];
let positionsArrarRealValue = [];

hubConnectionMemory.start();


hubConnectionMemory.on('getPositionsArrayMemory', data => {
    console.log("Så här ser min getPositionsArrayMemory ut: " + data);
   ///nsole.log("Så här ser min getPositionsArrayMemory ut: " + data.returnList);
   // console.log("Så här ser min getPositionsArrayMemory ut: " + data.Array);
    positionsArrarRealValue = data;
});

hubConnectionMemory.on('waitingForOpponentMemory', data => {
    $('#divInfo').html("<br/><span><strong> Waiting for <i>" + data + "</i> to make a move.</strong></span>");
});

//Finish your turn
hubConnectionMemory.on('waitingForMoveMemory', data => {
    $('#divInfo').html("<br/><span><strong> Your turn <i>" + playerName + "</i>! Make a winning move!</strong></span>");
});

//What character you will be shown
hubConnectionMemory.on('moveMadeMemory', data => {
    console.log("Vilken user är jag: " + userName)
    if (data.opponentName === userName) {
        console.log(userName + " if movemade");
        console.log(data.imagePosition + " if movemade");
        $("#" + data.imagePosition).css('background-color', 'blue');
    }
    else {
        console.log(userName + " else movemade");
        console.log(data.imagePosition + " else movemade");
        $("#" + data.imagePosition).css('background-color', 'red');
    }
    console.log("min motståndare: " + data.opponentName);
    console.log(data);
    $("#" + data.imagePosition).addClass("notAvailable");
    $('#divInfo').html("<br/><strong>Waiting for <i>" + data.opponentName + "</i> to make a move.</strong>");
});

let waitingforMoveCheckAnswer = false;

hubConnectionMemory.on('waitingforMoveCheck', data => {
    waitingforMoveCheckAnswer = data;
    console.log("hubConnectionMemory.on('waitingforMoveCheck' " + data)
});

//// Triggers on clicking the grid cell.
$(document).on('click', '.marker', function () {
    hubConnectionMemory.invoke('WaitingforMoveCheck');
    console.log('(document).on(click, .marker ' + waitingforMoveCheckAnswer);

    if (waitingforMoveCheckAnswer) {

        if ($(this).hasClass("notAvailable")) { //// Cell is already taken.
            return;
        }

        // positionsArrarRealValue;

        if (positionsArray[0] == null) {
            positionsArray.push($(this)[0].id);
            //$("#" + positionsArray[0]).css('background-color', 'yellow');
            if (positionsArray[0] == positionsArrarRealValue[0]) {
                $("#" + positionsArray[0]).css('background-color', 'yellow');
            }
            else if (positionsArray[0] == positionsArrarRealValue[1]) {
                $("#" + positionsArray[0]).css('background-color', 'brown');
            }
            else if (positionsArray[0] == positionsArrarRealValue[2]) {
                $("#" + positionsArray[0]).css('background-color', 'purple');
            }
            else if (positionsArray[0] == positionsArrarRealValue[3]) {
                $("#" + positionsArray[0]).css('background-color', 'orange');
            }
            else if (positionsArray[0] == positionsArrarRealValue[4]) {
                $("#" + positionsArray[0]).css('background-color', 'black');
            }
            else if (positionsArray[0] == positionsArrarRealValue[5]) {
                $("#" + positionsArray[0]).css('background-color', 'cyan');
            }
            else if (positionsArray[0] == positionsArrarRealValue[6]) {
                $("#" + positionsArray[0]).css('background-color', 'Darkgreen');
            }
            else if (positionsArray[0] == positionsArrarRealValue[7]) {
                $("#" + positionsArray[0]).css('background-color', 'gray');
            }
            else if (positionsArray[0] == positionsArrarRealValue[8]) {
                $("#" + positionsArray[0]).css('background-color', 'pink');
            }
            else if (positionsArray[0] == positionsArrarRealValue[9]) {
                $("#" + positionsArray[0]).css('background-color', 'DarkGoldenRod');
            }
            else if (positionsArray[0] == positionsArrarRealValue[10]) {
                $("#" + positionsArray[0]).css('background-color', 'DarkMagenta');
            }
            else if (positionsArray[0] == positionsArrarRealValue[11]) {
                $("#" + positionsArray[0]).css('background-color', 'DarkSalmon');
            }
            else if (positionsArray[0] == positionsArrarRealValue[12]) {
                $("#" + positionsArray[0]).css('background-color', 'DodgerBlue');
            }
            else if (positionsArray[0] == positionsArrarRealValue[13]) {
                $("#" + positionsArray[0]).css('background-color', 'IndianRed');
            }
            else if (positionsArray[0] == positionsArrarRealValue[14]) {
                $("#" + positionsArray[0]).css('background-color', 'LawnGreen ');
            }
            else  if (positionsArray[0] == positionsArrarRealValue[15]) {
                $("#" + positionsArray[0]).css('background-color', 'LightGreen');
            }

            console.log($(this)[0].id);
            console.log("klick 1");
            console.log(positionsArray);
            return;
        }
        else
        {
            positionsArray.push($(this)[0].id);
            console.log(positionsArray+" inne i else sats");
            //$("#" + positionsArray[1]).css('background-color', 'yellow');
            if (positionsArray[1] == positionsArrarRealValue[0]) {
                $("#" + positionsArray[1]).css('background-color', 'yellow');
            }
            else if (positionsArray[1] == positionsArrarRealValue[1]) {
                $("#" + positionsArray[1]).css('background-color', 'brown');
            }
            else if (positionsArray[1] == positionsArrarRealValue[2]) {
                $("#" + positionsArray[1]).css('background-color', 'purple');
            }
            else if (positionsArray[1] == positionsArrarRealValue[3]) {
                $("#" + positionsArray[1]).css('background-color', 'orange');
            }
            else if (positionsArray[1] == positionsArrarRealValue[4]) {
                $("#" + positionsArray[1]).css('background-color', 'black');
            }
            else if (positionsArray[1] == positionsArrarRealValue[5]) {
                $("#" + positionsArray[1]).css('background-color', 'cyan');
            }
            else if (positionsArray[1] == positionsArrarRealValue[6]) {
                $("#" + positionsArray[1]).css('background-color', 'Darkgreen');
            }
            else if (positionsArray[1] == positionsArrarRealValue[7]) {
                $("#" + positionsArray[1]).css('background-color', 'gray');
            }
            else if (positionsArray[1] == positionsArrarRealValue[8]) {
                $("#" + positionsArray[1]).css('background-color', 'pink');
            }
            else if (positionsArray[1] == positionsArrarRealValue[9]) {
                $("#" + positionsArray[1]).css('background-color', 'DarkGoldenRod');
            }
            else if (positionsArray[1] == positionsArrarRealValue[10]) {
                $("#" + positionsArray[1]).css('background-color', 'DarkMagenta');
            }
            else if (positionsArray[1] == positionsArrarRealValue[11]) {
                $("#" + positionsArray[1]).css('background-color', 'DarkSalmon');
            }
            else if (positionsArray[1] == positionsArrarRealValue[12]) {
                $("#" + positionsArray[1]).css('background-color', 'DodgerBlue');
            }
            else if (positionsArray[1] == positionsArrarRealValue[13]) {
                $("#" + positionsArray[1]).css('background-color', 'IndianRed');
            }
            else if (positionsArray[1] == positionsArrarRealValue[14]) {
                $("#" + positionsArray[1]).css('background-color', 'LawnGreen');
            }
            else if (positionsArray[1] == positionsArrarRealValue[15]) {
                $("#" + positionsArray[1]).css('background-color', 'LightGreen');
            }

            console.log($(this)[0].id);
            console.log("klick 2");
            console.log("Klick to MakeAMoveMemory klick!!!");
            console.log(positionsArray);

            setTimeout(function () { callOnMakeAMoveMemory(); }, 3000);
        }

        waitingforMoveCheckAnswer = false;
    }
});


hubConnectionMemory.on('noMatch', data => {
    $('#divInfo').html("<br/><span><strong>No match for player: " + playerName + "! " + data + " </strong></span>");
    console.log(data + "noMatch");
});

hubConnectionMemory.on('gameOverMemory', data => {
    $('#divGame').hide();
    $('#divInfo').html("<br/><span><strong>Hey " + playerName + "! " + data + " </strong></span>");
    $('#divGameBoard').html(" ");
    $('#divGameBoardMemory').html(" ");
    $('#divGameInfo').html(" ");
    $('#divOpponentPlayer').hide();
});

//init Game
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

hubConnectionMemory.on('registrationCompleteMemory', data => {
    $("#divRegister").hide();
    $('#divOpponentPlayer').hide();
    $("#divFindOpponentPlayer").show();
});

hubConnectionMemory.on('opponentFoundMemory', (data, image) => {
    $('#divFindOpponentPlayer').hide();
    $('#divFindingOpponentPlayer').hide();
    $('#divGame').show();
    $('#divGameInformation').show();
    $('#divOpponentPlayer').show();
    opponentImage = image;
    $('#opponentImage').attr('src', opponentImage);
    $('#divGameInfo').html("<br/><span><strong> Hey " + playerName + "! You are playing against <i>" + data + "</i></strong></span>");
    $("#divGameBoard").html(" ");
    $("#divGameBoardMemory").html(" ");
    for (var i = 0; i < 16; i++) {
        $("#divGameBoard").append("<span class='marker' id=" + i + " style='display:block;border:2px solid black;height:25px;width:25px;float:left;margin:5px;'> </span>");
    }
    hubConnectionMemory.invoke('GetPositionsArrayMemory');
});

$(document).ready(function () {
    setTimeout(function () { registerFuncMemory(); }, 900);

});

//function getPositionsArrayMemory() {
//    //positionsArrarRealValue, anropas när du hittar moståndare
//    hubConnectionMemory.invoke('GetPositionsArrayMemory');
//};

function callOnMakeAMoveMemory() {
    hubConnectionMemory.invoke('MakeAMoveMemory', positionsArray);
    $("#" + positionsArray[0]).css('background-color', '#F5F6D4');
    $("#" + positionsArray[1]).css('background-color', '#F5F6D4');
    positionsArray = [];
};

function registerFuncMemory() {
    playerName = userName;
    playerImage = $('#previewImage').attr('src');
    var data = playerName.concat(hash, playerImage);
    hubConnectionMemory.invoke('RegisterPlayerMemory', data);
};

$("#btnFindOpponentPlayerMemory").click(function () {
    hubConnectionMemory.invoke('FindOpponentMemory');
});

hubConnectionMemory.on('opponentNotFoundMemory', data => {
    $('#divFindOpponentPlayer').hide();
    $('#divFindingOpponentPlayer').show();
});

hubConnectionMemory.on('opponentDisconnectedMemory', data => {
    $("#divRegister").hide();
    $('#divGame').hide();
    $('#divGameInfo').html(" ");
    $('#divInfo').html("<br/><span><strong>Your opponent disconnected or left the battle! You are the winner ! Hip Hip Hurray!!!</strong></span>");
    setTimeout(function () { $('#divFindOpponentPlayer').show(); }, 2000);
});



