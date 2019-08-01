async function loadShoppingCartDropdown() {
    const baseUrl = 'http://localhost:50329/api/shoppingCart/';

    let dropdownShoppingCart = '<h5 class="text-center">Your cart is empty.</h5>';
    let totalCost = 0;
    let shoppingCart = JSON.parse(localStorage.getItem('cart'));
    let productIds;

    if (shoppingCart) {
        productIds = Object.keys(shoppingCart).filter(x => shoppingCart[x] !== 0);

        if (productIds.length != 0) {
            dropdownShoppingCart = '';

            for (let id of productIds) {
                let productInfo = await fetch(baseUrl + id);
                let product = await productInfo.json();

                dropdownShoppingCart += `
            <div id="shoppingCart-item" class="d-flex justify-content-around mt-1">
                <img class="col-5" src="${product.imageUrl}" />
                <div class="col-6">
                    <h6 class="mb-3">${product.name}</h6>
                    <p>Price: $${product.price.toFixed(2)}</p>
                    <p>Quantity: ${shoppingCart[id]}</p>
                </div>
            </div>
            <hr />`;

                totalCost = totalCost + (+shoppingCart[id] * +product.price);
            }

            dropdownShoppingCart += `
        <div class="d-flex justify-content-between">
            <h5>
                Total:
            </h5>
            <div>
                $${totalCost.toFixed(2)}
            </div>
        </div>
        <a class="btn btn-danger w-100 mt-2" href="/Checkout/ShoppingCart">Buy</a>`;
        }
    }

    document.getElementById('dropdown-shopping-cart-items').innerHTML = dropdownShoppingCart;
}

loadShoppingCartDropdown();