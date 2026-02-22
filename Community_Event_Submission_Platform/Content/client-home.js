'use strict';

// ── Fade-in on scroll (same pattern as LandingPage.js) ──
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

// ── Navbar scroll shrink (same as LandingPage.js) ───────
window.addEventListener('scroll', () => {
    const navbar = document.querySelector('.navbar');
    if (window.scrollY > 50) {
        navbar.style.padding = '0.5rem 0';
    } else {
        navbar.style.padding = '1rem 0';
    }
});

// ── Logout confirmation ──────────────────────────────────
document.getElementById('logoutBtn')?.addEventListener('click', (e) => {
    e.preventDefault();
    if (confirm('Are you sure you want to log out?')) {
        window.location.href = '/Client/Logout';
    }
});

/*    
    Profile page JavaScript
*/
// ── Fade-in on load ───────────────────────────────
document.querySelectorAll('.fade-in').forEach(function (el, i) {
    setTimeout(function () {
        el.classList.add('visible');
    }, 100 + i * 120);
});

// ── Save profile (client-side preview) ───────────
// Wire saveProfileBtn to your MVC controller POST action as needed
document.getElementById('saveProfileBtn').addEventListener('click', function () {
    var loc = document.getElementById('inputLocation').value.trim();
    var bio = document.getElementById('inputBio').value.trim();

    if (loc) document.getElementById('displayLocation').textContent = loc;
    if (bio) document.getElementById('displayBio').textContent = bio;

    bootstrap.Modal.getInstance(document.getElementById('editModal')).hide();
});