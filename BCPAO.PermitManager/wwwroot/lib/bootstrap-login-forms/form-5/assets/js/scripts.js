
function ftnFitFormInWindow() {
    var vpw = $(window).width(),
        vph = $(window).height(),
        ele = $('#divForm'),
        elw = ele.width(),
        elh = ele.height(),
        elo = ele.offset();

    //$(window)
    //	.scrollTop(elo.top + (elh / 2) - (vph / 2))
    //	.scrollLeft(elo.left + (elw / 2) - (vpw / 2));

    //ele.css('left', parseInt((elw / 2) - (vpw / 2), 10));
    ele.css('top', parseInt((vph / 2) - (elh / 2), 10));

    //console.log('left: ' + parseInt((elw / 2) - (vpw / 2), 10));
    //console.log('top: ' + parseInt((elh / 2) - (vph / 2), 10));

    ele.fadeIn();
}

jQuery(document).ready(function () {


    $('.theme-panel-toggler').click(function () {
        alert('Right Panel');
    });

	/*
	    Navigation (toggle "navbar-no-bg" class)
	*/
    $('.l-form-5').waypoint(function () {
        $('nav').toggleClass('navbar-no-bg');
    });

    /*
        Background slideshow
    */
    //$('.l-form-5-container').backstretch("../img/backgrounds/1.jpg");
    $.backstretch("../lib/bootstrap-login-forms/form-5/assets/img/backgrounds/1.jpg");

    ftnFitFormInWindow();
    $(window).resize(function () {
        ftnFitFormInWindow();
    });

    /*
        Wow
    */
    new WOW().init();

    /*
	    Form validation
	*/
    $('.l-form-5-box input[type="text"], .l-form-5-box input[type="password"], .l-form-5-box textarea').on('focus', function () {
        $(this).removeClass('input-error');
    });

    $('.l-form-5-box form').on('submit', function (e) {

        $(this).find('input[type="text"], input[type="password"], textarea').each(function () {
            if ($(this).val() == "") {
                e.preventDefault();
                $(this).addClass('input-error');
            }
            else {
                $(this).removeClass('input-error');
            }
        });

    });

});
