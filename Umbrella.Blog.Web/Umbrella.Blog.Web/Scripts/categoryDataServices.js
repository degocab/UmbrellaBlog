function editCategory(id) {

    var newvalue = this.document.getElementById("newName").value;
    $.ajax({
        type: 'PUT',
        url: "http://localhost:59924/api/categories/" + id,
        data: "CategoryId=" + id + "&CategoryName=" + newvalue,
        success: function (status) {
            window.location = "http://localhost:59924/Category/Manage";
        }

    });


}
function postNewCategory(value) {


    $.ajax({
        type: 'POST',
        url: "http://localhost:59924/api/categories/",
        data: "CategoryId=0&CategoryName=" + value,
        success: function (status) {
            window.location = "http://localhost:59924/Category/Manage";
        }

    });




}
function deleteCategory(id) {
    if (id != "1") {
        $.ajax({
            type: 'DELETE',
            url: "http://localhost:59924/api/categories/" + id,
            success: function (status) {
                confirm("Are you sure you want to delete this category?"),
                    alert("Category has been deleted. All posts using that category have been changed to the Default (First on the list)");
                window.location = "http://localhost:59924/Category/Manage";
            }

        });


    } else {
        alert("You can not delete the default category!");
    }
    

}