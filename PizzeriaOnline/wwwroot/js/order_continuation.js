var products = JSON.parse(window.sessionStorage.getItem('productsArray')) || [];

var price = 0.0

let productsParent = document.getElementById("products");
for (i = 0; i < products.length; i++) {
    productsParent.children[i].children[0].setAttribute("value", products[i].Id);
    productsParent.children[i].children[1].setAttribute("value", products[i].Size);
    productsParent.children[i].children[2].setAttribute("value", products[i].Quantity);

    if (products[i].Size == 1) {
        price += products[i].PriceSmall * products[i].Quantity
    }
    else if (products[i].Size == 2) {
        price += products[i].PriceMedium * products[i].Quantity
    }
    else price += products[i].PriceLarge * products[i].Quantity
}
$('#price').text("Koszt " + price + "zł");

var delivery = document.getElementById("delivery");

if(price < 50.0) delivery.setAttribute("disabled", "disabled");