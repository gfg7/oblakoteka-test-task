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

function DeleteProduct(id) {
    $.ajax({
        url: `/product/${id}`,
        type: 'DELETE', 
        success: function(result) {
            GetProducts($('#search-bar').val())
        },
        error: function(xhr, status, error) {
            console.log(error); 
        }
    });
}

function GetProduct(id) {
    var data = $(`tr[data-id=${id}] td .data`).map(function(){ return this.innerText })
    $('#editModal input[name=id]').val(id)
    $('#editModal input[name=Name]').val(data[0])
    $('#editModal input[name=Description]').val(data[1])
}

function EditProduct() {
    $.ajax({
        url: `/product/${$('#editModal input[name=id]').val()}`,
        type: 'PUT', 
        data : $('#editModal form').serialize(),
        beforeSend: function() {
            $('#editModal').modal('hide')
            $('.spinner-border').show();
        },
        complete: function() {
            $('.spinner-border').hide();
        },
        success: function(result) {
            GetProducts($('#search-bar').val());
        },
        error: function(xhr, status, error) {
            console.log(error); 
        }
    });
}

function CreateProduct() {
    $.ajax({
        url: `/product/`,
        type: 'POST', 
        data : $('#createModal form').serialize(),
        beforeSend: function() {
            $('#createModal').modal('hide')
            $('.spinner-border').show();
        },
        complete: function() {
            $('.spinner-border').hide();
        },
        success: function(result) {
            GetProducts($('#search-bar').val());
        },
        error: function(xhr, status, error) {
            console.log(error); 
        }
    });
}

  