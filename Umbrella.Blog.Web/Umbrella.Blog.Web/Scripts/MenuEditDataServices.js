function editMenu(id) {

    var newNameValue = this.document.getElementById("new-1-" + id).value;
    var newLinkValue = this.document.getElementById("new-2-" + id).value;

    $.ajax({
        type: 'PUT',
        url: "http://localhost:59924/api/menuitems/edit/" + id,
        data: "MenuItemID=" + id + "&MenuText=" + newNameValue + "&MenuLink=" + newLinkValue,
        success: function (status) {
            window.location = "http://localhost:59924/Manage/ManageMenu";
        }

    });


}
function postNewMenu(value,value2) {


    $.ajax({
        type: 'POST',
        url: "http://localhost:59924/api/menuitems/add",
        data: "MenuItemID=0&MenuText=" + value + "&MenuLink=" + value2,
        success: function (status) {
            window.location = "http://localhost:59924/Manage/ManageMenu";
        }

    });




}
function deleteMenuItem(id) {
   
        $.ajax({
            type: 'DELETE',
            url: "http://localhost:59924/api/menuitems/delete/" + id,
            success: function (status) {
                confirm("Are you sure you want to delete this item?"),
                window.location = "http://localhost:59924/Manage/ManageMenu";
            }

        });


  

}