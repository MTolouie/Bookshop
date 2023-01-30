window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Success")
    }

    if (type === "error") {
        toastr.error(message, "Operation Error")
    }

    if (type === "warning") {
        toastr.warning(message, "Operation Warning")
    }
}

window.ShowSweatAlert = ( type,title, message) => {

    Swal.fire(
        title,
        message,
        type
    )
}

function ShowDeleteConfirmationModal() {
    $("#deleteConfirmationModal").modal("show");
}

function HideDeleteConfirmationModal() {
    $("#deleteConfirmationModal").modal("hide");
}

function ShowCategoryCreateModal() {
    $("#CategoryCreateModal").modal("show");
}

function HideCategoryCreateModal() {
    $("#CategoryCreateModal").modal("hide");
}
