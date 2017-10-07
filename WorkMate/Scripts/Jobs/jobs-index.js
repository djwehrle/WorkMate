$(document).ready(function () {

    $(".job").click(function () {
        location.href = "/jobs/edit?jobID=" + $(this).data("job-id");
    });

});