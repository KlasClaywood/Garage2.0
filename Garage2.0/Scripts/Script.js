$(function () {
    /*
    Reinitialize table if redraw on dom.
    $('.table').bootstrapTable();
    */

    $('.datepickercheckin').datetimepicker();

    $('body').on("submit", "#modalForm", FormSubmitted);

    $('body').popover({
        selector: '.detailsLink',
        trigger: 'focus',
        viewport: 'body'
    });
})

var FormSubmitted = function () {
    $this = $(this);
    if (!$this.valid()) {
        return false;
    }
};

var ModalLoaded = function () {
    //$('.modal-title').html(data.title);
    //$('.modal-body').html(data.body);
    //$('.modal-submit').val(data.submit);
    $('#Modal').modal('toggle');
    $.validator.unobtrusive.parse('body');
    //return false;
};

var FormSuccess = function (data) {
    $('#Modal').modal('hide');
    refreshVehicleList();
};

var refreshVehicleList = function () {
    var options = {
        url: "/Home/Index",
        type: 'get',
    };

    $.ajax(options).done(function (data) {
        var $target = $('#VechicleListContainer');

        $target.replaceWith(data);

        $('.table').bootstrapTable();
    });
};