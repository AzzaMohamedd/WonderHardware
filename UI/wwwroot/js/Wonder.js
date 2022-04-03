
// Sorting Ascending in Processor
$(document).ready(function () {
    $("#Price").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = "";
        $.ajax({
            type: "GET",
            url: "/Home/AscendingProdoucts?Id=" + $Price,
            success: function (result) {
                $("#data").empty();
                $.each(result,function (i,e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.proName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.proPrice + '</span>' +
                        '<del class="product-old-price" > $' + (e.proPrice + 100) + '</del ></h4 >' +
                        '<div class="product-rating">' +
                        '<i class="fa fa-star"></i>' +
                        '<i class="fa fa-star"></i>' +
                        ' <i class="fa fa-star"></i>' +
                        ' <i class="fa fa-star"></i>' +
                        '<i class="fa fa-star"></i>' +
                        '</div>' +
                        '<div class="product-btns">' +
                        ' <button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button class="add-to-compare"><i class="fa fa-exchange"></i><span class="tooltipp">add to compare</span></button>' +
                        ' <button class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        ' </div>' +
                        '</div>' +
                        ' <div class="add-to-cart">' +
                        '<button class="add-to-cart-btn"><i class="fa fa-shopping-cart"></i> add to cart</button>' +
                        '</div > ' +
                        '</div > ' +
                        ' </div>';
                });
                $('#data').html($html);
            }
        });


    });


});
// Sorting Descending in Processor 
