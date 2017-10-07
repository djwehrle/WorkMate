"use strict";

var baseURL = "";

$(document).ready(function () {

    if (location.hostname.toUpperCase() !== "LOCALHOST") {
        baseURL = "/WorkMate";
    }

    $("#paychecks").DataTable({
        ajax: {
            url: baseURL + "/api/Paychecks",
            dataSrc: ""
        },
        columns: [
            { data: "ID"},
            { data: "PayDate" },
            { data: "JobName" },
            { data: "StartDate" },
            { data: "EndDate" },
            {
                data: function (data) {
                    return "$" + data.NetPay.toFixed(2).toLocaleString();
                }
            }

        ],
        initComplete: function () {
            PostTableLoad();
        }
    });

    $("#refresh").click(function () {
        Refresh();
    });

});

function PostTableLoad() {
    $("#paychecks tbody tr").click(function () {
        location.href = "/paychecks/paycheck?paycheckID=" + $("td", this).first().text();
    });
}

function Refresh() {
    $("#paychecks").DataTable().ajax.reload();
}