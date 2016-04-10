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
        $("#add-match-form").hide();
        $("#score1").val("");
        $("#score2").val("");
    });
}

sKa.addGame = function () {
    var data = { Name: $("#game-name").val(), Description: $("#game-description").val(), HighestScoreWins: $("#highest-score-wins").prop("checked") };
    $.post("../api/Game", data, function (data) {
        $("#games-list").empty();
        sKa.listGames();
        sKa.clearForm();
        $("#add-game-form").hide();
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
    if ($("#" +this.id).hasClass("selected")){
        $("#" + this.id).removeClass("selected")
        $("#game-details-container").hide();
        $("#show-match-form-button").show();

    }else{
        $(".selected").removeClass("selected");
        $("#game-details-container").show();
        $("#" + this.id).addClass("selected")
         sKa.showGame();
        $("#show-match-form-button").show();
    }
}

sKa.showGameForm = function () {
    if ($("#add-game-form").is(":visible")) {
        $("#add-game-form").hide();
        $("#show-game-form-button").removeClass("selected");
        sKa.clearForm();
    } else {
        $("#add-game-form").show();
        $("#game-name").focus();
        $("#show-game-form-button").addClass("selected");
    }
}

sKa.showMatchForm = function () {
    if ($("#add-match-form").is(":visible")) {
        $("#show-match-form-button").removeClass("selected");
        $("#add-match-form").hide();
    } else {
        $("#show-match-form-button").addClass("selected");
        $("#add-match-form").show();
        $("#player1").focus();
    }
}

$(document).ready(function () {
    sKa.clearForm();
    sKa.listGames();

    $("#add-game-form").hide();
    $("#add-match-form").hide();
    $("#show-match-form-button").hide();

    $("#add-game").click(sKa.addGame);
    $("#add-match").click(sKa.addMatch);
    $("#show-game-form-button").click(sKa.showGameForm);
    $("#show-match-form-button").click(sKa.showMatchForm);
});
