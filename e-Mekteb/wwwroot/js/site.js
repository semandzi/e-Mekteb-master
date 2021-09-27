// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


//Filter imena .
    $(document).ready(function () {
        $("#myInput").on("keyup", function () {
            var value = $(this).val().toLowerCase();
            $("#myDIV *").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
            $('input:checkbox').show();

        });
        
    });

//Filter padajuceg izbornika
$(document).ready(function () {
    //copy options
    var options = $('#theList option').clone();
    //react on keyup in textbox
    $('#search_input').keyup(function () {
        var val = $(this).val();
        $('#theList').empty();
        //take only the options containing your filter text or all if empty
        options.filter(function (idx, el) {
            return val === '' || $(el).text().indexOf(val) >= 0;
        }).appendTo('#theList');//add it to list
    });
});
//Prisutosti-funkcionalnosti sa checkbox-om
$(function () {
    var $tblChkBox = $("#checkBoxes input:checkbox");
    $("#checkAll").on("click", function () {
        $($tblChkBox).prop('checked', $(this).prop('checked'));
    });
});
