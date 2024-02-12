﻿function statistics() {
    $('#statictics_btn').on('click', function (e) {
       
        e.preventDefault();
        e.stopPropagation();
       

        if ($('#statistics_box').hasClass('d-none')) {
            $.get('https://localhost:7242/api/statistics', function (data) {
                $('#total_houses').text(data.totalHouses + " Houses")
                $('#total_rents').text(data.totalRents + " Rents")

                $('#statistics_box').removeClass('d-none');

                $('#statistics_btn').text('Hide Statistics');
                $('#statistics_btn').removeClass('btn-primary');
                $('#statistics_btn').addClass('btn-danger');
            });
        }
        else {
            $('#statistics_box').addClass('d-none');

            $('#statistics_btn').text('Show Statistics');
            $('#statistics_btn').addClass('btn-danger');
            $('#statistics_btn').removeClass('btn-primary');
        }

    });
}