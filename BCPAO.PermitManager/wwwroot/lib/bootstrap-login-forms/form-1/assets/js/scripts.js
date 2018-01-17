
jQuery(document).ready(function() {
	
	/*
	    Navigation (toggle "navbar-no-bg" class)
	*/
	$('.l-form-1').waypoint(function() {
		$('nav').toggleClass('navbar-no-bg');
	});
	
    /*
        Background slideshow
    */
    $('.l-form-1-container').backstretch("assets/img/backgrounds/1.jpg");
    
    /*
        Wow
    */
    new WOW().init();
    
    /*
	    Form validation
	*/
	$('.l-form-1-box input[type="text"], .l-form-1-box input[type="password"], .l-form-1-box textarea').on('focus', function() {
		$(this).removeClass('input-error');
	});
	
	$('.l-form-1-box form').on('submit', function(e) {
		
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
