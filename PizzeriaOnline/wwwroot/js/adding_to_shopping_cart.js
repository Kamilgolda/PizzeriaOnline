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
        'PriceLarge': parseInt(parent.children[5].innerHTML),
        'Thumbnail': parent.children[1].children[0].getAttribute("src")
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
        product.Quantity -= 1
    displayCart();
})
// +1
$('.show-cart').on("click", ".plus-item", function (event) {
    if (product.Quantity < 10)
        product.Quantity += 1
    displayCart();
})

var products = JSON.parse(window.sessionStorage.getItem('productsArray')) || [];

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
            'PriceLarge': product.PriceLarge,
            'Thumbnail': product.Thumbnail
        };
        products.push(newItem)
        window.sessionStorage.setItem('productsArray', JSON.stringify(products));
        $('#cart').modal('hide');

    }
    else alert("Nie wybrano rozmiaru")
}
//********************************************