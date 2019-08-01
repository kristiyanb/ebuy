function incrementQuantity(e) {
    e.preventDefault();
    quantity.value = +quantity.value + 1;
}

function decrementQuantity(e) {
    e.preventDefault();

    if (+quantity.value === 1) {
        quantity.value = 1;
        return;
    }

    quantity.value = +quantity.value - 1;
}

function addProduct(e) {
    e.preventDefault();
    e.stopPropagation();

    let productId = document.querySelector('#product-id').value;
    let quantity = +document.querySelector('#quantity').value;

    if (quantity > 0) {
        let shoppingCart = JSON.parse(localStorage.getItem('cart'));

        if (!shoppingCart) {
            shoppingCart = {};
        }

        if (!shoppingCart.hasOwnProperty(productId)) {
            shoppingCart[productId] = 0;
        }

        shoppingCart[productId] += quantity;

        localStorage.setItem('cart', JSON.stringify(shoppingCart));

        loadShoppingCartDropdown();
    }

    document.querySelector('#shoppingCartDropdownMenuButton').setAttribute('aria-expanded', true);
    document.querySelector('#shoppingCart').classList.add('show');
    document.querySelector('#shoppingCart').parentElement.classList.add('show');

    setTimeout(() => {
        document.querySelector('#shoppingCartDropdownMenuButton').setAttribute('aria-expanded', false);
        document.querySelector('#shoppingCart').classList.remove('show');
        document.querySelector('#shoppingCart').parentElement.classList.remove('show');
    }, 2500);
}

let quantity = document.getElementById('quantity');

document.querySelector('#quantity-increment').addEventListener('click', incrementQuantity);
document.querySelector('#quantity-decrement').addEventListener('click', decrementQuantity);
document.querySelector('#add-to-cart').addEventListener('click', addProduct);