$(function () {
    /*
    Reinitialize table if redraw on dom.
    $('.table').bootstrapTable();
    */

    $('.datepickercheckin').datetimepicker();

    $('body').on("submit", "#modalForm", FormSubmitted);
})

var FormSubmitted = function () {
    $this = $("#" + $(this).attr("Form"));
    console.log("submitted");
    console.log("this:" + $this);
    if (!$this.valid()) {
        return false;
        console.log("notValid");
    }
    return false;
};

var ModalLoaded = function (data) {
    //$('.modal-title').html(data.title);
    //$('.modal-body').html(data.body);
    //$('.modal-submit').val(data.submit);

    $('#Modal').modal('toggle');
    //return false;
};

var refreshVehicleList = function () {
    var options = {
        url: "/Index",
        type: 'get',
        data: $obj.serialize()
    };

    $.ajax(options).done(function (data) {
        var $target = $('#VechicleListContainer');

        $target.replaceWith(data);

        $('.table').bootstrapTable();
    });
};