// WYBOR ROZMIARU I ILOSCI 
function select_product(elmnt) {
    let parent = elmnt.parentElement.parentElement;
    product = {
        'Id': parent.children[0].innerHTML,
        'Title': parent.children[2].children[0].children[0].innerHTML,
        'Size': "",
        'Quantity': 1,
        'PriceSmall': parseInt(parent.children[3].innerHTML),
        'PriceMedium': parseInt(parent.children[4].innerHTML),
        'PriceLarge': parseInt(parent.children[5].innerHTML)
    };
    displayCart()
}


function displayCart() {
    $('#prodTitle').text(product.Title);
    $('#prodQuantity').val(product.Quantity)
}


// -1
$('.show-cart').on("click", ".minus-item", function (event) {
    if (product.Quantity > 1)
    product.Quantity-=1
    displayCart();
})
// +1
$('.show-cart').on("click", ".plus-item", function (event) {
    if (product.Quantity < 10)
        product.Quantity += 1
    displayCart();
})

var products = JSON.parse(sessionStorage.getItem('productsArray')) || [];

function addToShoppingCart() {
    size = $(".prodSize option:selected").val();
    price = 0;
    if (size != 0) {
        for (i = 0; i < products.length; i++) {
            if (product.Id == products[i].Id && size == products[i].Size) {
                $('#cart').modal('hide');
                return;
            }
        }
        newItem = {
            'Id': product.Id,
            'Title': product.Title,
            'Size': size,
            'Quantity': product.Quantity,
            'PriceSmall': product.PriceSmall,
            'PriceMedium': product.PriceMedium,
            'PriceLarge': product.PriceLarge
        };
        products.push(newItem)
        sessionStorage.setItem('productsArray', JSON.stringify(products));
        $('#cart').modal('hide');
        
    }
    else alert("Nie wybrano rozmiaru")
}
//********************************************

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
            + "<img src='http://lorempixel.com/output/technics-q-c-300-300-4.jpg' alt='' class='itemImg' />"
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
    sessionStorage.setItem('productsArray', JSON.stringify(products));
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
    sessionStorage.setItem('productsArray', JSON.stringify(products));
    displayShoppingCart()
}
