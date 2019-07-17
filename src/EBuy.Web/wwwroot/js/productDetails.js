let quantity = document.getElementById('quantity');

document.querySelector('#quantity-increment').addEventListener('click', incrementQuantity);
document.querySelector('#quantity-decrement').addEventListener('click', decrementQuantity);

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