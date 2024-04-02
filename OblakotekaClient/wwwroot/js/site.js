$(document).ready(function() {
    GetProducts();
});

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
  });

$("#search-bar").on('keyup', function() {
        GetProducts($('#search-bar').val());
});

function GetProducts(search) {
    $.ajax({
        url: `/product/find?search=${search ?? ''}`,
        type: 'GET', 
        beforeSend: function() {
            $('.spinner-border').show();
        },
        complete: function() {
            $('.spinner-border').hide();
        },
        success: function(result) {
            $('.product-table').html(result);
        },
        error: function(xhr, status, error) {
            console.log(error); 
        }
    });
}