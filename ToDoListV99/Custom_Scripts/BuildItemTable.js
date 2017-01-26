$(document).ready(function () {

    $.ajax({
        url: '/Lists/BuildItemTable',
        success: function (result) {
            $('#itemTableDiv').html(result);
        }
    });


});