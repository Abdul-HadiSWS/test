
    $(document).ready(function () {

        
        $(".menu-button").click(function () {
            
            var id1 = $(this).attr('id');

            $(this).parent().parent().parent().parent().children().css("background-color", "white");
            $(this).parent().parent().parent().css("background-color", "yellow");
            
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
                   
                 
                
                   //vd(player);
           
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
















   





    



  








