﻿
$(document).ready(function () {
    lg();
});


function lg() {

    var player = videojs(document.querySelector("video"), {
        //inactivityTimeout: 0 // HIDE OR NOTE CONTROL BAR


    });

    $(document).ready(function () {

        var type = $('#type').val();
        var start = $('#start').val();
        var mediaid = $('#mediaid').val();



        if (type == "video/mp4") {
            player.currentTime(start);
        }
        videojs('vemvo-player').on('play', function () {


            if (player.readyState() < 1) {
                // wait for loadedmetdata event
                player.one("readymetadata", Onready);
            }
            else {
                Onready();
            }
            function Onready() {
                setInterval(function () {


                    datasend(mediaid, player.currentTime().toString(), player.duration().toString());
                }, 1000);
            }
        });
    });

    var Button = videojs.getComponent("Button");
    // Extend default
    var PrevButton = videojs.extend(Button, {
        //constructor: function(player, options) {
        constructor: function () {
            Button.apply(this, arguments);
            //this.addClass('vjs-chapters-button');

            /* FONT AWESOME ICON PREVIOUS NEXT */
            // this.addClass("icon-angle-left");
            // this.controlText("Previous");

            /* NEW VIDEOJS ICON PREV NEXT */
            this.addClass("vjs-icon-previous-item");
            this.controlText("Previous");

        },

        // constructor: function() {
        //   Button.apply(this, arguments);
        //   this.addClass('vjs-play-control vjs-control vjs-button vjs-paused');
        // },

        // createEl: function() {
        //   return Button.prototype.createEl('button', {
        //     //className: 'vjs-next-button vjs-control vjs-button',
        //     //innerHTML: 'Next >'
        //   });
        // },

        handleClick: function () {
            console.log("click");
            var courseid = $('#CourseId').val();

            var prevmediaid = $('#prevmediaid').val();
            var mediaid = $('#mediaid').val();
            //window.location.href = courseid.trim() + "?sid=" + prevmediaid.trim();

            videoget(courseid, prevmediaid, mediaid);


            //player.playlist.previous();

        }
    });


    /* ADD BUTTON */
    var Button = videojs.getComponent('Button');







    // Extend default
    var NextButton = videojs.extend(Button, {
        //constructor: function(player, options) {
        constructor: function () {
            Button.apply(this, arguments);
            //this.addClass('vjs-chapters-button');

            /* FONT AWESOME ICON PREVIOUS NEXT */
            // this.addClass("icon-angle-right");
            // this.controlText("Next");

            /* NEW VIDEOJS ICON PREV NEXT */
            this.addClass("vjs-icon-next-item");
            this.controlText("Next");
        },

        handleClick: function () {





            var courseid = $('#CourseId').val();
            var mediaid = $('#mediaid').val();
            var nextmediaid = $('#nextmediaid').val();
            console.log("click");

            //window.location.href = courseid.trim() + "?sid=" + nextmediaid.trim();

            videoget(courseid, nextmediaid, mediaid);
            // player.playlist.next();
            //newVideo(courseid, nextmediaid);

        }
    });



    function videoget(CouID, id1 , id2) {


        $(document).ready(function () {
         
            $('#' + id2).parent().parent().parent().css("background-color", "white");

            $('#' + id1).parent().parent().parent().parent().children().css("background-color", "white");
            $('#' + id1).parent().parent().parent().css("background-color", "yellow");

            var id = $('#' + id1).parent().parent().parent().parent().attr('id');
          

            $("#" + id).collapse('show');
          

            $('#video-card').empty();


            

            $.ajax({
               
                url: '/Home/videoplayer',
                type: 'GET',
                data: {
                    //Passing Input parameter
                    cid: CouID,
                    sid: id1
                },
                success: function (result) {
                    //write something

                    $('#video-card').append(result);
                    $('#video-card').show();
                   
                },
                error: function () {
                    alert("error");
                }
            });


        });
    }


    //}


    function datasend(id, currentime, tduration) {

        $(document).ready(function () {

            val1 = id;
            check = parseInt(tduration);
            val2 = parseInt(currentime);

            if ((check) == (val2 + 1)) {
                val2 = check;
            } else {

                val2 = parseInt(currentime);
            }
            $.ajax({
                type: "POST",
                url: '/Home/UpdateUserMedia',
                data: { number1: val1, number2: val2 },

                success: function (msg) {

                },
                error: function (req, status, error) {
                    console.log(error.toString());
                }
            });
        });

    }

    // Register the new component
    videojs.registerComponent("NextButton", NextButton);
    videojs.registerComponent("PrevButton", PrevButton);
    //player.getChild('controlBar').addChild('SharingButton', {});
    player.getChild("controlBar").addChild("PrevButton", {}, 0);
    player.getChild("controlBar").addChild("NextButton", {}, 2);



}