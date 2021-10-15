function scroll_to(clicked_link, nav_height) {
    var element_class = clicked_link.attr('href').replace('#', '.');
    var scroll_to = 0;
    if (element_class != '.top-content') {
        element_class += '-container';
        scroll_to = $(element_class).offset().top - nav_height;
    }
    if ($(window).scrollTop() != scroll_to) {
        $('html, body').stop().animate({ scrollTop: scroll_to }, 1000);
    }
}


$(document).ready(function () {



    $('#carouselId').on('slide.bs.carousel', function (e) {

        /*
            CC 2.0 License Iatek LLC 2018
            Attribution required
        */
        var $e = $(e.relatedTarget);
        var idx = $e.index();
        var itemsPerSlide = 4;
        var totalItems = $('.s .carousel-item').length;

        if (idx >= totalItems - (itemsPerSlide - 1)) {
            var it = itemsPerSlide - (totalItems - idx);
            for (var i = 0; i < it; i++) {
                // append slides to end
                if (e.direction == "left") {
                    $('.s .carousel-item').eq(i).appendTo('.s .carousel-inner');
                }
                else {
                    $('.s .carousel-item').eq(0).appendTo(' .s .carousel-inner');
                }
            }
        }
    });




    $('#carouselId1').on('slide.bs.carousel', function (e) {

        /*
            CC 2.0 License Iatek LLC 2018
            Attribution required
        */
        var $e = $(e.relatedTarget);
        var idx = $e.index();
        var itemsPerSlide = 4;
        var totalItems = $('.r .carousel-item').length;

        if (idx >= totalItems - (itemsPerSlide - 1)) {
            var it = itemsPerSlide - (totalItems - idx);
            for (var i = 0; i < it; i++) {
                // append slides to end
                if (e.direction == "left") {
                    $('.r .carousel-item').eq(i).appendTo('.r .carousel-inner');
                }
                else {
                    $('.r .carousel-item').eq(0).appendTo(' .r .carousel-inner');
                }
            }
        }
    });
});



//function scroll_to(clicked_link, nav_height) {
//    var element_class = clicked_link.attr('href').replace('#', '.');
//    var scroll_to = 0;
//    if(element_class != '.top-content') {
//        element_class += '-container';
//        scroll_to = $(element_class).offset().top - nav_height;
//    }
//    if($(window).scrollTop() != scroll_to) {
//        $('html, body').stop().animate({scrollTop: scroll_to}, 1000);
//    }
//}


//$(document).ready(function() {



//    $('#carouselId').on('slide.bs.carousel', function (e) {

//        /*
//            CC 2.0 License Iatek LLC 2018
//            Attribution required
//        */
//        var $e = $(e.relatedTarget);
//        var idx = $e.index();
//        var itemsPerSlide = 5;
//        var totalItems = $('.carousel-item').length;

//        if (idx >= totalItems-(itemsPerSlide-1)) {
//            var it = itemsPerSlide - (totalItems - idx);
//            for (var i=0; i<it; i++) {
//                // append slides to end
//                if (e.direction=="left") {
//                    $('.carousel-item').eq(i).appendTo('.carousel-inner');
//                }
//                else {
//                    $('.carousel-item').eq(0).appendTo('.carousel-inner');
//                }
//            }
//        }
//    });

//});