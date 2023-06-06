function checkLogin() {
    f = document.frmLogin;

    var email_pattern = /^[a-zA-Z]\w*(\w+)*\@\w+(\.\w{2,3})$/;

    if(email_pattern.test(f.ad_gmail.value) == false) {
        alert("Please enter a valid email");
        f.ad_gmail.focus();
        return false;
    }
    if(f.ad_pass.value.length < 6) {
        alert("Passworrd length must be from 6");
        f.ad_pass.focus();
        return false;
    }
    return true;


}