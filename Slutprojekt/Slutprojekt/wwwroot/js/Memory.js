

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
var waitingforMoveCheckAnswer = false;

hubConnectionMemory.start();


hubConnectionMemory.on('getPositionsArrayMemory', data => {
    console.log("Så här ser min getPositionsArrayMemory ut: " + data);
    positionsArrarRealValue = data;
});

hubConnectionMemory.on('waitingForOpponentMemory', data => {
    $('#divInfo').html("<br/><span><strong> Waiting for <i>"  + data + "</i> to make a move.</strong></span>");
});

//Finish your turn
hubConnectionMemory.on('waitingForMoveMemory', data => {
    $('#divInfo').html("<br/><span><strong> Your turn <i>" + playerName + "</i>! Make a winning move!</strong></span>");
});

hubConnectionMemory.on('showOpponentMove', data => {

    console.log(data + " showOpponentMove data");
    $("#" + data.imagePosition).css("background-color", data.image);

    var inputShowOpponentPOS = data.imagePosition;
    var inputShowOpponentIMAGE = data.image;

    setTimeout(function () { removeTiles(inputShowOpponentPOS); }, 2500);

    function removeTiles(a) {
        $("#" + a).css('background-color', '#F5F6D4');
    }

    console.log("showOpponentMove se vilken data: " + data);
});



//What character you will be shown
hubConnectionMemory.on('moveMadeMemory', data => {
    console.log("Vilken user är jag: " + userName);
    if (data.opponentName !== userName) {
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


hubConnectionMemory.on('waitingforMoveCheck', data => {
    waitingforMoveCheckAnswer = data;
    console.log("hubConnectionMemory.on('waitingforMoveCheck' " + data);
});

//// Triggers on clicking the grid cell.
$(document).on('click', '.marker', function () {
    //waitingforMoveCheckAnswer = false;
    hubConnectionMemory.invoke('WaitingforMoveCheck');
    console.log('(document).on(click, .marker ' + waitingforMoveCheckAnswer);

    if (waitingforMoveCheckAnswer) {

        if ($(this).hasClass("notAvailable")) { //// Cell is already taken.
            return;
        }

        // positionsArrarRealValue;

        if (positionsArray[0] == null) {
            positionsArray.push($(this)[0].id);
            let index = positionsArray[0];
            //$("#" + positionsArray[0]).css('background-color', 'yellow');
            console.log("värde på positionsArray[0]: " + positionsArray[0]);
            console.log("värde på positionsArrarRealValue[index]: " + positionsArrarRealValue[index]);
            console.log("felsök " + positionsArray[0]);

            if (positionsArrarRealValue[index] == 0) {
                $("#" + positionsArray[0]).css('background-color', 'yellow');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[0], 'yellow');

            }
            else if (positionsArrarRealValue[index] == 1) {
                $("#" + positionsArray[0]).css('background-color', 'brown');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[0], 'brown');

            }
            else if (positionsArrarRealValue[index] == 2) {
                $("#" + positionsArray[0]).css('background-color', 'purple');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[0], 'purple');

            }
            else if (positionsArrarRealValue[index] == 3) {
                $("#" + positionsArray[0]).css('background-color', 'pink');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[0], 'pink');

            }
            else if (positionsArrarRealValue[index] == 4) {
                $("#" + positionsArray[0]).css('background-color', 'orange');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[0], 'orange');

            }
            else if (positionsArrarRealValue[index] == 5) {
                $("#" + positionsArray[0]).css('background-color', 'black');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[0], 'black');
            }
            else if (positionsArrarRealValue[index] == 6) {
                $("#" + positionsArray[0]).css('background-color', 'cyan');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[0], 'cyan');
            }
            else if (positionsArrarRealValue[index] == 7) {
                $("#" + positionsArray[0]).css('background-color', 'Darkgreen');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[0], 'Darkgreen');
            }

            //console.log($(this)[0].id);
            console.log($(this)[0].id + " klick 1, onclickmarker");
            //console.log(positionsArray);
            return;

        }
        else {
            positionsArray.push($(this)[0].id);
            let index2 = positionsArray[1];
            console.log("klick 2, onclickmarker");
            console.log(positionsArray);
            if (positionsArrarRealValue[index2] == 0) {
                $("#" + positionsArray[1]).css('background-color', 'yellow');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[1], 'yellow');
            }
            else if (positionsArrarRealValue[index2] == 1) {
                $("#" + positionsArray[1]).css('background-color', 'brown');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[1], 'brown');

            }
            else if (positionsArrarRealValue[index2] == 2) {
                $("#" + positionsArray[1]).css('background-color', 'purple');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[1], 'purple');

            }
            else if (positionsArrarRealValue[index2] == 3) {
                $("#" + positionsArray[1]).css('background-color', 'pink');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[1], 'pink');

            }
            else if (positionsArrarRealValue[index2] == 4) {
                $("#" + positionsArray[1]).css('background-color', 'orange');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[1], 'orange');

            }
            else if (positionsArrarRealValue[index2] == 5) {
                $("#" + positionsArray[1]).css('background-color', 'black');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[1], 'black');

            }
            else if (positionsArrarRealValue[index2] == 6) {
                $("#" + positionsArray[1]).css('background-color', 'cyan');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[1], 'cyan');

            }
            else if (positionsArrarRealValue[index2] == 7) {
                $("#" + positionsArray[1]).css('background-color', 'Darkgreen');
                hubConnectionMemory.invoke('ShowOpponentMove', positionsArray[1], 'Darkgreen');

            }
            console.log($(this)[0].id + "klick 2, onclickmarker");
            console.log("positionsArray precis innan callmakeamovememory " + positionsArray);
            var input0 = positionsArray[0];
            var input1 = positionsArray[1];

            setTimeout(function () { callOnMakeAMoveMemory(input0, input1); }, 3000);
            function callOnMakeAMoveMemory(a, b) {

                console.log("callOnMakeAMoveMemory har anropas, detta är positionsarray6: " + a + " " + b);
                let array = [a, b];
                console.log("callOnMakeAMoveMemory har anropas, detta är positionsarray7: " + array);
                hubConnectionMemory.invoke('MakeAMoveMemory', array);
                $("#" + array[0]).css('background-color', '#F5F6D4');
                $("#" + array[1]).css('background-color', '#F5F6D4');
                //positionsArray = [];
            }
        }


        waitingforMoveCheckAnswer = false;
        positionsArray = [];
    }
    return;
}
);

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
}

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
    hubConnectionMemory.invoke('WaitingforMoveCheck');

});

$(document).ready(function () {
    setTimeout(function () { registerFuncMemory(); }, 1000);

});



function registerFuncMemory() {
    playerName = userName;
    playerImage = $('#previewImage').attr('src');
    var data = playerName.concat(hash, playerImage);
    hubConnectionMemory.invoke('RegisterPlayerMemory', data);
}

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



