$(document).ready(function () {

    $.validator.addMethod(
        "currency",
        function (value, element, regexp) {
            return this.optional(element) || regexp.test(value);
        },
        "Must be a valid number with two decimal places (eg. 12.34)."
    );

    $("#PayRate").rules("add", { currency: /^[0-9]+[.][0-9]{2}$/ });

    $(".js-delete").click(function () {
        bootbox.confirm("Are you sure you want to delete this job?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/jobs/" + $(".js-delete").data("job-id"),
                    method: "DELETE",
                    success: function () {
                        window.location.replace("/Jobs");
                    }
                });
            }
        });
    });

});