// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const setTheme = theme => {
    if (theme === "auto") {
        document.documentElement.setAttribute("data-bs-theme", (window.matchMedia("(prefers-color-scheme: dark)").matches ? "dark" : "light"));
    } else {
        document.documentElement.setAttribute("data-bs-theme", theme);
    }
}