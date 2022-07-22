function burgerMenu(selector){
	let menu = $(selector);
	let button = menu.find('.burger-menu_button');
	let link = menu.find('.burger-menu_link');
	let overlay = menu.find('.burger-menu_overlay');

	button.on('click',(e)=>{
		e.preventDefault();
		toggleMenu();
	});

	link.on('click',()=>toggleMenu());
	overlay.on('click',()=>toggleMenu());


	function toggleMenu(){
		menu.toggleClass('burger-menu_activ');

		if(menu.hasClass('burger-menu_activ')){
			$('body').css('overflow','hidden');
		}else{
			$('body').css('overflow','visible');
		}
	}
}
burgerMenu('.burger-menu');

	$(function() {

 		$(window).scroll(function() {

 			if($(this).scrollTop() != 0) {

 				$("#go_top").fadeIn();

 			} else {

	 			$("#go_top").fadeOut();
 
			}
 
		});

		$("#go_top").click(function() {

 			$("html, body").animate({scrollTop: 0}, 800);

 		});

 	});