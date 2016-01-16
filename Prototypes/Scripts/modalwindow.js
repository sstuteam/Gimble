$(document).ready(function(){
    //показ формы входа
    $("#btnShowLogin").click(function () {
        $("#loginModal").modal('show');
    });

    var showModalReg = false;
    //при нажатии на кнопку зарегистрироваться
    $("#registerLink").click(function (event) {
        //предотвратить переход по ссылке
        event.preventDefault();
        //отобразить модальное окно #registerModal
        showModalReg = true;
        //скрыть 1 модальное окно
        $("#loginModal").modal('hide');
    });

    //после скрытия 1 модального окна
    $("#loginModal").on('hidden.bs.modal', function () {
        //если переменная showModalReg равна true, то
        if (showModalReg == true) {
            //отобразить модальное окно, содержащее заказ
            $("#registerModal").modal('show');
            //присвоить переменной showModalOrder значение false
            showModalReg = false
        }
    });
});