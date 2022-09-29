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



//Spinner Svi vjeroucitelji AdministrationView
$('#btnSubmitAdministrationAllUsers').click(function () {
    $('.spinner').css('display', 'block');
});

//Spinner Svi ucenici MuftijaView
$('#btnSubmitMuftijaAllUsers').click(function () {
    $('.spinner').css('display', 'block');
});

//Spinner ListUsers VjerouciteljView
$('#listUsersTeachers').click(function () {
    console.log("AAAAAA");
    $('.spinner').css('display', 'block');
});

$('#school_check').click(function (d) {
    var isChecked = $('#school_check').is(':checked');    
    var studentsChkBoxes = $("#checkBoxes input:checkbox");
    var school = document.querySelector('.school input[type="checkbox"]:checked');
    var schoolName = school.nextElementSibling.innerHTML;
    console.log(schoolName);
            
})

/*---------------------------------STUDENT PRESENCE------------------------------*/
//$(document).ready(function () {
//    $('#checkAll').click(function () {
//        if ($(this).prop('checked')) {

//        console.log('senad')
//        }
//    })

//})

function selectAll(d) {       
    var parent = $(d).parent()[0];
    var parentOfParent = $(parent).parent()[0];
    var checkboxes = parentOfParent.querySelectorAll('input');
    if ($(d).prop('checked')) {
        $(checkboxes).prop('checked', true);                      
    }
    else {
        $(checkboxes).prop('checked', false);
        
    }               
}

function unselectAll() {     
    $('.school_container').css('height', '190px')//set container to 190px
    var checkboxes = document.querySelectorAll('input');
    $(checkboxes).prop('checked', false);// set all checkboxes to false
    $('.school').css('display', 'none');//colapse all students in a school container
    $('.colapsed').css('display', 'flex');//set all schools to plus icon
    $('.expanded').css('display', 'none');//set all minus icon do display none
}

function colapse(d) {
    $('.school_container').css('height', '190px')//set container to 19 px
    var parent = $(d).parent()[0];
    var parentOfParent = $(parent).parent()[0];
    var schools= parentOfParent.querySelectorAll('.school');
    $(schools).css('display', 'none');//students inside school gets colapsed
    $(d).css('display', 'none');//minus icon will be hidden
    var colapsed = $(parent)[0].querySelector('.colapsed');
    $(colapsed).css('display', 'flex');    //plus icon will be shown
}

function expand(d) {
    console.log('expand')
    $('.school_container').css('height', '350px')// set container to 350px
    var parent = $(d).parent()[0];
    var parentOfParent = $(parent).parent()[0];
    var schools = parentOfParent.querySelectorAll('.school');
    $(schools).css('display', 'flex');//students inside school gets expanded
    $(d).css('display', 'none');//plus icon will be hidden    
    $(d).next('.expanded').css('display', 'flex');//minus icon will be shown    
}
    
   
    
        

    
   
    
    



