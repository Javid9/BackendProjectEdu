(function ($) {
"use strict";  


    
/*------------------------------------
	Sticky Menu 
--------------------------------------*/
 var windows = $(window);
    var stick = $(".header-sticky");
	windows.on('scroll',function() {    
		var scroll = windows.scrollTop();
		if (scroll < 5) {
			stick.removeClass("sticky");
		}else{
			stick.addClass("sticky");
		}
	});  
/*------------------------------------
	jQuery MeanMenu 
--------------------------------------*/
	$('.main-menu nav').meanmenu({
		meanScreenWidth: "767",
		meanMenuContainer: '.mobile-menu'
	});
    
    
    /* last  2 li child add class */
    $('ul.menu>li').slice(-2).addClass('last-elements');
/*------------------------------------
	Owl Carousel
--------------------------------------*/
    $('.slider-owl').owlCarousel({
        loop:true,
        nav:true,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        smartSpeed: 2500,
        navText:['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>'],
        responsive:{
            0:{
                items:1
            },
            768:{
                items:1
            },
            1000:{
                items:1
            }
        }
    });

    $('.partner-owl').owlCarousel({
        loop:true,
        nav:true,
        navText:['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>'],
        responsive:{
            0:{
                items:1
            },
            768:{
                items:3
            },
            1000:{
                items:5
            }
        }
    });  

    $('.testimonial-owl').owlCarousel({
        loop:true,
        nav:true,
        navText:['<i class="fa fa-angle-left"></i>','<i class="fa fa-angle-right"></i>'],
        responsive:{
            0:{
                items:1
            },
            768:{
                items:1
            },
            1000:{
                items:1
            }
        }
    });
/*------------------------------------
	Video Player
--------------------------------------*/
    $('.video-popup').magnificPopup({
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,
        zoom: {
            enabled: true,
        }
    });
    
    $('.image-popup').magnificPopup({
        type: 'image',
        gallery:{
            enabled:true
        }
    }); 
/*----------------------------
    Wow js active
------------------------------ */
    new WOW().init();
/*------------------------------------
	Scrollup
--------------------------------------*/
    $.scrollUp({
        scrollText: '<i class="fa fa-angle-up"></i>',
        easingType: 'linear',
        scrollSpeed: 900,
        animation: 'fade'
    });
/*------------------------------------
	Nicescroll
--------------------------------------*/
     $('body').scrollspy({ 
            target: '.navbar-collapse',
            offset: 95
        });
$(".notice-left").niceScroll({
            cursorcolor: "#EC1C23",
            cursorborder: "0px solid #fff",
            autohidemode: false,
            
        });

})(jQuery);	
//search


$(document).ready(function () {

    //let search = $(this).val().trim();
    let hidden = $("#hidden").val().trim();

    $(document).on("keyup", ".input-search", function () {
        let search = $(".input-search").val().trim();
        $(".searchList li").slice(1).remove();
        //$(".searchList").html()
        if (search.length > 0) {
            $.ajax({
                url: "/Ajax/Search?search=" + search+"&hidden="+hidden,
                type: "Get",
                success: function (res) {
                    if (hidden == "teacher") {
                        $(".searchList").append(`<div class="card" style="width: 18rem;" style="position:absolute">
                          <img class="card-img-top" src="img/teacher/${res[0].image}" alt="Card image cap">
                          <div class="card-h5 class="card-title">${res[0].fullName}body">
                            <</h5>
                            <p class="card-text"> ${res[0].position}</p>
                             <a class="btn btn-dark mr-3" asp-controller="Teacher" asp-action="Detail">Go Detail</a>
                          </div>
                        </div>`);
                    }
                    else if (hidden == "blog") {
                        $(".searchList").append(`<div class="card" style="width: 18rem;" style="position:absolute">
                          <img class="card-img-top" src="img/blog/${res[0].image}" alt="Card image cap">
                          <div class="card-body">
                            <h5 class="card-title">${res[0].title}</h5>
                            <p class="card-text"> ${res[0].author}</p>
                             <a class="btn btn-dark mr-3" asp-controller="Blog" asp-action="Detail" asp-route-id="${res[0].id}>Go Detail</a>
                          </div>
                        </div>`);
                    } else if(hidden=="course"){
                        $(".searchList").append(`<div class="card" style="width: 18rem;" style="position:absolute">
                          <img class="card-img-top" src="img/course/${res[0].image}" alt="Card image cap">
                          <div class="card-body">
                            <h5 class="card-title">${res[0].title}</h5>
                            <p class="card-text"> ${res[0].description}</p>
                             <a class="btn btn-dark mr-3" href="/Courses/Detail/${res[0].id}">Go Detail</a>
                          </div>
                        </div>`);

                    }
                  
                    //console.log(res[0].image)
                    console.log(res)
                   //console.log(res)
                  
                }
            })
        }
        

    })
    

})
