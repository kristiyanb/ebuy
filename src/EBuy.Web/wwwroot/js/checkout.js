const url = 'http://localhost:50329/api/orders/send';

async function sendOrder(e) {
    e.preventDefault();
    e.stopPropagation();

    let nameElement = document.querySelector('#name');
    let emailElement = document.querySelector('#email');
    let phoneNumberElement = document.querySelector('#phoneNumber');
    let addressElement = document.querySelector('#address');
    let paymentMethod = document.querySelector('#paymentMethod').value;
    let products = JSON.parse(localStorage.getItem('cart'));

    nameElement.addEventListener('click', () => document.querySelector('#invalid-name-notification').style.display = 'none');
    emailElement.addEventListener('click', () => document.querySelector('#invalid-email-notification').style.display = 'none');
    phoneNumberElement.addEventListener('click', () => document.querySelector('#invalid-phoneNumber-notification').style.display = 'none');
    addressElement.addEventListener('click', () => document.querySelector('#invalid-address-notification').style.display = 'none');

    let fullName = nameElement.value;
    let email = emailElement.value;
    let phoneNumber = phoneNumberElement.value;
    let address = addressElement.value;

    let validData = true;

    if (!fullName || fullName.length < 2 || fullName.length > 30) {
        let notificationBox = document.querySelector('#invalid-name-notification');

        notificationBox.style.display = 'block';
        notificationBox.textContent = 'Name must be between 2 and 30 characters long.';

        validData = false;
    }
    if (!email || email.length < 4 || email.length > 40 || !email.includes('@') || !email.includes('.')) {
        let notificationBox = document.querySelector('#invalid-email-notification');

        notificationBox.style.display = 'block';
        notificationBox.textContent = 'Please enter a valid email.';

        validData = false;
    }
    if (!phoneNumber || phoneNumber.length < 10 || phoneNumber.length > 17) {
        let notificationBox = document.querySelector('#invalid-phoneNumber-notification');

        notificationBox.style.display = 'block';
        notificationBox.textContent = 'Please enter a valid phone number.';

        validData = false;
    }
    if (!address || address.length < 10 || address.length > 40) {
        let notificationBox = document.querySelector('#invalid-address-notification');

        notificationBox.style.display = 'block';
        notificationBox.textContent = 'Address must be between 10 and 40 characters long.';

        validData = false;
    }

    if (validData) {
        let order = {
            fullName,
            email,
            phoneNumber,
            address,
            paymentMethod,
            products
        };

        await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(order)
        });

        localStorage.removeItem('cart');

        document.querySelector('.container main').innerHTML = `
        <div class="alert alert-success alert-dismissible mt-5 text-center" role="alert">
            <h5 class="text-center mt-5 mb-5">Your order has been registered. Thank you for shopping with us.</h5>
            <a href="/Home" class="text-dark">Back to website</a>
        </div>`;

        loadShoppingCartDropdown();
    }
}

document.querySelector('#order-btn').addEventListener('click', sendOrder);