﻿/// <reference path="jquery-1.10.2.intellisense.js" />
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

$(function () {
    var trying = false;
    var partialPages = ["#last-story-panel", "#best-story-7", "#best-story-30"];
    var currentIdPanel;
    for (var item in partialPages) {
        if ($(item).hasClass("active"));
        currentIdPanel = item;
    }
    //подгрузка элементов
    function loadItems() {
        $('div#loading').hide();
        if (!trying) {
            trying = true;
            $('div#loading').show();
            $.ajax({ //ajax-запрос
                type: 'GET',
                url: '/Home/Index/' + currentIdPanel,
                success: function (data, status) {
                    if (data != '') {
                        $(currentIdPanel).append(data);
                    }
                    else {
                        page = -1;
                    }
                    trying = false;
                    $('div#loading').hide();
                }
            });
        }
    }
    // обработка события скроллинга
    $(window).scroll(function () {
        if ($(window).scrollTop() ==
            $(document).height() - $(window).height()) {
            loadItems();
        }
    });
})