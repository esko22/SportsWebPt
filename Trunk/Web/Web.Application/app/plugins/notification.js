﻿'use strict';

jQueryPluginModule.value('toastr', toastr);
jQueryPluginModule.service('notifierService', function (toastr) {

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "positionClass": "toast-center",
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };


    return {
            notify: function (msg) {
            toastr.success(msg);
            console.log(msg);
        },
            warn: function (msg) {
            toastr.warning(msg);
            console.log(msg);
        },
            error: function (msg) {
                toastr.error(msg);
                console.log(msg);
        }
    };
});
