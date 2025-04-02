<script>
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
</script>
