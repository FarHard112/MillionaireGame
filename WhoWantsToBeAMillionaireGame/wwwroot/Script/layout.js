$(document).ready(function () {
    const currentUrl = window.location.href;
    $('#admin-nav .nav-item a').each(function () {
        const linkUrl = $(this).attr('href');
        if (currentUrl.includes(linkUrl)) {
            $(this).addClass('active');
            $(this).parents('.nav-item').addClass('menu-open');
        }
    });
});