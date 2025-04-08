$(document).ready(function () {
    $('#btnDeleteTransaction').click(function () {
        var transID = $(this).data('id');
        console.log(transID);
    });
});