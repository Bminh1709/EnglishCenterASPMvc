function checkRegister() {
    f = document.frmRegister;

    var phone_pattern = /^(\(0\d{1,3}\)\d{7})|(0\d{9,10})$/;
    var email_pattern = /^[a-zA-Z]\w*(\w+)*\@\w+(\.\w{2,3})$/;

    if(f.us_firstname.value=="") {
        alert("First Name can't be empty");
        f.us_firstname.focus();
        return false;
    }
    if(f.us_lastname.value=="") {
        alert("Last Name can't be empty");
        f.us_lastname.focus();
        return false;
    }
    if(email_pattern.test(f.us_gmail.value) == false) {
        alert("Invalid email address");
        f.us_gmail.focus();
        return false;
    }
    if(f.us_pass.value.length < 6) {
        alert("Passworrd length must be from 6 to 20 characters");
        f.us_pass.focus();
        return false;
    }
    if(f.us_phone.value=="") {
        alert("Invalid phone number");
        f.us_phone.focus();
        return false;
    }
    var s=document.getElementById("Male");
        var n=document.getElementById("Female");
        if(!s.checked && !n.checked){
            alert("Please select your gender");
            return false;
        }
    if(f.us_img.value=="") {
        alert("Choose an image");
        f.us_img.focus();
        return false;
    }

    return true;


}