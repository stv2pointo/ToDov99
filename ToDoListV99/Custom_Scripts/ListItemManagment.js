$(document).ready(function () {

    $('.ActiveCheck').change(function () {

        var self = $(this);
        var id = self.attr('id');
        var value = self.prop('checked');

        $.ajax({
            url: '/Lists/AJAXEditItem',
            data: {
                id: id,
                value: value
            },
            type: 'POST',
            success: function (result) {
                $('#itemTableDiv').html(result);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.status);
                console.log(xhr.responseText);
                console.log(thrownError);
            }
        });

    });

    $("#ListItems").on("click", ".js-delete",
    function () {
        var button = $(this);
        bootbox.confirm("Are you sure you want to delete this item?",
            function (result) {
                if (result) {
                    $.ajax({
                        url: "/api/items/" + button.attr("data-item-id"),
                        method: "DELETE",
                        success: function () {
                            // var row = button.parentNode.parentNode; // why doesn't this work??
                            var row = document.getElementById("ii-" + button.attr("data-item-id"));
                            row.parentNode.removeChild(row);
                        }
                    });
                }
            });
    });

});