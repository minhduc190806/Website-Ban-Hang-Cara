/**
 * CARA ECOMMERCE - CART & PRODUCT MANAGEMENT SCRIPT
 * Quản lý giỏ hàng, chi tiết sản phẩm, đặt hàng và cập nhật giao diện
 */

// Danh mục dữ liệu 16 sản phẩm chuẩn
const PRODUCTS_DATA = [
    {
        id: 1,
        name: "Cartoon Astronaut T-Shirts V1",
        brand: "Adidas",
        price: 100,
        oldPrice: 140,
        image: "img/products/f1.jpg",
        category: "Áo Thun & Sơ Mi Hè",
        description: "Áo thun cotton cao cấp in hình phi hành gia cartoon sắc nét, chất liệu thoáng khí, co giãn 4 chiều mang lại cảm giác thoải mái suốt cả ngày.",
        isFeatured: true
    },
    {
        id: 2,
        name: "Cartoon Astronaut T-Shirts V2",
        brand: "Adidas",
        price: 110,
        oldPrice: 150,
        image: "img/products/f2.jpg",
        category: "Áo Thun & Sơ Mi Hè",
        description: "Áo thun họa tiết cây hoa nhiệt đới mùa hè rực rỡ, chất vải mềm mịn, co giãn tốt, màu sắc bền đẹp không phai khi giặt.",
        isFeatured: true
    },
    {
        id: 3,
        name: "Cartoon Astronaut T-Shirts V3",
        brand: "Adidas",
        price: 105,
        oldPrice: 135,
        image: "img/products/f3.jpg",
        category: "Áo Thun & Sơ Mi Hè",
        description: "Thiết kế áo phong cách vintage retro, chất vải cotton 100% thấm hút mồ hôi tối đa, đường may 2 kim tỉ mỉ chuẩn xuất khẩu.",
        isFeatured: true
    },
    {
        id: 4,
        name: "Cartoon Astronaut T-Shirts V4",
        brand: "Adidas",
        price: 95,
        oldPrice: 120,
        image: "img/products/f4.jpg",
        category: "Áo Thun & Sơ Mi Hè",
        description: "Áo phông họa tiết phong cách đường phố cá tính, form rộng streetwear cá tính, dễ dàng phối đồ với quần short hay quần túi hộp.",
        isFeatured: true
    },
    {
        id: 5,
        name: "Cartoon Astronaut T-Shirts V5",
        brand: "Adidas",
        price: 125,
        oldPrice: 160,
        image: "img/products/f5.jpg",
        category: "Bộ Sưu Tập Nổi Bật",
        description: "Sơ mi hoa lá mùa hè hawaii nổi bật, nhẹ nhàng bay bổng, thích hợp cho các chuyến du lịch biển và dã ngoại ngoài trời.",
        isFeatured: true
    },
    {
        id: 6,
        name: "Cartoon Astronaut T-Shirts V6",
        brand: "Adidas",
        price: 130,
        oldPrice: 170,
        image: "img/products/f6.jpg",
        category: "Bộ Sưu Tập Nổi Bật",
        description: "Áo khoác sơ mi dài tay phối hai màu cá tính, đường may chắc chắn, cúc áo dập logo cao cấp tạo điểm nhấn ấn tượng.",
        isFeatured: true
    },
    {
        id: 7,
        name: "Cartoon Astronaut T-Shirts V7",
        brand: "Adidas",
        price: 85,
        oldPrice: 115,
        image: "img/products/f7.jpg",
        category: "Bộ Sưu Tập Nổi Bật",
        description: "Quần short ống rộng thoải mái đi biển, túi khóa tiện lợi chống rơi đồ, cạp chun có dây rút dễ dàng điều chỉnh kích cỡ.",
        isFeatured: true
    },
    {
        id: 8,
        name: "Cartoon Astronaut T-Shirts V8",
        brand: "Adidas",
        price: 90,
        oldPrice: 120,
        image: "img/products/f8.jpg",
        category: "Bộ Sưu Tập Nổi Bật",
        description: "Áo blouse nữ họa tiết thời trang thanh lịch, nhẹ nhàng, chất voan hàn mềm mát đem lại phong cách nữ tính nhẹ nhàng.",
        isFeatured: true
    },
    {
        id: 9,
        name: "Cartoon Astronaut Shirt N1",
        brand: "Zara",
        price: 115,
        oldPrice: 150,
        image: "img/products/n1.jpg",
        category: "Hàng Mới Về 2026",
        description: "Sơ mi công sở vải Oxford cao cấp chống nhăn, tôn dáng thanh lịch, phù hợp cho cả đi làm lẫn dạo phố cuối tuần.",
        isNewArrival: true
    },
    {
        id: 10,
        name: "Cartoon Astronaut Shirt N2",
        brand: "Zara",
        price: 120,
        oldPrice: 160,
        image: "img/products/n2.jpg",
        category: "Hàng Mới Về 2026",
        description: "Sơ mi caro xám trẻ trung, dễ phối đồ với quần jeans hoặc kaki, form regular fit chuẩn form người Việt.",
        isNewArrival: true
    },
    {
        id: 11,
        name: "Cartoon Astronaut Shirt N3",
        brand: "Zara",
        price: 99,
        oldPrice: 130,
        image: "img/products/n3.jpg",
        category: "Hàng Mới Về 2026",
        description: "Sơ mi trắng basic tối giản tinh tế, cổ áo đứng form chuẩn, chất liệu cotton silk mịn mát màng tự nhiên.",
        isNewArrival: true
    },
    {
        id: 12,
        name: "Cartoon Astronaut Shirt N4",
        brand: "Nike",
        price: 108,
        oldPrice: 145,
        image: "img/products/n4.jpg",
        category: "Hàng Mới Về 2026",
        description: "Áo polo thể thao chất vải cá sấu gai mịn, thấm hút mồ hôi nhanh, phong cách khỏe khoắn năng động.",
        isNewArrival: true
    },
    {
        id: 13,
        name: "Cartoon Astronaut Shirt N5",
        brand: "Nike",
        price: 112,
        oldPrice: 140,
        image: "img/products/n5.jpg",
        category: "Thời Trang Nữ & Phụ Kiện",
        description: "Sơ mi denim xanh cá tính bền bỉ, phối wash nhẹ hiện đại, có thể mặc riêng hoặc khoác ngoài áo phông.",
        isNewArrival: true
    },
    {
        id: 14,
        name: "Cartoon Astronaut Shirt N6",
        brand: "Gucci",
        price: 75,
        oldPrice: 95,
        image: "img/products/n6.jpg",
        category: "Thời Trang Nữ & Phụ Kiện",
        description: "Quần soóc kaki co giãn nhẹ, phối túi tiện lợi phong cách năng động, thích hợp cho các hoạt động thể thao ngoài trời.",
        isNewArrival: true
    },
    {
        id: 15,
        name: "Cartoon Astronaut Shirt N7",
        brand: "Gucci",
        price: 129,
        oldPrice: 175,
        image: "img/products/n7.jpg",
        category: "Thời Trang Nữ & Phụ Kiện",
        description: "Sơ mi vàng cát họa tiết bohemieng phá cách độc đáo, chất lụa cát rũ nhẹ tạo nét lãng tử phong trần.",
        isNewArrival: true
    },
    {
        id: 16,
        name: "Cartoon Astronaut Shirt N8",
        brand: "Adidas",
        price: 118,
        oldPrice: 155,
        image: "img/products/n8.jpg",
        category: "Thời Trang Nữ & Phụ Kiện",
        description: "Áo sơ mi tay ngắn tone đen sang trọng, vải lụa tơ tằm siêu mềm mát, cúc đen ánh ngọc trai tinh tế.",
        isNewArrival: true
    }
];

// Lấy danh sách sản phẩm động từ localStorage (đồng bộ 100% với Quản Trị Admin CRUD)
function getCaraProducts() {
    try {
        const stored = localStorage.getItem("cara_crud_products");
        if (stored) {
            const list = JSON.parse(stored);
            if (Array.isArray(list) && list.length > 0) {
                return list;
            }
        }
    } catch (e) {
        console.error("Lỗi đọc cara_crud_products từ localStorage:", e);
    }
    // Khởi tạo vào localStorage nếu chưa có
    try {
        localStorage.setItem("cara_crud_products", JSON.stringify(PRODUCTS_DATA));
    } catch (e) {}
    return PRODUCTS_DATA;
}

// Lưu danh sách sản phẩm động
function saveCaraProducts(list) {
    try {
        localStorage.setItem("cara_crud_products", JSON.stringify(list));
    } catch (e) {}
}

window.getCaraProducts = getCaraProducts;
window.saveCaraProducts = saveCaraProducts;

// Helper giỏ hàng
const Cart = {
    getKey() {
        return "cara_shopping_cart";
    },

    getItems() {
        try {
            const raw = localStorage.getItem(this.getKey());
            return raw ? JSON.parse(raw) : [];
        } catch (e) {
            console.error("Lỗi đọc giỏ hàng:", e);
            return [];
        }
    },

    saveItems(items) {
        localStorage.setItem(this.getKey(), JSON.stringify(items));
        this.updateBadge();
    },

    addItem(productId, quantity = 1, size = "L") {
        const allProducts = getCaraProducts();
        const product = allProducts.find(p => p.id === parseInt(productId)) || PRODUCTS_DATA.find(p => p.id === parseInt(productId));
        if (!product) {
            this.showToast("Không tìm thấy sản phẩm!", "error");
            return;
        }

        const items = this.getItems();
        const existingIndex = items.findIndex(item => item.id === product.id && item.size === size);

        if (existingIndex > -1) {
            items[existingIndex].quantity += parseInt(quantity);
        } else {
            items.push({
                id: product.id,
                name: product.name,
                brand: product.brand,
                price: product.price,
                image: product.image,
                size: size,
                quantity: parseInt(quantity)
            });
        }

        this.saveItems(items);
        this.showToast(`Đã thêm "${product.name}" (${size}) vào giỏ hàng!`);
    },

    updateQuantity(productId, size, quantity) {
        let items = this.getItems();
        const q = parseInt(quantity);
        if (q <= 0) {
            this.removeItem(productId, size);
            return;
        }

        const item = items.find(i => i.id === parseInt(productId) && i.size === size);
        if (item) {
            item.quantity = q;
            this.saveItems(items);
        }
    },

    removeItem(productId, size) {
        let items = this.getItems();
        items = items.filter(i => !(i.id === parseInt(productId) && i.size === size));
        this.saveItems(items);
        this.showToast("Đã xóa sản phẩm khỏi giỏ hàng");
    },

    clear() {
        localStorage.removeItem(this.getKey());
        this.updateBadge();
    },

    getTotalCount() {
        const items = this.getItems();
        return items.reduce((sum, item) => sum + item.quantity, 0);
    },

    getTotalPrice() {
        const items = this.getItems();
        return items.reduce((sum, item) => sum + (item.price * item.quantity), 0);
    },

    updateBadge() {
        const count = this.getTotalCount();
        const navBag = document.getElementById("lg-bag");
        if (!navBag) return;

        let badge = navBag.querySelector(".cart-badge");
        if (!badge) {
            badge = document.createElement("span");
            badge.className = "cart-badge";
            const link = navBag.querySelector("a");
            if (link) link.appendChild(badge);
        }

        badge.textContent = count;
        badge.style.display = count > 0 ? "inline-block" : "none";
    },

    showToast(message, type = "success") {
        let container = document.getElementById("toast-container");
        if (!container) {
            container = document.createElement("div");
            container.id = "toast-container";
            document.body.appendChild(container);
        }

        const toast = document.createElement("div");
        toast.className = `toast ${type === "error" ? "toast-error" : ""}`;
        toast.innerHTML = `
            <i class="fa ${type === "error" ? "fa-circle-xmark" : "fa-circle-check"}" style="color:${type === "error" ? "#e74c3c" : "#2ecc71"}"></i>
            <span>${message}</span>
        `;
        container.appendChild(toast);

        setTimeout(() => {
            toast.remove();
        }, 3000);
    }
};

// Khởi chạy khi tài liệu sẵn sàng
document.addEventListener("DOMContentLoaded", () => {
    Cart.updateBadge();
    checkUserLoginState();

    // 0. Render sản phẩm động trên trang chủ (index.html) đồng bộ với Admin CRUD
    renderIndexProducts();

    // 1. Tự động liên kết các thẻ sản phẩm (.pro) trong trang index.html và shop.html
    bindProductCards();

    // 2. Nếu đang ở trang chi tiết sản phẩm (sproduct.html)
    if (window.location.pathname.includes("sproduct.html")) {
        renderProductDetailPage();
    }

    // 3. Nếu đang ở trang giỏ hàng (cart.html)
    if (window.location.pathname.includes("cart.html")) {
        renderCartPage();
    }

    // 4. Nếu đang ở trang thanh toán (checkout.html)
    if (window.location.pathname.includes("checkout.html")) {
        renderCheckoutPage();
    }
});

// Kiểm tra trạng thái đăng nhập trên Navbar
function checkUserLoginState() {
    const userStr = localStorage.getItem("cara_user");
    const navbar = document.getElementById("navbar");
    if (!navbar) return;

    let loginLi = Array.from(navbar.querySelectorAll("li")).find(li => {
        const a = li.querySelector("a");
        return a && a.getAttribute("href") === "login.html";
    });

    if (userStr && loginLi) {
        try {
            const user = JSON.parse(userStr);
            loginLi.innerHTML = `
                <a href="javascript:void(0)" style="color: #fff; font-size: 15px; font-weight: 600;">
                    <i class="fa fa-circle-user" style="color: #2ecc71;"></i> ${user.name}
                    <span onclick="logoutUser()" title="Đăng xuất" style="color: #ff7675; margin-left: 8px; cursor: pointer; font-size: 13px;">[Thoát]</span>
                </a>
            `;
        } catch (e) {}
    }
}

function logoutUser() {
    localStorage.removeItem("cara_user");
    Cart.showToast("Đã đăng xuất tài khoản thành công!");
    setTimeout(() => {
        window.location.reload();
    }, 600);
}

// Gán liên kết nhấp chuột cho các thẻ sản phẩm tĩnh nếu chưa có onclick
function bindProductCards() {
    const proCards = document.querySelectorAll("#product1 .pro");
    if (!proCards || proCards.length === 0) return;

    proCards.forEach((card, index) => {
        // Nếu thẻ đã có sẵn sự kiện onclick thì bỏ qua để không ghi đè ID thực tế
        if (card.getAttribute("onclick") && card.getAttribute("onclick").includes("sproduct.html")) {
            return;
        }
        const id = index + 1;
        card.style.cursor = "pointer";

        // Click vào thẻ sản phẩm chuyển sang chi tiết
        card.addEventListener("click", (e) => {
            if (e.target.closest(".cart") || e.target.closest("a.cart-btn")) {
                e.preventDefault();
                e.stopPropagation();
                Cart.addItem(id, 1, "L");
                return;
            }
            window.location.href = `sproduct.html?id=${id}`;
        });

        // Nút icon giỏ hàng trong thẻ
        const cartIcon = card.querySelector(".cart");
        if (cartIcon && !cartIcon.parentElement.onclick) {
            cartIcon.parentElement.onclick = (e) => {
                e.preventDefault();
                e.stopPropagation();
                Cart.addItem(id, 1, "L");
            };
        }
    });
}

// Xử lý trang Chi tiết sản phẩm (sproduct.html)
function renderProductDetailPage() {
    const params = new URLSearchParams(window.location.search);
    const id = parseInt(params.get("id")) || 1;
    const allProducts = getCaraProducts();
    const product = allProducts.find(p => p.id === id) || PRODUCTS_DATA.find(p => p.id === id) || PRODUCTS_DATA[0];

    // Cập nhật thông tin sản phẩm
    const mainImg = document.getElementById("MainImg");
    const proCategory = document.getElementById("pro-category");
    const proName = document.getElementById("pro-name");
    const proPrice = document.getElementById("pro-price");
    const proDesc = document.getElementById("pro-desc");
    const sizeSelect = document.getElementById("pro-size");
    const qtyInput = document.getElementById("pro-quantity");
    const addBtn = document.getElementById("btn-add-to-cart");

    if (mainImg) mainImg.src = product.image;
    if (proCategory) proCategory.textContent = `Trang chủ / ${product.category}`;
    if (proName) proName.textContent = product.name;
    if (proPrice) {
        proPrice.innerHTML = `$${product.price} <span class="old-price">$${product.oldPrice || product.price + 30}</span>`;
    }
    if (proDesc) proDesc.textContent = product.description;

    // Xử lý nút Thêm vào giỏ hàng
    if (addBtn) {
        addBtn.onclick = () => {
            const size = sizeSelect ? sizeSelect.value : "L";
            const qty = qtyInput ? parseInt(qtyInput.value) || 1 : 1;
            Cart.addItem(product.id, qty, size);
        };
    }

    // Xử lý đổi ảnh nhỏ (small-img)
    const smallImgs = document.querySelectorAll(".small-img");
    smallImgs.forEach(img => {
        img.onclick = function() {
            if (mainImg) mainImg.src = this.src;
        };
    });

    // Render 4 sản phẩm liên quan
    renderRelatedProducts(product.id);
}

function renderRelatedProducts(currentId) {
    const relatedContainer = document.getElementById("related-products-container") || document.getElementById("product-container");
    if (!relatedContainer) return;

    relatedContainer.className = "pro-container";
    relatedContainer.style.display = "flex";
    relatedContainer.style.flexDirection = "row";
    relatedContainer.style.flexWrap = "wrap";
    relatedContainer.style.justifyContent = "center";
    relatedContainer.style.gap = "25px";

    const allProducts = getCaraProducts();
    const related = allProducts.filter(p => p.id !== currentId).slice(0, 4);
    let html = "";
    related.forEach(p => {
        html += `
        <div class="pro" onclick="window.location.href='sproduct.html?id=${p.id}'" style="cursor:pointer; width:22%; min-width:240px; max-width:280px; box-sizing:border-box;">
            <img src="${p.image}" alt="${p.name}" style="width:100%; height:250px; object-fit:cover; border-radius:12px;">
            <div class="mota" style="padding:10px 0;">
                <span style="font-size:13px; color:#888;">${p.brand}</span>
                <h5 style="margin:5px 0; font-size:15px; color:#222;">${p.name}</h5>
                <div class="star" style="margin-bottom:6px;">
                    <i class="fa fa-star checked" style="color: #f39c12;"></i>
                    <i class="fa fa-star checked" style="color: #f39c12;"></i>
                    <i class="fa fa-star checked" style="color: #f39c12;"></i>
                    <i class="fa fa-star checked" style="color: #f39c12;"></i>
                    <i class="fa fa-star checked" style="color: #f39c12;"></i>
                </div>
                <h4 style="color:#e94747; font-size:16px;">$${p.price}</h4>
            </div>
            <div>
                <a href="javascript:void(0)" onclick="event.stopPropagation(); Cart.addItem(${p.id}, 1, 'L');">
                    <i class="fa-solid fa-cart-plus cart"></i>
                </a>
            </div>
        </div>
        `;
    });
    relatedContainer.innerHTML = html;
}

// Render sản phẩm trên trang chủ (index.html) đồng bộ với Quản Trị Admin CRUD
function renderIndexProducts() {
    const featuredContainer = document.getElementById("featured-products-container");
    const newArrivalsContainer = document.getElementById("new-arrivals-container");

    if (!featuredContainer && !newArrivalsContainer) return;

    const allProducts = getCaraProducts();
    const fallbackImg = "img/products/f1.jpg";

    if (featuredContainer) {
        const featured = allProducts.filter(p => p.isFeatured !== false).slice(0, 8);
        let html = "";
        featured.forEach(p => {
            html += `
            <div class="pro" onclick="window.location.href='sproduct.html?id=${p.id}'" style="cursor: pointer;">
                <img src="${p.image || fallbackImg}" alt="${p.name}" onerror="this.onerror=null; this.src='${fallbackImg}';">
                <div class="mota">
                    <span>${p.brand || 'Adidas'}</span>
                    <h5>${p.name}</h5>
                    <div class="star">
                        <i class="fa fa-star checked" style="color: #f39c12;"></i>
                        <i class="fa fa-star checked" style="color: #f39c12;"></i>
                        <i class="fa fa-star checked" style="color: #f39c12;"></i>
                        <i class="fa fa-star checked" style="color: #f39c12;"></i>
                        <i class="fa fa-star checked" style="color: #f39c12;"></i>
                    </div>
                    <h4>$${p.price}</h4>
                </div>
                <div>
                    <a href="javascript:void(0)" onclick="event.stopPropagation(); Cart.addItem(${p.id}, 1, 'L');" title="Thêm vào giỏ">
                        <i class="fa-solid fa-cart-plus cart"></i>
                    </a>
                </div>
            </div>`;
        });
        featuredContainer.innerHTML = html;
    }

    if (newArrivalsContainer) {
        const newArrivals = allProducts.filter(p => p.isNewArrival === true || p.category === "Hàng Mới Về 2026").slice(0, 8);
        let html = "";
        newArrivals.forEach(p => {
            html += `
            <div class="pro" onclick="window.location.href='sproduct.html?id=${p.id}'" style="cursor: pointer;">
                <img src="${p.image || 'img/products/n1.jpg'}" alt="${p.name}" onerror="this.onerror=null; this.src='img/products/n1.jpg';">
                <div class="mota">
                    <span>${p.brand || 'Zara'}</span>
                    <h5>${p.name}</h5>
                    <div class="star">
                        <i class="fa fa-star checked" style="color: #f39c12;"></i>
                        <i class="fa fa-star checked" style="color: #f39c12;"></i>
                        <i class="fa fa-star checked" style="color: #f39c12;"></i>
                        <i class="fa fa-star checked" style="color: #f39c12;"></i>
                        <i class="fa fa-star checked" style="color: #f39c12;"></i>
                    </div>
                    <h4>$${p.price}</h4>
                </div>
                <div>
                    <a href="javascript:void(0)" onclick="event.stopPropagation(); Cart.addItem(${p.id}, 1, 'L');" title="Thêm vào giỏ">
                        <i class="fa-solid fa-cart-plus cart"></i>
                    </a>
                </div>
            </div>`;
        });
        newArrivalsContainer.innerHTML = html;
    }
}

// Xử lý trang Giỏ hàng (cart.html)
function renderCartPage() {
    const tbody = document.getElementById("cart-table-body");
    const subtotalEl = document.getElementById("cart-subtotal");
    const totalEl = document.getElementById("cart-total");
    const emptyBox = document.getElementById("empty-cart-message");
    const cartSection = document.getElementById("cart-items-section");
    const cartAddSection = document.getElementById("cart-add");

    const items = Cart.getItems();

    if (!items || items.length === 0) {
        if (cartSection) cartSection.style.display = "none";
        if (cartAddSection) cartAddSection.style.display = "none";
        if (emptyBox) emptyBox.style.display = "block";
        return;
    }

    if (cartSection) cartSection.style.display = "block";
    if (cartAddSection) cartAddSection.style.display = "flex";
    if (emptyBox) emptyBox.style.display = "none";

    let html = "";
    items.forEach(item => {
        const itemTotal = item.price * item.quantity;
        html += `
        <tr>
            <td>
                <button class="btn-remove-item" onclick="Cart.removeItem(${item.id}, '${item.size}'); renderCartPage();">
                    <i class="fa-regular fa-trash-can"></i>
                </button>
            </td>
            <td><img src="${item.image}" alt="${item.name}"></td>
            <td>
                <strong>${item.name}</strong><br>
                <small style="color:#777">Hãng: ${item.brand || 'Adidas'} | Size: <span style="font-weight:700; color:#e94747">${item.size}</span></small>
            </td>
            <td>$${item.price.toFixed(2)}</td>
            <td>
                <input type="number" min="1" max="99" value="${item.quantity}" 
                       onchange="Cart.updateQuantity(${item.id}, '${item.size}', this.value); renderCartPage();">
            </td>
            <td><strong style="color:#2c3e50">$${itemTotal.toFixed(2)}</strong></td>
        </tr>
        `;
    });

    if (tbody) tbody.innerHTML = html;

    const subtotal = Cart.getTotalPrice();
    const shipping = subtotal > 0 ? 0 : 0; // Miễn phí vận chuyển
    const total = subtotal + shipping;

    if (subtotalEl) subtotalEl.textContent = `$${subtotal.toFixed(2)}`;
    if (totalEl) totalEl.textContent = `$${total.toFixed(2)}`;
}

// Xử lý trang Đặt hàng / Thanh toán (checkout.html)
function renderCheckoutPage() {
    const items = Cart.getItems();
    const listContainer = document.getElementById("checkout-items-list");
    const subtotalEl = document.getElementById("checkout-subtotal");
    const totalEl = document.getElementById("checkout-total");
    const form = document.getElementById("checkout-form");

    if (!items || items.length === 0) {
        window.location.href = "cart.html";
        return;
    }

    let html = "";
    items.forEach(item => {
        html += `
        <div class="checkout-item">
            <div class="checkout-item-info">
                <img src="${item.image}" alt="${item.name}">
                <div>
                    <h5 style="margin:0 0 4px 0; font-size:15px">${item.name}</h5>
                    <small style="color:#777">Size: ${item.size} × ${item.quantity}</small>
                </div>
            </div>
            <strong>$${(item.price * item.quantity).toFixed(2)}</strong>
        </div>
        `;
    });

    if (listContainer) listContainer.innerHTML = html;

    const total = Cart.getTotalPrice();
    if (subtotalEl) subtotalEl.textContent = `$${total.toFixed(2)}`;
    if (totalEl) totalEl.textContent = `$${total.toFixed(2)}`;

    if (form) {
        form.onsubmit = (e) => {
            e.preventDefault();
            const fullName = document.getElementById("c_name").value;
            const phone = document.getElementById("c_phone").value;
            const email = document.getElementById("c_email").value;
            const address = document.getElementById("c_address").value;
            const notes = document.getElementById("c_notes").value;
            const payment = document.querySelector('input[name="payment_method"]:checked')?.value || "COD";

            const orderCode = "MD-" + Date.now().toString().slice(-6);

            const orderData = {
                orderCode: orderCode,
                customer: { fullName, phone, email, address },
                notes: notes,
                paymentMethod: payment,
                totalAmount: total,
                items: items,
                orderDate: new Date().toLocaleString("vi-VN")
            };

            // Lưu đơn hàng vào danh sách orders trong localStorage
            const orders = JSON.parse(localStorage.getItem("cara_orders") || "[]");
            orders.unshift(orderData);
            localStorage.setItem("cara_orders", JSON.stringify(orders));

            // Làm rỗng giỏ hàng
            Cart.clear();

            // Chuyển hướng đến trang thành công kèm mã
            window.location.href = `order-success.html?code=${orderCode}&name=${encodeURIComponent(fullName)}&total=${total.toFixed(2)}`;
        };
    }
}
