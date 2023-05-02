$(function () {
    $.ajaxSetup({
        type: 'POST',
        url: '/Product/SelectSystem/',
        dataType: 'JSON'
    });
    $.extend({
        GetCategory: function () {
            $.ajax({
                data: { "tip": "GetCategory" },
                success: function (result) {
                    if (result.ok) {
                        $.each(result.text, function (CategorySubcategory, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#category").append(optionhtml);
                        });

                    } else {
                        $.each(result.text, function (CategorySubcategory, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#category").append(optionhtml);
                        });
                    }
                }
            });
        },
        GetSubcategory: function (categoryId) {
            $.ajax({
                data: { "categoryId": categoryId, "tip": "GetSubcategory" },
                success: function (result) {
                    $("#subcategory option").remove();
                    if (result.ok) {
                        $("#subcategory").prop("disabled", false);
                        $.each(result.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#subcategory").append(optionhtml);
                        });

                    } else {
                        $.each(result.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#subcategory").append(optionhtml);
                        });
                    }
                }
            });
        }
    });
    $.GetCategory();
    $("#category").on("change", function () {
        var categoryId = $(this).val();
        $.GetSubcategory(categoryId);
    });
})