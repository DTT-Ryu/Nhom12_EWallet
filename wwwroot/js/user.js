
$(document).ready(function () {
        $('#BirthDate').on('change', function () {
            var birthDate = new Date($(this).val());
            var today = new Date();
            var age = today.getFullYear() - birthDate.getFullYear();

            if (today < new Date(birthDate.setFullYear(birthDate.getFullYear() + age))) {
                age--;
            }

            if (age < 16) {
                $('#birthdate-error').text("Bạn phải đủ 16 tuổi!").show();
            } else {
                $('#birthdate-error').hide();
            }
        });
});




//profile
let getUserId = () => {
    return fetch('/get-user-session')
        .then(response => response.json())
        .then(userData => {
            if (!userData) return null;
            window.userID = userData.userId;
        });
};


    let loadUserInfor = () => {
        fetch('/get-user-session')
            .then(response => response.json())
            .then(userData => {
                if (!userData) return;
                let userID = userData.userId; // Khai báo biến đúng

                fetch("/get-user/" + userID) // Sửa tên biến đúng
                    .then(response => response.json()) // Đóng ngoặc đúng chỗ
                    .then(userDetail => {
                        console.log(userDetail);
                        $('#proName').val(userDetail.fullName || '');
                        $('#proPhoneNumber').val(userDetail.phoneNumber || '');
                        $('#proCCCD').val(userDetail.cccd || '');
                        $('#proEmail').val(userDetail.email || '');
                        $('#proBirthDate').val(userDetail.birthDate || '');
                        $('#proBalance').val(userDetail.balance !== null && userDetail.balance !== undefined
                            ? userDetail.balance.toLocaleString('en-US') + ' VNĐ'  : '');
                    });
            });
    };
$(document).ready(() => {
    getUserId().then(() => {
        loadUserInfor(); // Chỉ tải thông tin khi đã có UserID
    });
});



$("#btnEditProfile").click(function () {
    let inputs = document.querySelectorAll(".editable-field");
    let isDisabled = inputs[0].disabled;

    inputs.forEach(input => input.disabled = !isDisabled);

    if (this.innerHTML === 'Lưu thay đổi') {
        let data = {
            fullName: $('#proName').val(),
            email: $('#proEmail').val(),
            birthDate: $('#proBirthDate').val()
        };
        console.log("UserID:", window.userID);

        fetch("/update-user-infor/" + window.userID, {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
            .then(response => response.json())
            .then(data => {
                if (data.success === true) {
                    alert(data.message);
                    console.log(data.data);
                    loadUserInfor();
                } else {
                    alert(data.message);
                }
            });
    }

    this.innerHTML = isDisabled ? 'Lưu thay đổi' : 'Sửa';
    this.classList.toggle('btn-primary', !isDisabled);
    this.classList.toggle('btn-success', isDisabled);
    loadUserInfor();
});
