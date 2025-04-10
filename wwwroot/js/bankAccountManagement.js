

//hiển thị thông tin khi sửa
//dùng jquery
//$(document).ready(function () {
//    $('.btnEditBankAcc').click(function () {
//        var accId = $(this).data('id');
//        console.log(accId);
//        $.ajax({
//            url: '/Admin/BankAccountManagement/GetBankAccountByID',
//            type: 'GET',
//            data: { id: accId },
//            success: function (response) {
//                if (response.success) {
//                    console.log(response);
//                    var acc = response.acc;
//                    $('#editBankAccID').val(acc.accountId);
//                    $('#editBankAccNumber').val(acc.accountNumber);
//                    $('#editBankAccSelectUser').val(acc.userId);
//                    $('#editBankAccSelectBank').val(acc.bankId);

//                    $.ajax({
//                        url: '/Admin/BankAccountManagement/GetListUser',
//                        type: 'GET',
//                        success: function (users) {
//                            $('#editBankAccSelectUser').empty();
//                            $.each(users, function (i, user) {
//                                //console.log("User Item:", user);
//                                $('#editBankAccSelectUser').append(`
//                                <option class="mx-2" value="${user.iUserIdPk}">${user.sFullName} - ${user.sPhoneNumber}</option>`);
//                            });
//                            $('#editBankAccSelectUser').val(acc.userId);
//                        }
//                    });

//                    $.ajax({
//                        url: '/Admin/BankAccountManagement/GetListBank',
//                        type: 'GET',
//                        success: function (banks) {
//                            $('#editBankAccSelectBank').empty();
//                            $.each(banks, function (i, bank) {
//                                //console.log("Bank Item:", bank);
//                                $('#editBankAccSelectBank').append(`
//                                <option class="mx-2" value="${bank.sBankIdPk}">${bank.sBankIdPk} - ${bank.sBankName}</option>`);
//                            });
//                            $('#editBankAccSelectBank').val(acc.bankId);
//                        }
//                    });

//                    $('#modalEditBankAcc').modal('show');

//                } else {
//                    alert('Không tìm thấy tài khoản ngân hàng!');
//                }
//            },
//            error: function (error) {
//                alert('Error load bank account: ' + error);
//            }
//        });
//    });
//});

document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('.btnEditBankAcc').forEach(button => {
        button.addEventListener("click", async function () {
            let accId = this.dataset.id;
            console.log(accId);

            try {
                // Lấy thông tin tài khoản ngân hàng
                let response = await fetch(`/Admin/BankAccountManagement/GetBankAccountByID?id=${accId}`);
                let data = await response.json();

                if (data.success) {
                    console.log(data);
                    let acc = data.acc;

                    document.getElementById('editBankAccID').value = acc.accountId;
                    document.getElementById('editBankAccNumber').value = acc.accountNumber;
                    document.getElementById('editBankAccSelectUser').value = acc.userId;
                    document.getElementById('editBankAccSelectBank').value = acc.bankId;

                    // Lấy danh sách người dùng
                    let usersResponse = await fetch('/Admin/BankAccountManagement/GetListUser');
                    let users = await usersResponse.json();
                    let userSelect = document.getElementById('editBankAccSelectUser');
                    userSelect.innerHTML = '';
                    users.forEach(user => {
                        let option = document.createElement('option');
                        option.value = user.iUserIdPk;
                        option.textContent = `${user.sFullName} - ${user.sPhoneNumber}`;
                        userSelect.appendChild(option);
                    });
                    userSelect.value = acc.userId;

                    // Lấy danh sách ngân hàng
                    let banksResponse = await fetch('/Admin/BankAccountManagement/GetListBank');
                    let banks = await banksResponse.json();
                    let bankSelect = document.getElementById('editBankAccSelectBank');
                    bankSelect.innerHTML = '';
                    banks.forEach(bank => {
                        let option = document.createElement('option');
                        option.value = bank.sBankIdPk;
                        option.textContent = `${bank.sBankIdPk} - ${bank.sBankName}`;
                        bankSelect.appendChild(option);
                    });
                    bankSelect.value = acc.bankId;

                    // Hiển thị modal chỉnh sửa
                    //let modal = new bootstrap.Modal(document.getElementById('modalEditBankAcc'));
                    //modal.show();

                } else {
                    alert('Không tìm thấy tài khoản ngân hàng!');
                }
            } catch (error) {
                alert('Lỗi khi tải tài khoản ngân hàng: ' + error);
            }
        });
    });
});



//hiển thị thông tin khi khóa
$(document).ready(function () {
    $('.btnLockBankAcc').click(function () {
        var accId = $(this).data('id');
        console.log(accId);
        $.ajax({
            url: '/Admin/BankAccountManagement/GetBankAccountByID',
            type: 'GET',
            data: { id: accId },
            success: function (response) {
                if (response.success) {
                    var acc = response.acc;
                    if (acc.status == "active") {
                        $('#modalLockBankAccTitle').html('Xác nhận khóa tài khoản ngân hàng?');
                        $('#btnSaveLockBankAcc').html('Khóa');
                    } else {
                        $('#modalLockBankAccTitle').html('Xác nhận mở khóa tài khoản ngân hàng?');
                        $('#btnSaveLockBankAcc').html('Mở khóa');
                    }
                    $('#lockBankAccID').val(acc.accountId);
                    $('#lockBankAccName').val(acc.userName);
                    $('#lockBankAccPhone').val(acc.userPhoneNumber);
                    $('#lockBankAccBankID').val(acc.bankId);
                    $('#lockBankAccBankName').val(acc.bankName);
                    $('#lockBankAccNumber').val(acc.accountNumber);
                } else {
                    alert('Không tìm thấy tài khoản ngân hàng!');
                }
            },
            error: function (error) {
                alert('Error load bank account: ' + error)
            }
        });
    })
});

//Validate số tài khoản khi sửa
document.addEventListener("DOMContentLoaded", function () {
    var ipNumber = document.getElementById('editBankAccNumber');
    var errorNumber = document.getElementById('editBankAccNumberError');
    function validateNumber() {
        if (ipNumber.value.trim() === '') {
            errorNumber.textContent = 'Số tài khoản không được để trống!';
        } else {
            errorNumber.textContent = '';
        }
    }

    ipNumber.addEventListener("input", validateNumber);

    document.getElementById('frmEditBankAcc').addEventListener('submit', function (event) {
        validateNumber();
        if (errorNumber.textContent) {
            event.preventDefault();
        }
    });

    document.getElementById('frmEditBankAcc').addEventListener('reset', function () {
        errorNumber.textContent = '';
    });
});


//check account number khi sửa
$(document).ready(function () {
    $('#editBankAccNumber').on('input', function () {
        var number = $(this).val().trim();
        $.ajax({
            url: '/Admin/BankAccountManagement/GetBankAccountByNumber',
            type: 'POST',
            data: { n : number },
            success: function (response) {
                if (response.success) {
                    $('#editBankAccNumberError').text('Số6 tài khoản đã tồn tại!');
                }
            },
            error: function () {
                alert('Lỗi kiểm tra số tài khoản');
            }
        });
    });
});













