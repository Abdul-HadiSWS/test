
$(document).ready(function () {

    var categoryId = $("#CategoryId").val();
    var subcategoryId = $("#SubCategoryId").val();
    var search = $("#searchText").val();
    var checkft = $("#CheckFt").is(":checked");

    //console.log(categoryId + " " + subcategoryId + search + checkft)

    $(".courseList").load("/Admin/CourseList", { catid: categoryId, subcatid: subcategoryId, tag: search, check: checkft });

   
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