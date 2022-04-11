// ===================================== Start Processors===============================================
// Sort by Price
$(document).ready(function () {
    $("#ProcessorPrice").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = "";
        $.ajax({
            type: "GET",
            url: "/Home/AscendingProcessorProdoucts?Id=" + $Price,
            success: function (result) {
                $("#data").empty();
                $.each(result, function (i, e) {
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
// Sort by char
$(document).ready(function () {
    $("#ProcessorProduct").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultProcessor?PageSize=" + $Price,
            success: function (data) {
                $.each(data, function (i, e) {
                    $("#data").empty();
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
                $("#data").html($html);
            }


        })
    })
});
// Checkbox
$(document).ready(function () {
    var arr = [];
    $("input[type='checkbox'].Kabear").click(function () {
        debugger;
        $(this).each(function () {
            if (this.checked) {
                arr.push($(this).val().trim());
            } else {
                arr.pop($(this).val().trim());
            }
        });
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfProcessorBrand",
            data: { brand: arr },
            dataType: "html",
            contentType: "application/x-www-form-urlencoded",
            success: function (data) {
                console.log(arr)
                if (data == 1) {
                    alert("success");
                } else {
                    alert("Falied")
                }
            }

        });


    });

});



















// ===================================== End Processors===============================================
/*New Product Motherboard*/
//===================================== Start Motherboards============================================
$(document).ready(function () {
    $("#MotherPrice").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = "";
        $.ajax({
            type: "GET",
            url: "/Home/AscendingMotherboardProdoucts?Id=" + $Price,
            success: function (result) {
                $("#data").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.motherPrice + '</span>' +
                        '<del class="product-old-price" > $' + (e.motherPrice + 100) + '</del ></h4 >' +
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
// Sort by char
$(document).ready(function () {
    $("#MotherProduct").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultMotherboard?PageSize=" + $Price,
            success: function (data) {
                $.each(data, function (i, e) {
                    console.log(data);
                    $("#data").empty();
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.motherPrice + '</span>' +
                        '<del class="product-old-price" > $' + (e.motherPrice + 100) + '</del ></h4 >' +
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
                $("#data").html($html);
            }


        })
    })
});
// Checkbox
$(document).ready(function () {
   
    $('input[type=checkbox]').click(function () {
        $(this).each(function () {
            var $check = $(this).val().trim();
            if (this.checked) {  
                if ($check == "MSI") {
                    var $html = '';
                    $.ajax({
                        type: "GET",
                        url: "/Home/ProductsOfMotherboardBrand?brand=" + $check,
                        success: function (data) {
                            $.each(data, function (i, e) {
                                console.log(data);
                                $("#data").empty();
                                $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                                    '<div class="product">' +
                                    '<div class="product-img">' +
                                    '<img src="/img/product01.png" alt="">' +

                                    '</div>' +
                                    '<div class="product-body">' +
                                    '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                                    '<h4 class="product-price"><span class="price">$' + e.motherPrice + '</span>' +
                                    '<del class="product-old-price" > $' + (e.motherPrice + 100) + '</del ></h4 >' +
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
                            $("#data").html($html);

                        }

                    })
                } else if ($check === "Gigabyte") {
                    var $html = '';
                    $.ajax({
                        type: "GET",
                        url: "/Home/ProductsOfMotherboardBrand?brand=" + $check,
                        success: function (data) {
                            $.each(data, function (i, e) {
                                console.log(data);
                                $("#data").empty();
                                $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                                    '<div class="product">' +
                                    '<div class="product-img">' +
                                    '<img src="/img/product01.png" alt="">' +

                                    '</div>' +
                                    '<div class="product-body">' +
                                    '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                                    '<h4 class="product-price"><span class="price">$' + e.motherPrice + '</span>' +
                                    '<del class="product-old-price" > $' + (e.motherPrice + 100) + '</del ></h4 >' +
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
                            $("#data").html($html);

                        }
                    });
                }
            } else {
                if ($check == "Gigabyte") {
                    var $html = '';
                    $.ajax({
                        type: "GET",
                        url: "/Home/ProductsOfMotherboardBrand?brand=MSI",
                        success: function (data) {
                            $.each(data, function (i, e) {
                                console.log(data);
                                $("#data").empty();
                                $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                                    '<div class="product">' +
                                    '<div class="product-img">' +
                                    '<img src="/img/product01.png" alt="">' +

                                    '</div>' +
                                    '<div class="product-body">' +
                                    '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                                    '<h4 class="product-price"><span class="price">$' + e.motherPrice + '</span>' +
                                    '<del class="product-old-price" > $' + (e.motherPrice + 100) + '</del ></h4 >' +
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
                            $("#data").html($html);

                        }
                    });
                } else if ($check == "MSI") {
                    var $html = '';
                    $.ajax({
                        type: "GET",
                        url: "/Home/ProductsOfMotherboardBrand?brand=Gigabyte",
                        success: function (data) {
                            $.each(data, function (i, e) {
                                console.log(data);
                                $("#data").empty();
                                $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                                    '<div class="product">' +
                                    '<div class="product-img">' +
                                    '<img src="/img/product01.png" alt="">' +

                                    '</div>' +
                                    '<div class="product-body">' +
                                    '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                                    '<h4 class="product-price"><span class="price">$' + e.motherPrice + '</span>' +
                                    '<del class="product-old-price" > $' + (e.motherPrice + 100) + '</del ></h4 >' +
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
                            $("#data").html($html);

                        }

                    })
                }
            }

        });
    })


});
//===================================== End Motherboards==============================================
/*New Product HDD*/
//===================================== Start HDD============================================
$(document).ready(function () {
    $("#HDDPrice").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = "";
        $.ajax({
            type: "GET",
            url: "/Home/AscendingHDDProdoucts?Id=" + $Price,
            success: function (result) {
                console.log(result);
                $("#data").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.hddprice + '</span>' +
                        '<del class="product-old-price" > $' + (e.hddprice + 100) + '</del ></h4 >' +
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
// Sort by char
$(document).ready(function () {
    $("#HDDProduct").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultHDD?PageSize=" + $Price,
            success: function (data) {
                console.log(data);
                $.each(data, function (i, e) {
                    console.log(data);
                    $("#data").empty();
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.hddprice + '</span>' +
                        '<del class="product-old-price" > $' + (e.hddprice + 100) + '</del ></h4 >' +
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
                $("#data").html($html);
            }


        })
    })
});
// Checkbox
$(document).ready(function () {

    $('input[type=checkbox]').click(function () {
        $(this).each(function () {
            var $check = $(this).val().trim();
            if (this.checked) {
                if ($check == "Seagate") {
                    var $html = '';
                    $.ajax({
                        type: "GET",
                        url: "/Home/ProductsOfHDDBrand?brand=" + $check,
                        success: function (data) {
                            $.each(data, function (i, e) {
                                console.log(data);
                                $("#data").empty();
                                $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                                    '<div class="product">' +
                                    '<div class="product-img">' +
                                    '<img src="/img/product01.png" alt="">' +

                                    '</div>' +
                                    '<div class="product-body">' +
                                    '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                                    '<h4 class="product-price"><span class="price">$' + e.hddprice + '</span>' +
                                    '<del class="product-old-price" > $' + (e.hddprice + 100) + '</del ></h4 >' +
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
                            $("#data").html($html);

                        }

                    })
                } else if ($check === "Western Digital") {
                    var $html = '';
                    $.ajax({
                        type: "GET",
                        url: "/Home/ProductsOfHDDBrand?brand=" + $check,
                        success: function (data) {
                            $.each(data, function (i, e) {
                                console.log(data);
                                $("#data").empty();
                                $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                                    '<div class="product">' +
                                    '<div class="product-img">' +
                                    '<img src="/img/product01.png" alt="">' +

                                    '</div>' +
                                    '<div class="product-body">' +
                                    '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                                    '<h4 class="product-price"><span class="price">$' + e.hddprice + '</span>' +
                                    '<del class="product-old-price" > $' + (e.hddprice + 100) + '</del ></h4 >' +
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
                            $("#data").html($html);

                        }
                    });
                }
            } else {
                if ($check == "Western Digital") {
                    var $html = '';
                    $.ajax({
                        type: "GET",
                        url: "/Home/ProductsOfHDDBrand?brand=Seagate",
                        success: function (data) {
                            $.each(data, function (i, e) {
                                console.log(data);
                                $("#data").empty();
                                $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                                    '<div class="product">' +
                                    '<div class="product-img">' +
                                    '<img src="/img/product01.png" alt="">' +

                                    '</div>' +
                                    '<div class="product-body">' +
                                    '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                                    '<h4 class="product-price"><span class="price">$' + e.hddprice + '</span>' +
                                    '<del class="product-old-price" > $' + (e.hddprice + 100) + '</del ></h4 >' +
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
                            $("#data").html($html);

                        }
                    });
                } else if ($check == "Seagate") {
                    var $html = '';
                    $.ajax({
                        type: "GET",
                        url: "/Home/ProductsOfHDDBrand?brand=Western Digital",
                        success: function (data) {
                            $.each(data, function (i, e) {
                                console.log(data);
                                $("#data").empty();
                                $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                                    '<div class="product">' +
                                    '<div class="product-img">' +
                                    '<img src="/img/product01.png" alt="">' +

                                    '</div>' +
                                    '<div class="product-body">' +
                                    '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                                    '<h4 class="product-price"><span class="price">$' + e.hddprice + '</span>' +
                                    '<del class="product-old-price" > $' + (e.hddprice + 100) + '</del ></h4 >' +
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
                            $("#data").html($html);

                        }

                    })
                }
            }

        });
    })


});
//===================================== End HDD==============================================
//===================================== Start RAM============================================
$(document).ready(function () {
    $("#RAMPrice").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = "";
        $.ajax({
            type: "GET",
            url: "/Home/AscendingRAMProdoucts?Id=" + $Price,
            success: function (result) {
                console.log(result);
                $("#data").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.ramName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.ramPrice + '</span>' +
                        '<del class="product-old-price" > $' + (e.ramPrice + 100) + '</del ></h4 >' +
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
// Sort by char
$(document).ready(function () {
    $("#RAMProduct").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultRAM?PageSize=" + $Price,
            success: function (data) {
                console.log(data);
                $.each(data, function (i, e) {
                    console.log(data);
                    $("#data").empty();
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.ramName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.ramPrice + '</span>' +
                        '<del class="product-old-price" > $' + (e.ramPrice + 100) + '</del ></h4 >' +
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
                $("#data").html($html);
            }


        })
    })
});
// Checkbox
$(document).ready(function () {

    $('input[type=checkbox]').click(function () {
        $(this).each(function () {
            var $check = $(this).val().trim();
            if (this.checked) {
                    var $html = '';
                    $.ajax({
                        type: "GET",
                        url: "/Home/ProductsOfRAMBrand?brand=" + $check,
                        success: function (data) {
                            $.each(data, function (i, e) {
                                console.log(data);
                                $("#data").empty();
                                $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                                    '<div class="product">' +
                                    '<div class="product-img">' +
                                    '<img src="/img/product01.png" alt="">' +

                                    '</div>' +
                                    '<div class="product-body">' +
                                    '<h3 class="product-name"><a href="#">' + e.ramName + '</a></h3>' +
                                    '<h4 class="product-price"><span class="price">$' + e.ramPrice + '</span>' +
                                    '<del class="product-old-price" > $' + (e.ramPrice + 100) + '</del ></h4 >' +
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
                            $("#data").html($html);

                        }

                    })
               
            } else {
               // Unchecked
            }

        });
    })


});
//===================================== End RAM==============================================
//===================================== Start SSD ============================================
$(document).ready(function () {
    $("#SSDPrice").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = "";
        $.ajax({
            type: "GET",
            url: "/Home/AscendingSSDProdoucts?Id=" + $Price,
            success: function (result) {
                console.log(result);
                $("#data").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.ssdname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.ssdprice + '</span>' +
                        '<del class="product-old-price" > $' + (e.ssdprice + 100) + '</del ></h4 >' +
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
// Sort by char
$(document).ready(function () {
    $("#SSDProduct").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultSSD?PageSize=" + $Price,
            success: function (data) {
                console.log(data);
                $.each(data, function (i, e) {
                    console.log(data);
                    $("#data").empty();
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.ssdname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.ssdprice + '</span>' +
                        '<del class="product-old-price" > $' + (e.ssdprice + 100) + '</del ></h4 >' +
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
                $("#data").html($html);
            }


        })
    })
});
// Checkbox
$(document).ready(function () {

    $('input[type=checkbox]').click(function () {
        $(this).each(function () {
            var $check = $(this).val().trim();
            if (this.checked) {
                var $html = '';
                $.ajax({
                    type: "GET",
                    url: "/Home/ProductsOfSSDBrand?brand=" + $check,
                    success: function (data) {
                        $.each(data, function (i, e) {
                            console.log(data);
                            $("#data").empty();
                            $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                                '<div class="product">' +
                                '<div class="product-img">' +
                                '<img src="/img/product01.png" alt="">' +

                                '</div>' +
                                '<div class="product-body">' +
                                '<h3 class="product-name"><a href="#">' + e.ssdname + '</a></h3>' +
                                '<h4 class="product-price"><span class="price">$' + e.ssdprice + '</span>' +
                                '<del class="product-old-price" > $' + (e.ssdprice + 100) + '</del ></h4 >' +
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
                        $("#data").html($html);

                    }

                })

            } else {
                // Unchecked
            }

        });
    })


});
//===================================== End SSD ==============================================
//===================================== Start Graphics Card ============================================
$(document).ready(function () {
    $("#CardPrice").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = "";
        $.ajax({
            type: "GET",
            url: "/Home/AscendingCardProdoucts?Id=" + $Price,
            success: function (result) {
                console.log(result);
                $("#data").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.vganame + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.vgaprice + '</span>' +
                        '<del class="product-old-price" > $' + (e.vgaprice + 100) + '</del ></h4 >' +
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
// Sort by char
$(document).ready(function () {
    $("#CardProduct").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultCard?PageSize=" + $Price,
            success: function (data) {
                console.log(data);
                $.each(data, function (i, e) {
                    console.log(data);
                    $("#data").empty();
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.vganame + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.vgaprice + '</span>' +
                        '<del class="product-old-price" > $' + (e.vgaprice + 100) + '</del ></h4 >' +
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
                $("#data").html($html);
            }


        })
    })
});
// Checkbox
$(document).ready(function () {

    $('input[type=checkbox]').click(function () {
        $(this).each(function () {
            var $check = $(this).val().trim();
            if (this.checked) {
                var $html = '';
                $.ajax({
                    type: "GET",
                    url: "/Home/ProductsOfCardBrand?brand=" + $check,
                    success: function (data) {
                        $.each(data, function (i, e) {
                            console.log(data);
                            $("#data").empty();
                            $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                                '<div class="product">' +
                                '<div class="product-img">' +
                                '<img src="/img/product01.png" alt="">' +

                                '</div>' +
                                '<div class="product-body">' +
                                '<h3 class="product-name"><a href="#">' + e.vganame + '</a></h3>' +
                                '<h4 class="product-price"><span class="price">$' + e.vgaprice + '</span>' +
                                '<del class="product-old-price" > $' + (e.vgaprice + 100) + '</del ></h4 >' +
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
                        $("#data").html($html);

                    }

                })

            } else {
                // Unchecked
            }

        });
    })


});
//===================================== End Graphics Card  ==============================================
//===================================== Start Case ============================================
$(document).ready(function () {
    $("#CasePrice").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = "";
        $.ajax({
            type: "GET",
            url: "/Home/AscendingCaseProdoucts?Id=" + $Price,
            success: function (result) {
                console.log(result);
                $("#data").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.caseName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.casePrice + '</span>' +
                        '<del class="product-old-price" > $' + (e.casePrice + 100) + '</del ></h4 >' +
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
// Sort by char
$(document).ready(function () {
    $("#CaseProduct").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultCase?PageSize=" + $Price,
            success: function (data) {
                console.log(data);
                $.each(data, function (i, e) {
                    console.log(data);
                    $("#data").empty();
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.caseName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.casePrice + '</span>' +
                        '<del class="product-old-price" > $' + (e.casePrice + 100) + '</del ></h4 >' +
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
                $("#data").html($html);
            }


        })
    })
});
// Checkbox
$(document).ready(function () {

    $('input[type=checkbox]').click(function () {
        $(this).each(function () {
            var $check = $(this).val().trim();
            if (this.checked) {
                var $html = '';
                $.ajax({
                    type: "GET",
                    url: "/Home/ProductsOfCaseBrand?brand=" + $check,
                    success: function (data) {
                        $.each(data, function (i, e) {
                            console.log(data);
                            $("#data").empty();
                            $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                                '<div class="product">' +
                                '<div class="product-img">' +
                                '<img src="/img/product01.png" alt="">' +

                                '</div>' +
                                '<div class="product-body">' +
                                '<h3 class="product-name"><a href="#">' + e.caseName + '</a></h3>' +
                                '<h4 class="product-price"><span class="price">$' + e.casePrice + '</span>' +
                                '<del class="product-old-price" > $' + (e.casePrice + 100) + '</del ></h4 >' +
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
                        $("#data").html($html);

                    }

                })

            } else {
                // Unchecked
            }

        });
    })


});
//===================================== End Case ==============================================
//===================================== Start PowerSuply ============================================
$(document).ready(function () {
    $("#PowerPrice").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = "";
        $.ajax({
            type: "GET",
            url: "/Home/AscendingPowerSuplyProdoucts?Id=" + $Price,
            success: function (result) {
                console.log(result);
                $("#data").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.vganame + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.vgaprice + '</span>' +
                        '<del class="product-old-price" > $' + (e.vgaprice + 100) + '</del ></h4 >' +
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
// Sort by char
$(document).ready(function () {
    $("#PowerProduct").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultPowerSuply?PageSize=" + $Price,
            success: function (data) {
                console.log(data);
                $.each(data, function (i, e) {
                    console.log(data);
                    $("#data").empty();
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.vganame + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">$' + e.vgaprice + '</span>' +
                        '<del class="product-old-price" > $' + (e.vgaprice + 100) + '</del ></h4 >' +
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
                $("#data").html($html);
            }


        })
    })
});
// Checkbox
$(document).ready(function () {

    $('input[type=checkbox]').click(function () {
        $(this).each(function () {
            var $check = $(this).val().trim();
            if (this.checked) {
                var $html = '';
                $.ajax({
                    type: "GET",
                    url: "/Home/ProductsOfCaseBrand?brand=" + $check,
                    success: function (data) {
                        $.each(data, function (i, e) {
                            console.log(data);
                            $("#data").empty();
                            $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                                '<div class="product">' +
                                '<div class="product-img">' +
                                '<img src="/img/product01.png" alt="">' +

                                '</div>' +
                                '<div class="product-body">' +
                                '<h3 class="product-name"><a href="#">' + e.vganame + '</a></h3>' +
                                '<h4 class="product-price"><span class="price">$' + e.vgaprice + '</span>' +
                                '<del class="product-old-price" > $' + (e.vgaprice + 100) + '</del ></h4 >' +
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
                        $("#data").html($html);

                    }

                })

            } else {
                // Unchecked
            }

        });
    })


});
//===================================== End PowerSuply ==============================================







