
var html = `
  <header class="head-page row">
            <!--logotype-->
            <div class="logo_pull-left">
                <a id="head-logo" href="index.html"></a>
            </div>
            <ul class="profile-button list-unstyled pull-right">
                <li class="dropdown">
                    <a class="drop-btn dropdown-toggle" data-toggle="dropdown">
                        <span class="glyphicon glyphicon-user"></span>
                        Имя пользователя
                        <span class="glyphicon glyphicon-triangle-bottom"></span>
                    </a>
                    <ul class="dropdown-menu">
					     <li><a href="addpost.html">Создать запись</a></li>
                        <li class="divider"></li>
                        <li><a href="profile.html">Мой профиль</a></li>
                        <li><a href="settings.html">Настройки</a></li>
                        <li class="divider"></li>
                        <li><a href="#">Выйти из себя</a></li>
                    </ul>
                </li>
            </ul>
</header>
`;

$(".container-fluid:first").prepend(html);

(function ($) {
    $(function () {
        var scr = $(document).scrollTop();
        var limit = $('.head-page').outerHeight();
        $(this).scroll(function () {
            var outLimit = $(document).scrollTop();
            if (outLimit > limit)
                $('.head-page').css({ 'top': '50px', 'transition': '1' });
            else
                $('.head-page').css({ 'top': '0px', 'transition': '1' });
            if (outLimit > scr)
                $('.head-page').css({ 'top': '-100px', 'transition': '1' });
            else
                $('.head-page').css({ 'top': '0px', 'transition': '1' });
            scr = $(document).scrollTop();
        });
    });
})(jQuery);
