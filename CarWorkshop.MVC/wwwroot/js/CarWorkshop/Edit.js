$(document).ready(function(){

    LoadCarWorkshopServices()

    $("#createCarWorkshopServiceModal form").submit(function(e){
        e.preventDefault();

        $.ajax({
            url: $(this).attr("action"),
            type: $(this).attr("method"),
            data: $(this).serialize(),
            success: function(data){
                toastr["success"]("Created car workshop service")
                LoadCarWorkshopServices()
            },
            error: function(){
                toastr["error"]("Something went wrong")
            }
        })
    })
})