  function renderHTML(data) {
      var blogDOM = document.getElementById("top3Container");

            var htmlToAdd = '';

            for (i = 0; i < data.length; i++) {

                htmlToAdd += '<div class="col-md-4" id="blogpost" style="margin:auto"><div id="blogcontent"><h3>' + data[i].PostTitle + '</h3><hr /><div id="RecentBlogPostData">' + data[i].PostText + '</div></div></div>';
                //if (i == 0) {
                //    htmlToAdd += '<div id="tagList" class="col-md-3"><div class="col-md-12"><h2> Popular Tags </div><hr id="blackHr" /></div>';
                //}
            }

            blogDOM.insertAdjacentHTML('beforeend', htmlToAdd);


        }