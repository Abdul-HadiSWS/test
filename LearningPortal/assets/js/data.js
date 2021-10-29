
    $(document).ready(function () {

        $(".menu-button").click(function () {
            
            var id1 = $(this).attr('id');

            $(this).parent().parent().parent().parent().children().css("background-color", "white");
            $(this).parent().parent().parent().css("background-color", "yellow");
            //$("#video-card").hide();


            var CouID = $('#CourseId').val();
            //var SecId = $('#SectionMediaId').val();
          
            $('#video-card').empty();
           
            
            //$("#video-card").show();

            $.ajax({
                //base address/controller/Action
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
                   // $(a).css("background-color", "yellow");
                    //$('DOCTYPE h').val = result;
                   // $('html').append("" + result);
                },
                error: function () {
                    alert("error");
                }
            });
        });

    });


    function newVideo(id, sid) {

        $(document).ready(function () { 
                val1 = id;
                val2 = sid;
            // console.log(val + " " + val1);



            $.ajax({
                type: "GET",
            url: '/Home/StudentCourse/1?',
            

            success: function (msg) {
                alert("dwa");
                        },
            error: function (req, status, error) {
                console.log(error.toString());
                        }
                    });
      
        });
    }
