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



function ShowSwalModal (type, title, message) {
     Swal.fire({
        title: title,
        text: message,
        icon: type,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
         confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            //Swal.fire(
            //    ConfirmedTitle,
            //    ConfirmedMessage,
            //    'success'
            //)
            document.getElementById("DeleteAddressBtn").click();   
        }
    })

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



function StartProductSlider() {
    $('.pgwSlideshow').pgwSlideshow();
}
