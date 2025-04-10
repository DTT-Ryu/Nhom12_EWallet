//hiển thị list bank

$(document).ready(function () {
    $.ajax({
        url: '/getAllBanks',
        type: 'GET',
        success: function (banks) {
            $('#addBankSelect').empty();
            $.each(banks, function (i, bank) {
                $('#addBankSelect').append(`
                <option class="mx-2" value="${bank.sBankIdPk}">${bank.sBankIdPk} - ${bank.sBankName}</option>`);
            });
        }
    });
});

//check acc number
$(document).ready(function () {
    $('#addAccountNumber').on('input', function () {
        var number = $(this).val().trim();
        var errorField = $('#addAccountNumberError');

        if (!number) {
            errorField.text(''); // Xóa lỗi khi input rỗng
            return;
        }

        $.ajax({
            url: "/getNumber",
            method: 'POST',
            data: { n: number },
            cache: false, // Ngăn cache AJAX
            success: function (response) {
                if (response.success) {
                    errorField.text('Số tài khoản đã tồn tại!');
                } else {
                    errorField.text('');
                }
            },
            error: function () {
                alert('Lỗi kiểm tra số tài khoản');
            }
        });
    });
});
