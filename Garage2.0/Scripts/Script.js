$(function () {
    /*
    Reinitialize table if redraw on dom.
    $('.table').bootstrapTable();
    */

    $('.datepickercheckin').datetimepicker();

    
})

var ModalLoaded = function (data) {
    //$('.modal-title').html(data.title);
    //$('.modal-body').html(data.body);
    //$('.modal-submit').val(data.submit);

    $('#Modal').modal('toggle');
    //return false;
};