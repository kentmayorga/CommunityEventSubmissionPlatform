// ── Edit Profile — Save Changes ──────────────────────────────────────────────
document.getElementById('saveProfileBtn').addEventListener('click', function () {
    var username = document.getElementById('inputName').value.trim();
    var address = document.getElementById('inputLocation').value.trim();
    var bio = document.getElementById('inputBio').value.trim();

    if (!username) {
        alert('Display name cannot be empty.');
        return;
    }

    var btn = this;
    btn.disabled = true;
    btn.innerHTML = '<span class="spinner-border spinner-border-sm me-1"></span> Saving...';

    fetch('/Client/UpdateProfile', {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        // FIX: parameter names must match controller action params: username, address, bio
        body: new URLSearchParams({ username: username, address: address, bio: bio })
    })
        .then(function (res) { return res.json(); })
        .then(function (data) {
            if (data.success) {
                // Update page live without reload
                document.querySelectorAll('[data-profile-username]').forEach(function (el) {
                    el.textContent = data.username;
                });

                var avatarEl = document.getElementById('avatarInitials');
                if (avatarEl) avatarEl.textContent = data.username.charAt(0).toUpperCase();

                var locEl = document.getElementById('displayLocation');
                if (locEl) locEl.textContent = data.address || 'Not set';

                var bioEl = document.getElementById('displayBio');
                if (bioEl) bioEl.textContent = data.bio || 'No bio yet — tell the community a little about yourself!';

                // Keep modal inputs in sync
                document.getElementById('inputName').value = data.username;
                document.getElementById('inputLocation').value = data.address || '';
                document.getElementById('inputBio').value = data.bio || '';

                // Close modal
                bootstrap.Modal.getInstance(document.getElementById('editModal')).hide();

                showToast(data.message, 'success');
            } else {
                showToast(data.message, 'error');
            }
        })
        .catch(function (err) {
            showToast('Something went wrong. Please try again.', 'error');
            console.error(err);
        })
        .finally(function () {
            btn.disabled = false;
            btn.innerHTML = '<i class="bi bi-check2 me-1"></i> Save Changes';
        });
});

// ── Preview Profile — populate before show ───────────────────────────────────
document.getElementById('previewModal').addEventListener('show.bs.modal', function () {
    var username = document.getElementById('inputName').value.trim()
        || document.getElementById('avatarInitials').textContent.trim();
    var address = document.getElementById('inputLocation').value.trim() || 'Not set';
    var bio = document.getElementById('inputBio').value.trim()
        || 'No bio yet — tell the community a little about yourself!';

    document.getElementById('previewName').textContent = username;
    document.getElementById('previewHandle').textContent = '@' + username;
    document.getElementById('previewLocation').textContent = address;
    document.getElementById('previewBio').textContent = bio;
    document.getElementById('previewAvatar').textContent = username.charAt(0).toUpperCase();
});

// ── Toast helper ─────────────────────────────────────────────────────────────
function showToast(message, type) {
    var existing = document.getElementById('profileToast');
    if (existing) existing.remove();

    var color = type === 'success' ? '#22c55e' : '#ef4444';
    var icon = type === 'success' ? 'bi-check-circle-fill' : 'bi-x-circle-fill';

    var toast = document.createElement('div');
    toast.id = 'profileToast';
    toast.style.cssText = [
        'position:fixed', 'bottom:24px', 'right:24px', 'z-index:9999',
        'background:#1e2538', 'color:#fff', 'padding:12px 18px',
        'border-radius:10px', 'display:flex', 'align-items:center', 'gap:10px',
        'box-shadow:0 4px 24px rgba(0,0,0,0.35)', 'font-size:0.875rem',
        'border-left:4px solid ' + color,
        'animation:fadeInUp 0.3s ease'
    ].join(';');

    toast.innerHTML = '<i class="bi ' + icon + '" style="color:' + color + '; font-size:1.1rem;"></i>'
        + '<span>' + message + '</span>';

    document.body.appendChild(toast);
    setTimeout(function () {
        toast.style.animation = 'fadeOutDown 0.3s ease forwards';
        setTimeout(function () { toast.remove(); }, 300);
    }, 3000);
}

// Inject toast keyframes once
(function () {
    if (document.getElementById('toastStyles')) return;
    var style = document.createElement('style');
    style.id = 'toastStyles';
    style.textContent = [
        '@keyframes fadeInUp   { from { opacity:0; transform:translateY(16px); } to { opacity:1; transform:translateY(0); } }',
        '@keyframes fadeOutDown { from { opacity:1; transform:translateY(0); } to { opacity:0; transform:translateY(16px); } }'
    ].join('');
    document.head.appendChild(style);
})();