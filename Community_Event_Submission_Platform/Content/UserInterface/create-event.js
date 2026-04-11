'use strict';

// ── Category pill selection ───────────────────────────────────────────
document.querySelectorAll('.cat-pill').forEach(function (pill) {
    pill.addEventListener('click', function () {
        document.querySelectorAll('.cat-pill').forEach(p => p.classList.remove('selected'));
        pill.classList.add('selected');
    });
});

// ── Multi-step form navigation ────────────────────────────────────────
const stepIndicatorItems = document.querySelectorAll('.step-indicator .step');
const stepLines = document.querySelectorAll('.step-indicator .step-line');
const formSteps = document.querySelectorAll('.form-step');

function goToStep(stepNum) {
    // Hide all steps, show target
    formSteps.forEach(function (s) { s.classList.remove('active'); });
    const target = document.getElementById('step-' + stepNum);
    if (target) {
        target.classList.add('active');
        // Trigger fade-in for cards inside the step
        target.querySelectorAll('.fade-in').forEach(function (el, i) {
            el.classList.remove('visible');
            setTimeout(function () { el.classList.add('visible'); }, 80 + i * 100);
        });
    }

    // Update step indicator
    stepIndicatorItems.forEach(function (s, idx) {
        const num = idx + 1;
        s.classList.remove('active', 'done');
        if (num === stepNum) s.classList.add('active');
        else if (num < stepNum) s.classList.add('done');
    });

    // Update connector lines
    stepLines.forEach(function (line, idx) {
        line.classList.toggle('done', idx + 1 < stepNum);
    });

    window.scrollTo({ top: 0, behavior: 'smooth' });
}

// Step 1 → 2
document.getElementById('nextStep1')?.addEventListener('click', function () {
    const titleInput = document.querySelector('[name="title"]');
    const catSelected = document.querySelector('.cat-pill.selected');
    const descInput = document.querySelector('[name="description"]');

    if (!titleInput?.value.trim()) {
        titleInput?.focus();
        titleInput?.classList.add('is-invalid');
        return;
    }
    titleInput.classList.remove('is-invalid');

    if (!catSelected) {
        document.getElementById('categoryPills')?.scrollIntoView({ behavior: 'smooth', block: 'center' });
        document.getElementById('categoryPills').style.outline = '2px solid rgba(99,102,241,0.6)';
        setTimeout(() => document.getElementById('categoryPills').style.outline = '', 1500);
        return;
    }

    if (!descInput?.value.trim()) {
        descInput?.focus();
        descInput?.classList.add('is-invalid');
        return;
    }
    descInput.classList.remove('is-invalid');

    goToStep(2);
});

// Step 2 → 1 (back)
document.getElementById('prevStep2')?.addEventListener('click', function () {
    goToStep(1);
});

// Step 2 → 3
document.getElementById('nextStep2')?.addEventListener('click', function () {
    const dateInput = document.querySelector('[name="event_date"]');
    const timeInput = document.querySelector('[name="event_time"]');
    const locInput = document.querySelector('[name="location"]');

    let valid = true;
    [dateInput, timeInput, locInput].forEach(function (inp) {
        if (inp && !inp.value.trim()) {
            inp.classList.add('is-invalid');
            if (valid) inp.focus();
            valid = false;
        } else {
            inp?.classList.remove('is-invalid');
        }
    });

    if (valid) goToStep(3);
});

// Step 3 → 2 (back)
document.getElementById('prevStep3')?.addEventListener('click', function () {
    goToStep(2);
});

// ── Image upload ──────────────────────────────────────────────────────
const imgUploadZone = document.getElementById('imgUploadZone');
const imgInput = document.getElementById('imgInput');
const imgPreviewWrap = document.getElementById('imgPreviewWrap');
const imgUploadDefault = document.getElementById('imgUploadDefault');
const imgPreviewEl = document.getElementById('imgPreview');

function showPreview(file) {
    if (!file || !file.type.startsWith('image/')) return;
    const reader = new FileReader();
    reader.onload = function (e) {
        imgPreviewEl.src = e.target.result;
        imgUploadDefault.classList.add('d-none');
        imgPreviewWrap.classList.remove('d-none');
    };
    reader.readAsDataURL(file);
}

imgUploadZone?.addEventListener('click', function (e) {
    if (!e.target.closest('#removeImg')) imgInput?.click();
});

imgInput?.addEventListener('change', function () {
    if (imgInput.files[0]) showPreview(imgInput.files[0]);
});

// Drag & Drop
imgUploadZone?.addEventListener('dragover', function (e) {
    e.preventDefault();
    imgUploadZone.classList.add('dragover');
});
imgUploadZone?.addEventListener('dragleave', function () {
    imgUploadZone.classList.remove('dragover');
});
imgUploadZone?.addEventListener('drop', function (e) {
    e.preventDefault();
    imgUploadZone.classList.remove('dragover');
    const file = e.dataTransfer.files[0];
    if (file) {
        // Assign to input for form submission
        const dt = new DataTransfer();
        dt.items.add(file);
        imgInput.files = dt.files;
        showPreview(file);
    }
});

document.getElementById('removeImg')?.addEventListener('click', function (e) {
    e.stopPropagation();
    imgInput.value = '';
    imgPreviewEl.src = '';
    imgPreviewWrap.classList.add('d-none');
    imgUploadDefault.classList.remove('d-none');
});

// ── Free/Paid toggle → show price row ────────────────────────────────
document.getElementById('isFreeToggle')?.addEventListener('change', function () {
    const priceRow = document.getElementById('priceRow');
    if (priceRow) {
        priceRow.classList.toggle('d-none', this.checked);
    }
});

// ── Preview modal ─────────────────────────────────────────────────────
const categoryEmojiMap = {
    'Sports & Fitness': '🏃',
    'Music': '🎵',
    'Community': '🤝',
    'Education': '📚',
    'Food & Drink': '🍽️',
    'Arts & Culture': '🎨',
    'Technology': '💻',
    'Health & Wellness': '🧘',
};

document.getElementById('previewBtn')?.addEventListener('click', function () {
    const title = document.querySelector('[name="title"]')?.value.trim() || 'Untitled Event';
    const desc = document.querySelector('[name="description"]')?.value.trim() || '';
    const date = document.querySelector('[name="event_date"]')?.value || '';
    const time = document.querySelector('[name="event_time"]')?.value || '';
    const location = document.querySelector('[name="location"]')?.value.trim() || '';
    const catPill = document.querySelector('.cat-pill.selected');
    const cat = catPill ? catPill.dataset.cat : 'Event';
    const emoji = categoryEmojiMap[cat] || '📅';

    // Format date nicely
    let dateDisplay = date;
    if (date) {
        const d = new Date(date + 'T00:00:00');
        dateDisplay = d.toLocaleDateString('en-US', { month: 'short', day: 'numeric', year: 'numeric' });
    }

    // Image
    let imgHtml = '';
    if (imgPreviewEl?.src && imgPreviewEl.src !== window.location.href) {
        imgHtml = '<img src="' + imgPreviewEl.src + '" alt="Event" class="preview-img" />';
    } else {
        imgHtml = '<div class="preview-img-placeholder">' + emoji + '</div>';
    }

    document.getElementById('previewBody').innerHTML =
        imgHtml +
        '<span class="event-category-badge">' + cat + '</span>' +
        '<h4>' + title + '</h4>' +
        (dateDisplay ? '<div class="preview-meta highlight"><i class="bi bi-calendar3"></i> ' + dateDisplay + (time ? ' &nbsp;at ' + time : '') + '</div>' : '') +
        (location ? '<div class="preview-meta"><i class="bi bi-geo-alt"></i> ' + location + '</div>' : '') +
        (desc ? '<p class="preview-desc">' + desc + '</p>' : '');

    new bootstrap.Modal(document.getElementById('previewModal')).show();
});

// ── Input validation: remove is-invalid on input ──────────────────────
document.querySelectorAll('.ce-input').forEach(function (inp) {
    inp.addEventListener('input', function () {
        inp.classList.remove('is-invalid');
    });
});