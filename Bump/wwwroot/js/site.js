// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function deleteMessage(id , url) {
    $.ajax({
        url: url,
        type: 'DELETE',
        data: { 'id': id},
        success: function(result) {
            location.reload();
        }
    });
}

function VoteMessage(id , url) {
    $.ajax({
        url: url,
        type: 'PATCH',
        data: { 'id': id},
        success: function(result) {
            location.reload();
        }
    });
}