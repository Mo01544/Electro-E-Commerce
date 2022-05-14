// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('input[type=file]').change(function (e) {
    $in = $(this);
    $in.next().html($in.val());

});

$('.uploadButton').click(function () {
    var fileName = $("#fileUpload").val();
    if (fileName) {
        alert(fileName + " can be uploaded.");
    }
    else {
        alert("Please select a file to upload");
    }
});