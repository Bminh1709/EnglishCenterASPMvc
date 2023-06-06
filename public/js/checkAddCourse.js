function formValidCourse() {
    f = document.frmAddCourse;

    if(f.co_Id.value=="") {
        alert("Id can't be empty");
        f.co_Id.focus();
        return false;
    }
    if(f.co_name.value=="") {
        alert("Name can't be empty");
        f.co_name.focus();
        return false;
    }
    if(f.co_teacher.value=="") {
        alert("Teacher can't be empty");
        f.co_teacher.focus();
        return false;
    }
    if(f.co_startday.value=="") {
        alert("Start Day can't be empty");
        f.co_startday.focus();
        return false;
    }
    if(f.co_endday.value=="") {
        alert("End Day can't be empty");
        f.co_endday.focus();
        return false;
    }
    if(f.co_des.value=="") {
        alert("Description can't be empty");
        f.co_des.focus();
        return false;
    }
    if(f.co_price.value=="") {
        alert("Price can't be empty");
        f.co_price.focus();
        return false;
    }
    if(f.co_img.value=="") {
        alert("Img can't be empty");
        f.co_img.focus();
        return false;
    }

    // alert("You have filled out all the information.");
    return true;


}