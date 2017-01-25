$(document).ready(function () {

    $.ajax({
        url: '/Lists/BuildListTable',
        success: function (result) {
            $('#listTableDiv').html(result);
        }
    });


});