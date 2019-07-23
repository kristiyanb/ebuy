// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$('input:file').change(
    function(e){
        $('input:file').next().text(e.target.files[0].name);
    }
);