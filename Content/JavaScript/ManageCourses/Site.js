$(document).ready(function () {


    $('#co_img').on('change', function (event) {
        getImagePreview(event);
    });
    //function getImagePreview(event) {
    //    // URL.createObjectURL() generates a temporary URL that represents the image file
    //    var image = URL.createObjectURL(event.target.files[0]);
    //    var imagediv = document.getElementById('preview');
    //    var newimg = document.createElement('img');
    //    // clears any existing content inside the imagediv element 
    //    imagediv.innerHTML = '';
    //    newimg.src = image;
    //    newimg.width = "300";
    //    newimg.height = "200";
    //    newimg.style.objectFit = "cover"; // Set the object-fit property
    //    newimg.style.borderRadius = "5px"; // Set the border-radius property
    //    // appends the newimg element as a child to the imagediv element
    //    imagediv.appendChild(newimg);
    //}
    // --- JQUERY ---
    function getImagePreview(event) {
        var image = URL.createObjectURL(event.target.files[0]);
        var imagediv = $('#preview');
        imagediv.empty();
        var newimg = $('<img>').attr('src', image).css({
            'width': '300px',
            'height': '200px',
            'object-fit': 'cover',
            'border-radius': '5px'
        });
        imagediv.append(newimg);
    }


    // ADD COURSE
    $('#courseForm').submit(function (e) {
        e.preventDefault(); // Prevent the form from submitting normally

        var formData = new FormData(this);

        if (formValidCourse()) {

        $.ajax({
            url: '/Admin/Home/Add',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (rs) {
                if (rs.success) {
                    window.location.reload();
                    alert("Success");
                }
                else {
                    window.location.reload();
                    //$('#modelCourses').modal('hide'); // Hide the modal
                    //$('.modal-backdrop').remove(); // Remove the modal backdrop
                    alert("Fail");
                }
            }
        });

        }
    });




    // Delete Course
    $('body').on('click', '.btnDeleteCourse', function () {
        var tmpId = $(this).data("id");
        // -- Jquery --
        var conf = confirm("Are you sure to delete this course?");
        // Ajax
        if (conf == true) {
            $.ajax({
                url: '/Admin/Home/Delete',
                type: 'Post',
                data: { id: tmpId },
                success: function (rs) {
                    if (rs.success == true) {
                        $('#trow_' + tmpId).remove();
                    }
                }
            });
        }
        // -- Ajax --
    });


       


});