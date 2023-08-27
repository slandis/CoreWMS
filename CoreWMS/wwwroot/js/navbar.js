$(document).ready(function () {
    $("#submenu").on('change', function () {
        window.location.href = "/" + this.value;
    });

});