// AdminDashboard.js — Dashboard-specific scripts
// Depends on: Chart.js (loaded via CDN in the view)

(function () {
    'use strict';

    /* ── Shared chart defaults ─────────────────────────────── */
    const PRIMARY = '#6366f1';
    const SECONDARY = '#8b5cf6';
    const SUCCESS = '#10b981';
    const WARNING = '#f59e0b';
    const DANGER = '#ef4444';
    const TEXT_SEC = '#94a3b8';
    const GRID = 'rgba(99,102,241,0.08)';

    Chart.defaults.color = TEXT_SEC;
    Chart.defaults.font.family = "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif";
    Chart.defaults.font.size = 12;
    Chart.defaults.plugins.legend.display = false;

    /* ── Gradient helper ───────────────────────────────────── */
    function makeGradient(ctx, color, alpha1 = 0.45, alpha2 = 0.0) {
        const g = ctx.createLinearGradient(0, 0, 0, 300);
        g.addColorStop(0, color.replace(')', `, ${alpha1})`).replace('rgb', 'rgba'));
        g.addColorStop(1, color.replace(')', `, ${alpha2})`).replace('rgb', 'rgba'));
        return g;
    }

    /* ── 1. User Registrations — Line Chart ────────────────── */
    (function buildUsersLine() {
        const el = document.getElementById('chartUsersLine');
        if (!el) return;
        const ctx = el.getContext('2d');

        const grad = ctx.createLinearGradient(0, 0, 0, 260);
        grad.addColorStop(0, 'rgba(99,102,241,0.40)');
        grad.addColorStop(1, 'rgba(99,102,241,0.00)');

        new Chart(ctx, {
            type: 'line',
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                datasets: [{
                    label: 'New Users',
                    data: [38, 52, 61, 74, 68, 95, 110, 128, 104, 139, 158, 172],
                    borderColor: PRIMARY,
                    backgroundColor: grad,
                    borderWidth: 2.5,
                    pointBackgroundColor: PRIMARY,
                    pointRadius: 4,
                    pointHoverRadius: 6,
                    tension: 0.42,
                    fill: true,
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: true,
                interaction: { mode: 'index', intersect: false },
                plugins: {
                    tooltip: {
                        backgroundColor: '#1e293b',
                        borderColor: PRIMARY,
                        borderWidth: 1,
                        titleColor: '#f1f5f9',
                        bodyColor: TEXT_SEC,
                        padding: 10,
                    }
                },
                scales: {
                    x: { grid: { color: GRID }, ticks: { color: TEXT_SEC } },
                    y: {
                        grid: { color: GRID },
                        ticks: { color: TEXT_SEC, stepSize: 30 },
                        beginAtZero: true,
                    }
                }
            }
        });
    })();

    /* ── 2. Events per Month — Bar Chart ───────────────────── */
    (function buildEventsBar() {
        const el = document.getElementById('chartEventsBar');
        if (!el) return;
        const ctx = el.getContext('2d');

        const grad = ctx.createLinearGradient(0, 0, 0, 280);
        grad.addColorStop(0, 'rgba(139,92,246,0.85)');
        grad.addColorStop(1, 'rgba(99,102,241,0.25)');

        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                datasets: [{
                    label: 'Events Created',
                    data: [5, 9, 12, 8, 15, 20, 18, 22, 17, 25, 19, 28],
                    backgroundColor: grad,
                    borderColor: SECONDARY,
                    borderWidth: 1.5,
                    borderRadius: 8,
                    borderSkipped: false,
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: true,
                interaction: { mode: 'index', intersect: false },
                plugins: {
                    tooltip: {
                        backgroundColor: '#1e293b',
                        borderColor: SECONDARY,
                        borderWidth: 1,
                        titleColor: '#f1f5f9',
                        bodyColor: TEXT_SEC,
                        padding: 10,
                    }
                },
                scales: {
                    x: { grid: { display: false }, ticks: { color: TEXT_SEC } },
                    y: {
                        grid: { color: GRID },
                        ticks: { color: TEXT_SEC, stepSize: 5 },
                        beginAtZero: true,
                    }
                }
            }
        });
    })();

    /* ── 3. User Role Distribution — Doughnut ──────────────── */
    (function buildRoleDoughnut() {
        const el = document.getElementById('chartRoleDoughnut');
        if (!el) return;
        const ctx = el.getContext('2d');

        new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: ['Attendees', 'Organizers', 'Admins'],
                datasets: [{
                    data: [680, 95, 12],
                    backgroundColor: [PRIMARY, SECONDARY, SUCCESS],
                    hoverBackgroundColor: ['#818cf8', '#a78bfa', '#34d399'],
                    borderColor: '#1e293b',
                    borderWidth: 3,
                    hoverOffset: 6,
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: true,
                cutout: '70%',
                plugins: {
                    legend: {
                        display: true,
                        position: 'bottom',
                        labels: { color: TEXT_SEC, padding: 14, boxWidth: 12, borderRadius: 6 }
                    },
                    tooltip: {
                        backgroundColor: '#1e293b',
                        borderColor: PRIMARY,
                        borderWidth: 1,
                        titleColor: '#f1f5f9',
                        bodyColor: TEXT_SEC,
                        padding: 10,
                    }
                }
            }
        });
    })();

    /* ── 4. Event Category Breakdown — Horizontal Bar ──────── */
    (function buildCategoryBar() {
        const el = document.getElementById('chartCategoryBar');
        if (!el) return;
        const ctx = el.getContext('2d');

        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['Music', 'Sports', 'Tech', 'Art', 'Food', 'Health'],
                datasets: [{
                    label: 'Events',
                    data: [42, 35, 28, 22, 19, 16],
                    backgroundColor: [
                        'rgba(99,102,241,0.75)',
                        'rgba(139,92,246,0.75)',
                        'rgba(16,185,129,0.75)',
                        'rgba(245,158,11,0.75)',
                        'rgba(239,68,68,0.75)',
                        'rgba(20,184,166,0.75)',
                    ],
                    borderColor: 'transparent',
                    borderRadius: 6,
                    borderSkipped: false,
                }]
            },
            options: {
                indexAxis: 'y',
                responsive: true,
                maintainAspectRatio: true,
                plugins: {
                    tooltip: {
                        backgroundColor: '#1e293b',
                        borderColor: PRIMARY,
                        borderWidth: 1,
                        titleColor: '#f1f5f9',
                        bodyColor: TEXT_SEC,
                        padding: 10,
                    }
                },
                scales: {
                    x: {
                        grid: { color: GRID },
                        ticks: { color: TEXT_SEC, stepSize: 10 },
                        beginAtZero: true,
                    },
                    y: { grid: { display: false }, ticks: { color: TEXT_SEC } }
                }
            }
        });
    })();

    /* ── Scroll-reveal (re-use design system pattern) ──────── */
    const revealOpts = { threshold: 0.1, rootMargin: '0px 0px -40px 0px' };
    const revealObs = new IntersectionObserver((entries) => {
        entries.forEach(e => { if (e.isIntersecting) e.target.classList.add('visible'); });
    }, revealOpts);

    document.querySelectorAll('.chart-card, .stat-card').forEach(el => {
        revealObs.observe(el);
    });

    /* ── Animated counters for stat cards ──────────────────── */
    function animateCount(el, target, duration = 1400) {
        const start = performance.now();
        const initial = 0;
        function step(now) {
            const elapsed = now - start;
            const progress = Math.min(elapsed / duration, 1);
            const eased = 1 - Math.pow(1 - progress, 3); // ease-out cubic
            el.textContent = Math.floor(initial + (target - initial) * eased).toLocaleString();
            if (progress < 1) requestAnimationFrame(step);
        }
        requestAnimationFrame(step);
    }

    const counterObs = new IntersectionObserver((entries) => {
        entries.forEach(e => {
            if (e.isIntersecting) {
                const target = parseInt(e.target.dataset.count, 10);
                animateCount(e.target, target);
                counterObs.unobserve(e.target);
            }
        });
    }, { threshold: 0.5 });

    document.querySelectorAll('[data-count]').forEach(el => counterObs.observe(el));

})();