
//$(function () {
//    $('#@Html.IdFor(m=>m.ProductCategory)').change(function () {
//        var categoryId = $(this).val();
//        var subCategoryItems = "";
//        $.getJSON(
//        "../Product/GetSubCategories",
//        categoryId : categoryId,
//            type : "GET",
//                success: function (data) {
//                    $.each(data, function (index, item) {
//                        subCategoryItems += "<option value'" + item.Id + "'>" + item.Name + "</option>";
//                    });
//                    $('#@Html.IdFor(m=>m.ProductSubCategoryList)').html(subCategoryItems);
//                }
//        );
//    });
//});