$(document)
    .ready(function() {
        $('#applicationForm')
            .validate({
                rules: {
                    'app_FirstName': {
                        required: true
                    },
                    'app_LastName': {
                        required: true
                    },
                    'app_Resume': {
                        required: true
                    },
                    'app_Phone': {
                        required: true,
                        phoneUS: true
                    },
                    'app_Email': {
                        required: true,
                        email: true
                    }
                },
                messages: {
                    'app_FirstName': "Required",
                    'app_LastName': "Required",
                    'app_Resume': "Required",
                    'app_Phone': {
                        required: "Required",
                        phoneUS: "Required phone"
                    },
                    'app_Email': "Required"
                }
            });
    });