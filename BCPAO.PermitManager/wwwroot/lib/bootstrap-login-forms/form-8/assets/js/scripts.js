
jQuery(document).ready(function() {
	
	/*
	    Navigation (toggle "navbar-no-bg" class)
	*/
	$('.l-form-8').waypoint(function() {
		$('nav').toggleClass('navbar-no-bg');
	});
	
    /*
        Background slideshow
    */
    $('.l-form-8-container').backstretch("assets/img/backgrounds/1.jpg");
    
    /*
        Wow
    */
    new WOW().init();
    
    /*
	    Form validation
	*/
	$('.l-form-8-box input[type="text"], .l-form-8-box input[type="password"], .l-form-8-box textarea').on('focus', function() {
		$(this).removeClass('input-error');
	});
	
	$('.l-form-8-box form').on('submit', function(e) {
		
		$(this).find('input[type="text"], input[type="password"], textarea').each(function(){
			if( $(this).val() == "" ) {
				e.preventDefault();
				$(this).addClass('input-error');
			}
			else {
				$(this).removeClass('input-error');
			}
		});
		
	});
	
});
