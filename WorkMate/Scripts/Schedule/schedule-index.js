$(document).ready(function () {

    $(".schedule").click(function () {
        location.href = "schedule/edit/?scheduleID=" + $(this).data("schedule-id");
    });

});