

function makeTextbox(id) {
    $(".edit").attr("disabled", true);
    $(".delete").attr("disabled", true);

    var value = document.getElementById('1-'+id).innerHTML.trim();
    var value2 = document.getElementById('2-'+id).innerHTML.trim();

    document.getElementById('1-' + id).innerHTML = '<input type="text" id="new-1-' + id +'" value="' + value + '">';
    document.getElementById('2-' + id).innerHTML = '<input type="text" style="width:100%" id="new-2-'+id+'" value="' + value2 + '">';

    document.getElementById("edit-Btn+" + id).hidden = true;
    document.getElementById("save-Btn+" + id).hidden = false;
    document.getElementById("cancel-Btn+" + id).hidden = false;

}

function saveNewMenuItem() {

    var value = document.getElementById("newMenuName").value;
    var value2 = document.getElementById("newMenuLink").value;


    if (value !== '' && value2 !== '') {
        postNewMenu(value, value2);
    }
    


}
function newItem() {

    document.getElementById("newItemInfo").hidden = false;
    //document.getElementById("newMenuLink").hidden = false;

    //document.getElementById("saveNewCat").hidden = false;





}