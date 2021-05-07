//Corrección para mostrar correctamente los botones de páginas (problema de la librería)
function fixPageButton() {
    $('ul.pagination > li.disabled > a').addClass('page-link');
}

function successMessage() {
    swal({
        title: "Cambios realizados correctamente!",
        icon: "success"
    })
}

function deleteMessage (id){
    swal({
        title: "¿Está seguro?",
        text: "Una vez eliminado no podrá recuperarse",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: 'Home/Delete/',
                    data: { id: id },
                    success: swal({
                        title: "El usuario se ha eliminado",
                        icon: "success",
                        button: true
                    }).then(() => {
                        window.location.reload();
                    })
                });
            }
            else {
                swal({
                    title: "Usuario no eliminado"
                });
            }
        });
}