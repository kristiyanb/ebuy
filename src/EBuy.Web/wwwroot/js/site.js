// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$('input:file').change(
    function (e) {
        $('input:file').next().text(e.target.files[0].name);
    }
);

$('.vote').toArray().forEach(x => {
    $(x).mouseover(displayRating);
    $(x).mouseleave(hideRating);

    function displayRating(e) {
        let rating = e.target.id.split('-')[1];

        for (let i = 1; i <= 5; i++) {
            if (rating >= i) {
                $(`#rating-${i}`).css('color', 'darkorange'); //addClass('checked');
            } else {
                $(`#rating-${i}`).css('color', 'black');
            }
        }
    }

    function hideRating(e) {
        for (let i = 1; i <= 5; i++) {
            let star = $(`#rating-${i}`);

            if (!star.hasClass('checked')) {
                star.css('color', 'black');
            } else {
                star.css('color', 'darkorange');
            }
        }
    }
});