
$(document).ready(function () {

    var categoryId = $("#CategoryId").val();
    var subcategoryId = $("#SubCategoryId").val();
    var search = $("#searchText").val();
    var checkft = $("#CheckFt").is(":checked");

    //console.log(categoryId + " " + subcategoryId + search + checkft)

    $(".courseList").load("/Admin/CourseList", { catid: categoryId, subcatid: subcategoryId, tag: search, check: checkft });
    if (window.history.replaceState) {
        window.history.replaceState(null, null, window.location.href);
    }
    $(".addimg").on('change',function () {
        var imgext = $(this).val().toString().replace(/^.*\./, "").toLowerCase();
        var imgwidth = $(this).width();
        var imgheight = $(this).height();
        console.log(imgext, typeof (imgwidth), imgheight)

        if (imgwidth === 256 && imgheight === 256 && imgext == "png" || imgext == "jpg" || imgext == "jpeg") {
            $("#upbtn").prop('disabled', false);
            $(this).siblings().addClass("text-muted");
            $(this).siblings().removeClass("text-danger");
        }
        else {
            $(this).siblings().removeClass("text-muted");
            $(this).siblings().addClass("text-danger");
        }





    });
   
    //$("#CategoryId").change(function () {
    //    var categoryId = $(this).val();

    //    $.ajax({
    //        type: "post",
    //        url: "/Admin/GetSubCategoriesList?CategoryId=" + categoryId,
    //        contentType: "html",
    //        success: function (response) {
               
    //            $("#SubCategoryId").empty();
                
    //            $("#SubCategoryId").append(response);
    //        },
    //        error: function () {
    //            alert(error);
    //        }
    //    })

    //});
    $("#check").click(function () {
        var categoryId = $("#CategoryId").val();
        var subcategoryId = $("#SubCategoryId").val();
        var search = $("#searchText").val();
        var checkft = $("#CheckFt").is(":checked");
        $(".courseList").load("/Admin/CourseList", { catid: categoryId, subcatid: subcategoryId, tag: search, check: checkft });
    })

 
})