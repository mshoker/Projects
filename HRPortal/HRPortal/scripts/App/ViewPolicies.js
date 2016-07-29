$(document)
    .ready(function () {
        $('.showDetail')
            .on('click',
                function () {
                    //$('#policyDetail').show();
                    $(this).parent().siblings(".policyDetail").show();
                    $(this).siblings('.hideDetail').show();
                    $(this).hide();
                });
        $('.policyDetail, .hideDetail')
            .each(function () {
                $(this).hide();
            });

        $('.hideDetail')
            .on('click',
                function () {
                    $(this).parent().siblings(".policyDetail").hide();
                    $(this).siblings('.showDetail').show();
                    $(this).hide();
                });
        $('#SelectedCategory')
                    .change(function () {
                        $('.policyCategory').hide();
                        var selected = '.' + $(this).find("option:selected").text();
                        $(selected).show();
                    });
    });