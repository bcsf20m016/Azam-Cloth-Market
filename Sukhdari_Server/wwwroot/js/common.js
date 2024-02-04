window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful");
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed");
    }
    if (type === "warning") {
        toastr.warning(message, "Warning");

    }
}

function ShowDeleteConfirmationModal() {

    $('#deleteConfirmationModal').modal('show');
    
}
function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}