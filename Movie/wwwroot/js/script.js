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

function getPaintings(type) {

	$.ajax({
		url: '/Home/getPaintings',
		data: JSON.stringify(type), //your data
		type: 'POST',
		contentType: 'application/json',
		dataType: 'json',
		success: function (result) {
			var boxMovie = document.getElementById('BoxMovie');
			var arraryObj = JSON.parse(result);
			var formTexts = "";
			for (var i = 0; i < arraryObj.length; i++) {

				formTexts += '<form class="InputPageMovie" method="get" action="Home/PageMovie">' + '<input type="hidden" name="model" value="' + '{ &quot;id&quot;:&quot;' + arraryObj[i]['Id'] + '&quot;, &quot;type&quot;:&quot;' + type + '&quot; }' + '"/>' + '<div class="MovieContent" onclick="this.parentNode.submit();"><div class="Movie"><img src="' + arraryObj[i]['PosterPath'] + '" runat="server" alt=""></div><div class="NameMovie"><p>' + arraryObj[i]['Name'] + '</p>';

				if (type == 'Serial') {
					formTexts += '<p>Season: ' + arraryObj[i]['Season'] + 'Episode: ' + arraryObj[i]['Episode'] + '</p></div></div></form>';
				}
				else {
					formTexts += '</div></div></form>';
				}

			}

			boxMovie.innerHTML = formTexts;
		},
		complete: function () {
			console.log('working...');
		},
		failure: function (err) {
			console.log(err);
        }
    });

}