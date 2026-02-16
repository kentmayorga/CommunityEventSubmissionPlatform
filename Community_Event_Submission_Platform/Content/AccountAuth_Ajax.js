$(document).ready(function () {
    // Login Form Handler
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