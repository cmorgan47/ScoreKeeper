var sKa = sKa || {};

sKa.listGames = function() {
    $.get("../api/Game", {}, function (data) {
        $.each(data, function (ctr, record) {
            $("#games-list").append("<li id='"+ record.Id +"' class='game-title'>" + record.Name + "</li>");
        });
        $(".game-title").click(sKa.selectGame);
    });
}


sKa.addMatch = function () {
    var data =
        {
            GameId: $(".selected")[0].id,
            Scores : 
                [
                    { PlayerName: $("#player1").val(), Points: $("#score1").val() },
                    { PlayerName: $("#player2").val(), Points: $("#score2").val() }
                ]
        }
    $.post("../api/Match", data, function (res) {
        sKa.showGame();
    });
}

sKa.addGame = function () {
    var data = { Name: $("#game-name").val(), Description: $("#game-description").val(), HighestScoreWins: $("#highest-score-wins").prop("checked") };
    $.post("../api/Game", data, function (data) {
        $("#games-list").empty();
        listGames();
        clearForm();
    });
}

sKa.clearForm = function () {
    $("#game-name").val("");
    $("#game-description").val("");
    $("#highest-score-wins").prop("checked", true);
}

sKa.showGame = function ()
{
    
    $.get("../api/Game/" + $(".selected")[0].id, {}, function (data) {
        $("#game-details-title").html(data.Name);
        $("#game-details-description").html(data.Description);
        $("#game-details-match-list").empty();
        if (data.Matches) {
            $.each(data.Matches, function (ctr, match) {
                $("#game-details-match-list").append("<li>" + match.Description +  "</li>");
            });
        }

    });

}

sKa.selectGame = function () {
    $(".selected").removeClass("selected");
    $("#" + this.id).addClass("selected")
    sKa.showGame();
}


$(document).ready(function () {
    sKa.clearForm();
    sKa.listGames();

    $("#add-game").click(sKa.addGame);
    $("#add-match").click(sKa.addMatch);

});
