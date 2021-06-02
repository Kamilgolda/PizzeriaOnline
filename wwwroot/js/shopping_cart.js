// Remove Items From Shopping-cart
$('a.remove').click(function () {
    event.preventDefault();
    $(this).parent().parent().parent().hide(400);

})



// WYBOR ROZMIARU I ILOSCI 
function select_product(elmnt) {
    let parent = elmnt.parentElement.parentElement;
    product = {
        'Id': parent.children[0].innerHTML,
        'Title': parent.children[2].children[0].children[0].innerHTML,
        'Size': "",
        'Quantity': 1
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

function addToShoppingCart() {
    size = $(".prodSize option:selected").val();
    if (size != 0) {
        var products = JSON.parse(sessionStorage.getItem('productsArray')) || [];
        for (i = 0; i < products.length; i++) {
            if (product.Id == products[i].Id) {
                $('#cart').modal('hide');
                return;
            }
        }
        newItem = {
            'Id': product.Id,
            'Title': product.Title,
            'Size': size,
            'Quantity': product.Quantity
        };
        products.push(newItem)
        sessionStorage.setItem('productsArray', JSON.stringify(products));
        $('#cart').modal('hide');
        
    }
    else alert("Nie wybrano rozmiaru")
}
//********************************************