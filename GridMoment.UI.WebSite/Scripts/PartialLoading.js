/// <reference path="jquery-1.10.2.intellisense.js" />
/// <reference path="jquery-2.2.0.js" />
(function () {
    $(document.body).on("click", "ajax-loading", function (event) {
        var elem = $(this);
        $.ajax({
            url: elem.attr("href"),
            data: null,
            method: "get",
            success: function (data) {
                $("#main").empty().append(data);
            }
        })
    })
})();