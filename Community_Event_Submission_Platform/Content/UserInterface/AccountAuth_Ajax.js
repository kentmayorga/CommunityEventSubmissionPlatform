$(document).ready(function () {
    $("#loginForm").submit(function (e) {
        e.preventDefault();

        var $submitBtn = $(this).find("button[type='submit']");
        var originalText = $submitBtn.html();
        $submitBtn.prop("disabled", true).text("Logging in...");

        $.ajax({
            url: "/Account/Login",
            type: "POST",
            data: $(this).serialize(),
            success: function (res) {
                if (res.success) {
                    alert(res.message);
                    window.location.href = res.redirectUrl;
                } else {
                    alert(res.message);
                    $submitBtn.prop("disabled", false).html(originalText);
                }
            },
            error: function (xhr, status, error) {
                alert("Login failed: " + error);
                $submitBtn.prop("disabled", false).html(originalText);
            }
        });
    });

    // Register Form Handler
    $("#registerForm").submit(function (e) {
        e.preventDefault();

        // Validate passwords match
        var password = $("#registerPassword").val();
        var confirmPassword = $("#confirmPassword").val();

        if (password !== confirmPassword) {
            alert("Passwords do not match!");
            return false;
        }

        var $submitBtn = $(this).find("button[type='submit']");
        var originalText = $submitBtn.html();
        $submitBtn.prop("disabled", true).text("Creating Account...");

        $.ajax({
            url: "/Account/Register",
            type: "POST",
            data: $(this).serialize(),
            success: function (res) {
                if (res.success) {
                    alert(res.message);
                    window.location.href = res.redirectUrl;
                } else {
                    alert(res.message);
                    $submitBtn.prop("disabled", false).html(originalText);
                }
            },
            error: function (xhr, status, error) {
                alert("Registration failed: " + error);
                $submitBtn.prop("disabled", false).html(originalText);
            }
        });
    });
});

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


$('#forgotPasswordForm').on('submit', function (e) {
    e.preventDefault();

    var btn = $('#submitBtn');
    btn.prop('disabled', true).html('<i class="bi bi-hourglass-split"></i> Sending...');

    $.ajax({
        url: '/Account/ForgotPassword',  
        type: 'POST',
        data: {
            username: $('#username').val(),
            email: $('#email').val()
        },
        success: function (response) {
            if (response.success) {
                $('#forgotPasswordForm input').prop('disabled', true);
                btn.prop('disabled', true).html('<i class="bi bi-check-circle"></i> Email Sent!');
                $('#alertMessage').html(`
                    <div style="color:#4ade80; text-align:center; padding:10px;
                                background:rgba(74,222,128,0.1); border-radius:8px;">
                        ✅ Reset link sent to your email!<br>
                        <small>Please check your inbox and you can close this tab.</small>
                    </div>
                `);
            } else {
                $('#alertMessage').html(`
                    <div style="color:#f87171; text-align:center; padding:10px;
                                background:rgba(248,113,113,0.1); border-radius:8px;">
                        ❌ ${response.message}
                    </div>
                `);
                btn.prop('disabled', false).html('<i class="bi bi-send me-2"></i> Send Reset Request');
            }
        },
        error: function () {
            $('#alertMessage').html(`
                <div style="color:#f87171; text-align:center; padding:10px;">
                    ❌ Something went wrong. Please try again.
                </div>
            `);
            btn.prop('disabled', false).html('<i class="bi bi-send me-2"></i> Send Reset Request');
        }
    });
});