const GoTripApp = {
    // --- State ---
    state: {
        user: null,
        isDarkMode: false,
    },

    // --- DOM Elements ---
    elements: {},

    // --- Initialization ---
    init() {
        this.cacheDOMElements();
        this.loadInitialState();
        this.bindEvents();
        this.render();
    },

    cacheDOMElements() {
        this.elements = {
            html: document.documentElement,
            mobileMenuButton: document.getElementById('mobile-menu-button'),
            mobileMenu: document.getElementById('mobile-menu'),
            darkModeToggle: document.getElementById('dark-mode-toggle'),
            loginBtn: document.getElementById('login-btn'),
            registerBtn: document.getElementById('register-btn'),
            loginModal: document.getElementById('login-modal'),
            registerModal: document.getElementById('register-modal'),
            closeLoginModal: document.getElementById('close-login-modal'),
            closeRegisterModal: document.getElementById('close-register-modal'),
            authContainer: document.getElementById('auth-container'),
            userMenu: document.getElementById('user-menu'),
            userMenuButton: document.getElementById('user-menu-button'),
            userDropdown: document.getElementById('user-dropdown'),
            usernameDisplay: document.getElementById('username-display'),
            logoutBtn: document.getElementById('logout-btn'),
            loginForm: document.getElementById('login-form'),
            mobileAuthContainer: document.getElementById('mobile-auth-container'),
        };
    },

    loadInitialState() {
        this.state.user = JSON.parse(localStorage.getItem('user')) || null;
        this.state.isDarkMode = localStorage.getItem('darkMode') === 'true';
    },

    // --- Event Binding ---
    bindEvents() {
        this.elements.mobileMenuButton.addEventListener('click', () => this.toggleMobileMenu());
        this.elements.darkModeToggle.addEventListener('click', () => this.toggleDarkMode());
        this.elements.loginBtn.addEventListener('click', () => this.openModal(this.elements.loginModal));
        this.elements.registerBtn.addEventListener('click', () => this.openModal(this.elements.registerModal));
        this.elements.closeLoginModal.addEventListener('click', () => this.closeModal(this.elements.loginModal));
        this.elements.closeRegisterModal.addEventListener('click', () => this.closeModal(this.elements.registerModal));
        this.elements.loginForm.addEventListener('submit', (e) => this.handleLogin(e));
        this.elements.logoutBtn.addEventListener('click', () => this.handleLogout());
        this.elements.userMenuButton.addEventListener('click', () => this.toggleUserDropdown());

        window.addEventListener('click', (e) => {
            if (e.target === this.elements.loginModal) this.closeModal(this.elements.loginModal);
            if (e.target === this.elements.registerModal) this.closeModal(this.elements.registerModal);
            if (!this.elements.userMenu.contains(e.target)) {
                this.elements.userDropdown.classList.add('hidden');
            }
        });
    },

    // --- Rendering / UI Updates ---
    render() {
        this.renderDarkMode();
        this.renderAuthState();
    },

    renderDarkMode() {
        if (this.state.isDarkMode) {
            this.elements.html.classList.add('dark');
            this.elements.darkModeToggle.innerHTML = '<i class="fas fa-sun"></i>';
        } else {
            this.elements.html.classList.remove('dark');
            this.elements.darkModeToggle.innerHTML = '<i class="fas fa-moon"></i>';
        }
    },

    renderAuthState() {
        this.elements.mobileAuthContainer.innerHTML = ''; // Clear previous buttons

        if (this.state.user) {
            this.elements.authContainer.classList.add('hidden');
            this.elements.userMenu.classList.remove('hidden');
            this.elements.usernameDisplay.textContent = this.state.user.email.split('@')[0];
            this.elements.mobileAuthContainer.innerHTML = `
                    <div class="p-2">
                        <p class="font-semibold text-gray-700 dark:text-gray-200 mb-2">Welcome, ${this.state.user.email.split('@')[0]}</p>
                        <a href="#" id="mobile-logout-btn" class="block w-full text-center py-2 px-4 text-sm text-red-500 bg-red-50 dark:bg-gray-700 dark:text-red-400 rounded-md">Logout</a>
                    </div>
                `;
            document.getElementById('mobile-logout-btn').addEventListener('click', () => this.handleLogout());

        } else {
            this.elements.authContainer.classList.remove('hidden');
            this.elements.userMenu.classList.add('hidden');
            this.elements.userDropdown.classList.add('hidden');
            this.elements.mobileAuthContainer.innerHTML = `
                    <div class="flex items-center space-x-2">
                        <button id="mobile-login-btn" class="flex-1 text-center py-2 px-4 text-sm font-medium text-gray-700 dark:text-gray-300 bg-gray-100 dark:bg-gray-700 rounded-md">Login</button>
                        <button id="mobile-register-btn" class="flex-1 text-center py-2 px-4 text-sm font-medium text-white bg-blue-600 rounded-md">Register</button>
                    </div>
                `;
            document.getElementById('mobile-login-btn').addEventListener('click', () => this.openModal(this.elements.loginModal));
            document.getElementById('mobile-register-btn').addEventListener('click', () => this.openModal(this.elements.registerModal));
        }
    },

    // --- Actions & Helpers ---
    handleLogin(e) {
        e.preventDefault();
        const email = this.elements.loginForm.querySelector('#login-email').value;
        this.state.user = { email }; // Simulate login
        localStorage.setItem('user', JSON.stringify(this.state.user));
        this.closeModal(this.elements.loginModal);
        this.render();
    },

    handleLogout() {
        this.state.user = null;
        localStorage.removeItem('user');
        this.render();
    },

    toggleDarkMode() {
        this.state.isDarkMode = !this.state.isDarkMode;
        localStorage.setItem('darkMode', this.state.isDarkMode);
        this.renderDarkMode();
    },

    toggleMobileMenu() {
        this.elements.mobileMenu.classList.toggle('hidden');
    },

    toggleUserDropdown() {
        this.elements.userDropdown.classList.toggle('hidden');
    },

    openModal(modal) { modal.classList.remove('hidden'); },
    closeModal(modal) { modal.classList.add('hidden'); },
};

// --- Start the App ---
document.addEventListener('DOMContentLoaded', () => GoTripApp.init());

const words = document.querySelectorAll(".cd-words-wrapper b");
let current = 0;

setInterval(() => {
    words[current].classList.remove("is-visible");
    current = (current + 1) % words.length;
    words[current].classList.add("is-visible");
}, 2000);


const slider = document.getElementById("hotelSlider");
const next = document.getElementById("nextHotel");
const prev = document.getElementById("prevHotel");

let scrollAmount = 320; // each card width + gap

next.addEventListener("click", () => {
    slider.scrollBy({ left: scrollAmount, behavior: "smooth" });
});

prev.addEventListener("click", () => {
    slider.scrollBy({ left: -scrollAmount, behavior: "smooth" });
});

const loginModal = document.getElementById("login-modal");
const openLoginBtn = document.getElementById("open-login-modal");
const closeLoginBtn = document.getElementById("close-login-modal");

openLoginBtn?.addEventListener("click", () => loginModal.classList.remove("hidden"));
closeLoginBtn?.addEventListener("click", () => loginModal.classList.add("hidden"));

// Register Modal
const registerModal = document.getElementById("register-modal");
const openRegisterBtn = document.getElementById("open-register-modal");
const closeRegisterBtn = document.getElementById("close-register-modal");

openRegisterBtn?.addEventListener("click", () => registerModal.classList.remove("hidden"));
closeRegisterBtn?.addEventListener("click", () => registerModal.classList.add("hidden"));

function openModal(modalId) {
    const modal = document.getElementById(modalId);
    modal.classList.remove("hidden");
    setTimeout(() => {
        modal.querySelector(".modal-content").classList.remove("scale-95", "opacity-0");
        modal.querySelector(".modal-content").classList.add("scale-100", "opacity-100");
    }, 10);
}

function closeModal(modalId) {
    const modal = document.getElementById(modalId);
    modal.querySelector(".modal-content").classList.add("scale-95", "opacity-0");
    modal.querySelector(".modal-content").classList.remove("scale-100", "opacity-100");
    setTimeout(() => modal.classList.add("hidden"), 300);
}

// Open buttons
document.getElementById("open-login-modal").addEventListener("click", () => openModal("login-modal"));
document.getElementById("open-register-modal").addEventListener("click", () => openModal("register-modal"));

// Close buttons
document.getElementById("close-login-modal").addEventListener("click", () => closeModal("login-modal"));
document.getElementById("close-register-modal").addEventListener("click", () => closeModal("register-modal"));

// Close modal when clicking outside
document.querySelectorAll(".modal").forEach(modal => {
    modal.addEventListener("click", e => {
        if (e.target === modal) closeModal(modal.id);
    });
});

// Tab switching logic
const roundTripTab = document.getElementById('round-trip-tab');
const oneWayTab = document.getElementById('one-way-tab');
const roundTripSection = document.getElementById('round-trip-section');
const oneWaySection = document.getElementById('one-way-section');
const breadcrumbText = document.getElementById('breadcrumb-text');
function switchTab(tripType) {
    if (tripType === 'roundTrips') {
        roundTripSection.classList.remove('hidden');
        oneWaySection.classList.add('hidden');
        roundTripTab.classList.add('active', 'bg-blue-600', 'text-white');
        oneWayTab.classList.remove('active', 'bg-blue-600', 'text-white');
        oneWayTab.classList.add('bg-gray-200', 'text-gray-700');
        breadcrumbText.textContent = 'Round Trips';
    } else {
        oneWaySection.classList.remove('hidden');
        roundTripSection.classList.add('hidden');
        oneWayTab.classList.add('active', 'bg-blue-600', 'text-white');
        roundTripTab.classList.remove('active', 'bg-blue-600', 'text-white');
        roundTripTab.classList.add('bg-gray-200', 'text-gray-700');
        breadcrumbText.textContent = 'One-Way Trips';
    }
    renderFlights(tripType);
    renderPagination(tripType);
}

roundTripTab.addEventListener('click', () => switchTab('roundTrips'));
oneWayTab.addEventListener('click', () => switchTab('oneWays'));

// Initial render on page load
document.addEventListener('DOMContentLoaded', () => {
    switchTab('roundTrips');
});
document.addEventListener("DOMContentLoaded", function () {
    const toggleBtn = document.getElementById("dark-mode-toggle");
    const icon = toggleBtn.querySelector("i");

    // Update icon on load
    function updateIcon() {
        if (document.documentElement.classList.contains("dark")) {
            icon.classList.remove("fa-moon");
            icon.classList.add("fa-sun");
        } else {
            icon.classList.remove("fa-sun");
            icon.classList.add("fa-moon");
        }
    }
    updateIcon();

    toggleBtn.addEventListener("click", function () {
        document.documentElement.classList.toggle("dark");

        if (document.documentElement.classList.contains("dark")) {
            localStorage.setItem("theme", "dark");
        } else {
            localStorage.setItem("theme", "light");
        }

        updateIcon();
    });
});

