const cookieName = "cart-items";
function addToCart(id, name, price, picture) {
    let products = $.cookie(cookieName);
    if (products === undefined) {
        products = [];
    } else {
        products = JSON.parse(products);
    }

    const count = $("#product-count").val();
    const currentProduct = products.find(p => p.id === id);
    if (currentProduct !== undefined) {
        products.find(p => p.id === id).count = parseInt(currentProduct.count) + parseInt(count);
    } else {
        const product = {
            id, name, unitPrice: price, picture, count
        };

        products.push(product);
    }

    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function updateCart() {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    $("#cart-items-count").text(products.length);
    const cartItemsWrapper = $("#cart-items-wrapper");
    cartItemsWrapper.html('');
    products.forEach(p => {
        const product = `<div class="single-cart-item">
                            <a href="javascript:void(0)" class="remove-icon" onclick="removeFromCart('${p.id}')">
                                <i class="ion-android-close"></i>
                            </a>
                            <div class="image">
                                <a href="single-product.html">
                                    <img src="/UploadedFiles/${p.picture}" class="img-fluid" alt="${p.name}">
                                </a>
                            </div>
                            <div class="content">
                                <p class="product-title">
                                    <a href="single-product.html">محصول: ${p.name}</a>
                                </p>
                                <p class="count">
                                    <span>تعداد: ${p.count}</span>
                                </p>
                                <p class="price">
                                    <span>قیمت واحد: ${p.unitPrice} تومان</span
                                </p>
                            </div>
                       </div>`;
        cartItemsWrapper.append(product);
    });
}

function removeFromCart(id) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    const itemToRemove = products.findIndex(p => p.id === id);
    products.splice(itemToRemove, 1);
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function changeCartItemCount(id, totalId, count) {
    var products = $.cookie(cookieName);
    products = JSON.parse(products);
    const productIndex = products.findIndex(p => p.id === id);
    products[productIndex].count = count;
    const product = products[productIndex];
    const newPrice = parseInt(product.unitPrice) * parseInt(count);
    $(`#${totalId}`).text(newPrice);
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}
