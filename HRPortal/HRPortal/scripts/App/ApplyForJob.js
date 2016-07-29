$(document)
    .ready(function() {
        $('.days')
            .change(function() {
                var dayValue = 0;
                $('.days:checked')
                    .each(function() {
                        dayValue += parseInt($(this).val());
                    });
                $('#app_PreferredDays').val(dayValue);
            });
    });