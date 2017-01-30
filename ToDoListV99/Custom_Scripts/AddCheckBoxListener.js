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

});