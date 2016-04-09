var sKa = sKa || {};

sKa.listGames = function() {
    $.get("../api/Game", {}, function (data) {
        console.log("fetched games");
        $.each(data, function (ctr, record) {
            $("#games-list").append("<li class='game'>" + record.Name + " - " + record.Description + "</li>");
        });
    });
}


sKa.listMatches = function () {
    $.get("../api/Match", {}, function (data) {
        console.log("fetched data");
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







$(document).ready(function () {
    sKa.clearForm();
    sKa.listGames();

    $("#add-game").click(addGame);
});
