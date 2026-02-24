'use strict';

// ── Fade-in on scroll ────────────────────────────────────────────────
const observerOptions = {
    threshold: 0.1,
    rootMargin: '0px 0px -50px 0px'
};
const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('visible');
        }
    });
}, observerOptions);
document.querySelectorAll('.fade-in').forEach(el => observer.observe(el));

// ── Fade-in on load (staggered) ──────────────────────────────────────
document.querySelectorAll('.fade-in').forEach(function (el, i) {
    setTimeout(function () {
        el.classList.add('visible');
    }, 100 + i * 120);
});

// ── Navbar scroll shrink ─────────────────────────────────────────────
window.addEventListener('scroll', () => {
    const navbar = document.querySelector('.navbar');
    if (navbar) {
        navbar.style.padding = window.scrollY > 50 ? '0.5rem 0' : '1rem 0';
    }
});

// ── Logout confirmation ──────────────────────────────────────────────
document.getElementById('logoutBtn')?.addEventListener('click', (e) => {
    e.preventDefault();
    if (confirm('Are you sure you want to log out?')) {
        window.location.href = e.currentTarget.href;
    }
});

// ── Save Profile (only runs if the modal elements exist on the page) ─
const saveProfileBtn = document.getElementById('saveProfileBtn');
if (saveProfileBtn) {
    saveProfileBtn.addEventListener('click', function () {
        const loc = document.getElementById('inputLocation')?.value.trim();
        const bio = document.getElementById('inputBio')?.value.trim();

        if (loc) {
            const displayLoc = document.getElementById('displayLocation');
            if (displayLoc) displayLoc.textContent = loc;
        }
        if (bio) {
            const displayBio = document.getElementById('displayBio');
            if (displayBio) displayBio.textContent = bio;
        }

        const editModal = document.getElementById('editModal');
        if (editModal) {
            bootstrap.Modal.getInstance(editModal)?.hide();
        }
    });
}