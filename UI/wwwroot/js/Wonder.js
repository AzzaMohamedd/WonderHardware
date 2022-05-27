
// ===================================== Start Processors===============================================



// Hight To Low
$(document).ready(function () {
    $("#ProcessorPrice").on("change", function () {
        debugger;
        var $Price = $(this).val();

        $.ajax({
            type: "GET",
            url: "/Home/AscendingProcessorProdoucts?Id=" + $Price,
            success: function (result) {
                $("#Pro").empty();
                var $html = "";
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.proName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.proPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.proPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    debugger;
                    for (var i = 1; i < Math.round(e.proRate, 1); i++) {
                        $html += '<i class="fa fa-star"></i> ';
                    }
                    for (var i = Math.round(e.proRate, 1); i <= 5; i++) {
                        if (Math.round(e.proRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    debugger;
                    $html += '</div>' +
                        //end Rate
                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.proCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.proName + "'" + ', Code:' + "'" + e.proCode + "'" + ', Price:' + e.proPrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                });
                $('#Pro').html($html);
            }
        });


    });


});
// Display Size
$(document).ready(function () {
    $("#ProcessorProduct").on("change", function () {
        debugger;
        var $Price = $(this).val(), $html = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultProcessor?PageSize=" + $Price,
            success: function (data) {
                $.each(data, function (i, e) {
                    $("#Pro").empty();

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.proName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.proPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.proPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    debugger;
                    for (var i = 1; i < Math.round(e.proRate, 1); i++) {
                        $html += '<i class="fa fa-star"></i> ';
                    }
                    for (var i = Math.round(e.proRate, 1); i <= 5; i++) {
                        if (Math.round(e.proRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    debugger;
                    $html += '</div>' +
                        //end Rate
                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.proCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.proName + "'" + ', Code:' + "'" + e.proCode + "'" + ', Price:' + e.proPrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                });
                $("#Pro").html($html);
            }


        })
    })
});
// Checkbox
$(document).ready(function () {
    var arr = []
        ;
    $("input[type='checkbox'].Kabear").click(function () {
        debugger;
        var $val = $(this).val().trim();
        if (this.checked) {
            arr.push($val)

        } else {
            for (var i = 0; i < arr.length; i++) {

                if (arr[i] === $val) {
                    arr.splice(i, 1);

                }
            }
        }
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfProcessorBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (data) {
                var $html = ''
                $("#Pro").empty();
                $.each(data, function (i, e) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.proName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.proPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.proPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    debugger;
                    for (var i = 1; i < Math.round(e.proRate, 1); i++) {
                        $html += '<i class="fa fa-star"></i> ';
                    }
                    for (var i = Math.round(e.proRate, 1); i <= 5; i++) {
                        if (Math.round(e.proRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    debugger;
                    $html += '</div>' +
                        //end Rate
                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.proCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.proName + "'" + ', Code:' + "'" + e.proCode + "'" + ', Price:' + e.proPrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#Pro').html($html);



            }

        });



    });



});
// Price 
$(document).ready(function () {
    $("#processor #price-slider").on("click", function () {
        var $Input1 = parseInt($("#processor #price-min").val()),
            $Input2 = parseInt($("#processor #price-max").val());
        console.log($Input1 + "" + $Input2);
        $.ajax({
            type: "GET",
            url: "/Home/GetProcessorPrice?min=" + $Input1 + "&max=" + $Input2,
            dataType: "json",
            success: (data) => {
                debugger;
                var $html = '';
                $("#Pro").empty();
                $.each(data, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.proName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.proPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.proPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    debugger;
                    for (var i = 1; i < Math.round(e.proRate, 1); i++) {
                        $html += '<i class="fa fa-star"></i> ';
                    }
                    for (var i = Math.round(e.proRate, 1); i <= 5; i++) {
                        if (Math.round(e.proRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    debugger;
                    $html += '</div>' +
                        //end Rate
                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.proCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.proName + "'" + ', Code:' + "'" + e.proCode + "'" + ', Price:' + e.proPrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                });
                $("#Pro").html($html);
            }
        });

    });

});
// Increase - Decrease
$(document).ready(function () {
    $(".Processor-up").on("click", function () {
        var $minval = parseInt($("#processor #price-min").val()),
            $maxval = parseInt($("#processor #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetProcessorPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: (data) => {
                    debugger;
                    var $html = '';
                    $("#Pro").empty();
                    $.each(data, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#">' + e.proName + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.proPrice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.proPrice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        debugger;
                        for (var i = 1; i < Math.round(e.proRate, 1); i++) {
                            $html += '<i class="fa fa-star"></i> ';
                        }
                        for (var i = Math.round(e.proRate, 1); i <= 5; i++) {
                            if (Math.round(e.proRate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.proRate, 1)) {
                                    $html += '<i class="fa fa-star"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.proRate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        debugger;
                        $html += '</div>' +
                            //end Rate
                            '<div class="product-btns">' +
                            '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                            '<button onclick="gotoDetails(' + "'" + e.proCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                            '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.proName + "'" + ', Code:' + "'" + e.proCode + "'" + ', Price:' + e.proPrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                            ' </div>' +
                            '</div>' +

                            '</div > ' +
                            ' </div>';
                    });
                    $("#Pro").html($html);
                }
            });
        })


    });

    $(".Processor-down").on("click", function () {
        var $minval = parseInt($("#processor #price-min").val()),
            $maxval = parseInt($("#processor #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetProcessorPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: (data) => {
                    debugger;
                    var $html = '';
                    $("#Pro").empty();
                    $.each(data, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#">' + e.proName + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.proPrice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.proPrice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        debugger;
                        for (var i = 1; i < Math.round(e.proRate, 1); i++) {
                            $html += '<i class="fa fa-star"></i> ';
                        }
                        for (var i = Math.round(e.proRate, 1); i <= 5; i++) {
                            if (Math.round(e.proRate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.proRate, 1)) {
                                    $html += '<i class="fa fa-star"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.proRate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        debugger;
                        $html += '</div>' +
                            //end Rate
                            '<div class="product-btns">' +
                            '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                            '<button onclick="gotoDetails(' + "'" + e.proCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                            '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.proName + "'" + ', Code:' + "'" + e.proCode + "'" + ', Price:' + e.proPrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                            ' </div>' +
                            '</div>' +

                            '</div > ' +
                            ' </div>';
                    });
                    $("#Pro").html($html);
                }
            });
        });


    });

});

// ===================================== End Processors===============================================
/*New Product Motherboard*/
//===================================== Start Motherboards============================================
// Hight To Low
$(document).ready(function () {
    $("#MotherPrice").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = "";
        $.ajax({
            type: "GET",
            url: "/Home/AscendingMotherboardProdoucts?Id=" + $Price,
            success: function (result) {
                console.log(result);
                $("#Pro").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.motherPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.motherPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    debugger;
                    for (var i = 1; i < Math.round(e.motherRate, 1); i++) {
                        $html += '<i class="fa fa-star"></i> ';
                    }
                    for (var i = Math.round(e.motherRate, 1); i <= 5; i++) {
                        if (Math.round(e.motherRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    debugger;
                    $html += '</div>' +
                        //end Rate
                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.motherName + "'" + ', Code:' + "'" + e.mothCode + "'" + ', Price:' + e.motherPrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#Moth').html($html);

            }
        });


    });


});
// Display Size
$(document).ready(function () {
    $("#MotherProduct").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultMotherboard?PageSize=" + $Price,
            success: function (data) {
                console.log(data);
                $("#Moth").empty();
                $.each(data, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.motherPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.motherPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    debugger;
                    for (var i = 1; i < Math.round(e.motherRate, 1); i++) {
                        $html += '<i class="fa fa-star"></i> ';
                    }
                    for (var i = Math.round(e.motherRate, 1); i <= 5; i++) {
                        if (Math.round(e.motherRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    debugger;
                    $html += '</div>' +
                        //end Rate
                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.motherName + "'" + ', Code:' + "'" + e.mothCode + "'" + ', Price:' + e.motherPrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#Moth').html($html);

            }


        })
    })
});
// Checkbox
$(document).ready(function () {
    var arr = [];
    $("input[type='checkbox'].Kabear1").click(function () {
        debugger;
        $(this).each(function () {
            var $val = $(this).val().trim();
            if (this.checked) {
                arr.push($val)

            } else {
                for (var i = 0; i < arr.length; i++) {

                    if (arr[i] === $val) {
                        arr.splice(i, 1);

                    }
                }
            }
        });
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfMotherboardBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (data) {
                var $html = '';
                console.log(data);
                $("#Moth").empty();
                $.each(data, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.motherPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.motherPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    debugger;
                    for (var i = 1; i < Math.round(e.motherRate, 1); i++) {
                        $html += '<i class="fa fa-star"></i> ';
                    }
                    for (var i = Math.round(e.motherRate, 1); i <= 5; i++) {
                        if (Math.round(e.motherRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    debugger;
                    $html += '</div>' +
                        //end Rate
                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.motherName + "'" + ', Code:' + "'" + e.mothCode + "'" + ', Price:' + e.motherPrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#Moth').html($html);

            }

        });


    });



});
// Price
$(document).ready(function () {
    $("#motherboard #price-slider").on("click", function () {
        var $minval = parseInt($("#motherboard #price-min").val()),
            $maxval = parseInt($("#motherboard #price-max").val()),
            $html = '';

        $.ajax({
            type: "GET",
            url: "/Home/GetMotherboardPrice?min=" + $minval + "&max=" + $maxval,
            dataType: "json",
            success: function (data) {
                console.log(data);
                $("#Moth").empty();
                $.each(data, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.motherPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.motherPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    debugger;
                    for (var i = 1; i < Math.round(e.motherRate, 1); i++) {
                        $html += '<i class="fa fa-star"></i> ';
                    }
                    for (var i = Math.round(e.motherRate, 1); i <= 5; i++) {
                        if (Math.round(e.motherRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    debugger;
                    $html += '</div>' +
                        //end Rate
                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.motherName + "'" + ', Code:' + "'" + e.mothCode + "'" + ', Price:' + e.motherPrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#Moth').html($html);
            }


        })

    });

});
// Increase - Decrease
$(document).ready(function () {
    $(".mother-up").on("click", function () {
        var $minval = parseInt($("#motherboard #price-min").val()),
            $maxval = parseInt($("#motherboard #price-max").val());
        $(".mother-up").each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetMotherboardPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: (response) => {
                    console.log(response);
                    var $html = '';
                    $("#Moth").empty();
                    $.each(response, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.motherPrice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.motherPrice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        debugger;
                        for (var i = 1; i < Math.round(e.motherRate, 1); i++) {
                            $html += '<i class="fa fa-star"></i> ';
                        }
                        for (var i = Math.round(e.motherRate, 1); i <= 5; i++) {
                            if (Math.round(e.motherRate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.motherRate, 1)) {
                                    $html += '<i class="fa fa-star"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.motherRate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        debugger;
                        $html += '</div>' +
                            //end Rate
                            '<div class="product-btns">' +
                            '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                            '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                            '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.motherName + "'" + ', Code:' + "'" + e.mothCode + "'" + ', Price:' + e.motherPrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                            ' </div>' +
                            '</div>' +

                            '</div > ' +
                            ' </div>';
                    })
                    $('#Moth').html($html);
                }
            });
        })


    });

    $(".mother-down").on("click", function () {
        var $minval = parseInt($("#motherboard #price-min").val()),
            $maxval = parseInt($("#motherboard #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetMotherboardPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: (response) => {
                    console.log(response);
                    var $html = '';
                    $("#Moth").empty();
                    $.each(response, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.motherPrice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.motherPrice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        debugger;
                        for (var i = 1; i < Math.round(e.motherRate, 1); i++) {
                            $html += '<i class="fa fa-star"></i> ';
                        }
                        for (var i = Math.round(e.motherRate, 1); i <= 5; i++) {
                            if (Math.round(e.motherRate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.motherRate, 1)) {
                                    $html += '<i class="fa fa-star"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.motherRate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        debugger;
                        $html += '</div>' +
                            //end Rate
                            '<div class="product-btns">' +
                            '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                            '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                            '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.motherName + "'" + ', Code:' + "'" + e.mothCode + "'" + ', Price:' + e.motherPrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                            ' </div>' +
                            '</div>' +

                            '</div > ' +
                            ' </div>';
                    })
                    $('#Moth').html($html);
                }
            });
        });


    });

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
                console.log(response);
                $("#hdd").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    debugger;
                    for (var i = 1; i < Math.round(e.hddRate, 1); i++) {
                        $html += '<i class="fa fa-star"></i> ';
                    }
                    for (var i = Math.round(e.hddRate, 1); i <= 5; i++) {
                        if (Math.round(e.hddRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.hddRate, 1)) {
                                $html += '<i class="fa fa-star"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.hddRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    debugger;
                    $html += '</div>' +
                        //end Rate
                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.hddname + "'" + ', Code:' + "'" + e.hddCode + "'" + ', Price:' + e.hddprice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#hdd').html($html);

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
            success: function (result) {
                console.log(response);
                $("#hdd").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    debugger;
                    for (var i = 1; i < Math.round(e.hddRate, 1); i++) {
                        $html += '<i class="fa fa-star"></i> ';
                    }
                    for (var i = Math.round(e.hddRate, 1); i <= 5; i++) {
                        if (Math.round(e.hddRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.hddRate, 1)) {
                                $html += '<i class="fa fa-star"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.hddRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    debugger;
                    $html += '</div>' +
                        //end Rate
                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.hddname + "'" + ', Code:' + "'" + e.hddCode + "'" + ', Price:' + e.hddprice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#hdd').html($html);

            }


        })
    })
});
// Checkbox
$(document).ready(function () {
    var arr = [];
    $("input[type='checkbox'].Kabear2").click(function () {
        debugger;
        $(this).each(function () {
            var $val = $(this).val().trim();
            if (this.checked) {
                arr.push($val)
            } else {
                for (var i = 0; i < arr.length; i++) {

                    if (arr[i] === $val) {

                        arr.splice(i, 1);
                    }

                }
            }

        });
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfHDDBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (result) {
                console.log(response);
                $("#hdd").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    debugger;
                    for (var i = 1; i < Math.round(e.hddRate, 1); i++) {
                        $html += '<i class="fa fa-star"></i> ';
                    }
                    for (var i = Math.round(e.hddRate, 1); i <= 5; i++) {
                        if (Math.round(e.hddRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.hddRate, 1)) {
                                $html += '<i class="fa fa-star"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.hddRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    debugger;
                    $html += '</div>' +
                        //end Rate
                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.hddname + "'" + ', Code:' + "'" + e.hddCode + "'" + ', Price:' + e.hddprice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#hdd').html($html);

            }

        });


    });

});
// Price
$(document).ready(function () {
    $("#Hdd #price-slider").on("click", function () {
        var $minval = parseInt($("#Hdd #price-min").val()),
            $maxval = parseInt($("#Hdd #price-max").val()),
            $html = '';

        $.ajax({
            type: "GET",
            url: "/Home/GetHddPrice?min=" + $minval + "&max=" + $maxval,
            dataType: "json",
            success: function (result) {
                console.log(response);
                $("#hdd").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    debugger;
                    for (var i = 1; i < Math.round(e.hddRate, 1); i++) {
                        $html += '<i class="fa fa-star"></i> ';
                    }
                    for (var i = Math.round(e.hddRate, 1); i <= 5; i++) {
                        if (Math.round(e.hddRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.hddRate, 1)) {
                                $html += '<i class="fa fa-star"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.hddRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    debugger;
                    $html += '</div>' +
                        //end Rate
                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.hddname + "'" + ', Code:' + "'" + e.hddCode + "'" + ', Price:' + e.hddprice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#hdd').html($html);

            }


        })

    });

});
// Increase - Decrease
$(document).ready(function () {
    $(".hdd-up").on("click", function () {
        var $minval = parseInt($("#Hdd #price-min").val()),
            $maxval = parseInt($("#Hdd #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetHddPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: function (result) {
                    console.log(response);
                    $("#hdd").empty();
                    $.each(result, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        debugger;
                        for (var i = 1; i < Math.round(e.hddRate, 1); i++) {
                            $html += '<i class="fa fa-star"></i> ';
                        }
                        for (var i = Math.round(e.hddRate, 1); i <= 5; i++) {
                            if (Math.round(e.hddRate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.hddRate, 1)) {
                                    $html += '<i class="fa fa-star"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.hddRate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        debugger;
                        $html += '</div>' +
                            //end Rate
                            '<div class="product-btns">' +
                            '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                            '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                            '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.hddname + "'" + ', Code:' + "'" + e.hddCode + "'" + ', Price:' + e.hddprice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                            ' </div>' +
                            '</div>' +

                            '</div > ' +
                            ' </div>';
                    })
                    $('#hdd').html($html);

                }
            });
        })


    });

    $(".hdd-down").on("click", function () {
        var $minval = parseInt($("#Hdd #price-min").val()),
            $maxval = parseInt($("#Hdd #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetHddPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: function (result) {
                    console.log(response);
                    $("#hdd").empty();
                    $.each(result, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        debugger;
                        for (var i = 1; i < Math.round(e.hddRate, 1); i++) {
                            $html += '<i class="fa fa-star"></i> ';
                        }
                        for (var i = Math.round(e.hddRate, 1); i <= 5; i++) {
                            if (Math.round(e.hddRate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.hddRate, 1)) {
                                    $html += '<i class="fa fa-star"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.hddRate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        debugger;
                        $html += '</div>' +
                            //end Rate
                            '<div class="product-btns">' +
                            '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                            '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                            '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.hddname + "'" + ', Code:' + "'" + e.hddCode + "'" + ', Price:' + e.hddprice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                            ' </div>' +
                            '</div>' +

                            '</div > ' +
                            ' </div>';
                    })
                    $('#hdd').html($html);

                }
            });
        });


    });

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
                        '<h3 class="product-name"style="font-size:11px"><a href="#" >' + e.ramName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.ramPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ramPrice + 100) + ' LE</del ></h4 >' +
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
// Sort by Default
$(document).ready(function () {
    $("#RAMProduct").on("change", function () {
        debugger;
        var $Price = $(this).val(),
            $html = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultRAM?PageSize=" + $Price,
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
                        '<h3 class="product-name"style="font-size:11px"><a href="#" >' + e.ramName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.ramPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ramPrice + 100) + ' LE</del ></h4 >' +
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
    $("input[type='checkbox'].Kabear3").click(function () {
        debugger;
        $(this).each(function () {
            var $val = $(this).val().trim();
            if (this.checked) {
                arr.push($val)
            } else {
                for (var i = 0; i < arr.length; i++) {

                    if (arr[i] === $val) {

                        arr.splice(i, 1);
                    }

                }
            }

        });
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfRAMBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (data) {
                var $html = '';
                $("#data").empty();
                $.each(data, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +
                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#" style="font-size:11px">' + e.ramName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.ramPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ramPrice + 100) + ' LE</del ></h4 >' +
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


    });

});
// Price
$(document).ready(function () {
    $("#Ram #price-slider").on("click", function () {
        var $minval = parseInt($("#Ram #price-min").val()),
            $maxval = parseInt($("#Ram #price-max").val()),
            $html = '';

        $.ajax({
            type: "GET",
            url: "/Home/GetHddPrice?min=" + $minval + "&max=" + $maxval,
            dataType: "json",
            success: function (data) {
                $("#data").empty();
                $.each(data, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +
                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +
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

    });

});
// Increase - Decrease
$(document).ready(function () {
    $(".ram-up").on("click", function () {
        var $minval = parseInt($("#Ram #price-min").val()),
            $maxval = parseInt($("#Ram #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetHddPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: (data) => {
                    debugger;
                    var $html = '';
                    $("#data").empty();
                    $.each(data, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +
                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +
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
        })


    });

    $(".ram-down").on("click", function () {
        var $minval = parseInt($("#Ram #price-min").val()),
            $maxval = parseInt($("#Ram #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetHddPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: (data) => {
                    debugger;
                    var $html = '';
                    $("#data").empty();
                    $.each(data, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +
                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +
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
        });


    });

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
                        '<h4 class="product-price"><span class="price">' + e.ssdprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ssdprice + 100) + ' LE</del ></h4 >' +
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
                        '<h4 class="product-price"><span class="price">' + e.ssdprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ssdprice + 100) + ' LE</del ></h4 >' +
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
    $("input[type='checkbox'].Kabear4").click(function () {
        debugger;
        $(this).each(function () {
            var $val = $(this).val().trim();
            if (this.checked) {
                arr.push($val)
            } else {
                for (var i = 0; i < arr.length; i++) {

                    if (arr[i] === $val) {

                        arr.splice(i, 1);
                    }

                }
            }

        });
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfSSDBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (data) {
                var $html = '';
                $("#data").empty();
                $.each(data, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +
                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.ssdname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.ssdprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ssdprice + 100) + ' LE</del ></h4 >' +
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


    });

});
// Price
$(document).ready(function () {
    $("#ssd #price-slider").on("click", function () {
        var $minval = parseInt($("#ssd #price-min").val()),
            $maxval = parseInt($("#ssd #price-max").val()),
            $html = '';

        $.ajax({
            type: "GET",
            url: "/Home/GetSDDPrice?min=" + $minval + "&max=" + $maxval,
            dataType: "json",
            success: function (data) {
                $("#data").empty();
                $.each(data, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +
                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.ssdname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.ssdprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ssdprice + 100) + ' LE</del ></h4 >' +
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

    });

});
// Increase - Decrease
$(document).ready(function () {
    $(".ssd-up").on("click", function () {
        var $minval = parseInt($("#ssd #price-min").val()),
            $maxval = parseInt($("#ssd #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetSDDPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: (data) => {
                    debugger;
                    var $html = '';
                    $("#data").empty();
                    $.each(data, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +
                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#">' + e.ssdname + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.ssdprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.ssdprice + 100) + ' LE</del ></h4 >' +
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
        })


    });

    $(".ssd-down").on("click", function () {
        var $minval = parseInt($("#ssd #price-min").val()),
            $maxval = parseInt($("#ssd #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetSDDPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: (data) => {
                    debugger;
                    var $html = '';
                    $("#data").empty();
                    $.each(data, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +
                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#">' + e.ssdname + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.ssdprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.ssdprice + 100) + ' LE</del ></h4 >' +
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
        });


    });

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
                        '<h3 class="product-name" style="font-size:11px"><a href="#">' + e.vganame + '</a></h3>' +
                        '<h4 class="product-price"style="font-size:11px><span class="price">' + e.vgaprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.vgaprice + 100) + ' LE</del ></h4 >' +
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
                        '<h3 class="product-name"style="font-size:11px"><a href="#">' + e.vganame + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.vgaprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.vgaprice + 100) + ' LE</del ></h4 >' +
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
    $("input[type='checkbox'].Kabear5").click(function () {
        debugger;
        $(this).each(function () {
            var $val = $(this).val().trim();
            if (this.checked) {
                arr.push($val)
            } else {
                for (var i = 0; i < arr.length; i++) {

                    if (arr[i] === $val) {

                        arr.splice(i, 1);
                    }

                }
            }

        });
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfCardBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (data) {
                var $html = '';
                console.log(data);
                $("#data").empty();
                $.each(data, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +
                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"style="font-size:11px"><a href="#">' + e.vganame + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.vgaprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.vgaprice + 100) + ' LE</del ></h4 >' +
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


    });

});
// Price
$(document).ready(function () {
    $("#GCard #price-slider").on("click", function () {
        var $minval = parseInt($("#GCard #price-min").val()),
            $maxval = parseInt($("#GCard #price-max").val()),
            $html = '';

        $.ajax({
            type: "GET",
            url: "/Home/GetGCPrice?min=" + $minval + "&max=" + $maxval,
            dataType: "json",
            success: function (data) {
                $("#data").empty();
                $.each(data, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +
                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"style="font-size:11px"><a href="#">' + e.vganame + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.vgaprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.vgaprice + 100) + ' LE</del ></h4 >' +
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

    });

});
// Increase - Decrease
$(document).ready(function () {
    $(".GCard-up").on("click", function () {
        var $minval = parseInt($("#GCard #price-min").val()),
            $maxval = parseInt($("#GCard #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetGCPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: (data) => {
                    debugger;
                    var $html = '';
                    $("#data").empty();
                    $.each(data, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +
                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"style="font-size:11px"><a href="#">' + e.vganame + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.vgaprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.vgaprice + 100) + ' LE</del ></h4 >' +
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
        })


    });

    $(".GCard-down").on("click", function () {
        var $minval = parseInt($("#GCard #price-min").val()),
            $maxval = parseInt($("#GCard #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetGCPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: (data) => {
                    debugger;
                    var $html = '';
                    $("#data").empty();
                    $.each(data, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +
                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"style="font-size:11px"><a href="#">' + e.vganame + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.vgaprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.vgaprice + 100) + ' LE</del ></h4 >' +
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
        });


    });

});

//===================================== End Graphics Card  ==============================================
//===================================== Start Case ============================================
// Hight To Low
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
                        '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +
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
// Display Size
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
                        '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +
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
    $("input[type='checkbox'].Kabear6").click(function () {
        debugger;
        $(this).each(function () {
            var $val = $(this).val().trim();
            if (this.checked) {
                arr.push($val)
            } else {
                for (var i = 0; i < arr.length; i++) {

                    if (arr[i] === $val) {

                        arr.splice(i, 1);
                    }

                }
            }

        });
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfCaseBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (data) {
                var $html = '';
                $("#data").empty();
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
                        '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +
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


    });

});
// Price 
$(document).ready(function () {
    $("#Case #price-slider").on("click", function () {
        var $Input1 = parseInt($("#Case #price-min").val()),
            $Input2 = parseInt($("#Case #price-max").val());
        console.log($Input1 + "" + $Input2);
        $.ajax({
            type: "GET",
            url: "/Home/GetCasePrice?min=" + $Input1 + "&max=" + $Input2,
            dataType: "json",
            success: (data) => {
                debugger;
                var $html = '';
                $("#data").empty();
                $.each(data, function (i, e) {
                    console.log(data);

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.caseName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +
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

    });

});
// Increase - Decrease
$(document).ready(function () {
    $(".case-up").on("click", function () {
        var $minval = parseInt($("#Case #price-min").val()),
            $maxval = parseInt($("#Case #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetCasePrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: (data) => {
                    debugger;
                    var $html = '';
                    $("#data").empty();
                    $.each(data, function (i, e) {
                        console.log(data);

                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#">' + e.caseName + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +
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
        })


    });

    $(".case-down").on("click", function () {
        var $minval = parseInt($("#Case #price-min").val()),
            $maxval = parseInt($("#Case #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetCasePrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: (data) => {
                    debugger;
                    var $html = '';
                    $("#data").empty();
                    $.each(data, function (i, e) {
                        console.log(data);

                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#">' + e.caseName + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +
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
        });


    });

});
//===================================== End Case ==============================================
//===================================== Start PowerSuply ============================================
// Hight To Low
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
                        '<h3 class="product-name"><a href="#">' + e.psuname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +
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
// Display Size
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
                    $("#data").empty();
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.psuname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +
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
    $("input[type='checkbox'].Kabear6").click(function () {
        debugger;
        $(this).each(function () {
            var $val = $(this).val().trim();
            if (this.checked) {
                arr.push($val)
            } else {
                for (var i = 0; i < arr.length; i++) {

                    if (arr[i] === $val) {

                        arr.splice(i, 1);
                    }

                }
            }

        });
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfPowerSuplyBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (data) {
                var $html = '';
                $("#data").empty();
                $.each(data, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +
                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.psuname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +
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


    });

});
// Price 
$(document).ready(function () {
    $("#PS #price-slider").on("click", function () {
        var $Input1 = parseInt($("#PS #price-min").val()),
            $Input2 = parseInt($("#PS #price-max").val());
        console.log($Input1 + "" + $Input2);
        $.ajax({
            type: "GET",
            url: "/Home/GetPSPrice?min=" + $Input1 + "&max=" + $Input2,
            dataType: "json",
            success: (data) => {
                debugger;
                var $html = '';
                $("#data").empty();
                $.each(data, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +
                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.psuname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +
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

    });

});
// Increase - Decrease
$(document).ready(function () {
    $(".PS-up").on("click", function () {
        var $minval = parseInt($("#PS #price-min").val()),
            $maxval = parseInt($("#PS #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetPSPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: (data) => {
                    debugger;
                    var $html = '';
                    $("#data").empty();
                    $.each(data, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +
                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#">' + e.psuname + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +
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
        })


    });

    $(".PS-down").on("click", function () {
        var $minval = parseInt($("#PS #price-min").val()),
            $maxval = parseInt($("#PS #price-max").val());
        $(this).each(function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetPSPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: (data) => {
                    debugger;
                    var $html = '';
                    $("#data").empty();
                    $.each(data, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +
                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#">' + e.psuname + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +
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
        });


    });

});
//===================================== End PowerSuply ==============================================




//////////////////////////////////////////Shared Functions//////////////////////////////////////////

//Details page
function gotoDetails(ProductCode) {
    debugger;
    if (ProductCode.startsWith("S")) {
        window.location = "/Home/SsdDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("R")) {
        window.location = "/Home/RamDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("PS")) {
        window.location = "/Home/PowerSupplyDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("Pr")) {
        window.location = "/Home/ProcessorDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("M")) {
        window.location = "/Home/MotherboardDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("H")) {
        window.location = "/Home/HddDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("C")) {
        window.location = "/Home/CaseDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("V")) {
        window.location = "/Home/GraphicsCardDetails?code=" + ProductCode
    }
}

//AddToCart
function AddToCart(Product) {
    var cart = getItemStorage("Cart") || [];
    let counter = 0;
    if (cart.length == 0) {
        setItemStorage("Cart", cart);
        addValueToItemStorage("Cart", Product);
        toastr.success('Done', '', { timeOut: 7000 });
        setTimeout(function () {
            location.reload();
        }, 1500);
    }
    else {
        $.each(cart, function (key, item) {
            if (!(item.Code == Product.Code)) {
                counter++;
                if (counter == cart.length) {
                    setItemStorage("Cart", cart);
                    addValueToItemStorage("Cart", Product);
                    toastr.success('Done', '', { timeOut: 7000 });
                    setTimeout(function () {
                        location.reload();
                    }, 1500);
                }
            }
            else {
                toastr.error('You Choose This Product Before!!', '', { timeOut: 7000 });
            }
        });
    }
}

//Add Or Delete From WL
function AddOrDeleteWL(Product , addordelete) {
    debugger;
    if ($('#love').hasClass("fa fa-heart") || addordelete == "Delete") {
        //لو ملونه هيروح بالاجكس على اكشن للديليت
        $.ajax({
            type: "POST",
            url: "/Home/DeleteFromWL?ProductCode=" + Product,
            dataType: "json",
            success: function (result) {
                if (result == "Deleted Done") {
                    setItemStorage("WishListMsg", "deletedFromWL");
                    location.reload();

                }
                else if (result == "Error") {
                    toastr.success('Something Wrong to delete , Try Again.', '', { timeOut: 7000 });
                }

            }
        });
    }
    else {
        $.ajax({
            //لو مش ملونه
            url: "/Home/AddToWL?ProductCode=" + Product,
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result == 'Saved Done') {
                    debugger;
                    setItemStorage("WishListMsg", "addedToWL");
                    location.reload();

                }
                else if (result == 'LoginRegisterPopup') {
                    debugger;
                    $('#login-register').click();
                }
                else {
                    debugger;
                    toastr.error('Something Wrong to add , Try again!!', '', { timeOut: 7000 });
                }

            }
        });
    }
}

//start statement of LocalStorage
function setItemStorage(itemKey, itemValue) {
    localStorage.setItem(itemKey, JSON.stringify(itemValue));
}
function getItemStorage(itemKey) {
    return JSON.parse(localStorage.getItem(itemKey));
}
function addValueToItemStorage(itemKey, val) {
    let item = getItemStorage(itemKey);
    item.push(val);
    setItemStorage(itemKey, item);
}
function removeItemStorage(itemKey, val) {
    let item = getItemStorage(itemKey);
    item.splice(val, 1);
    setItemStorage(itemKey, item);
}
function removeStorage(itemKey) {
    return JSON.parse(localStorage.removeItem(itemKey));
}
