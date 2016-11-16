$(function () {
    /*
    Reinitialize table if redraw on dom.
    $('.table').bootstrapTable();
    */
    $("#general-alert").hide();

    $('.datepickercheckin').datetimepicker();

    $('body').on("submit", "#modalForm", FormSubmitted);

    $('body').popover({
        selector: '.detailsLink',
        trigger: 'focus',
        viewport: 'body'
    });
});

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

    if(data.type != "" && data.type != null)
        ShowAlert(data.type, data.message);

    refreshVehicleList();
};

var refreshVehicleList = function () {
    var options = {
        url: "/Home/Index",
        type: 'get'
    };

    $.ajax(options).done(function (data) {
        $('#VechicleListContainer').html(data);
        $('.table').bootstrapTable();
    });
};

var ShowAlert = function (type, message) {
    if (type)
    {
        $("#general-alert").html("<strong>" + message + "</strong>");
        $("#general-alert").addClass("alert-success");
        $("#general-alert").show();
        $("#general-alert").alert();
        $("#general-alert").fadeTo(2000, 500).slideUp(500, function () {
            $("#general-alert").slideUp(500);
            $("#general-alert").removeClass("alert-success");
            $("#general-alert").html("");
        });
    }
    else {
        $("#general-alert").html("<strong>" + message + "</strong>");
        $("#general-alert").addClass("alert-danger");
        $("#general-alert").show();
        $("#general-alert").alert();
        $("#general-alert").fadeTo(2000, 500).slideUp(500, function () {
            $("#general-alert").slideUp(500);
            $("#general-alert").removeClass("alert-danger");
            $("#general-alert").html("");
        });
    }
};
