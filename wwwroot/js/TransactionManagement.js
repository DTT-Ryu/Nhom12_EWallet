$(document).ready(function () {
    $('.btnDeleteTransaction').click(function () {
        var transID = $(this).data("id");
        console.log(transID);
        $.ajax({
            url: '/Admin/TransactionManagement/GetTransactionByID',
            type: 'GET',
            data: { id: transID },
            success: function (response) {
                if (response.success) {
                    console.log(response);
                    var t = response.trans;
                    $('#deleteTransactionID').val(t.transactionId);
                    if (t.transactionType == "deposit") {
                        $('#deleteTranType').val("Nạp tiền");
                    } else if (t.transactionType == "withdraw") {
                        $('#deleteTranType').val("Rút tiền");
                    } else if (t.transactionType == "transfer") {
                        $('#deleteTranType').val("Chuyển tiền");
                    }
                    
                    $('#deleteTranSender').val(t.senderUserPhone);
                    if (t.transactionType == "transfer") {
                        $('#deleteTranRecipient').val(t.recipientUserPhone);
                    } else {
                        $('#deleteTranRecipient').val(t.bankAccName);
                    }
                    
                    $('#deleteTranAmount').val(t.amount);
                    $('#deleteTranDescript').val(t.description);
                    $('#deleteTranCreate').val(t.createAt);

                    if (t.status == "pending") {
                        $('#deleteTranStatus').val('Chưa giải quyết');
                    } else if (t.status == "completed") {
                        $('#deleteTranStatus').val('Hoàn thành');
                    } else {
                        $('#deleteTranStatus').val('Thất bại');
                    }
                    
                } else {
                    alert('Không tìm thấy giao dịch');
                }
            },
            error: function (error) {
                alert('Error load transaction: ' + error);
            }
        });
    });
});