define('error.helper', ['jquery', 'config'], function($,config) {

    var onError = function(message) {
        config.notifier.clear();
        config.notifier.error(message, 'Unexpected Error', { extendedTimeOut: 0, timeOut: 0 });
    };

    return {
        onError: onError
    };

});
