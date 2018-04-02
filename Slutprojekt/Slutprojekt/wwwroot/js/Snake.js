
    let hubUrlSnake = '/gameHubSnake';
    let httpConnectionSnake = new signalR.HttpConnection(hubUrlSnake);
    let connectionSnake = new signalR.HubConnection(httpConnectionSnake);
    var playerName = "";
    var playerImage = "";
    var opponentImage = "";
    var hash = "#";
    
    connectionSnake.start()

connectionSnake.on('registrationCompleteSnake', data => {
        $("#divRegister").hide();
        $('#divOpponentPlayer').hide();
        $("#divFindOpponentPlayer").show();
    });

    $("#btnFindOpponentPlayerSnake").click(function () {
        console.log("click on btnFindOpponentPlayerSnake");
        connectionSnake.invoke('FindOpponentSnake');
    });

connectionSnake.on('opponentNotFoundSnake', data => {
        console.log("log opponentNotFoundSnake")
        $('#divFindOpponentPlayer').hide();
        $('#divFindingOpponentPlayer').show();
});

connectionSnake.on('opponentDisconnected', data => {
    $("#divRegister").hide();
    $('#divGame').hide();
    $('#divGameInfo').html(" ");
    $('#divInfo').html("<br/><span><strong>Your opponent disconnected or left the battle! You are the winner ! Hip Hip Hurray!!!</strong></span>");
    setTimeout(function () { $('#divFindOpponentPlayer').show(); }, 2000);
});


connectionSnake.on('opponentFound', (data, image) => {
    $('#divFindOpponentPlayer').hide();
    $('#divFindingOpponentPlayer').hide();
    $('#divGame').show();
    $('#divGameInformation').show();
    $('#divOpponentPlayer').show();
    opponentImage = image;
    $('#opponentImage').attr('src', opponentImage);
    $('#divGameInfo').html("<br/><span><strong> Hey " + playerName + "! You are playing against <i>" + data + "</i></strong></span>");
    //$("#divGameBoard").html(" ");
    for (var i = 0; i < 16; i++) {
        $("#divGameBoard").append("<span class='marker' id=" + i + " style='display:block;border:2px solid black;height:25px;width:25px;float:left;margin:5px;'> </span>");
    }
});


    $(document).ready(function () {
        setTimeout(function () { registerFuncSnake(); }, 1000);
    });

    function registerFuncSnake() {
        playerName = userName;
        playerImage = $('#previewImage').attr('src');
        var data = playerName.concat(hash, playerImage);
        connectionSnake.invoke('RegisterPlayerSnake', data);
};







