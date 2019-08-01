//function addProduct(e) {
//    e.preventDefault();
//    e.stopPropagation();

//    let productId = document.querySelector('#productId').value;
//    let quantity = +document.querySelector('#quantity').value;

//    let shoppingCart = JSON.parse(localStorage.getItem('cart'));

//    if (!shoppingCart) {
//        shoppingCart = {};
//    }

//    if (!shoppingCart.hasOwnProperty(productId)) {
//        shoppingCart[productId] = 0;
//    }

//    shoppingCart[productId] += quantity;

//    localStorage.setItem('cart', JSON.stringify(shoppingCart));
//}

//document.getElementById('dropdown-shopping-cart-items').innerHTML();

//let addBtn = document.getElementById('add-to-cart');

//addBtn.addEventListener('click', addProduct);