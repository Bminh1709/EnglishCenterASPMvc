$(document).ready(function () {

    //$('#adminImg').click(function () {
    //    $('#adminImg').attr('contenteditable', 'true');
    //});

    $('#adminImg').click(function () {
        $('#uploadInput').click();
    });

    $('#uploadInput').change(function () {
        // retrieves the first file from the <input> element with the 'files' property
        var file = this.files[0];
        var formData = new FormData();
        // key and value into formData
        formData.append('file', file);

        $.ajax({
            url: '/Admin/Home/UpdateProfilePhoto',
            type: 'POST',
            data: formData,
            // false: not automatically convert objects and arrays into a query string
            // jQuery will not process the data. The data being sent is already in the desired format (e.g., a string, FormData object, or a file)
            processData: false,
            // false: automatically determine the appropriate content type based on the FormData object. This is useful when sending files or other binary data.
            contentType: false,
            success: function (rs) {
                if (rs.success) {
                    // Handle the response from the server (e.g., display success message, reload page, etc.)
                    alert("Profile updated successfully!");
                    window.location.reload();
                }
                else {
                    // Handle the error (e.g., display error message, enable the "Save Changes" button, etc.)
                    alert("Failed to update profile. Please try again.");
                    $('#saveChangesBtn').prop('disabled', false);
                }
            }
        });
    });




    $('#editInfoBtn').click(function () {
        // Hide the "Edit Info" button
        $(this).hide();

        // Show the "Save Changes" button
        $('#saveChangesBtn').show();

        // Enable editing of Last Name and First Name fields
        $('#adminLastName').attr('contenteditable', 'true');
        $('#adminFirstName').attr('contenteditable', 'true');
        // click onto lastname
        $('#adminLastName').focus();
    });

    $('#saveChangesBtn').click(function () {
        // Disable the "Save Changes" button
        $(this).prop('disabled', true);

        // Get the updated Last Name and First Name values
        var updatedLastName = $('#adminLastName').text();
        var updatedFirstName = $('#adminFirstName').text();

        // Make the AJAX request to update the data on the server
        $.ajax({
            url: '/Admin/Home/UpdateProfile',
            type: 'POST',
            data: {
                lastName: updatedLastName,
                firstName: updatedFirstName
            },
            success: function (rs) {
                if (rs.success) {
                    // Handle the response from the server (e.g., display success message, reload page, etc.)
                    alert("Profile updated successfully!");
                    window.location.reload();
                }
                else {
                    // Handle the error (e.g., display error message, enable the "Save Changes" button, etc.)
                    alert("Failed to update profile. Please try again.");
                    $('#saveChangesBtn').prop('disabled', false);
                }
            }
        });
    });
});
