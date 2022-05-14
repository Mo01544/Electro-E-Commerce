function search() {
    var productName = $("#search-input").val();
    console.log(productName);
    $.ajax({
        url: `/Products/Search?ProductName=${productName}`,
        success: function (products) {
            /*$("#main").html(products);*/
            $('#store-products').html(products)
        }
    });
}