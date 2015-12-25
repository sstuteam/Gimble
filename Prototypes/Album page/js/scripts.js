
$(":button").click(function () {
    event.preventDefault();
});

$("#favour_button").click(function (event) {

    if ($("#favour_button").hasClass('btn-success')) {
        $("#favour_button").removeClass('btn-success');
        $("#favour_button").addClass('btn-default');
        $("#favour_button").text('В избранное');
    }
    else {
        $("#favour_button").removeClass('btn-default');
        $("#favour_button").addClass('btn-success');
        $("#favour_button").text('В избранном');
    }
});

$("#plus-rating-btn").click(function (event) {
    
    if(!($("#plus-rating-btn").hasClass('disabled'))){
        $('.rating').text((parseInt($('.rating').text())) + 1);
    }

    if ($("#minus-rating-btn").hasClass('disabled')) {
        $("#minus-rating-btn").removeClass('disabled');

    }
    else
        $("#plus-rating-btn").addClass('disabled');
});

$("#minus-rating-btn").click(function (event) {

    if (!($("#minus-rating-btn").hasClass('disabled'))) {
        $('.rating').text((parseInt($('.rating').text())) - 1);
    }

    if ($("#plus-rating-btn").hasClass('disabled')) {
        $("#plus-rating-btn").removeClass('disabled');

    }
    else
        $("#minus-rating-btn").addClass('disabled');
});
