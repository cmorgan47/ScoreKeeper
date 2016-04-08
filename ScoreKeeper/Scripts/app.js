$(document).ready(function () {
    clearForm();
    listGames();

    $("#add-game").click(addGame);
});

function listGames() {
    $.get("../api/Game", {}, function (data) {
        console.log("fetched games");
        $.each(data, function (ctr, record) {
            $("#games-list").append("<li class='game'>" + record.Name + " - " + record.Description + "</li>");
        });
    });
}

function listMatches() {
    $.get("../api/Match", {}, function (data) {
        console.log("fetched data");
    });
}

function addGame() {
    var data = { Name: $("#game-name").val(), Description: $("#game-description").val(), HighestScoreWins: $("#highest-score-wins").prop("checked") };
    $.post("../api/Game", data, function (data) {
        $("#games-list").empty();
        listGames();
        clearForm();
    });
}

function clearForm() {
    $("#game-name").val("");
    $("#game-description").val("");
    $("#highest-score-wins").prop("checked", true);
}



