// Notification message
setTimeout(function () {
    var alert = document.querySelector('.alert');
    if (alert) {
        var bsAlert = new bootstrap.Alert(alert);
        bsAlert.close();
    }
}, 3000);

//  Delete Modal
//function confirmDelete(id, userName) {
//    Swal.fire({
//        title: 'Xác nhận xóa?',
//        text: `Bạn có chắc chắn muốn xóa tài khoản "${userName}"?`,
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonText: 'Xóa',
//        cancelButtonText: 'Hủy',
//        confirmButtonColor: '#d33',
//        cancelButtonColor: '#3085d6'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            // Tạo form ẩn để gửi POST
//            const form = document.createElement('form');
//            form.method = 'post';
//            form.action = `/User/Delete/${id}`;
//            document.body.appendChild(form);
//            form.submit();
//        }
//    });
//}

function confirmDelete(id, userName) {
    Swal.fire({
        title: 'Xác nhận xóa?',
        text: `Bạn có chắc chắn muốn xóa tài khoản "${userName}"?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Xóa',
        cancelButtonText: 'Hủy',
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6'
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById('deleteUserId').value = id;
            document.getElementById('deleteForm').submit();
        }
    });
}