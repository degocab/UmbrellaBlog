

function makeTextbox(id) {
    $(".edit").attr("disabled", true);
    $(".delete").attr("disabled", true);

    var value = document.getElementById(id).innerHTML.trim();
    document.getElementById(id).innerHTML = '<input type="text" id="newName" value="' + value + '">';
    document.getElementById("edit-Btn+" + id).hidden = true;
    document.getElementById("save-Btn+" + id).hidden = false;
    document.getElementById("cancel-Btn+" + id).hidden = false;

}

function saveNewCategory() {

    var value = document.getElementById("newCategory").value;

    if (value !== '') {
        postNewCategory(value);
    }


}
function newCategory() {

    document.getElementById("newItem").hidden = false;






}