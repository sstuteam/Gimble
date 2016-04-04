$(this).scroll(function () {
    var outLimit = $(document).scrollTop();
    var scr = $(document).scrollTop();
    var limit = $('.headhesive').outerHeight();
    if (outLimit > limit)
        $(this).scroll(function () {
            var outLimit = $(document).scrollTop();
            if (outLimit > limit)
                $('.headhesive').css({ 'top': '50px', 'transition': 'all 0.5s ease-in-out' });
            else
                $('.headhesive').css({ 'top': '0px', 'transition': 'all 0.5s ease-in-out' });
            if (outLimit > scr)                
                $('.headhesive').css({ 'top': '-150px', 'transition': 'all 0.5s ease-in-out' });
            else
                $('.headhesive').css({ 'top': '0px', 'transition': 'all 0.5s ease-in-out' });
            scr = $(document).scrollTop();
        });
});
