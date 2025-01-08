document.addEventListener('DOMContentLoaded', function () {
    const dropdownButton = document.getElementById('dropdownMenuButton');
    const dropdownMenu = document.querySelector('.dropdown-menu');

    dropdownButton.addEventListener('click', function () {
        dropdownMenu.classList.toggle('show');
    });
});