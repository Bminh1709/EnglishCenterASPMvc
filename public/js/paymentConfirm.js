function paymentConfirm()
{
    if(confirm("Remember to pay the money!"))
    {
        alert("Your courses will be verified successfully when you transfered enough money!");
        return true;
    }
    else 
    {
        return false;
    }
}