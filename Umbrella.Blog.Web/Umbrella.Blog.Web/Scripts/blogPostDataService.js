
$(document).ready(function () {
    var blogId = document.getElementById("blogId").value;

    var APIRequest = new XMLHttpRequest();
    var url = 'http://localhost:59924/api/posts/' + blogId;
    APIRequest.open('GET', url);
    APIRequest.onload = function () {

        var postData = JSON.parse(APIRequest.responseText);
        renderHTML(postData);
        blogId=postData.PostId;
    };
    APIRequest.send();
   
});
function deletePost() {
    var blogId = document.getElementById("blogId").value;

    $.ajax({
        type: 'DELETE',
        url: "http://localhost:59924/api/posts/" + blogId,
        success: function (status) {
            alert("Post has been deleted.")
            window.location = "http://localhost:59924/Home/Blog";
        }

    });

};
function approvePost() {
    var blogId = document.getElementById("blogId").value;

    $.ajax({
        type: 'POST',
        url: "http://localhost:59924/api/posts/approve/" + blogId,
        success: function (status) {
            alert("Post has been approved.")
            window.location = "http://localhost:59924/Home/BlogPost/" + blogId;
        }

    });

};