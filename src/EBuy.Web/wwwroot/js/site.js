// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

let quantity = document.getElementById('quantity');

document.querySelector('#quantity-increment').addEventListener('click', incrementQuantity);
document.querySelector('#quantity-decrement').addEventListener('click', decrementQuantity);

function incrementQuantity(e) {
    e.preventDefault();
    quantity.value = +quantity.value + 1;
}

function decrementQuantity(e) {
    e.preventDefault();
    quantity.value = +quantity.value - 1;
}