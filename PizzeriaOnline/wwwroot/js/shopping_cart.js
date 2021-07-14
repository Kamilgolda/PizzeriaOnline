var products = JSON.parse(window.sessionStorage.getItem('productsArray')) || [];
var create_order_button = document.getElementById("create_order");
function displayShoppingCart() {
    var output = "";
    finalPrice = 0;
    for (i = 0; i < products.length; i++) {
        if (i % 2 == 0) classitem = 'odd';
        else classitem = 'even'
        if (products[i].Size == 1) {
            size = "mała";
            price = products[i].PriceSmall;
            totalprice = products[i].Quantity * price
        }
        if (products[i].Size == 2) {
            size = "średnia";
            price = products[i].PriceMedium;
            totalprice = products[i].Quantity * price
        }
        if (products[i].Size == 3) {
            size = "duża";
            price = products[i].PriceLarge;
            totalprice = products[i].Quantity * price
        }
        finalPrice += totalprice

        output += "<li class='items " + classitem +"'>"
            + "<div class='infoWrap'>"
            + "<div class='cartSection'>"
            + "<img src='" + products[i].Thumbnail +"' alt='' class='itemImg' />"
            + "<h3>" + products[i].Title + "</h3>"
            + "<p>" + size + "</p><br>"
            + "<p> <input type='text' class='qty' oninput='changeQuantity(this)' placeholder='" + products[i].Quantity + "' /> x " + price +"zł</p>"
            + "</div>"
            + "<div class='prodTotal cartSection'>"
            + "<p>" + totalprice +"zł</p>"
            + "</div>"
            + "<div class='cartSection removeWrap'>"
            + "<a href='#' class='remove' onclick='removeProduct(this)'>x</a>"
            + "</div>"
            + "</div>"
            + "</li>";
    }
    $('.cartWrap').html(output);
    $('.value').text(finalPrice + "zł");
    if (products.length == 0) create_order_button.classList.add("disabled")
    else create_order_button.classList.remove("disabled");
}
// Remove Items From Shopping-cart
function removeProduct(elmnt) {
    productTitle = elmnt.parentElement.parentElement.children[0].children[1].innerHTML;
    productSize = elmnt.parentElement.parentElement.children[0].children[2].innerHTML;
    if (productSize == "mała") Size = 1;
    if (productSize == "średnia") Size = 2;
    if (productSize == "duża") Size = 3;
    for (i = 0; i < products.length; i++) {
        if (products[i].Title == productTitle && products[i].Size == Size)
            products.splice(i, 1);
    }
    window.sessionStorage.setItem('productsArray', JSON.stringify(products));
    $(elmnt).parent().parent().parent().hide(400, function () {
        displayShoppingCart()
    });
}

function changeQuantity(elmnt) {
    newQuantity = elmnt.value;
    Title = elmnt.parentElement.parentElement.children[1].innerHTML
    Size = elmnt.parentElement.parentElement.children[2].innerHTML
    if (Size == "mała") Size = 1;
    if (Size == "średnia") Size = 2;
    if (Size == "duża") Size = 3;
    for (i = 0; i < products.length; i++) {
        if (Title == products[i].Title && Size == products[i].Size) {
            products[i].Quantity = newQuantity;
        }
    }
    window.sessionStorage.setItem('productsArray', JSON.stringify(products));
    displayShoppingCart()
}
