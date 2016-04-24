$(function () {
    var trying = false;
    var partialPages = ["#panel1", "#panel2"];
    var currentIdPanel;
    var url = "";
    for (var item in partialPages) {
        if ($(item).hasClass("active"));
        currentIdPanel = item;
    }
    switch (item) {
        case "#panel1":
            url = "/Post/ShowUserNews/";
            break;
        case "#panel2":
            url = "/Post/ShowUserBookmarks/";
            break;
        default:

    }
    function loadItems() {
        $('div#loading').hide();
        if (!trying) {
            trying = true;
            $('div#loading').show();
            $.ajax({ //ajax-запрос
                type: 'GET',
                url: url + currentIdPanel,
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
})();