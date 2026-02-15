// Toggle password visibility
function togglePassword(inputId, icon) {
    const input = document.getElementById(inputId);
    if (input.type === 'password') {
        input.type = 'text';
        icon.classList.remove('bi-eye');
        icon.classList.add('bi-eye-slash');
    } else {
        input.type = 'password';
        icon.classList.remove('bi-eye-slash');
        icon.classList.add('bi-eye');
    }
}

// Show alert message
function showAlert(message, type) {
    const alertBox = document.getElementById('alertMessage');
    alertBox.textContent = message;
    alertBox.className = `alert-message alert-${type}`;
    alertBox.style.display = 'block';

    // Auto hide after 5 seconds
    setTimeout(hideAlert, 5000);
}

// Hide alert message
function hideAlert() {
    const alertBox = document.getElementById('alertMessage');
    if (alertBox) {
        alertBox.style.display = 'none';
    }
}

// Handle login form submission
function handleLoginSubmit(e) {
    e.preventDefault();
    showAlert('Login successful! Redirecting...', 'success');

    // Simulate redirect after 2 seconds
    setTimeout(() => {
        // window.location.href = 'dashboard.html';
        console.log('Redirecting to dashboard...');
    }, 2000);
}

// Handle register form submission
function handleRegisterSubmit(e) {
    e.preventDefault();

    const password = document.getElementById('registerPassword').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    if (password !== confirmPassword) {
        showAlert('Passwords do not match!', 'error');
        return;
    }

    showAlert('Account created successfully! Redirecting to login...', 'success');

    // Redirect to login after 2 seconds
    setTimeout(() => {
        window.location.href = 'login.html';
    }, 2000);
}

// Handle social login/signup buttons
function handleSocialAuth(e) {
    e.preventDefault();
    const platform = this.querySelector('i').classList[1].split('-')[1];
    showAlert(`${platform.charAt(0).toUpperCase() + platform.slice(1)} authentication coming soon!`, 'success');
}

// Initialize event listeners when DOM is loaded
document.addEventListener('DOMContentLoaded', function () {
    // Login form
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
        loginForm.addEventListener('submit', handleLoginSubmit);
    }

    // Register form
    const registerForm = document.getElementById('registerForm');
    if (registerForm) {
        registerForm.addEventListener('submit', handleRegisterSubmit);
    }

    // Social buttons
    document.querySelectorAll('.social-btn').forEach(btn => {
        btn.addEventListener('click', handleSocialAuth);
    });
});