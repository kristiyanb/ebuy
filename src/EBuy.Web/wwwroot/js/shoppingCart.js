async function loadShoppingCart() {
    const baseUrl = 'http://localhost:50329/api/shoppingCart/';

    let indexShoppingCart = '<h3 class="text-center">Your items: </h3><hr />';
    let totalCost = 0;
    let shoppingCart = JSON.parse(localStorage.getItem('cart'));
    let productIds;

    if (shoppingCart) {
        productIds = Object.keys(shoppingCart).filter(x => shoppingCart[x] !== 0);

        if (productIds.length == 0) {
            document.getElementById('shopping-cart-products').innerHTML = `
            <h3 class="text-center mt-5">Your cart is empty.</h3>
            <div class="text-center mt-3">
                <a class="btn-link" href="/Home">Back to website.</a>
            </div>`
        } else {
            for (let id of productIds) {
                let productInfo = await fetch(baseUrl + id);
                let product = await productInfo.json();

                indexShoppingCart += `
            <div id="shoppingCart-item" class="d-flex justify-content-between mt-1 mb-1">
                <img width="200" height="150" src="${product.imageUrl}" />
                <div class="w-25 text-center">
                    <h6 class="mb-3">${product.name}</h6>
                    <p>Price: $${product.price.toFixed(2)}</p>
                    <div class="d-flex border h-25 w-100 justify-content-between mb-2">
                        <input value="${id}" hidden/>
                        <button class="btn bg-white decrement" type="button" onClick=decrementShoppingCartQuantity(event)>-</button>
                        <input class="quantity border-0" type="number" value="${shoppingCart[id]}" disabled/>
                        <button class="btn bg-white increment" type="button" onClick=incrementShoppingCartQuantity(event)>+</button>
                    </div>
                    <a href="#" class="btn-link text-danger" onClick=removeItem(event)>Remove</a>
                </div>
            </div>
            <hr />`;

                totalCost = totalCost + (+shoppingCart[id] * +product.price);
            }

            indexShoppingCart += `
        <div class="d-flex justify-content-between mt-4 mb-3">
            <h5>
                Total:
            </h5>
            <div>
                $${totalCost.toFixed(2)}
            </div>
        </div>
        <div class="text-center mt-2">
            <a class="btn btn-danger w-25 mt-2" href="/Checkout/Order">Checkout</a>
        </div>`

            document.getElementById('shopping-cart-products').innerHTML = indexShoppingCart;
        }
    }
}

function incrementShoppingCartQuantity(e) {
    let id = e.target.parentElement.querySelector('input').value;
    let shoppingCart = JSON.parse(localStorage.getItem('cart'));

    shoppingCart[id] = +shoppingCart[id] + 1;
    localStorage.setItem('cart', JSON.stringify(shoppingCart));

    loadShoppingCart();
    loadShoppingCartDropdown();
}

function decrementShoppingCartQuantity(e) {
    let id = e.target.parentElement.querySelector('input').value;
    let shoppingCart = JSON.parse(localStorage.getItem('cart'));

    if (+shoppingCart[id] > 1) {
        shoppingCart[id] = +shoppingCart[id] - 1;
        localStorage.setItem('cart', JSON.stringify(shoppingCart));

        loadShoppingCart();
        loadShoppingCartDropdown();
    }
}

function removeItem(e) {
    e.preventDefault();
    e.stopPropagation();

    let id = e.target.parentElement.querySelector('div input').value;
    let shoppingCart = JSON.parse(localStorage.getItem('cart'));

    shoppingCart[id] = 0;
    localStorage.setItem('cart', JSON.stringify(shoppingCart));

    loadShoppingCart();
    loadShoppingCartDropdown();
}

loadShoppingCart();