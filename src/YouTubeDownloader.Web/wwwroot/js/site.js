(function () {
    const themeToggle = document.getElementById("themeToggle");
    const html = document.documentElement;

    const savedTheme = localStorage.getItem("clipvault-theme") || "light";

    setTheme(savedTheme);

    if (themeToggle) {
        themeToggle.addEventListener("click", function () {
            const currentTheme = html.getAttribute("data-theme");
            const nextTheme = currentTheme === "dark" ? "light" : "dark";

            setTheme(nextTheme);
            localStorage.setItem("clipvault-theme", nextTheme);
        });
    }

    function setTheme(theme) {
        html.setAttribute("data-theme", theme);

        if (!themeToggle) return;

        if (theme === "dark") {
            themeToggle.innerText = "☀️ Light";
        } else {
            themeToggle.innerText = "🌙 Dark";
        }
    }
})();