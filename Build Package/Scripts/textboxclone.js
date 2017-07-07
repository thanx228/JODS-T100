$(function () {
    $('input[id$=TextBox1').keyup(function () {
        var txtClone = $(this).val();
        $('input[id$=TextBox2').val(txtClone);
    });
});