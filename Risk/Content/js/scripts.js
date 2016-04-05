/*
	*********************************************************************************************************************
	Custom Javascript - Project: Loan Pricing - Acceleris
	Design & Front development: ElEquipoE
	Tasks: Html / Css / jQuery
	Client: The Analytics Boutique
	Version: 1.0
	********************************************************************************************************************
*/



/* *********************************************************************************************************************
 * DOCUMENT READY
 */
$(document).ready(function() {



	/* ************************************************************************************************************** */
	//  VARIABLES
	var myWindow = $(window),
		windowWidth = myWindow.width(),
		windowHeight = myWindow.height(),
		htmlbody = $('html, body'),		
		contentHeight = htmlbody.height(),
		wrapper = $('.wrapper'),
		menu = $('#menu'),
		content = $('.content'),
		menuBt = $('.collapse-menu-bt'),
		scrollUp = $('.scrollup'),
		tablet = false,
		tabletPortrait = false,
		mobile = false;









	/* ************************************************************************************************************** */
	//  CHECKERS

	// DETECT MOBILES & TABLETS (ANDROID & IOS & WINDOWS8 & BLACKBERRY)
	var android = MobileEsp.DetectAndroid();
	var ios = MobileEsp.DetectIos();
	var windows = MobileEsp.DetectWindowsPhone();
	var blackberry = MobileEsp.DetectBlackBerry();
	if (android == true || ios == true || windows == true || blackberry == true) {
		tablet = true;
		if(windowWidth < 768) mobile = true;
		if(windowWidth <= 980) tabletPortrait = true;
	};


	console.log('mobile:' + mobile + ' / tablet:' + tablet + ' / tabletPortrait:' + tabletPortrait);










	/* ************************************************************************************************************** */
	// ON SCROLL EVENTS

	var scrollUpVisible = false;
	var controlScroll = function() {

		var scrollTop = myWindow.scrollTop();

		// SCROLL TO TOP
		if (scrollTop > 250) {
			if (!mobile && !scrollUpVisible) {
				scrollUp.stop(true, false).fadeIn(500,'linear');
				scrollUpVisible = true;
			}
		} else {
			if (!mobile && scrollUpVisible) {
				scrollUp.stop(true, false).fadeOut(400, 'linear');
				scrollUpVisible = false;
			}
		}

	};
	
	controlScroll();

	// SCROLL UP BUTTON
	scrollUp.click(function () {
		htmlbody.animate({ scrollTop: 0 }, 600);
		return false;
	});

	// WINDOW SCROLL EVENT
	myWindow.scroll(function() {
		controlScroll();
	});











	/* ************************************************************************************************************** */ 
	// MENU

	// ACTIVATE METISMENU
	$('#menu > ul').metisMenu();

	// COLLAPSE FUNCTION
	function collapseMenu() {
		wrapper.toggleClass('collapsed');
	};

	menuBt.click(function () {
		collapseMenu();
		return false;
	});

	//COLLAPSE MENU ON TABLET PORTRAIT
	if (tabletPortrait) collapseMenu();














	/* ************************************************************************************************************** */ 
	// BOOTSTRAP COMPONENTS CALLS

	// DATEPICKER
	$('.datepicker').datepicker({
		format: 'dd/mm/yyyy',
		weekStart: 0,
		todayHighlight: true
	})












	/* ************************************************************************************************************** */ 
	// STUFF FOR MOBILE & TABLET + WINDOW ONRESIZE EVENT
	if (mobile || tablet) {
		$(function() {
			FastClick.attach(document.body);
		});

	} else {

		myWindow.resize(function() {
			
		});
	}

	
	
});