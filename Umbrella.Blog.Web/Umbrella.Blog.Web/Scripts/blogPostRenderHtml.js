function renderHTML(data) {
    var CommentDOM = document.getElementById("blogComment");

    var blogDOM = document.getElementById("blogContent");
            var htmlToAdd = '';

            if (document.contains(document.getElementById("approve-Btn"))) {

                if (data.Approved === true) {
                    document.getElementById("approve-Btn").hidden = true;
                }
            }
            htmlToAdd += '<div class="col-md-12" id="blogHeader"><h1> ' + data.PostTitle + '</h1></div > <hr /> <div id="blogText">'+data.PostText+'</div>';

            blogDOM.insertAdjacentHTML('beforeend', htmlToAdd);
            //var Comments = '';
            //for (var i = 0; i < data.Comments.length; i++) {
            //    console.log(data.Comments[i].CommentText);
            //    var date = new Date(data.Comments[i].CommentDate);
            //    date = date.toISOString().slice(0, 10);

            //    Comments += ' <div id="blogComment" >' + date +':'+data.Comments[i].CommentText + '</div>';
                


            //}
            //CommentDOM.insertAdjacentHTML('beforeend', Comments);

        }