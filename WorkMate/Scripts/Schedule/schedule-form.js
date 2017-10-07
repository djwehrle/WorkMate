$(document).ready(function () {

    $deleteBtn = $(".js-delete");

    $deleteBtn.click(function () {
        bootbox.confirm("Are you sure you want to delete this schedule?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/schedule/" + $deleteBtn.data("schedule-id"),
                    method: "DELETE",
                    success: function () {
                        window.location.replace("/Schedule");
                    }
                });
            }
        });
    });

});