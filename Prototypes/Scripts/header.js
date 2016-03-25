var html = `
  <div class ="head-page">
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
                        <li><a href="profile.html">Мой профиль</a></li>
                        <li><a href="settings.html">Настройки</a></li>
                        <li class="divider"></li>
                        <li><a href="#">Выйти из себя</a></li>
                    </ul>
                </li>
            </ul>
</div>
`;
$("body").prepend(html);
var options = {
    offset: 400
}
var header = new Headhesive('.head-page', options);
