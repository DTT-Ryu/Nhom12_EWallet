
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
                    var user = response.model;
                    $("#editUserID").val(user.id);
                    $("#editUserName").val(user.fullName);
                    $("#editUserPhoneNumber").val(user.phoneNumber);
                    $("#editUserEmail").val(user.email);
                    $("#editUserCCCD").val(user.cccd);
                    $("#editUserBalance").val(user.balance);
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
                    $("#blockUserBalance").val(user.balance);
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



