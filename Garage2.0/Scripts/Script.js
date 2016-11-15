$(function () {
    /*
    Reinitialize table if redraw on dom.
    $('.table').bootstrapTable();
    */

    $('.datepickercheckin').datetimepicker();

    var ModalLoaded = function (data) {
        var json = data.get_response().get_object();
        $('.modal-title').html(json.title);
        $('.modal-body').html(json.body);
    }
})