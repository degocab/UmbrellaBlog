 $(document).ready(function () {
        var APIRequest = new XMLHttpRequest();
        APIRequest.open('GET', 'http://localhost:59924/api/posts/top');
        APIRequest.onload = function () {

            var postData = JSON.parse(APIRequest.responseText);
            renderHTML(postData);
        };
        APIRequest.send();

      
    });