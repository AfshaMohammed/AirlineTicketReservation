

var userype = $("#type option:selected").val();

if (userype == "Admin") {
    alert(userype);
    $('#liRegistration').hide();
}