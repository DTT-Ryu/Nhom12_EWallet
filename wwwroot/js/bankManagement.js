
////Validate create bank
document.addEventListener("DOMContentLoaded", function () {
    var ipID = document.getElementById("createBankId");
    var ipName = document.getElementById("createBankName");
    var ipImg = document.getElementById("createBankImg");

    var errorID = document.getElementById('createBankIdError');
    var errorName = document.getElementById('createBankNameError');
    var errorImg = document.getElementById('createBankImgError');

    function validateBankID() {
        if (ipID.value.trim() === '') {
            errorID.textContent = "Mã ngân hàng không được để trống.";
        } else {
            errorID.textContent = '';
        }
    }

    function validateBankName() {
        if (ipName.value.trim() === '') {
            errorName.textContent = 'Tên ngân hàng không được để trống';
        } else {
            errorName.textContent = '';
        }
    }

    function validateBankImg() {
        var img = ipImg.files[0];    //lấy file
        if (!img) {
            errorImg.textContent = 'Ảnh không được để trống';
        } else {
            var allowExtension = ['image/jpeg', 'image/png', 'image/jpg'];
            var maxSize = 2 * 1024 * 1024;
            if (!allowExtension.includes(img.type)) {
                errorImg.textContent = 'Chỉ chấp nhận định dạng ảnh JPEG, PNG, JPG.';
            } else if (img.size > maxSize) {
                errorImg.textContent = 'Ảnh phải nhỏ hơn 2MB';
            } else {
                errorImg.textContent = '';
            }
        }
    }

    //gán sụ kiện lắng nghe cho inp
    ipID.addEventListener("input", validateBankID);
    ipName.addEventListener("input", validateBankName);
    ipImg.addEventListener("change", validateBankImg);

    //Kiểm tra tất cả khi submit form
    document.getElementById("frmCreateBank").addEventListener("submit", function (event) {
        validateBankID();
        validateBankName();
        validateBankImg();
        if (errorID.textContent || errorName.textContent || errorImg.textContent) {
            event.preventDefault();
        }
    });

    //nút reset
    document.getElementById("frmCreateBank").addEventListener("reset", function () {
        errorID.textContent = "";
        errorName.textContent = "";
        errorImg.textContent = "";
    });
});


//check id khi tạo
$(document).ready(function () {
    $('#createBankId').on('input', function () {
        var id = $(this).val().trim();
        $.ajax({
            url: '/Admin/BankManagement/ValidateByID',
            type: 'POST',
            data: { id: id },
            success: function (response) {
                if (!response.success) {
                    $("#createBankIdError").text(response.message);
                }
            },
            error: function () {
                alert('Lỗi kiểm tra id');
            }
        });
    });
});

//check name khi tạo
$(document).ready(function () {
    $('#createBankName').on('input', function () {
        var name = $(this).val().trim();
        $.ajax({
            url: '/Admin/BankManagement/ValidateByName',
            type: 'POST',
            data: { name: name },
            success: function (response) {
                if (!response.success) {
                    $("#createBankNameError").text(response.message);
                }
            },
            error: function () {
                alert('Lỗi kiểm tra tên');
            }
        });
    })
});


//validate sửa
document.addEventListener("DOMContentLoaded", function () {
    var ipEName = document.getElementById('editBankName');
    var ipEImg = document.getElementById('editBankImg');

    var errorEName = document.getElementById('editBankNameError');
    var errorEImg = document.getElementById('editBankImgError');

    function validateEditName() {
        if (ipEName.value.trim() === '') {
            errorEName.textContent = "Tên ngân hàng không được để trống";
        } else {
            errorEName.textContent = '';
        }
    }

    function validateEditImg() {
        var img = ipEImg.files[0];
        var allowExtension = ['image/jpeg', 'image/png', 'image/jpg'];
        var maxSize = 2 * 1024 * 1024;
        if (!allowExtension.includes(img.type)) {
            errorEImg.textContent = 'Chỉ chấp nhận định dạng ảnh JPEG, PNG, JPG.';
        } else if (img.size > maxSize) {
            errorEImg.textContent = 'Ảnh phải nhỏ hơn 2MB';
        } else {
            errorEImg.textContent = '';
        }
    }

    ipEName.addEventListener("input", validateEditName);
    ipEImg.addEventListener("input", validateEditImg);

    //kiểm tra all khi submit
    document.getElementById("frmEditBank").addEventListener("submit", function (event) {
        validateEditName();
        validateEditImg();
        if (errorEName.textContent || errorEImg.textContent) {
            event.preventDefault();
        }
    });

    document.getElementById("frmEditBank").addEventListener("reset", function () {
        errorEName.textContent = '';
        errorEImg.textContent = '';
    });
});


//check name khi sửa
$(document).ready(function () {
    $('#editBankName').on('input', function () {
        var name = $(this).val().trim();
        $.ajax({
            url: '/Admin/BankManagement/ValidateByName',
            type: 'POST',
            data: { name: name },
            success: function (response) {
                if (!response.success) {
                    $("#editBankNameError").text(response.message);
                }
            },
            error: function () {
                alert('Lỗi kiểm tra tên');
            }
        });
    })
});

//hiển thị thông tin khi sửa
$(document).ready(function () {
    $('.btnEditBank').click(function () {
        var bankId = $(this).data("id");
        console.log(bankId);
        $.ajax({
            url: '/Admin/BankManagement/GetBankByID',
            type: 'GET',
            data: { id: bankId },
            success: function (response) {
                if (response.success) {
                    console.log(response);
                    var bank = response.bank;
                    console.log(bank);
                    $('#editBankId').val(bank.sBankIdPk);
                    $('#editBankName').val(bank.sBankName);
                    if (bank.sImage && bank.sImage.trim() !== '') {
                        $('#editImg').attr('src', '/images/bank/' + bank.sImage);
                    } else {
                        $('#editImg').attr('src', '/images/bank/default-thumbnail.jpg');

                    }
                    $('#modalEditBank').modal('show');
                } else {
                    alert("Không tìm thấy ngân hàng!");
                }
            },
            error: function (error) {
                console.log("Error load bank: " + error);
            }
        });
    });
});


//hiển thị thông tin khi xóa
$(document).ready(function () {
    $('.btnLockBank').click(function () {
        var bankId = $(this).data("id");
        console.log(bankId);
        $.ajax({
            url: "/Admin/BankManagement/GetBankByID",
            type: 'GET',
            data: { id: bankId },
            success: function (response) {
                if (response.success) {
                    console.log(response);
                    var bank = response.bank;
                    if (bank.deleted) {
                        $('#modalLockTitle').html('Xác nhận mở khóa ngân hàng này?');
                        $('#btnSaveLockBank').html('Mở khóa');
                    } else {
                        $('#modalLockTitle').html('Xác nhận khóa ngân hàng này?');
                        $('#btnSaveLockBank').html('Khóa');
                    }
                    $('#lockBankId').val(bank.sBankIdPk);
                    $('#lockBankName').val(bank.sBankName);
                    if (bank.sImage && bank.sImage.trim() !== '') {
                        $('#lockImg').attr('src', '/images/bank/' + bank.sImage);
                    } else {
                        $('#lockImg').attr('src', '/images/bank/default-thumbnail.jpg');

                    }
                } else {
                    alert("Không tìm thấy ngân hàng!");
                }
            },
            error: function (error) {
                console.log("Error load bank: " + error);
            }
        });
    });
});






