$(window).load(function () {
    $("#checkAll").change(function () {
        $("input:checkbox:visible").prop('checked', $(this).prop("checked"));
    });
    $('input:checkbox').not(this).change(function () {
        var chbs = $('input:checkbox').not(this);
        if (chbs.length == chbs.filter(":checked").length) {
            $("#checkAll").prop('checked', this.checked);
        }
        if (chbs.length == chbs.filter(":checked").length + 1 && this.checked) {
            $("#checkAll").prop('checked', this.checked);
        }
    });
});