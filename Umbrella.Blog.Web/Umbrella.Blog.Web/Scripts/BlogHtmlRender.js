function renderHTML(data) {
    var blogDOM = document.getElementById("blogContent");
    blogDOM.innerHTML = "";
    var htmlToAdd = '';
    var tagText = '';
    var tagCount;
    var catText = '';
    for (i = 0; i < data.length; i++) {
        tagCount = data[i].Tags.length;
        var myDate = new Date(data[i].PostDate);
        try {
            for (var e = 0; e < tagCount; e++) {
                tagText += data[i].Tags[e].TagText + ' , ';
            }
            catText = data[i].CategoryName;
        } catch (e) {
            tagText = '';
        }
        if (data[i].Approved === false) {
            htmlToAdd += '<button type="button" style="background-color:red;border-radius:8px;border-style:solid">Not Yet Approved</button>';
        } 
        htmlToAdd += '<div id="blogPagePostSummary" class="col-md-12"><strong id="blogPagePostTitle" style="font-size:20px;">  <a href= "http://localhost:59924/Home/BlogPost/' + data[i].PostId + '" > ' + data[i].PostTitle + '</a ></strong> <hr/> <div id="col-md-4" > Date Posted: ' + myDate + ' </div><div id="col-md-4" >Tags: ' + tagText + '</div><div id="col-md-4" >Category: ' + catText + '</div><br/></div >';
        tagText = '';
        //if (i == 0) {
        //    htmlToAdd += '<div id="tagList" class="col-md-3"><div class="col-md-12"><h2> Popular Tags </div><hr id="blackHr" /></div>';
        //}
    }
    //var tagList =
    blogDOM.insertAdjacentHTML('beforeend', htmlToAdd);
    //blogDOM.insertAdjacentHTML('afterbegin', tagList);
}