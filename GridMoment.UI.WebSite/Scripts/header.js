$(this).scroll(function () {
    var outLimit = $(document).scrollTop();
    if (outLimit > limit)
        $(this).scroll(function () {
            var outLimit = $(document).scrollTop();
            if (outLimit > limit)
                $('.head-page').css({ 'top': '50px', 'transition': 'all 0.5s ease-in-out' });
            else
                $('.head-page').css({ 'top': '0px', 'transition': 'all 0.5s ease-in-out' });
            if (outLimit > scr)                
                $('.head-page').css({ 'top': '-100px', 'transition': 'all 0.5s ease-in-out' });
            else
                $('.head-page').css({ 'top': '0px', 'transition': 'all 0.5s ease-in-out' });
            scr = $(document).scrollTop();
        });
});
