var sKa = sKa || {};

sKa.listGames = function() {
    $.get("../api/Game", {}, function (data) {
        $.each(data, function (ctr, record) {
            $("#games-list").append("<li id='"+ record.Id +"' class='game-title'>" + record.Name + "</li>");
        });
        $(".game-title").click(sKa.showGame);
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

sKa.showGame = function ()
{
    $(".selected").removeClass("selected");
    $.get("../api/Game/" + this.id, {}, function (data) {
        $('#' + data.Id).addClass("selected");
        $("#game-details-title").html(data.Name);
        $("#game-details-description").html(data.Description);
        $("#game-details-match-list").empty();
        if (data.MatchIds) {
            $.each(data.MatchIds, function (ctr, match) {
                $("#game-details-match-list").append("<li>" + match + "</li>");
            });
        }

    });

}




$(document).ready(function () {
    sKa.clearForm();
    sKa.listGames();

    $("#add-game").click(sKa.addGame);
});
