$(document).ready(function () {
    $('#searchBox').on('input', function () {
        var keyword = $(this).val();

        $.ajax({
            url: '/api/users/search',
            type: 'GET',
            data: { keyword: keyword },
            success: function (response) {
                $('#userTable').empty(); // Xóa dữ liệu cũ

                if (response.length === 0) {
                    $('#userTable').append('<tr><td colspan="9">Không tìm thấy người dùng!</td></tr>');
                } else {
                    $.each(response, function (index, user) {
                        var roleClass = (user.role === "Admin") ? 'fw-bold text-danger' : '';
                        var statusBadge = (user.status === "active") ?
                            '<span class="badge bg-success">Hoạt động</span>' :
                            '<span class="badge bg-danger">Khóa</span>';

                        var actionButtons = '';
                        if (user.status === "active") {
                            actionButtons = `
                                        <button data-id="${user.id}" class="btnEditUser btn btn-sm btn-primary me-2" type="button" data-bs-toggle="modal" data-bs-target="#modalEditUser" title="Cập nhật quyền">
                                            <i class="fas fa-user-edit"></i>
                                        </button>
                                        <button data-id="${user.id}" class="btnBlockUser btn btn-sm btn-danger" type="button" data-bs-toggle="modal" data-bs-target="#modalBlockUser" title="Khóa">
                                            <i class="fas fa-user-lock"></i>
                                        </button>
                                    `;
                        } else {
                            actionButtons = `
                                        <button class="suatk btn btn-sm btn-primary me-2" type="button" disabled>
                                            <i class="fas fa-user-edit"></i>
                                        </button>
                                        <button data-id="${user.id}" class="btnBlockUser btn btn-sm btn-danger" type="button" data-bs-toggle="modal" data-bs-target="#modalBlockUser" title="Mở khóa">
                                            <i class="fas fa-lock-open"></i>
                                        </button>
                                    `;
                        }

                        var row = `
                                    <tr>
                                        <td>${user.id}</td>
                                        <td>${user.fullName}</td>
                                        <td>${user.phoneNumber}</td>
                                        <td>${user.cccd}</td>
                                        <td>${user.email}</td>
                                        <td>${parseInt(user.balance).toLocaleString()} VNĐ</td>
                                        <td><span class="${roleClass}">${user.role}</span></td>
                                        <td>${statusBadge}</td>
                                        <td>${actionButtons}</td>
                                    </tr>
                                `;

                        $('#userTable').append(row);
                    });
                }
            }
        });
    });
});


//Hiển thị thông tin trước khi sửa
$(document).ready(function () {
    $(".btnEditUser").click(function () {
        var userId = $(this).data("id");
        console.log(userId);
        $.ajax({
            url: "/Admin/UserManagement/GetUserByID",
            type: "GET",
            data: { id: userId },
            success: function (response) {
                if (response.success) {
                    console.log(response);
                    //console.log(typeof response.self, response.self);
                    var user = response.model;
                    $("#editUserID").val(user.id);
                    $("#editUserName").val(user.fullName);
                    $("#editUserPhoneNumber").val(user.phoneNumber);
                    $("#editUserEmail").val(user.email);
                    $("#editUserCCCD").val(user.cccd);
                    $("#editUserBalance").val(user.balance ? user.balance.toLocaleString('en-US') + ' VNĐ' : '');

                    $("#editUserSelectRole").val(user.roleId);

                    $.ajax({
                        url: "/Admin/UserManagement/GetAllRoles",
                        type: "GET",
                        success: function (roles) {
                            $("#editUserSelectRole").empty();
                            $.each(roles, function (i, role) {
                                $("#editUserSelectRole").append(`
                                <option class="mx-2" value="${role.iRoleIdPk}">${role.sRoleName}</option>`);
                            });
                            $("#editUserSelectRole").val(user.roleId);
                        }
                    });
                    $('#modalEditUser').modal('show');
                    if (response.self) {
                        alert("Bạn không thể sửa quyền của chính mình!");
                        $("#btnSaveEditUser").prop("disabled", true); // Chặn sửa số dư nếu là chính mình
                    } else {
                        $("#btnSaveEditUser").prop("disabled", false);
                    }
                    
                } else {
                    alert("Không tìm thấy người dùng!");
                }
            },
            error: function (error) {
                console.log("Error load user: " + error);
            }
        });
    });
});


//Hiển thị thông tin trước khi xóa
$(document).ready(function () {
    $(".btnBlockUser").click(function () {
        var userId = $(this).data("id");
        console.log(userId);
        $.ajax({
            url: "/Admin/UserManagement/GetUserByID",
            type: "GET",
            data: { id: userId },
            success: function (response) {
                if (response.success) {
                    console.log(response);
                    var user = response.model;
                    if (user.status == "active") {
                        $("#modalTitleBlockUser").html("Xác nhận khóa người dùng này?");
                        $("#btnSaveBlockUser").html("Khóa");
                    } else {
                        $("#modalTitleBlockUser").html("Xác nhận mở khóa người dùng này?");
                        $("#btnSaveBlockUser").html("Mở khóa");
                    }

                    $("#blockUserID").val(user.id);
                    $("#blockUserName").val(user.fullName);
                    $("#blockUserPhoneNumber").val(user.phoneNumber);
                    $("#blockUserEmail").val(user.email);
                    $("#blockUserCCCD").val(user.cccd);
                    $("#blockUserBalance").val(user.balance ? user.balance.toLocaleString('en-US') + ' VNĐ' : '');
                    $("#blockUserSelectRole").val(user.roleId);
                    $.ajax({
                        url: "/Admin/UserManagement/GetAllRoles",
                        type: "GET",
                        success: function (roles) {
                            $("#blockUserSelectRole").empty();
                            $.each(roles, function (i, role) {
                                $("#blockUserSelectRole").append(`
                                <option class="mx-2" value="${role.iRoleIdPk}">${role.sRoleName}</option>`);
                            });
                            $("#blockUserSelectRole").val(user.roleId);
                        }
                    });
                    $('#modalBlockUser').modal('show');
                    if (response.self) {
                        alert("Bạn không thể khóa tài khoản của chính mình!");
                        $("#btnSaveBlockUser").prop("disabled", true); // Chặn sửa số dư nếu là chính mình
                    } else {
                        $("#btnSaveBlockUser").prop("disabled", false);
                    }
                } else {
                    alert("Không tìm thấy người dùng!");
                }
            },
            error: function (error) {
                console.log("Error load user: " + error);
            }
        });
    });
});





//Cập nhật quyền với ajax

//                       $(document).ready(function () {
//     $("#frmEditUser").submit(function (e) {
//         e.preventDefault(); Ngăn chặn reload trang

//         var formData = {
//             id: $("#editUserID").val(),
//             fullName: $("#editUserName").val(),
//             roleId: $("#editUserSelectRole").val(),
//         };

//         $.ajax({
//             url: "/Admin/UserManagement/Edit",
//             type: "POST",
//             data: formData,
//             success: function (response) {
//                 console.log(response); Kiểm tra dữ liệu trả về
//                 if (response.success) {
//                     alert("Cập nhật thành công!");
//                     $("#modalEditUser").modal("hide");
//                     location.reload();
//                 } else {
//                     console.log("Lỗi chi tiết:", response.errors);
//                     alert("Lỗi: " + response.message);
//                 }
//             },
//             error: function () {
//                 alert("Có lỗi xảy ra, vui lòng thử lại.");
//             }
//         });
//     });
// });



