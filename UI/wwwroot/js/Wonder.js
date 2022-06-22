﻿
 /*Processors*/
$(document).ready(function () {
    $("body").on("change", "#ProcessorPrice", function () {
        var $Price = $(this).val(), $html = '', $pagin ='';

        $.ajax({
            type: "GET",
            url: "/Home/AscendingProcessorProdoucts?Id=" + $Price,
            success: function (response) {
                console.log(response);
                $("#Pro").empty();
                $("#Processor").empty();
                for (var e of response.data) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.proName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.proPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.proPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.proRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.proRate, 1); i <= 5; i++) {
                        if (Math.round(e.proRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.proCode + "'" + ')"class="add-to-wishlist"><i id="' + e.proCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.proCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.proCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.proCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.proQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.proCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }


                $pagin += '<ul class="store-pagination" id="pagin">'
                $pagin += '<li  onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#Pro").html($html);
                $("#Processor").html($pagin);
            }
        });


    });
    $("body").on("change", "#ProcessorProduct", function () {
        var $Price = $(this).val(), $html = '', $pagin = '';
        $.ajax({
            url: '/Home/DefaultProcessor?PageSize=' + $Price,
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                console.log(response);
                $("#Pro").empty();
                $("#Processor").empty();
                for (var e of response.data) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.proName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.proPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.proPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.proRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.proRate, 1); i <= 5; i++) {
                        if (Math.round(e.proRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.proCode + "'" + ')"class="add-to-wishlist"><i id="' + e.proCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.proCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.proCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.proCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.proQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.proCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }


                $pagin += '<ul class="store-pagination" id="pagin">'
                $pagin += '<li  onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#Pro").html($html);
                $("#Processor").html($pagin);
            }
        });
    })
    var arr = [];
    $("body").on("click", "input[type='checkbox'].Kabear", function () {
        var $val = $(this).val().trim();
        var $pagin = '', $html = '';
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
            dataType: 'json',
            data: { brand: arr },

            success: function (response) {
                console.log(response);
                $("#Pro").empty();
                $("#Processor").empty();
                for (var e of response.data) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.proName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.proPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.proPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.proRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.proRate, 1); i <= 5; i++) {
                        if (Math.round(e.proRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.proCode + "'" + ')"class="add-to-wishlist"><i id="' + e.proCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.proCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.proCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.proCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.proQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.proCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="pagin">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#Pro").html($html);
                $("#Processor").html($pagin);

            }


        });


    })
});

/*Motherboard*/

$(document).ready(function () {
    $("body").on("change","#MotherPrice", function () {
        var $Price = $(this).val(),
            $html = "", $pagin ='';
        $.ajax({
            url: "/Home/AscendingMotherboardProdoucts?Id=" + $Price,
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                console.log(response);
                $("#moth").empty();
                $("#mother").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.motherPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.motherPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.motherRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.motherRate, 1); i <= 5; i++) {
                        if (Math.round(e.motherRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.mothCode + "'" + ')"class="add-to-wishlist"><i id="' + e.mothCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.mothCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.mothCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.motherQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.mothCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginH">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#moth").html($html);
                $("#mother").html($pagin);

            }

        });


    });
    $("body").on("change","#MotherProduct", function () {
        var $Data = $(this).val(),
            $html = '', $pagin = '';
        $.ajax({
            url: "/Home/DefaultMotherboard?PageSize=" + $Data,
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                console.log(response);
                $("#moth").empty();
                $("#mother").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.motherPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.motherPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.motherRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.motherRate, 1); i <= 5; i++) {
                        if (Math.round(e.motherRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.mothCode + "'" + ')"class="add-to-wishlist"><i id="' + e.mothCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.mothCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.mothCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.motherQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.mothCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginH">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#moth").html($html);
                $("#mother").html($pagin);

            }

        });
    })
    var arr = [];
    $("body").on("click", "input[type='checkbox'].Kabear1", function () {
        var $val = $(this).val().trim();
        var $pagin = '', $html = '';
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
            url: "/Home/ProductsOfMotherboardBrand",
            data: { brand: arr },
            dataType: 'json',
            success: function (response) {
                console.log(response);
                $("#moth").empty();
                $("#mother").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.motherName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.motherPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.motherPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.motherRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.motherRate, 1); i <= 5; i++) {
                        if (Math.round(e.motherRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.mothCode + "'" + ')"class="add-to-wishlist"><i id="' + e.mothCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.mothCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.mothCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.mothCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.motherQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.mothCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginH">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#moth").html($html);
                $("#mother").html($pagin);

            }

        });


    })
});

 /*HDD*/
$(document).ready(function () {
    $("body").on("change","#HDDPrice", function () {
        var $Price = $(this).val(),
             $html = '', $pagin = '';
        $.ajax({
            type: "GET",
            url: "/Home/AscendingHDDProdoucts?Id=" + $Price,
            success: function (response) {
                console.log(response);
                $("#hdd").empty();
                $("#H").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.hddrate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.hddrate, 1); i <= 5; i++) {
                        if (Math.round(e.hddrate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.hddrate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.hddrate, 1)) {
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.hddcode + "'" + ')"class="add-to-wishlist"><i id="' + e.hddcode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.hddcode + "'" + ')"class="add-to-wishlist"><i  id="' + e.hddcode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.hddcode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.motherQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.mothCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginH">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#hdd").html($html);
                $("#H").html($pagin);

            }
        });


    });
    $("body").on("change","#HDDProduct", function () {
        var $Price = $(this).val(), $html = '', $pagin = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultHDD?PageSize=" + $Price,
            success: function (response) {
                console.log(response);
                $("#hdd").empty();
                $("#H").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.hddrate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.hddrate, 1); i <= 5; i++) {
                        if (Math.round(e.hddrate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.hddrate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.hddrate, 1)) {
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.hddcode + "'" + ')"class="add-to-wishlist"><i id="' + e.hddcode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.hddcode + "'" + ')"class="add-to-wishlist"><i  id="' + e.hddcode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.hddcode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.motherQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.mothCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginH">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#hdd").html($html);
                $("#H").html($pagin);

            }


        })
    })
    var arr = [];
    $("body").on("click", "input[type='checkbox'].Kabear2", function () {
        var $val = $(this).val().trim(), $html = '', $pagin = '';
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
            url: "/Home/ProductsOfHDDBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (response) {
                console.log(response);
                $("#hdd").empty();
                $("#H").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.hddrate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.hddrate, 1); i <= 5; i++) {
                        if (Math.round(e.hddrate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.hddrate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.hddrate, 1)) {
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.hddcode + "'" + ')"class="add-to-wishlist"><i id="' + e.hddcode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.hddcode + "'" + ')"class="add-to-wishlist"><i  id="' + e.hddcode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.hddcode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.motherQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.mothCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginH">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#hdd").html($html);
                $("#H").html($pagin);

            }

        });


    });
});

/*RAM*/
$(document).ready(function () {
    $("body").on("change", "#RAMPrice", function () {
        var $Price = $(this).val(),
            $html = "",$pagin='';
        $.ajax({
            type: "GET",
            url: "/Home/AscendingRAMProdoucts?Id=" + $Price,
            success: function (response) {
                console.log(response);
                $("#ram").empty();
                $("#R").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.ramName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.ramPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ramPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.ramRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.ramRate, 1); i <= 5; i++) {
                        if (Math.round(e.ramRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.ramRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.ramRate, 1)) {
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ramCode + "'" + ')"class="add-to-wishlist"><i id="' + e.ramCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ramCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.ramCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.ramCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.ramQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.ramCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginR">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#ram").html($html);
                $("#R").html($pagin);

            }
        });


    });
    $("body").on("change","#RAMProduct",function () {
        var $Price = $(this).val(),
            $html = '', $pagin ='';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultRAM?PageSize=" + $Price,
            success: function (response) {
                console.log(response);
                $("#ram").empty();
                $("#R").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4">' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.ramName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.ramPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ramPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.ramRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.ramRate, 1); i <= 5; i++) {
                        if (Math.round(e.ramRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.ramRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.ramRate, 1)) {
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ramCode + "'" + ')"class="add-to-wishlist"><i id="' + e.ramCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ramCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.ramCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.ramCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.ramQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.ramCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginR">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#ram").html($html);
                $("#R").html($pagin);

            }


        })
    })
    var arr = [];
    $("body").on("click", "input[type='checkbox'].Kabear3", function () {
        var $val = $(this).val().trim(),$html = '', $pagin = '';
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
            url: "/Home/ProductsOfRAMBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (response) {
                console.log(response);
                $("#ram").empty();
                $("#R").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.ramName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.ramPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ramPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.ramRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.ramRate, 1); i <= 5; i++) {
                        if (Math.round(e.ramRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.ramRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.ramRate, 1)) {
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ramCode + "'" + ')"class="add-to-wishlist"><i id="' + e.ramCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ramCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.ramCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.ramCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.ramQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.ramCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginR">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#ram").html($html);
                $("#R").html($pagin);

            }
        });


    })
});

 /*SSD */
$(document).ready(function () {
    $("body").on("change", "#SSDPrice" ,function () {
        var $Price = $(this).val(),
            $html = "",$pagin='';
        $.ajax({
            type: "GET",
            url: "/Home/AscendingSSDProdoucts?Id=" + $Price,
            success: function (response) {
                console.log(response);
                $("#SSD").empty();
                $("#S").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.ssdname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.ssdprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ssdprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.ssdrate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.ssdrate, 1); i <= 5; i++) {
                        if (Math.round(e.ssdrate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.ssdrate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.ssdrate, 1)) {
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ssdcode + "'" + ')"class="add-to-wishlist"><i id="' + e.ssdcode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ssdcode + "'" + ')"class="add-to-wishlist"><i  id="' + e.ssdcode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.ssdcode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.ssdquantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.ssdcode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginS" style="margin-top: 3.5rem;">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#SSD").html($html);
                $("#S").html($pagin);
            }
        });


    });
    $("body").on("change", "#SSDProduct", function () {
        var $Price = $(this).val(),
            $html = '', $pagin ='';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultSSD?PageSize=" + $Price,
            success: function (response) {
                console.log(response);
                $("#SSD").empty();
                $("#S").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.ssdname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.ssdprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ssdprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.ssdrate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.ssdrate, 1); i <= 5; i++) {
                        if (Math.round(e.ssdrate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.ssdrate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.ssdrate, 1)) {
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ssdcode + "'" + ')"class="add-to-wishlist"><i id="' + e.ssdcode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ssdcode + "'" + ')"class="add-to-wishlist"><i  id="' + e.ssdcode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.ssdcode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.ssdquantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.ssdcode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginS" style="margin-top: 3.5rem;">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#SSD").html($html);
                $("#S").html($pagin);
            }


        })
    })
    var arr = [];
    $("body").on("click", "input[type='checkbox'].Kabear4", function () {
   
        var $val = $(this).val().trim(), $html ='',$pagin='';
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
            url: "/Home/ProductsOfSSDBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (response) {
                console.log(response);
                $("#SSD").empty();
                $("#S").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.ssdname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.ssdprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ssdprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.ssdrate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.ssdrate, 1); i <= 5; i++) {
                        if (Math.round(e.ssdrate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.ssdrate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.ssdrate, 1)) {
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ssdcode + "'" + ')"class="add-to-wishlist"><i id="' + e.ssdcode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ssdcode + "'" + ')"class="add-to-wishlist"><i  id="' + e.ssdcode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.ssdcode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.ssdquantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.ssdcode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginS" style="margin-top: 3.5rem;">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#SSD").html($html);
                $("#S").html($pagin);
            }

        });


    });
   

});

/*Graphics Card */
$(document).ready(function () {
    $("body").on("change", "#CardPrice", function () {
        var $Price = $(this).val(),
            $html = "",$pagin='';
        $.ajax({
            type: "GET",
            url: "/Home/AscendingCardProdoucts?Id=" + $Price,
            success: function (response) {
                console.log(response);
                $("#card").empty();
                $("#C").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4"  >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.vganame + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.vgaprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.vgaprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.vgarate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.vgarate, 1); i <= 5; i++) {
                        if (Math.round(e.vgarate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.vgarate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.vgarate, 1)) {
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.vgacode + "'" + ')"class="add-to-wishlist"><i id="' + e.vgacode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.vgacode + "'" + ')"class="add-to-wishlist"><i  id="' + e.vgacode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.vgacode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.vgaquantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.vgacode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginC" style="margin-top: 3.5rem;">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#card").html($html);
                $("#C").html($pagin);
            }
        });


    });
    $("body").on("change", "#CardProduct", function () {
        var $Price = $(this).val(),
            $html = '', $pagin = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultCard?PageSize=" + $Price,
            success: function (response) {
                console.log(response);
                $("#card").empty();
                $("#C").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.vganame + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.vgaprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.vgaprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.vgarate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.vgarate, 1); i <= 5; i++) {
                        if (Math.round(e.vgarate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.vgarate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.vgarate, 1)) {
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.vgacode + "'" + ')"class="add-to-wishlist"><i id="' + e.vgacode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.vgacode + "'" + ')"class="add-to-wishlist"><i  id="' + e.vgacode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.vgacode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.vgaquantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.vgacode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginC" style="margin-top: 3.5rem;">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#card").html($html);
                $("#C").html($pagin);
            }
        })
    });
    var arr = [];
    $("body").on("click", "input[type='checkbox'].Kabear5", function () {
        var $val = $(this).val().trim(), $html = '', $pagin = '';
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
            url: "/Home/ProductsOfCardBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (response) {
                console.log(response);
                $("#card").empty();
                $("#C").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.vganame + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.vgaprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.vgaprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.vgarate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.vgarate, 1); i <= 5; i++) {
                        if (Math.round(e.vgarate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.vgarate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.vgarate, 1)) {
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
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.vgacode + "'" + ')"class="add-to-wishlist"><i id="' + e.vgacode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.vgacode + "'" + ')"class="add-to-wishlist"><i  id="' + e.vgacode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.vgacode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.vgaquantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.vgacode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginC" style="margin-top: 3.5rem;">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#card").html($html);
                $("#C").html($pagin);
            }
        });


    });
});

/* Case*/
// Hight To Low
$(document).ready(function () {
    $("#CasePrice").on("change", function () {
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
                        '<h3 class="product-name"><a href="#" style="font-size: 1.2rem;">' + e.caseName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +

                        '<div class="product-rating">';
                    var length = parseInt(Math.round(e.caseRate, 1));
                    for (var i = 1; i < length; i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = length; i <= 5; i++) {
                        if (Math.round(e.caseRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.caseRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.caseRate, 1)) {
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
                    $html += '</div>' +

                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.caseCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.caseName + "'" + ', Code:' + "'" + e.caseCode + "'" + ', Price:' + e.casePrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#data').html($html);
            }
        });


    });


});
// Display Size
$(document).ready(function () {
    $("#CaseProduct").on("change", function () {
        var $Price = $(this).val(),
            $html = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultCase?PageSize=" + $Price,
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
                        '<h3 class="product-name"><a href="#" style="font-size: 1.2rem;">' + e.caseName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +

                        '<div class="product-rating">';
                    var length = parseInt(Math.round(e.caseRate, 1));
                    for (var i = 1; i < length; i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = length; i <= 5; i++) {
                        if (Math.round(e.caseRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.caseRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.caseRate, 1)) {
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
                    $html += '</div>' +

                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.caseCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.caseName + "'" + ', Code:' + "'" + e.caseCode + "'" + ', Price:' + e.casePrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#data').html($html);
            }


        })
    })
});
// Checkbox
$(document).ready(function () {
    var arr = [];
    $("input[type='checkbox'].Kabear6").click(function () {
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
            success: function (result) {
                console.log(result);
                var $html = '';
                $("#data").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#" style="font-size: 1.2rem;">' + e.caseName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +

                        '<div class="product-rating">';
                    var length = parseInt(Math.round(e.caseRate, 1));
                    for (var i = 1; i < length; i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = length; i <= 5; i++) {
                        if (Math.round(e.caseRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.caseRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.caseRate, 1)) {
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
                    $html += '</div>' +

                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.caseCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.caseName + "'" + ', Code:' + "'" + e.caseCode + "'" + ', Price:' + e.casePrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#data').html($html);
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
            success: function (result) {
                console.log(result);
                var $html = '';
                $("#data").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#" style="font-size: 1.2rem;">' + e.caseName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +

                        '<div class="product-rating">';
                    var length = parseInt(Math.round(e.caseRate, 1));
                    for (var i = 1; i < length; i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = length; i <= 5; i++) {
                        if (Math.round(e.caseRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.caseRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.caseRate, 1)) {
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
                    $html += '</div>' +

                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.caseCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.caseName + "'" + ', Code:' + "'" + e.caseCode + "'" + ', Price:' + e.casePrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#data').html($html);
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
                success: function (result) {
                    console.log(result);
                    var $html = '';
                    $("#data").empty();
                    $.each(result, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#" style="font-size: 1.2rem;">' + e.caseName + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +

                            '<div class="product-rating">';
                        var length = parseInt(Math.round(e.caseRate, 1));
                        for (var i = 1; i < length; i++) {
                            $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                        }
                        for (var i = length; i <= 5; i++) {
                            if (Math.round(e.caseRate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.caseRate, 1)) {
                                    $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.caseRate, 1)) {
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
                        $html += '</div>' +

                            '<div class="product-btns">' +
                            '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                            '<button onclick="gotoDetails(' + "'" + e.caseCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                            '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.caseName + "'" + ', Code:' + "'" + e.caseCode + "'" + ', Price:' + e.casePrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                            ' </div>' +
                            '</div>' +

                            '</div > ' +
                            ' </div>';
                    })
                    $('#data').html($html);
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
                success: function (result) {
                    console.log(result);
                    var $html = '';
                    $("#data").empty();
                    $.each(result, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#" style="font-size: 1.2rem;">' + e.caseName + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +

                            '<div class="product-rating">';
                        var length = parseInt(Math.round(e.caseRate, 1));
                        for (var i = 1; i < length; i++) {
                            $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                        }
                        for (var i = length; i <= 5; i++) {
                            if (Math.round(e.caseRate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.caseRate, 1)) {
                                    $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.caseRate, 1)) {
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
                        $html += '</div>' +

                            '<div class="product-btns">' +
                            '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                            '<button onclick="gotoDetails(' + "'" + e.caseCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                            '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.caseName + "'" + ', Code:' + "'" + e.caseCode + "'" + ', Price:' + e.casePrice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                            ' </div>' +
                            '</div>' +

                            '</div > ' +
                            ' </div>';
                    })
                    $('#data').html($html);
                }
            });
        });


    });

});
//===================================== End Case ==============================================
//===================================== Start PowerSuply ============================================
// Hight To Low
$(document).ready(function () {
    $("#PSPrice").on("change", function () {
        var $Price = $(this).val(),
            $html = "";
        $.ajax({
            type: "GET",
            url: "/Home/AscendingPowerSuplyProdoucts?Id=" + $Price,
            success: function (result) {
                console.log(result);
                $("#PWS").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#" style="font-size: 1.2rem;">' + e.psuname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +

                        '<div class="product-rating">';
                    var length = parseInt(Math.round(e.psurate, 1));
                    for (var i = 1; i < length; i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = length; i <= 5; i++) {
                        if (Math.round(e.psurate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.psurate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.psurate, 1)) {
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
                    $html += '</div>' +

                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.psucode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.psuname + "'" + ', Code:' + "'" + e.psucode + "'" + ', Price:' + e.psuprice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#PWS').html($html);
            }
        });


    });


});
// Display Size
$(document).ready(function () {
    $("#PSProduct").on("change", function () {
        var $Price = $(this).val(),
            $html = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultPowerSuply?PageSize=" + $Price,
            success: function (result) {
                console.log(result);
                $("#PWS").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#" style="font-size: 1.2rem;">' + e.psuname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +

                        '<div class="product-rating">';
                    var length = parseInt(Math.round(e.psurate, 1));
                    for (var i = 1; i < length; i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = length; i <= 5; i++) {
                        if (Math.round(e.psurate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.psurate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.psurate, 1)) {
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
                    $html += '</div>' +

                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.psucode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.psuname + "'" + ', Code:' + "'" + e.psucode + "'" + ', Price:' + e.psuprice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#PWS').html($html);
            }


        })
    })
});
// Checkbox
$(document).ready(function () {
    var arr = [];
    $("input[type='checkbox'].Kabear7").click(function () {
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
            success: function (result) {
                console.log(result);
                var $html = '';
                $("#PWS").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#" style="font-size: 1.2rem;">' + e.psuname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +

                        '<div class="product-rating">';
                    var length = parseInt(Math.round(e.psurate, 1));
                    for (var i = 1; i < length; i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = length; i <= 5; i++) {
                        if (Math.round(e.psurate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.psurate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.psurate, 1)) {
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
                    $html += '</div>' +

                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.psucode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.psuname + "'" + ', Code:' + "'" + e.psucode + "'" + ', Price:' + e.psuprice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#PWS').html($html);
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
            url: "/Home/GetPowerSuplyPrice?min=" + $Input1 + "&max=" + $Input2,
            dataType: "json",
            success: function (result) {
                console.log(result);
                var $html = '';
                $("#PWS").empty();
                $.each(result, function (i, e) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img src="/img/product01.png" alt="">' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#" style="font-size: 1.2rem;">' + e.psuname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +

                        '<div class="product-rating">';
                    var length = parseInt(Math.round(e.psurate, 1));
                    for (var i = 1; i < length; i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = length; i <= 5; i++) {
                        if (Math.round(e.psurate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.psurate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.psurate, 1)) {
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
                    $html += '</div>' +

                        '<div class="product-btns">' +
                        '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                        '<button onclick="gotoDetails(' + "'" + e.psucode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                        '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.psuname + "'" + ', Code:' + "'" + e.psucode + "'" + ', Price:' + e.psuprice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                        '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                        ' </div>' +
                        '</div>' +

                        '</div > ' +
                        ' </div>';
                })
                $('#PWS').html($html);
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
                url: "/Home/GetPowerSuplyPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: function (result) {
                    console.log(result);
                    var $html = '';
                    $("#PWS").empty();
                    $.each(result, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#" style="font-size: 1.2rem;">' + e.psuname + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +

                            '<div class="product-rating">';
                        var length = parseInt(Math.round(e.psurate, 1));
                        for (var i = 1; i < length; i++) {
                            $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                        }
                        for (var i = length; i <= 5; i++) {
                            if (Math.round(e.psurate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.psurate, 1)) {
                                    $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.psurate, 1)) {
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
                        $html += '</div>' +

                            '<div class="product-btns">' +
                            '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                            '<button onclick="gotoDetails(' + "'" + e.psucode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                            '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.psuname + "'" + ', Code:' + "'" + e.psucode + "'" + ', Price:' + e.psuprice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                            ' </div>' +
                            '</div>' +

                            '</div > ' +
                            ' </div>';
                    })
                    $('#PWS').html($html);
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
                url: "/Home/GetPowerSuplyPrice?min=" + $minval + "&max=" + $maxval,
                dataType: "json",
                success: function (result) {
                    console.log(result);
                    var $html = '';
                    $("#PWS").empty();
                    $.each(result, function (i, e) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img src="/img/product01.png" alt="">' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name"><a href="#" style="font-size: 1.2rem;">' + e.psuname + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +

                            '<div class="product-rating">';
                        var length = parseInt(Math.round(e.psurate, 1));
                        for (var i = 1; i < length; i++) {
                            $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                        }
                        for (var i = length; i <= 5; i++) {
                            if (Math.round(e.psurate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.psurate, 1)) {
                                    $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.psurate, 1)) {
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
                        $html += '</div>' +

                            '<div class="product-btns">' +
                            '<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>' +
                            '<button onclick="gotoDetails(' + "'" + e.psucode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>' +
                            '<button onclick="AddToCart({ Image:' + "'" + e.image + "'" + ', Name:' + "'" + e.psuname + "'" + ', Code:' + "'" + e.psucode + "'" + ', Price:' + e.psuprice + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>' +

                            ' </div>' +
                            '</div>' +

                            '</div > ' +
                            ' </div>';
                    })
                    $('#PWS').html($html);
                }
            });
        });


    });

});
//===================================== End PowerSuply ==============================================




//////////////////////////////////////////Shared Functions//////////////////////////////////////////

//Details page
function gotoDetails(ProductCode) {
    if (ProductCode.startsWith("SSD")) {
        window.location = "/Home/SsdDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("R")) {
        window.location = "/Home/RamDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("CAS")) {
        window.location = "/Home/CaseDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("V")) {
        window.location = "/Home/GraphicsCardDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("PS")) {
        window.location = "/Home/PowerSupplyDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("PR")) {
        window.location = "/Home/ProcessorDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("MO")) {
        window.location = "/Home/MotherboardDetails?code=" + ProductCode
    } else if (ProductCode.startsWith("HD")) {
        window.location = "/Home/HddDetails?code=" + ProductCode
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
        cartconnection();
    }
    else {
        $.each(cart, function (key, item) {
            if (!(item.Code == Product.Code)) {
                counter++;
                if (counter == cart.length) {
                    setItemStorage("Cart", cart);
                    addValueToItemStorage("Cart", Product);
                    toastr.success('Done', '', { timeOut: 7000 });
                    cartconnection();
                }
            }
            else {
                toastr.error('You Choose This Product Before!!', '', { timeOut: 7000 });
            }
        });
    }
}

//Add Or Delete From WL
function AddOrDeleteWL(Product){
    if ($('#' + Product).hasClass("fa fa-heart")) {
        //لو ملونه هيروح بالاجكس على اكشن للديليت
        $.ajax({
            type: "POST",
            url: "/Home/DeleteFromWL?ProductCode=" + Product,
            dataType: "json",
            success: function (result) {
                if (result == "Deleted Done") {
                    $('#' + Product).removeClass('fa fa-heart').addClass("fa fa-heart-o").css('color', '');
                    toastr.success('Deleted Done.', '', { timeOut: 7000 });
                    wishlistconnection();
                }
                else if (result == "Error") {
                    toastr.error('Something Wrong to delete , Try Again.', '', { timeOut: 7000 });
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
                    $('#' + Product).removeClass('fa fa-heart-o').addClass("fa fa-heart").css('color', '#D10024');
                    toastr.success('Product successfully added to your wishlist', '', { timeOut: 7000 });
                    wishlistconnection();
                }
                else if (result == 'LoginRegisterPopup') {
                    $('#login-register').click();
                }
                else {
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
    localStorage.removeItem(itemKey);
}
